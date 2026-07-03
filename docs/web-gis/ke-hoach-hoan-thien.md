# Kế hoạch hoàn thiện Web GIS mới (Angular + Mapbox)

!!! warning "Cần xác nhận"
    `angular/package.json` hiện khai báo `maplibre-gl@3.6.2`, không phải `mapbox-gl`. Nếu yêu cầu là dùng đúng **Mapbox GL JS** (cần token/license Mapbox) thì cần đổi dependency; nếu MapLibre (fork mã nguồn mở, tương thích API, không cần token) là đủ thì giữ nguyên. Ảnh hưởng trực tiếp tới các mục style/token bên dưới — cần quyết định trước khi triển khai.

## 1. Hạ tầng bản đồ

- [ ] Chốt thư viện: `mapbox-gl` (có token, style Mapbox Studio) hoặc `maplibre-gl` (self-host style, không phụ thuộc bên thứ 3).
- [ ] Tạo `MapService` (Angular) bọc lifecycle của map instance (init/destroy theo route, resize theo layout AdminLTE hiện có).
- [ ] Cấu hình style nền: raster/vector tile server nội bộ hoặc style JSON tùy biến.
- [ ] Quản lý token/API key qua `environment.ts` (không hardcode, không commit key thật).

## 2. Lớp dữ liệu (layers)

- [ ] Chuẩn hoá nguồn dữ liệu: GeoJSON tĩnh, vector tile (MVT) từ backend ASP.NET Core, hoặc WMS/WFS từ GeoServer.
- [ ] Component quản lý danh sách layer (toggle hiện/ẩn, thứ tự z-index, opacity).
- [ ] Style layer theo thuộc tính (phân loại theo field, choropleth, cluster điểm).
- [ ] Popup/infobox khi click feature — lấy schema thuộc tính từ API.

## 3. Tương tác & công cụ

- [ ] Tìm kiếm địa điểm (geocoding) — tự viết hoặc dùng API sẵn có.
- [ ] Vẽ/chỉnh sửa hình học (draw point/line/polygon) — `mapbox-gl-draw` hoặc tương đương cho MapLibre.
- [ ] Đo khoảng cách/diện tích.
- [ ] Export dữ liệu vẽ (GeoJSON) gửi lên backend lưu trữ.

## 4. Tích hợp backend (`aspnet-core`)

- [ ] API CRUD cho các đối tượng địa lý (Controller mới hoặc mở rộng `kamrj.Application`).
- [ ] Lưu geometry trong DB — xác nhận provider (PostGIS/SQL Server spatial types) và cấu hình EF Core tương ứng.
- [ ] Endpoint phát vector tile (MVT) nếu chọn hướng tự phục vụ tile.
- [ ] Phân quyền theo layer/khu vực dựa trên module Authorization có sẵn của ABP.

## 5. Hiệu năng & UX

- [ ] Lazy-load module bản đồ (route riêng, không load `mapbox-gl`/`maplibre-gl` ở các trang không cần).
- [ ] Giới hạn/cluster điểm khi zoom thấp để tránh render quá nhiều feature.
- [ ] Responsive cho mobile (touch gesture, kích thước control).
- [ ] Loading state / skeleton khi tile hoặc API chậm.

## 6. Kiểm thử & hoàn thiện

- [ ] Test thủ công trên các trình duyệt chính (Chrome, Edge, Firefox).
- [ ] Kiểm tra theme AdminLTE không xung đột CSS với canvas bản đồ (z-index, overflow).
- [ ] Viết hướng dẫn sử dụng ngắn cho người dùng cuối.

---

*Checklist trên là khung tổng quát dựa trên các thành phần điển hình của một Web GIS. Cần rà lại theo yêu cầu nghiệp vụ cụ thể (loại dữ liệu, khối lượng feature, đối tượng người dùng) để bổ sung/loại bỏ mục cho phù hợp.*
