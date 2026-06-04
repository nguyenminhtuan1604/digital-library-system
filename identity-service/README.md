# Nhóm 3 - Identity & Report Service

Đề tài: Hệ thống quản lý thư viện số. Service Nhóm 3 phụ trách xác thực, hồ sơ độc giả, thẻ thư viện, thống kê và báo cáo.

## Công nghệ
- Backend: ASP.NET Core 8 Web API, JWT Bearer, Swagger/OpenAPI
- Frontend: Vue 3 + Vuetify 3
- Database: SQL Server, database riêng `IdentityDB`
- Container: Docker + docker-compose

## Chức năng đúng tiêu chí N3
1. Xác thực hệ thống JWT: đăng ký, đăng nhập, mã hóa mật khẩu SHA256 demo, phân quyền Admin/Librarian/Reader.
2. Quản lý hồ sơ độc giả và thẻ thư viện: danh sách, trạng thái, khóa/mở/gia hạn thẻ.
3. API cho Nhóm 2 kiểm tra thẻ trước khi mượn: `GET /api/cards/validate/{readerId}`.
4. Nhận event từ Nhóm 2: `POST /api/events/book-borrowed`, `POST /api/events/book-returned`.
5. Thống kê sách và mượn trả: `GET /api/reports/books`, `GET /api/reports/borrowing`.
6. Báo cáo tổng hợp: `GET /api/reports/overview`, `GET /api/reports/fines`.
7. Dashboard Admin: `GET /api/dashboard`.

## Chạy bằng Docker
```powershell
cd D:\TaiLieu\FullStack\Nhom3_IdentityReportService_Final
copy .env.example .env
docker compose up -d --build
```

Mở:
- Frontend: http://localhost:5173
- Swagger: http://localhost:5003/swagger
- Health: http://localhost:5003/health

## Tài khoản mẫu
- admin@library.local / Admin@123
- librarian@library.local / Librarian@123
- reader@library.local / Reader@123

## Kết nối SQL Server từ máy tính
- Server: `localhost,1433`
- Database: `IdentityDB`
- User: `sa`
- Password: `Your_password123`
- TrustServerCertificate: `True`

## API tích hợp với nhóm khác
N2 gọi N3 kiểm tra thẻ:
```http
GET http://localhost:5003/api/cards/validate/3
```

N2 gửi event mượn sách sang N3:
```http
POST http://localhost:5003/api/events/book-borrowed
Authorization: Bearer <token admin/librarian>
Content-Type: application/json

{
  "readerId": 3,
  "bookId": 101,
  "bookTitle": "Clean Code",
  "borrowedAt": "2026-06-01T08:00:00Z"
}
```

N2 gửi event trả sách sang N3:
```http
POST http://localhost:5003/api/events/book-returned
Authorization: Bearer <token admin/librarian>
Content-Type: application/json

{
  "readerId": 3,
  "bookId": 101,
  "returnedAt": "2026-06-10T08:00:00Z",
  "fineAmount": 15000
}
```
