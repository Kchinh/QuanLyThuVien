using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Models
{
    // Trạng thái tổng thể của phiếu mượn
    public enum TrangThaiPhieuMuon
    {
        DangMuon,   // còn ít nhất 1 sách chưa trả
        DaTraHet    // tất cả sách trong phiếu đã trả
    }

    public class PhieuMuon
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn độc giả")]
        public int DocGiaId { get; set; }
        [ForeignKey("DocGiaId")]
        public DocGia DocGia { get; set; }

        // Người lập phiếu (thủ thư/admin đang đăng nhập)
        public int NguoiDungId { get; set; }
        [ForeignKey("NguoiDungId")]
        public NguoiDung NguoiDung { get; set; }

        public DateTime NgayMuon { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Vui lòng chọn ngày hẹn trả")]
        public DateTime NgayHenTra { get; set; }

        public TrangThaiPhieuMuon TrangThai { get; set; } = TrangThaiPhieuMuon.DangMuon;

        // Navigation: 1 phiếu mượn có nhiều chi tiết (nhiều cuốn sách)
        public List<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; } = new();
    }
}
