namespace QuanLyThuVien.Repositories
{
    // Interface riêng cho Sach, kế thừa Generic + thêm hàm đặc thù
    public interface ISachRepository : IGenericRepository<Models.Sach>
    {
        // Lấy sách kèm thông tin Thể loại (join sẵn), dùng cho danh sách/tìm kiếm
        Task<IEnumerable<Models.Sach>> GetAllWithTheLoaiAsync();
        Task<Models.Sach> GetByIdWithTheLoaiAsync(int id);
        Task<IEnumerable<Models.Sach>> SearchAsync(string keyword);
    }
}
