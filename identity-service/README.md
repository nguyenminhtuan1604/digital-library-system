# Identity & Report Service

Vai trò:
- Đăng nhập JWT
- Quản lý người dùng
- Quản lý độc giả
- Quản lý thẻ thư viện
- Nhận event mượn/trả
- Dashboard báo cáo

Database:
- IdentityDB

API chính:
- POST /api/auth/login
- POST /api/auth/register
- GET /api/users/me
- GET /api/readers
- POST /api/readers
- GET /api/cards/validate/{readerId}
- POST /api/events/book-borrowed
- POST /api/events/book-returned
- GET /api/dashboard

Lưu ý:
- Service này không còn frontend riêng.
- Gateway chính nằm ở /api-gateway.
- Docker Compose chính nằm ở root repo.
