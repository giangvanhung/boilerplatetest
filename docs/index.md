# Tổng quan

Tài liệu này theo dõi 2 đầu việc chính của giai đoạn hiện tại (GĐ3):

1. **[Hoàn thiện Web GIS mới](web-gis/ke-hoach-hoan-thien.md)** — Angular + Mapbox/MapLibre.
2. **[Slide kiến trúc hệ thống mới](architecture/slide-kien-truc-hieu-nang.md)** — chứng minh hiệu năng vượt trội so với GĐ2.

!!! note "Trạng thái repo hiện tại"
    Repo `gServer_gd3` hiện là boilerplate ASP.NET Core + Angular (chưa có module GIS/Mapbox nào được implement). Angular đang có sẵn `maplibre-gl` trong `package.json`, chưa có mapbox-gl. Các nội dung dưới đây là **kế hoạch/outline** — cần điền số liệu, quyết định kỹ thuật thực tế khi triển khai.

## Cách build tài liệu này

```bash
pip install mkdocs-material
mkdocs serve   # xem tại http://127.0.0.1:8000
mkdocs build   # xuất site tĩnh vào ./site
```
