using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Models
{
    public class Sach
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sách")]
        [StringLength(200)]
        public string TenSach { get; set; }

        [StringLength(100)]
        public string TacGia { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không được âm")]
        public int SoLuong { get; set; }

        // Số lượng còn lại để cho mượn (giảm khi mượn, tăng khi trả)
        public int SoLuongConLai { get; set; }

        // Khóa ngoại đến TheLoai
        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        public int TheLoaiId { get; set; }

        [ForeignKey("TheLoaiId")]
        public TheLoai TheLoai { get; set; }
    }
}
