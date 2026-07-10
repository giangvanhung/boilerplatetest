# MapLibre / Mapbox là gì?

## Câu trả lời ngắn

**Mapbox GL JS** là một thư viện JavaScript vẽ bản đồ tương tác lên trình duyệt bằng **WebGL**. Thay vì tải về các ảnh bản đồ đã render sẵn (như Google Maps đời đầu hay Leaflet + ảnh PNG), nó tải về **dữ liệu hình học thô** rồi để card đồ họa của máy người dùng vẽ ra. Nhờ vậy bản đồ có thể xoay, nghiêng, phóng to mượt ở mọi mức zoom, và đổi màu/đổi kiểu ngay lập tức mà không cần tải lại gì.

**MapLibre GL JS** là bản **fork mã nguồn mở** của Mapbox GL JS.

## Vì sao lại có MapLibre? (phần bắt buộc phải hiểu)

Cuối năm 2020, Mapbox phát hành Mapbox GL JS **v2** và đổi giấy phép từ BSD-3 (tự do) sang **giấy phép thương mại độc quyền**. Kể từ v2:

- Bắt buộc phải có **access token** của Mapbox mới khởi tạo được bản đồ.
- Thư viện gửi telemetry về Mapbox và **tính tiền theo số lượt tải bản đồ** (map load), kể cả khi bạn dùng dữ liệu bản đồ của chính mình.
- Không được tự host hay sửa mã nguồn tùy ý.

Cộng đồng phản ứng bằng cách fork phiên bản cuối cùng còn giấy phép tự do — **v1.13** — và phát triển tiếp thành **MapLibre GL JS**. Vì thế:

| | Mapbox GL JS (v2+) | MapLibre GL JS |
|---|---|---|
| Giấy phép | Thương mại, độc quyền | Mã nguồn mở (BSD-3) |
| Access token | **Bắt buộc** | Không cần |
| Chi phí | Trả theo lượt tải bản đồ | Miễn phí |
| Nguồn dữ liệu bản đồ | Gắn với hạ tầng Mapbox | Tùy chọn, tự host được |
| Dịch vụ đi kèm (geocoding, chỉ đường…) | Có, đầy đủ | Không — tự lo hoặc dùng bên thứ ba |
| Chủ quản | Công ty Mapbox | Dự án cộng đồng, không phụ thuộc nhà cung cấp |

!!! success "Vì sao dự án này chọn MapLibre"
    Hệ thống tự host toàn bộ dữ liệu không gian trong SQL Server và tự phục vụ tile. Không có lý do gì để trả phí theo map load cho Mapbox, cũng không muốn bị khóa vào hạ tầng của họ.

## Vậy học cái nào?

Học **một** là biết **cả hai**. MapLibre giữ gần như nguyên vẹn API của Mapbox GL JS v1, chỉ đổi tên biến toàn cục:

```js
// Mapbox
const map = new mapboxgl.Map({ container: 'map', style: '...', center: [105.85, 21.03], zoom: 10 });

// MapLibre — giống hệt
const map = new maplibregl.Map({ container: 'map', style: '...', center: [105.85, 21.03], zoom: 10 });
```

Điều này có một hệ quả rất thực dụng: **phần lớn hướng dẫn, câu hỏi StackOverflow và ví dụ của Mapbox GL JS v1 vẫn áp dụng được cho MapLibre**. Chỉ cần cẩn thận khi gặp tài liệu của Mapbox v2/v3 — một số tính năng ở đó (globe projection, một số nguồn dữ liệu độc quyền) MapLibre không có hoặc làm theo cách khác.

Hai nhánh đã tách nhau từ 2020 nên ngày càng khác. Khi tra cứu chính xác cú pháp, hãy dùng **[maplibre.org/maplibre-gl-js/docs](https://maplibre.org/maplibre-gl-js/docs/)** và **[maplibre.org/maplibre-style-spec](https://maplibre.org/maplibre-style-spec/)** làm nguồn chuẩn.

## Ba khái niệm nền tảng

Nếu chỉ nhớ được ba thứ từ trang này, hãy nhớ ba thứ sau.

### 1. Vector tile — dữ liệu được cắt thành ô

Bản đồ thế giới quá lớn để tải một lần. Nó được cắt thành lưới ô vuông theo từng mức zoom, đánh địa chỉ `z/x/y` (zoom / cột / hàng). Trình duyệt chỉ tải những ô đang nằm trong khung nhìn.

- **Raster tile**: mỗi ô là một tấm ảnh PNG/JPG đã vẽ sẵn. Muốn đổi màu đường phố → phải render lại toàn bộ ở server.
- **Vector tile**: mỗi ô là một file nhị phân (`.pbf`, theo chuẩn Mapbox Vector Tile) chứa tọa độ hình học và thuộc tính. Trình duyệt tự vẽ. Muốn đổi màu → sửa một dòng style, hiệu lực tức thì.

MapLibre làm việc được với cả hai, nhưng vector tile mới là lý do người ta chọn nó.

### 2. Style Spec — trái tim của thư viện

Bản đồ trong MapLibre **không** được cấu hình bằng lời gọi hàm, mà bằng một **đối tượng JSON** mô tả toàn bộ diện mạo. Đây là điểm khiến người quen Leaflet thấy lạ nhất, và cũng là thứ đáng đầu tư thời gian nhất.

```json
{
  "version": 8,
  "sources": {
    "cac-lop-cua-toi": { "type": "vector", "tiles": ["https://.../{z}/{x}/{y}.pbf"] }
  },
  "layers": [
    {
      "id": "duong-giao-thong",
      "type": "line",
      "source": "cac-lop-cua-toi",
      "source-layer": "roads",
      "paint": { "line-color": "#888", "line-width": 2 }
    }
  ]
}
```

Ghi nhớ sự phân biệt **source** và **layer**:

- **Source** = *dữ liệu ở đâu* (một endpoint vector tile, một đối tượng GeoJSON, một tấm ảnh…).
- **Layer** = *vẽ dữ liệu đó ra sao* (đường màu xám dày 2px).

Một source có thể nuôi nhiều layer. Ví dụ cùng một lớp `roads` có thể vẽ ba lần: một layer viền đen dày, một layer ruột trắng mỏng đè lên, một layer `symbol` hiện tên đường. Đó là cách bản đồ đẹp được tạo ra.

### 3. Expression — style biết tự tính toán

Giá trị trong `paint`/`layout` không nhất thiết là hằng số. Nó có thể là một biểu thức nhỏ, viết dưới dạng mảng JSON, được đánh giá cho từng đối tượng và từng mức zoom:

```json
"line-width": ["interpolate", ["linear"], ["zoom"], 8, 1, 16, 8],
"fill-color": ["match", ["get", "loai_dat"], "ONT", "#f4a", "OTC", "#4af", "#ccc"]
```

Dòng đầu: đường rộng 1px ở zoom 8, giãn dần tới 8px ở zoom 16. Dòng sau: tô màu theo thuộc tính `loai_dat` của từng thửa đất, mặc định xám.

Đây là công cụ mạnh nhất và cũng gây bối rối nhất của thư viện. Không thành thạo expression thì sẽ có xu hướng viết JavaScript thủ công để tô màu từng đối tượng — cách làm này giết chết hiệu năng.

## Còn hệ sinh thái xung quanh?

Bản thân MapLibre **chỉ vẽ bản đồ**. Nó không có sẵn: nền bản đồ, tìm kiếm địa chỉ, chỉ đường, công cụ vẽ, hay phép tính không gian. Bạn phải tự ghép:

- **Nền bản đồ (basemap)**: tự dựng từ dữ liệu OpenStreetMap, hoặc dùng dịch vụ như MapTiler, Protomaps, OpenFreeMap.
- **Máy chủ tile**: Martin, pg_tileserv, Tegola, GeoServer, hoặc phục vụ file tĩnh (`.mbtiles`, `.pmtiles`).
- **Vẽ / sửa hình học**: repo này dùng `@mapbox/mapbox-gl-draw`.
- **Phép tính không gian** (diện tích, vùng đệm, điểm-trong-đa-giác): repo này dùng `@turf/turf`.

Sự "thiếu thốn" này là cái giá của tự do — đổi lại, bạn kiểm soát toàn bộ hạ tầng.

---

Đã hiểu MapLibre là gì? Tiếp theo: [**cần học và cần quan tâm những gì**](lo-trinh-hoc.md).
