// src/app/layers/layers.component.ts
import { Component, OnInit, Injector } from '@angular/core';
import { LayerServiceProxy, LayerDto, FeatureServiceProxy, StyleDto, FeatureDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';
import * as maplibregl from 'maplibre-gl';   // "lấy tất cả, đặt tên là maplibre-gl"
// import MaplibreTerradrawControl from '@watergis/maplibre-gl-terradraw';
import MapboxDraw from '@mapbox/mapbox-gl-draw';
import '@mapbox/mapbox-gl-draw/dist/mapbox-gl-draw.css';
import * as turf from '@turf/turf';

@Component({                              // ~ Ext.define view
  selector: 'app-layers',
  templateUrl: './layers.component.html',
  styleUrls: ['./layers.component.css']
})
export class LayersComponent extends AppComponentBase implements OnInit {
  layers: LayerDto[] | undefined = [];                // ~ Store (nhưng chỉ là mảng thường)
  map: maplibregl.Map | undefined;   
  drawInstance: MapboxDraw | undefined;
  newLayer: any = {};
  newStyle: any = {};
  newFeature: any = {};
  layerStyles: any[] = []; 
  layerFeatures: any[] = []; 
  selectedLayer: LayerDto | undefined;
  constructor(injector: Injector, private _layerService: LayerServiceProxy, private _featureService: FeatureServiceProxy) {
    super(injector);                      // DI: khai báo cần gì ở constructor — y hệt IRepository bên C#
    this.newLayer = new LayerDto();
    this.newStyle = new StyleDto();
    this.newFeature = new FeatureDto();
  }

  ngOnInit(): void {                      // ~ initComponent: chạy khi component khởi tạo
    this._layerService.getAll("", 0, 100)
      .subscribe((result: any) => { 
        // console.log(result);
        this.layers = result.items; 
      });
  }

  ngAfterViewInit(): void {
    this.initMap();
    this.map?.on('load', () => this.loadFeatures());   // chú ý dòng này!
    this.map?.on('error', (e: Error) => console.error('Map error:', e.message));
  }

  initMap(): void {
    this.map = new maplibregl.Map({
      container: 'map', // id của thẻ div chứa bản đồ
      style: {
        version: 8,
        sources: {
          osm: {
            type: 'raster',
            tiles: ['https://tile.openstreetmap.org/{z}/{x}/{y}.png'],
            tileSize: 256,
            attribution: '© OpenStreetMap contributors'
          }
        },
        layers: [{ id: 'osm', type: 'raster', source: 'osm' }]
      },
      center: [105.854444, 21.028511], // tọa độ trung tâm (kinh độ, vĩ độ)
      zoom: 10 // mức zoom
    });

    // const draw = new MaplibreTerradrawControl({
    //     modes: [
    //         // 'render', comment this to always show drawing tool            
    //         'point',
    //         'linestring',
    //         'polygon',
    //         'rectangle',
    //         'circle',
    //         'freehand',
    //         'angled-rectangle',
    //         'sensor',
    //         'sector',
    //         'select',
    //         'delete-selection',
    //         'delete'
    //     ],
    //     open: true,
    // });
    // this.map.addControl(draw, 'top-left');

    const draw = new MapboxDraw({
      displayControlsDefault: false,
      controls: {
        point: true,        // Bật thêm nút vẽ điểm
        line_string: true,  // Bật thêm nút vẽ đường
        polygon: true,
        trash: true
      },
      defaultMode: 'simple_select',  // Đổi từ 'draw_polygon' → 'simple_select' để mặc định không tự vẽ luôn
      styles: [
        // ACTIVE (being drawn)
        // line stroke
        {
            "id": "gl-draw-line",
            "type": "line",
            "filter": ["all", ["==", "$type", "LineString"]],
            "layout": {
              "line-cap": "round",
              "line-join": "round"
            },
            "paint": {
              "line-color": "#D20C0C",
              "line-dasharray": [0.2, 2],
              "line-width": 2
            }
        },
        // polygon fill
        {
          "id": "gl-draw-polygon-fill",
          "type": "fill",
          "filter": ["all", ["==", "$type", "Polygon"]],
          "paint": {
            "fill-color": "#D20C0C",
            "fill-outline-color": "#D20C0C",
            "fill-opacity": 0.1
          }
        },
        // polygon mid points
        {
          'id': 'gl-draw-polygon-midpoint',
          'type': 'circle',
          'filter': ['all',
            ['==', '$type', 'Point'],
            ['==', 'meta', 'midpoint']],
          'paint': {
            'circle-radius': 3,
            'circle-color': '#fbb03b'
          }
        },
        // polygon outline stroke
        // This doesn't style the first edge of the polygon, which uses the line stroke styling instead
        {
          "id": "gl-draw-polygon-stroke-active",
          "type": "line",
          "filter": ["all", ["==", "$type", "Polygon"]],
          "layout": {
            "line-cap": "round",
            "line-join": "round"
          },
          "paint": {
            "line-color": "#D20C0C",
            "line-dasharray": [0.2, 2],
            "line-width": 2
          }
        },
        // vertex point halos
        {
          "id": "gl-draw-polygon-and-line-vertex-halo-active",
          "type": "circle",
          "filter": ["all", ["==", "meta", "vertex"], ["==", "$type", "Point"]],
          "paint": {
            "circle-radius": 5,
            "circle-color": "#FFF"
          }
        },
        // vertex points
        {
          "id": "gl-draw-polygon-and-line-vertex-active",
          "type": "circle",
          "filter": ["all", ["==", "meta", "vertex"], ["==", "$type", "Point"]],
          "paint": {
            "circle-radius": 3,
            "circle-color": "#D20C0C",
          }
        },
        {
          "id": "gl-draw-point",
          "type": "circle",
          "filter": ["all", ["==", "$type", "Point"], ["==", "meta", "feature"]],
          "paint": {
            "circle-radius": 6,
            "circle-color": "#D20C0C",
            "circle-stroke-width": 2,
            "circle-stroke-color": "#FFF"
          }
        }
      ]
    });

    this.drawInstance = draw;   // ← LƯU LẠI INSTANCE, quan trọng nhất!

    const updateArea = (e: any) => {
      const data = draw.getAll();
      const answer = document.getElementById('calculated-area');
      if (data.features.length > 0) {
        const area = turf.area(data);
        const rounded_area = Math.round(area * 100) / 100;
        answer!.innerHTML = `<p><strong>${rounded_area}</strong></p><p>square meters</p>`;
      } else {
        answer!.innerHTML = '';
        if (e.type !== 'draw.delete')
          alert('Click the map to draw a polygon.');
      }
    }

    this.map.addControl(draw);
    this.map.on('draw.create', (e:any) => updateArea(e));
    this.map.on('draw.delete', (e:any) => updateArea(e));
    this.map.on('draw.update', (e:any) => updateArea(e));
  }

  useDrawnGeometry(): void {
    if (!this.drawInstance) {
      alert('Bản đồ chưa sẵn sàng, vui lòng đợi vài giây rồi thử lại.');
      return;
    }

    const drawnData = this.drawInstance.getAll();
    
    if (drawnData.features.length === 0) {
      alert('Chưa vẽ hình nào trên bản đồ! Hãy vẽ điểm/đường/vùng trước khi bấm nút này.');
      return;
    }

    // Lấy hình vừa vẽ MỚI NHẤT (hình cuối cùng trong danh sách)
    const lastFeature = drawnData.features[drawnData.features.length - 1];

    // Điền tự động vào ô geometry (dạng chuỗi JSON, khớp với DTO backend)
    this.newFeature.geometry = JSON.stringify(lastFeature.geometry);

    // console.log('Đã lấy geometry:', this.newFeature.geometry);
  }

  clearDrawnGeometry(): void {
    this.drawInstance?.deleteAll();
    const answer = document.getElementById('calculated-area');
    if (answer) answer.innerHTML = '';
  }

  loadFeatures(): void {
    this._featureService.getAll("", 0, 1000).subscribe((result: any) => {
      const geojson: any = {
        type: 'FeatureCollection',
        features: result.items.map((f: any) => ({
          type: 'Feature',
          geometry: JSON.parse(f.geometry),          // chuỗi GeoJSON từ DTO → object
          properties: { id: f.id, name: f.name }
        }))
      };
      // console.log('GeoJSON:', geojson); // kiểm tra dữ liệu GeoJSON

      this.map?.addSource('app-features', { type: 'geojson', data: geojson });

      this.map?.addLayer({
        id: 'app-features-points',
        type: 'circle',                              // vẽ điểm thành chấm tròn
        source: 'app-features',
        paint: {
          'circle-radius': 8,
          'circle-color': '#d85a30',
          'circle-stroke-width': 2,
          'circle-stroke-color': '#ffffff'
        }
      });

      // console.log('Layers:', this.map?.getStyle().layers?.map(l => l.id));
      setTimeout(() => {
        // console.log('Rendered:', this.map?.queryRenderedFeatures(undefined, { layers: ['app-features-points'] }));
      }, 2000);
    });
  }

  async Fly(layer: LayerDto): Promise<void> {
    if (!this.map) return;
    var features  = await this._featureService.getFeatureByLayerId(layer.id).toPromise();
    if (!features || features.length === 0) {
      alert('Layer này chưa có Feature nào!');
      return;
    }

    // Gom TẤT CẢ toạ độ của MỌI feature lại thành 1 vùng bao (bounding box) chung
    let bounds: maplibregl.LngLatBounds | undefined;

    features.forEach((f: any) => {
      const geojson = JSON.parse(f.geometry);
      const coords = this.extractAllCoordinates(geojson);   // Hàm phụ, lấy hết toạ độ (viết bên dưới)

      coords.forEach((coord: [number, number]) => {
        if (!bounds) {
          bounds = new maplibregl.LngLatBounds(coord, coord);
        } else {
          bounds.extend(coord);
        }
      });
    });

    if (bounds) {
      this.map.fitBounds(bounds, { padding: 40 });
    }

    if(layer){
      if(this.selectedLayer != layer && this.selectedLayer != null){
        this.selectedLayer = layer;
      } else if(this.selectedLayer == layer && this.selectedLayer != null){
        this.selectedLayer = undefined;
      } else {
        this.selectedLayer = layer;
      }
    }
  }

  extractAllCoordinates(geojson: any): [number, number][] {
    if (geojson.type === 'Point') {
      return [geojson.coordinates];
    } else if (geojson.type === 'LineString') {
      return geojson.coordinates;
    } else if (geojson.type === 'Polygon') {
      return geojson.coordinates[0];   // Lấy vòng ngoài cùng của polygon
    }
    return [];
  }

  loadLayers(): void {
    if (!this.map) return;
    this._layerService.getAll("", 0, 100).subscribe((result: any) => {
      this.layers = result.items; 
    })
  }

  onCreateLayer(): void {
    this.newLayer.styles = this.layerStyles;
    this.newLayer.features = this.layerFeatures;
    // console.log(this.newLayer);
    const layerDto = new LayerDto(this.newLayer);
    this._layerService.create(layerDto).subscribe(
      (created: any) => {
        console.log('Created:', created);
        this.resetForm();
        this.loadLayers();
      },
      (error: any) => console.error('Error:', error)
    );
  }

  resetForm(): void {
    this.newLayer = {};
    this.newStyle = {};
    this.newFeature = {};
    this.layerStyles = [];
    this.layerFeatures = [];
  }

  addStyle(): void {
    if (this.newStyle.name && this.newStyle.styleJson) {
      const styleDto = new StyleDto(this.newStyle);
      this.layerStyles.push(styleDto);
      this.newStyle = {};
    }
  }

  removeStyle(index: number): void {
    this.layerStyles.splice(index, 1);
  }

  addFeature(): void {
    if (this.newFeature.geometry) {
      try {
        const geometryObj = JSON.parse(this.newFeature.geometry);
        const propertiesObj = this.newFeature.properties 
          ? JSON.parse(this.newFeature.properties) 
          : {};

        const featureDto = new FeatureDto({
          id: 0,
          name: propertiesObj.name || '',
          layerId: this.newLayer.id || 0,
          geometry: JSON.stringify(geometryObj),
          properties: JSON.stringify(propertiesObj)   // ← THÊM JSON.stringify() Ở ĐÂY
        });

        this.layerFeatures.push(featureDto);
        this.newFeature = {};
      } catch (err) {
        alert('Geometry hoặc Properties không đúng định dạng JSON! Vui lòng kiểm tra lại.');
      }
    } else {
      alert('Vui lòng nhập Geometry trước khi thêm Feature.');
    }
  }

  removeFeature(index: number): void {
    this.layerFeatures.splice(index, 1);
  }
}
