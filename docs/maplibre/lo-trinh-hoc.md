# MapLibre — Cần học & cần quan tâm gì?

Trang này chia kiến thức thành ba nhóm: **phải thạo**, **phải biết là có tồn tại**, và **cạm bẫy**. Đọc [trang giới thiệu](gioi-thieu.md) trước nếu chưa rõ MapLibre là gì.

---

## Nhóm 1 — Phải thạo

Đây là những thứ bạn sẽ chạm vào mỗi ngày. Không nắm chắc thì mọi việc đều chậm.

### Hệ tọa độ và thứ tự lng/lat

Dữ liệu GeoJSON dùng hệ **EPSG:4326** (kinh độ/vĩ độ, đơn vị độ). MapLibre render theo phép chiếu **Web Mercator (EPSG:3857)** và tự lo việc chuyển đổi — bạn không cần quan tâm.

Cái bạn **phải** quan tâm: mọi tọa độ trong MapLibre theo thứ tự **`[kinh độ, vĩ độ]`** — tức là `[lng, lat]`, kinh độ trước. Trực giác đọc "tọa độ 21.03, 105.85" của Hà Nội là *vĩ độ trước*, ngược lại. Đảo nhầm hai số này là lỗi phổ biến nhất của người mới, và triệu chứng luôn giống nhau: bản đồ nhảy ra giữa đại dương hoặc Nam Cực.

```ts
map.setCenter([105.85, 21.03]);   // Hà Nội — đúng
map.setCenter([21.03, 105.85]);   // đâu đó ngoài khơi Somalia — sai
```

### Style Spec

Đầu tư nhiều nhất vào đây. Cụ thể cần thuộc:

- **Các loại source**: `vector`, `geojson`, `raster`, `raster-dem`, `image`.
- **Các loại layer**: `fill` (vùng), `line` (đường), `circle` (điểm dạng chấm), `symbol` (icon + nhãn chữ), `fill-extrusion` (khối 3D), `heatmap`, `raster`, `background`.
- **Phân biệt `paint` và `layout`**: `layout` quyết định *cấu trúc* của thứ được vẽ (`visibility`, `text-field`, `icon-image`, `line-cap`) — đổi nó buộc MapLibre tính toán lại nhiều; `paint` chỉ quyết định *diện mạo* (`fill-color`, `line-width`, `circle-radius`, `opacity`) — đổi nó rẻ hơn nhiều. Khi cần bật/tắt lớp liên tục, ưu tiên đổi `opacity` (paint) thay vì `visibility` (layout).
- **`filter`**: lọc bớt đối tượng không muốn vẽ, ngay trong style.

### Expression

Thành thạo bốn cấu trúc này là đủ dùng cho 90% trường hợp:

| Expression | Dùng để |
|---|---|
| `["get", "ten_thuoc_tinh"]` | Đọc thuộc tính của đối tượng |
| `["interpolate", ["linear"], ["zoom"], z1, v1, z2, v2]` | Giá trị biến thiên mượt theo zoom |
| `["match", input, nhãn1, ra1, nhãn2, ra2, mặc_định]` | Ánh xạ giá trị rời rạc (phân loại) |
| `["case", điều_kiện, ra1, mặc_định]` | Rẽ nhánh theo điều kiện |

Cộng thêm `["step", ...]` cho ngưỡng rời rạc, và `["feature-state", "hover"]` cho tương tác (xem dưới).

### Vòng đời bản đồ và sự kiện `load`

Bạn **không thể** gọi `addSource` hay `addLayer` ngay sau khi `new maplibregl.Map(...)`. Style chưa tải xong. Mọi thao tác phải nằm trong callback:

```ts
map.on('load', () => {
  map.addSource('app-features', { type: 'geojson', data: geojson });
  map.addLayer({ id: 'features-fill', type: 'fill', source: 'app-features', paint: {...} });
});
```

Nếu style được đổi giữa chừng bằng `setStyle()`, toàn bộ source và layer bạn thêm vào sẽ **bị xóa sạch** và phải thêm lại. Đây là nguyên nhân kinh điển của lỗi "đổi nền bản đồ xong dữ liệu biến mất".

### Tương tác: click, hover, và `feature-state`

Bắt sự kiện trên **layer cụ thể**, không phải trên toàn bản đồ:

```ts
map.on('click', 'features-fill', (e) => {
  const feature = e.features?.[0];
  new maplibregl.Popup().setLngLat(e.lngLat).setHTML(feature.properties.name).addTo(map);
});
```

Để highlight đối tượng khi rê chuột, **đừng** sửa dữ liệu rồi `setData` lại. Dùng `feature-state` — nó chỉ cập nhật trạng thái trên GPU:

```ts
map.setFeatureState({ source: 'app-features', id: featureId }, { hover: true });
// trong style:  "fill-opacity": ["case", ["boolean", ["feature-state", "hover"], false], 1, 0.5]
```

Lưu ý: `feature-state` yêu cầu mỗi đối tượng có `id` số nguyên. Nếu id nằm trong `properties`, khai báo `promoteId: 'ten_truong_id'` ở source.

### Camera

`flyTo` (bay có hiệu ứng), `easeTo` (chuyển mượt), `jumpTo` (nhảy tức thì), và đặc biệt **`fitBounds`** — tự canh khung nhìn vừa khít một tập đối tượng. Component `layers` trong repo đã dựng `LngLatBounds` theo cách này. Kèm theo là bốn tham số camera: `center`, `zoom`, `bearing` (xoay), `pitch` (nghiêng).

---

## Nhóm 2 — Phải biết là có tồn tại

Chưa cần thạo, nhưng phải biết để không đi làm lại từ đầu thứ đã có sẵn.

- **Clustering**: `geojson` source có sẵn `cluster: true`, `clusterRadius`, `clusterMaxZoom` — gom hàng nghìn điểm thành cụm tự động. Không cần tự viết.
- **`queryRenderedFeatures` vs `querySourceFeatures`**: cái đầu chỉ trả về thứ **đang hiển thị trên màn hình** (dùng cho click, hover, chọn vùng); cái sau trả về thứ có trong tile đã tải, kể cả ngoài khung nhìn. Nhầm lẫn giữa hai cái này dẫn tới bug "chọn thiếu đối tượng".
- **Marker vs symbol layer**: `maplibregl.Marker` tạo một phần tử DOM thật. Đẹp, dễ gắn sự kiện, nhưng **chỉ dùng khi có vài chục điểm**. Vài nghìn điểm → dùng layer `circle` hoặc `symbol`, vẽ trên GPU.
- **Controls dựng sẵn**: `NavigationControl`, `ScaleControl`, `GeolocateControl`, `FullscreenControl`, `AttributionControl`.
- **`sprite` và `glyphs`**: hai URL trong style trỏ tới bộ icon và bộ font. Không có `glyphs`, layer `symbol` sẽ không hiện được chữ nào — lỗi im lặng khó đoán.
- **Terrain 3D và hillshade**: cần source `raster-dem`.
- **`@mapbox/mapbox-gl-draw`**: repo đang dùng để vẽ/sửa hình học. Cần nhớ nó vốn viết cho Mapbox, nên khi ghép với MapLibre phải để ý tương thích phiên bản và nhớ import CSS của nó. Lựa chọn thay thế nếu gặp trục trặc: `terra-draw` hoặc `@watergis/maplibre-gl-terradraw` (đã có sẵn một dòng import bị comment trong `layers.component.ts`).
- **`@turf/turf`**: mọi phép tính không gian ở client — `area`, `buffer`, `centroid`, `booleanPointInPolygon`, `distance`. Turf làm việc trực tiếp trên GeoJSON.

---

## Nhóm 3 — Cạm bẫy cần quan tâm

### Hiệu năng: đừng đánh nhau với GPU

MapLibre nhanh khi bạn để nó tự lo phần vẽ. Nó chậm thảm hại khi bạn can thiệp từng khung hình. Ba nguyên tắc:

1. **Một source, nhiều layer** — đừng tạo mỗi đối tượng một source.
2. **Đừng gọi `setPaintProperty` trong vòng lặp** — dùng expression đọc từ `properties` hoặc `feature-state`.
3. **Đừng `setData` liên tục** — mỗi lần gọi là một lần MapLibre parse lại toàn bộ GeoJSON.

Khi lượng dữ liệu vượt ngưỡng vài MB GeoJSON, hãy chuyển sang **vector tile** phía server thay vì cố tối ưu ở client. Đây là ranh giới kiến trúc, không phải vấn đề tinh chỉnh.

### Tích hợp với Angular (rất quan trọng ở repo này)

MapLibre phát ra sự kiện liên tục khi người dùng kéo/zoom bản đồ (`move`, `render`, hàng chục lần mỗi giây). Angular mặc định chạy change detection sau **mỗi** sự kiện. Kết quả: toàn bộ ứng dụng bị quét lại 60 lần/giây, giao diện giật.

Cách xử lý: khởi tạo bản đồ **ngoài** vùng Angular.

```ts
constructor(private ngZone: NgZone) {}

ngAfterViewInit() {
  this.ngZone.runOutsideAngular(() => {
    this.map = new maplibregl.Map({ container: 'map', ... });
  });
}

ngOnDestroy() {
  this.map?.remove();   // bắt buộc — nếu không sẽ rò rỉ WebGL context
}
```

Khi cần cập nhật giao diện Angular từ một callback của bản đồ, gói lại bằng `this.ngZone.run(() => { ... })`.

Hai điểm nữa: bản đồ phải được khởi tạo trong `ngAfterViewInit` (không phải `ngOnInit`) vì phần tử container chưa tồn tại; và nếu layout thay đổi (mở/đóng sidebar), phải gọi `map.resize()`, nếu không bản đồ sẽ bị méo.

### Nối với backend của dự án

Backend lưu `Feature.Geometry` dưới dạng `NetTopologySuite.Geometries.Geometry` trong SQL Server. Đường đi của dữ liệu tới bản đồ:

```
SQL Server (spatial) → NetTopologySuite → GeoJsonWriter → JSON qua API
                                                            → GeoJSON source trong MapLibre
```

Cần quan tâm: `Geometry` của NetTopologySuite **không serialize thẳng ra JSON được**. Phải chuyển qua `NetTopologySuite.IO.GeoJSON` (`GeoJsonWriter`) hoặc lưu dạng chuỗi WKT/GeoJSON trong DTO. Nếu quên, API sẽ trả về một đống thuộc tính nội bộ vô nghĩa, hoặc ném lỗi vòng lặp tham chiếu.

Cần quan tâm nữa: SQL Server phân biệt `geometry` (mặt phẳng) và `geography` (mặt cầu), và **quan tâm chiều xoay của đa giác** (ring orientation) đối với `geography`. Đa giác vẽ ngược chiều sẽ được hiểu là "toàn bộ thế giới trừ vùng này".

---

## Thứ tự học đề xuất

1. Chạy được một bản đồ trống với nền raster từ OpenStreetMap. Hiểu `container`, `style`, `center`, `zoom`.
2. Thêm một `geojson` source và một layer `circle`. Hiểu `map.on('load')`.
3. Đổi màu điểm theo thuộc tính bằng `["match", ["get", ...]]`. Hiểu expression.
4. Thêm click → popup. Hiểu sự kiện gắn theo layer.
5. Thêm hover highlight bằng `feature-state`. Hiểu vì sao không `setData`.
6. Đổi sang `vector` source từ một tile server. Hiểu `source-layer` (thuộc tính này **chỉ** vector source mới có, và quên nó là lỗi "layer không hiện gì" phổ biến nhất).
7. Đọc `layers.component.ts` trong repo, ánh xạ từng đoạn vào các bước trên.

## Nguồn tra cứu

- [MapLibre GL JS — API docs](https://maplibre.org/maplibre-gl-js/docs/) — tra cú pháp.
- [MapLibre Style Spec](https://maplibre.org/maplibre-style-spec/) — tra `paint`/`layout`/expression. Trang bạn sẽ mở nhiều nhất.
- [Turf.js](https://turfjs.org/) — tra phép tính không gian.
- Ví dụ của Mapbox GL JS **v1** vẫn dùng được; ví dụ v2/v3 thì cần kiểm chứng lại.
