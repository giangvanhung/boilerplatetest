# Tổng quan

Tài liệu này phục vụ hai mục đích:

1. **Onboarding** — người mới vào dự án cần hiểu hai nền tảng mà toàn bộ hệ thống đứng trên:
    - [MapLibre / Mapbox](maplibre/gioi-thieu.md) — tầng hiển thị bản đồ ở frontend.
    - [ASP.NET Boilerplate](abp/gioi-thieu.md) — framework backend.
2. **Theo dõi công việc GĐ3** — [kế hoạch hoàn thiện Web GIS](web-gis/ke-hoach-hoan-thien.md) và [slide kiến trúc hệ thống](architecture/slide-kien-truc-hieu-nang.md).

## Hệ thống đang dùng gì?

| Thành phần | Công nghệ | Phiên bản trong repo |
|---|---|---|
| Backend framework | ASP.NET Boilerplate (`Abp.*`) | 7.3.0, chạy trên .NET 6 |
| ORM | Entity Framework Core | 6.x |
| Cơ sở dữ liệu | SQL Server + kiểu dữ liệu không gian | qua `NetTopologySuite` |
| Frontend | Angular | 12 |
| Thư viện bản đồ | `maplibre-gl` | ~3.6.0 |
| Vẽ / sửa hình học | `@mapbox/mapbox-gl-draw` | ^1.5.1 |
| Tính toán không gian | `@turf/turf` | 6.5.0 |

!!! warning "Phân biệt hai thứ dễ nhầm nhất"
    - **ASP.NET Boilerplate** (namespace `Abp.*`, site `aspnetboilerplate.com`) **khác** **ABP Framework** (namespace `Volo.Abp.*`, site `abp.io`). Repo này dùng cái thứ nhất. Tra tài liệu nhầm site là mất thời gian vô ích.
    - **MapLibre GL JS** (mã nguồn mở, không cần token) **khác** **Mapbox GL JS v2+** (giấy phép thương mại, bắt buộc access token). Repo này dùng MapLibre. Chi tiết ở [trang giới thiệu](maplibre/gioi-thieu.md).

## Module GIS hiện có

Backend đã có sẵn ba entity trong `aspnet-core/src/kamrj.Core/Models/`:

- `Layer` — một lớp bản đồ, chứa nhiều `Feature` và nhiều `Style`.
- `Feature` — một đối tượng địa lý, có `Geometry` (kiểu `NetTopologySuite.Geometries.Geometry`) và `Properties` dạng chuỗi.
- `Style` — định nghĩa cách vẽ lớp.

Chúng được expose ra frontend qua các application service tại `/api/services/app/Layer`, `/api/services/app/Feature`, `/api/services/app/Style`, và được gọi từ Angular thông qua proxy sinh tự động trong `angular/src/shared/service-proxies/service-proxies.ts`.

## Cách build tài liệu này

```bash
pip install mkdocs-material
mkdocs serve   # xem tại http://127.0.0.1:8000
mkdocs build   # xuất site tĩnh vào ./site
```
