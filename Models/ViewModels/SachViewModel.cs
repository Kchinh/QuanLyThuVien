using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models.ViewModels
{
    // ViewModel dùng cho Form Thêm/Sửa Sách, kèm danh sách Thể loại để đổ vào dropdown
    public class SachViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sách")]
        [StringLength(200)]
        public string TenSach { get; set; }

        [StringLength(100)]
        public string TacGia { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        public int TheLoaiId { get; set; }

        // Dùng để đổ dữ liệu vào thẻ <select>, không phải input của người dùng
        public List<TheLoai> DanhSachTheLoai { get; set; } = new();
    }
}
