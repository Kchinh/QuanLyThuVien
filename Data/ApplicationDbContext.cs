using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<TheLoai> TheLoais { get; set; }
        public DbSet<Sach> Saches { get; set; }
        public DbSet<DocGia> DocGias { get; set; }
        public DbSet<PhieuMuon> PhieuMuons { get; set; }
        public DbSet<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Đảm bảo TenDangNhap là duy nhất (không trùng)
            modelBuilder.Entity<NguoiDung>()
                .HasIndex(u => u.TenDangNhap)
                .IsUnique();

            // Quan hệ 1 PhieuMuon - nhiều ChiTietPhieuMuon
            modelBuilder.Entity<ChiTietPhieuMuon>()
                .HasOne(ct => ct.PhieuMuon)
                .WithMany(p => p.ChiTietPhieuMuons)
                .HasForeignKey(ct => ct.PhieuMuonId)
                .OnDelete(DeleteBehavior.Cascade); // xóa phiếu thì xóa luôn chi tiết

            // Seed 1 tài khoản Admin mặc định để đăng nhập lần đầu
            // Tài khoản: admin / Mật khẩu: 123456 (đã hash sẵn bằng BCrypt)
            modelBuilder.Entity<NguoiDung>().HasData(new NguoiDung
            {
                Id = 1,
                HoTen = "Quản trị viên",
                TenDangNhap = "admin",
                MatKhauHash = "$2b$12$/BIkP98iFS/1qD0.MSqz3uEqYKkc8hv2YX4VwSzIIYKiiSGT0oQMq",
                VaiTro = VaiTro.Admin
            });
        }
    }
}
