# ASP.NET Boilerplate — Cần học & cần biết gì?

Trang này chia kiến thức thành ba nhóm: **phải thạo**, **phải biết là có tồn tại**, và **cạm bẫy**. Đọc [trang giới thiệu](gioi-thieu.md) trước nếu chưa rõ ABP là gì.

Điều kiện cần: đã biết C#, `async/await`, và LINQ ở mức căn bản. ABP không dạy bạn những thứ đó, và không che giấu chúng.

---

## Nhóm 1 — Phải thạo

### Entity và các lớp cơ sở

Mọi entity kế thừa `Entity<TKey>`. Nhưng ABP còn cho sẵn các lớp mang theo trường kiểm toán:

| Lớp cơ sở | Trường tự động thêm |
|---|---|
| `Entity<int>` | `Id` |
| `CreationAuditedEntity<int>` | `+ CreationTime`, `CreatorUserId` |
| `AuditedEntity<int>` | `+ LastModificationTime`, `LastModifierUserId` |
| `FullAuditedEntity<int>` | `+ IsDeleted`, `DeletionTime`, `DeleterUserId` |

Các trường này được ABP **tự điền** khi lưu — bạn không gán tay. Trong repo hiện tại, `Layer`, `Feature`, `Style` mới chỉ dùng `Entity<int>` trần; nếu cần biết ai sửa lớp bản đồ lúc nào, chỉ việc đổi lớp cơ sở.

Đặc biệt lưu ý `FullAuditedEntity` implement `ISoftDelete`: gọi `DeleteAsync` sẽ **không xóa dòng khỏi database**, chỉ đặt `IsDeleted = true`, và mọi truy vấn sau đó tự động lọc bỏ nó. Rất tiện, nhưng gây bối rối khi bạn mở SQL Server ra và thấy dữ liệu "đã xóa" vẫn còn nguyên.

### Application Service và DTO

Đây là nơi bạn viết phần lớn code.

- Kế thừa `AsyncCrudAppService<TEntity, TDto, TKey>` để có ngay 5 endpoint CRUD.
- Hoặc kế thừa `kamrjAppServiceBase` khi cần tự do hoàn toàn.
- **Không bao giờ trả về entity trực tiếp.** Luôn ánh xạ sang DTO qua `ObjectMapper.Map<TDto>(entity)`. Trả entity ra API sẽ lộ cấu trúc database, gây vòng lặp tham chiếu (`Layer` → `Feature` → `Layer` → …), và khóa chặt API vào schema.
- Cấu hình ánh xạ bằng attribute `[AutoMapFrom(typeof(Feature))]` trên DTO, hoặc khai báo trong `Application/Mapping/CustomDtoMapper.cs`.

### Repository và truy vấn

```csharp
// Cách dùng thường gặp
await Repository.GetAllListAsync(f => f.LayerId == layerId);
await Repository.FirstOrDefaultAsync(f => f.Id == id);
await Repository.InsertAsync(feature);

// Khi cần truy vấn phức tạp — trả về IQueryable
var query = Repository.GetAll()
    .Where(f => f.LayerId == layerId)
    .OrderBy(f => f.Name);
```

Hai điều phải nhớ:

1. **Lazy loading tắt mặc định.** `feature.Layer` sẽ là `null` trừ khi bạn nạp tường minh: `Repository.GetAllIncluding(f => f.Layer)` hoặc `.Include(...)` trên `GetAll()`.
2. **`GetAll()` chỉ dùng được bên trong một Unit of Work.** Trong application service thì luôn thỏa mãn. Ở nơi khác (một domain service gọi từ background job chẳng hạn), phải bọc bằng `[UnitOfWork]` hoặc `_unitOfWorkManager.Begin()`.

### Phân quyền

Quy trình ba bước, làm sai bước nào cũng hỏng:

```csharp
// 1. Đặt tên hằng — kamrj.Core/Authorization/PermissionNames.cs
public const string Pages_Layers = "Pages.Layers";

// 2. Đăng ký — kamrj.Core/Authorization/kamrjAuthorizationProvider.cs
context.CreatePermission(PermissionNames.Pages_Layers, L("Layers"));

// 3. Sử dụng
[AbpAuthorize(PermissionNames.Pages_Layers)]
public async Task<ListResultDto<LayerDto>> GetAll() { ... }
```

Bỏ qua bước 2 thì `[AbpAuthorize]` ném exception lúc chạy vì quyền không tồn tại — chứ không phải "cho qua". Kiểm tra quyền trong thân hàm bằng `PermissionChecker.IsGranted(...)` khi logic phức tạp hơn một attribute.

### Xử lý lỗi

Đây là thứ nhỏ nhưng ảnh hưởng trực tiếp tới trải nghiệm người dùng:

- Ném `UserFriendlyException("Lớp bản đồ này đang được sử dụng.")` → thông điệp **hiện nguyên văn** cho người dùng trên giao diện Angular.
- Ném bất kỳ exception nào khác → người dùng chỉ thấy "An internal error occurred", chi tiết đi vào log.

Nghĩa là: lỗi nghiệp vụ dùng `UserFriendlyException`; lỗi kỹ thuật cứ để nó ném ra tự nhiên.

### Vòng lặp phát triển: sửa backend → cập nhật frontend

```
1. Sửa/thêm AppService, DTO trong aspnet-core/
2. Chạy kamrj.Web.Host  (Swagger phải lên được tại /swagger)
3. Chạy angular/nswag/refresh.bat   ← sinh lại service-proxies.ts
4. Dùng proxy mới trong component Angular
```

**Quên bước 3 là lỗi phổ biến nhất của người mới.** Triệu chứng: bạn vừa viết `GetFeatureByLayerIdAsync` ở C# nhưng TypeScript báo "property does not exist". Backend phải **đang chạy** thì `refresh.bat` mới đọc được Swagger.

---

## Nhóm 2 — Phải biết là có tồn tại

Chưa cần đào sâu, nhưng phải biết để không tự viết lại thứ đã có.

- **Module system** — mỗi project có một class kế thừa `AbpModule` (`kamrjCoreModule`, `kamrjApplicationModule`), dùng `[DependsOn(...)]` để khai báo phụ thuộc, và ba hook vòng đời: `PreInitialize()` (cấu hình), `Initialize()` (đăng ký DI), `PostInitialize()` (chạy sau khi mọi thứ sẵn sàng). Cấu hình toàn cục hầu như luôn nằm trong `PreInitialize()`.

- **Multi-tenancy** — hệ thống hỗ trợ nhiều "người thuê" dùng chung một database. Entity implement `IMustHaveTenant` sẽ tự động bị lọc theo `AbpSession.TenantId` ở **mọi** truy vấn. Bạn không viết `WHERE TenantId = ...` bao giờ. Nếu chưa dùng tenant, chỉ cần biết cơ chế này tồn tại — vì có lúc nó khiến truy vấn "mất" dữ liệu và bạn cần biết tại sao (tạm bỏ qua bằng `_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant)`).

- **Data filter** — cơ chế đứng sau cả `ISoftDelete` lẫn multi-tenancy. Một bộ lọc EF Core toàn cục, có thể bật/tắt theo phạm vi.

- **Feature & Edition** — bật/tắt tính năng theo gói thuê bao. Repo có sẵn `Core/Features/`. Kiểm tra bằng `[RequiresFeature("...")]` hoặc `FeatureChecker`.

- **Setting management** — cấu hình có phân cấp (mặc định → tenant → người dùng), qua `SettingProvider` và `SettingManager`. Đừng nhét cấu hình động vào `appsettings.json`.

- **Localization** — file XML/JSON trong `Core/Localization/`, gọi bằng `L("TenKhoa")`. Chuỗi trả về đúng ngôn ngữ người dùng.

- **Audit logging** — ABP tự ghi lại mọi lời gọi application service (ai, khi nào, tham số gì, mất bao lâu) vào `AbpAuditLogs`. Miễn phí, không cần làm gì.

- **Background job** — `IBackgroundJobManager.EnqueueAsync<TJob, TArgs>(...)` cho việc chạy nền. Mặc định dùng hàng đợi trong database; thay được bằng Hangfire.

- **Event bus** — `IEventBus.Trigger(...)` và các sự kiện dựng sẵn như `EntityCreatedEventData<Feature>`. Cách để module này phản ứng với thay đổi ở module kia mà không phụ thuộc trực tiếp.

- **Caching** — `ICacheManager`, mặc định in-memory, đổi sang Redis được bằng một dòng cấu hình.

- **SignalR** — gói `Abp.AspNetCore.SignalR` đã có sẵn; phía Angular xem `shared/helpers/SignalRAspNetCoreHelper.ts`. Dùng khi cần đẩy dữ liệu realtime xuống client.

---

## Nhóm 3 — Cạm bẫy cần quan tâm

### `NetTopologySuite.Geometry` trong DTO

Cạm bẫy đặc thù và quan trọng nhất của repo này. `Feature.Geometry` có kiểu `NetTopologySuite.Geometries.Geometry` — đây là một object phức tạp, có tham chiếu vòng, **không serialize thẳng ra JSON được**, và AutoMapper cũng không tự chuyển nó thành GeoJSON.

Hướng xử lý: trong DTO, để `Geometry` dưới dạng `string` (GeoJSON hoặc WKT), và chuyển đổi tường minh bằng `NetTopologySuite.IO.GeoJSON` (`GeoJsonWriter` / `GeoJsonReader`) — gói này đã có trong `.csproj`. Nếu bỏ qua, triệu chứng là API trả về một mớ thuộc tính nội bộ (`Coordinates`, `Envelope`, `Factory`, `SRID`…) hoặc ném lỗi vòng lặp tham chiếu.

Cùng nhóm vấn đề: SQL Server phân biệt kiểu `geometry` (mặt phẳng) và `geography` (mặt cầu). Kiểu `geography` **quan tâm chiều xoay của đa giác** — vẽ ngược chiều thì nó hiểu là "toàn bộ trái đất trừ vùng này". Và luôn nhớ đặt đúng `SRID` (thường là 4326).

### "Magic" chỉ hoạt động khi bạn theo đúng quy ước

ABP đánh đổi sự tường minh lấy tốc độ viết code. Cái giá là: khi bạn lệch khỏi quy ước, mọi thứ hỏng **một cách im lặng**.

- Class không implement `IApplicationService` (trực tiếp hay gián tiếp) → không có endpoint nào được sinh ra, không có cảnh báo nào.
- Interface không đặt tên đúng dạng `IFooAppService` cho class `FooAppService` → DI không tự ghép cặp.
- Phương thức không `public` → không thành endpoint.

Khi một endpoint "biến mất", hãy kiểm tra ba điều trên trước khi nghi ngờ bất cứ thứ gì khác. Swagger UI (`/swagger`) là công cụ chẩn đoán đầu tiên: nếu endpoint không có ở đó, vấn đề nằm ở backend chứ không phải Angular.

### Đừng chống lại Unit of Work

Vì mỗi request là một transaction, gọi `SaveChanges()` thủ công giữa chừng thường thừa và đôi khi sai. Nếu cần dữ liệu được ghi ngay để dùng ID vừa sinh, dùng `await CurrentUnitOfWork.SaveChangesAsync()` — nó flush nhưng **không** commit transaction.

### AutoMapper cấu hình ở đâu

Ánh xạ DTO ↔ entity nằm rải ở hai nơi: attribute `[AutoMapFrom]`/`[AutoMapTo]` trên DTO, và `Application/Mapping/CustomDtoMapper.cs`. Khi một trường "im lặng bị null" sau khi map, gần như chắc chắn là do tên trường lệch nhau và AutoMapper không có quy tắc — nó không báo lỗi, chỉ bỏ qua.

---

## Thứ tự học đề xuất

1. Mở `kamrj.sln`, chạy `kamrj.Web.Host`, mở `/swagger`. Nhìn thấy các endpoint `/api/services/app/...` được sinh ra từ đâu.
2. Đọc `Models/FeatureModel/FeatureAppService.cs` — 20 dòng, nhưng chứa gần hết các khái niệm cốt lõi: `AsyncCrudAppService`, `IRepository`, `ObjectMapper`, DTO.
3. Thêm một phương thức mới vào `FeatureAppService`, chạy `refresh.bat`, gọi nó từ một component Angular. Đây là vòng lặp phát triển bạn sẽ lặp lại hàng trăm lần.
4. Thêm một entity mới → DbContext → migration → AppService → DTO → proxy. Đi hết một lượt từ database lên giao diện.
5. Thêm một quyền mới vào `PermissionNames` + `kamrjAuthorizationProvider`, gắn `[AbpAuthorize]`, kiểm tra `isGranted` ở Angular.
6. Đọc `kamrjCoreModule.cs` và `kamrjApplicationModule.cs` để hiểu mọi thứ được ráp lại thế nào.

## Nguồn tra cứu

- [aspnetboilerplate.com/Pages/Documents](https://aspnetboilerplate.com/Pages/Documents) — tài liệu chính thức. **Đây** là site đúng, không phải `abp.io`.
- Mã nguồn ABP trên GitHub (`aspnetboilerplate/aspnetboilerplate`) — khi tài liệu không đủ, đọc thẳng mã nguồn thường nhanh hơn đoán.
- Swagger UI của chính dự án — nguồn sự thật về API đang thực sự tồn tại.
