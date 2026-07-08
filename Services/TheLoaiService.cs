using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Services
{
    public class TheLoaiService : ITheLoaiService
    {
        private readonly IGenericRepository<TheLoai> _theLoaiRepo;
        private readonly ISachRepository _sachRepo;

        public TheLoaiService(IGenericRepository<TheLoai> theLoaiRepo, ISachRepository sachRepo)
        {
            _theLoaiRepo = theLoaiRepo;
            _sachRepo = sachRepo;
        }

        public async Task<IEnumerable<TheLoai>> GetAllAsync()
        {
            return await _theLoaiRepo.GetAllAsync();
        }

        public async Task<TheLoai> GetByIdAsync(int id)
        {
            return await _theLoaiRepo.GetByIdAsync(id);
        }

        public async Task AddAsync(TheLoai theLoai)
        {
            await _theLoaiRepo.AddAsync(theLoai);
            await _theLoaiRepo.SaveChangesAsync();
        }

        public async Task UpdateAsync(TheLoai theLoai)
        {
            _theLoaiRepo.Update(theLoai);
            await _theLoaiRepo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var theLoai = await _theLoaiRepo.GetByIdAsync(id);
            if (theLoai == null) return false;

            // Kiểm tra nghiệp vụ: không cho xóa thể loại nếu vẫn còn sách thuộc thể loại đó
            var sachTrongTheLoai = await _sachRepo.FindAsync(s => s.TheLoaiId == id);
            if (sachTrongTheLoai.Any())
                return false; // Controller sẽ hiển thị thông báo lỗi tương ứng

            _theLoaiRepo.Delete(theLoai);
            await _theLoaiRepo.SaveChangesAsync();
            return true;
        }
    }
}
