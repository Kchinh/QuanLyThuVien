using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models.ViewModels
{
    public class NguoiDungViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        // Bắt buộc khi Thêm mới; khi Sửa để trống = giữ nguyên mật khẩu cũ
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn vai trò")]
        public VaiTro VaiTro { get; set; }
    }
}
