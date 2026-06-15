# Catalog Service

Vai trò:
- Quản lý sách
- Quản lý thể loại
- Quản lý bản sao sách
- Kiểm tra trạng thái có thể mượn

Database:
- CatalogDB

API chính:
- GET /api/books
- POST /api/books
- PUT /api/books/{id}
- DELETE /api/books/{id}
- GET /api/books/{bookId}/copies
- GET /api/copies/{copyId}/availability
- PUT /api/copies/{copyId}/status

Lưu ý:
- Service này không có frontend riêng.
- Frontend chính nằm ở /frontend.
- API được gọi thông qua /api-gateway.