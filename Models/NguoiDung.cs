using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    // Vai trò của người dùng trong hệ thống
    public enum VaiTro
    {
        Admin,
        ThuThu
    }

    public class NguoiDung
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        // Lưu mật khẩu đã được hash bằng BCrypt, KHÔNG lưu plain text
        [Required]
        public string MatKhauHash { get; set; }

        public VaiTro VaiTro { get; set; } = VaiTro.ThuThu;
    }
}
