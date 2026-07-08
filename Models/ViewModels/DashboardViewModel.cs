namespace QuanLyThuVien.Models.ViewModels
{
    // Gom các số liệu thống kê để hiển thị lên Dashboard
    public class DashboardViewModel
    {
        public int TongSoSach { get; set; }
        public int TongSoDauSach { get; set; }
        public int TongSoDocGia { get; set; }
        public int SoPhieuDangMuon { get; set; }
        public int SoPhieuQuaHan { get; set; }

        // Top 5 sách được mượn nhiều nhất (Tên sách, Số lượt mượn)
        public List<(string TenSach, int SoLuotMuon)> TopSachMuonNhieu { get; set; } = new();
    }
}
