# Hệ thống Quản lý Thư viện

Đồ án Bài tập lớn môn Kỹ thuật Phần mềm (CSE702025) — Trường Công nghệ Thông tin, Đại học Phenikaa.

## Công nghệ sử dụng
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core + SQL Server (LocalDB)
- BCrypt.Net (mã hóa mật khẩu)
- Bootstrap 5

## Kiến trúc
- Mô hình MVC kết hợp kiến trúc 3 lớp: Controller → Service → Repository → Model
- Design Pattern: Repository Pattern (Generic Repository + Repository riêng cho các Entity cần truy vấn phức tạp)

## Chức năng chính
- Đăng nhập, phân quyền Admin / Thủ thư
- Quản lý Sách, Thể loại, Độc giả, Nhân viên (CRUD)
- Lập phiếu mượn sách (nhiều sách/phiếu), Trả sách
- Dashboard thống kê (tổng quan, top sách mượn nhiều, cảnh báo quá hạn)

## Cách chạy project

1. Clone repository:
   ```
   git clone <URL_repo_cua_ban>
   cd QuanLyThuVien
   ```

2. Khôi phục package:
   ```
   dotnet restore
   ```

3. Tạo database:
   ```
   dotnet ef database update
   ```

4. Chạy project:
   ```
   dotnet run
   ```

5. Mở trình duyệt tại địa chỉ được terminal hiển thị (VD: `https://localhost:xxxx`)

## Tài khoản mặc định
| Tên đăng nhập | Mật khẩu | Vai trò |
|---|---|---|
| admin | 123456 | Admin |

## Nhóm thực hiện
- [Điền tên + MSV các thành viên]

## Giảng viên hướng dẫn
TS. Trương Đức Phương
