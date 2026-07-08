using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Models
{
    // Bảng chi tiết: mỗi dòng = 1 cuốn sách trong 1 phiếu mượn
    public class ChiTietPhieuMuon
    {
        public int Id { get; set; }

        public int PhieuMuonId { get; set; }
        [ForeignKey("PhieuMuonId")]
        public PhieuMuon PhieuMuon { get; set; }

        public int SachId { get; set; }
        [ForeignKey("SachId")]
        public Sach Sach { get; set; }

        // Null = chưa trả, có giá trị = đã trả vào ngày này
        public DateTime? NgayTraThucTe { get; set; }
    }
}
