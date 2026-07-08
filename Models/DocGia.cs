using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class DocGia
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên độc giả")]
        [StringLength(100)]
        public string HoTen { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(15)]
        public string SoDienThoai { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }
    }
}
