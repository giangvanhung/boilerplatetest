// src/app/layers/layers.component.ts
import { Component, OnInit, Injector } from '@angular/core';
import { LayerServiceProxy, LayerDto, FeatureServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';
import * as maplibregl from 'maplibre-gl';   // "lấy tất cả, đặt tên là maplibregl"

@Component({                              // ~ Ext.define view
  selector: 'app-layers',
  templateUrl: './layers.component.html',
})
export class LayersComponent extends AppComponentBase implements OnInit {
  layers: LayerDto[] | undefined = [];                // ~ Store (nhưng chỉ là mảng thường)
  map: maplibregl.Map | undefined;                       // ~ Store (nhưng chỉ là mảng thường)
  constructor(injector: Injector, private _layerService: LayerServiceProxy, private _featureService: FeatureServiceProxy) {
    super(injector);                      // DI: khai báo cần gì ở constructor — y hệt IRepository bên C#
  }

  ngOnInit(): void {                      // ~ initComponent: chạy khi component khởi tạo
    this._layerService.getAll("", 0, 100)
      .subscribe(result => { 
        // console.log(result);
        this.layers = result.items; 
      });
  }

  ngAfterViewInit(): void {
    this.initMap();
    this.map?.on('load', () => this.loadFeatures());   // chú ý dòng này!
    this.map?.on('error', (e) => console.error('Map error:', e.error));
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
      console.log('GeoJSON:', geojson); // kiểm tra dữ liệu GeoJSON

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

      console.log('Layers:', this.map?.getStyle().layers?.map(l => l.id));
      setTimeout(() => {
        console.log('Rendered:', this.map?.queryRenderedFeatures(undefined, { layers: ['app-features-points'] }));
      }, 2000);
    });
  }
}
