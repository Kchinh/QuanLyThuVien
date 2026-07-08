using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyThuVien.Models
{
    public class TheLoai
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên thể loại")]
        [StringLength(100)]
        public string TenTheLoai { get; set; }

        // Navigation property: 1 thể loại có nhiều sách
        [NotMapped] // sẽ bỏ NotMapped nếu cần dùng navigation, tạm để đơn giản ở bước sau
        [ValidateNever] // không phải dữ liệu người dùng nhập từ form Create/Edit, loại khỏi ModelState validation
        public List<Sach> DanhSachSach { get; set; }
    }
}
