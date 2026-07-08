using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Services
{
    public class SachService : ISachService
    {
        private readonly ISachRepository _sachRepo;

        public SachService(ISachRepository sachRepo)
        {
            _sachRepo = sachRepo;
        }

        public async Task<IEnumerable<Sach>> GetAllAsync()
        {
            return await _sachRepo.GetAllWithTheLoaiAsync();
        }

        public async Task<Sach> GetByIdAsync(int id)
        {
            return await _sachRepo.GetByIdWithTheLoaiAsync(id);
        }

        public async Task<IEnumerable<Sach>> SearchAsync(string keyword)
        {
            return await _sachRepo.SearchAsync(keyword);
        }

        public async Task AddAsync(Sach sach)
        {
            // Khi thêm sách mới, số lượng còn lại = số lượng nhập vào
            sach.SoLuongConLai = sach.SoLuong;
            await _sachRepo.AddAsync(sach);
            await _sachRepo.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sach sach)
        {
            // Khi sửa sách: nếu tăng SoLuong thì cộng thêm phần chênh lệch vào SoLuongConLai
            var sachCu = await _sachRepo.GetByIdAsync(sach.Id);
            if (sachCu != null)
            {
                int chenhLech = sach.SoLuong - sachCu.SoLuong;
                sach.SoLuongConLai = sachCu.SoLuongConLai + chenhLech;

                // Không cho SoLuongConLai âm (trường hợp giảm số lượng quá số đang được mượn)
                if (sach.SoLuongConLai < 0)
                    sach.SoLuongConLai = 0;
            }

            _sachRepo.Update(sach);
            await _sachRepo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sach = await _sachRepo.GetByIdAsync(id);
            if (sach == null) return false;

            // Không cho xóa nếu sách đang được mượn (SoLuongConLai < SoLuong nghĩa là có cuốn chưa trả)
            if (sach.SoLuongConLai < sach.SoLuong)
                return false;

            _sachRepo.Delete(sach);
            await _sachRepo.SaveChangesAsync();
            return true;
        }
    }
}
