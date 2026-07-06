// src/app/layers/layers.component.ts
import { Component, OnInit, Injector } from '@angular/core';
import { LayerServiceProxy, LayerDto, FeatureServiceProxy, StyleDto, FeatureDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';
import * as maplibregl from 'maplibre-gl';   // "lấy tất cả, đặt tên là maplibre-gl"
// import { MaplibreTerradrawControl } from '@watergis/maplibre-gl-terradraw';

@Component({                              // ~ Ext.define view
  selector: 'app-layers',
  templateUrl: './layers.component.html',
})
export class LayersComponent extends AppComponentBase implements OnInit {
  layers: LayerDto[] | undefined = [];                // ~ Store (nhưng chỉ là mảng thường)
  map: maplibregl.Map | undefined;   
  newLayer: any = {};
  newStyle: any = {};
  newFeature: any = {};
  layerStyles: any[] = []; 
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
    //         'delete',
    //         'download'
    //     ],
    //     open: true,
    // });
    // this.map.addControl(draw, 'top-left');
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
    var feature  = await this._featureService.get(layer.id).toPromise();
    if (feature && feature.geometry) {
      var geojson = JSON.parse(feature.geometry);
      if (geojson.type === 'Point') {
        const [lng, lat] = geojson.coordinates;
        this.map.flyTo({ center: [lng, lat], zoom: 15 });
      } else if (geojson.type === 'Polygon') {
        const coordinates = geojson.coordinates[0];
        const bounds = coordinates.reduce((bounds: any, coord: any) => {
          return bounds.extend(coord);
        }, 
        new maplibregl.LngLatBounds(coordinates[0], coordinates[0]));
        this.map.fitBounds(bounds, { padding: 20 });
      } else if (geojson.type === 'LineString') {
        const coordinates = geojson.coordinates;
        const bounds = coordinates.reduce((bounds: any, coord: any) => {
          return bounds.extend(coord);
        },
        new maplibregl.LngLatBounds(coordinates[0], coordinates[0]));
        this.map.fitBounds(bounds, { padding: 20 });
      } else {
        // console.warn('Unsupported geometry type:', geojson.type);
        // alert("layer don't have feature or geometry");
      }
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

  loadLayers(): void {
    if (!this.map) return;
    this._layerService.getAll("", 0, 100).subscribe((result: any) => {
      this.layers = result.items; 
    })
  }

  onCreateLayer(): void {
    this.newLayer.styles = this.layerStyles;
    // console.log(this.newLayer);
    const layerDto = new LayerDto(this.newLayer);
    this._layerService.create(layerDto).subscribe(
      (created: any) => {
        // console.log('Created:', created);
        this.resetForm();
        this.loadLayers();
      },
      (error: any) => console.error('Error:', error)
    );
  }

  resetForm(): void {
    this.newLayer = {};
    this.newStyle = {};
    this.layerStyles = [];
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

  addProperties(): void {

  }
}
