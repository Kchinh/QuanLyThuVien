using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models.ViewModels
{
    // ViewModel dùng để gom dữ liệu từ Form "Lập phiếu mượn"
    // (không phải DTO tầng nghiệp vụ, chỉ để chứa input từ View)
    public class LapPhieuMuonViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn độc giả")]
        public int DocGiaId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày hẹn trả")]
        [DataType(DataType.Date)]
        public DateTime NgayHenTra { get; set; }

        // Danh sách Id các sách được chọn để mượn (checkbox trên View)
        [Required(ErrorMessage = "Vui lòng chọn ít nhất 1 cuốn sách")]
        [MinLength(1, ErrorMessage = "Vui lòng chọn ít nhất 1 cuốn sách")]
        public List<int> SachIds { get; set; } = new();

        // Dùng để load lại danh sách sách lên View (dropdown/checkbox), không phải input của user
        public List<Sach> DanhSachSachCoSan { get; set; } = new();
        public List<DocGia> DanhSachDocGia { get; set; } = new();
    }
}
