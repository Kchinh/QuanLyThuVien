using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Repositories;
using QuanLyThuVien.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Đăng ký DbContext dùng SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Bật Session để lưu thông tin đăng nhập
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // tự đăng xuất sau 60 phút không hoạt động
    options.Cookie.HttpOnly = true;
});

// Đăng ký các Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ISachRepository, SachRepository>();
builder.Services.AddScoped<IPhieuMuonRepository, PhieuMuonRepository>();
builder.Services.AddScoped<IGenericRepository<QuanLyThuVien.Models.TheLoai>, GenericRepository<QuanLyThuVien.Models.TheLoai>>();
builder.Services.AddScoped<IGenericRepository<QuanLyThuVien.Models.DocGia>, GenericRepository<QuanLyThuVien.Models.DocGia>>();
builder.Services.AddScoped<IGenericRepository<QuanLyThuVien.Models.NguoiDung>, GenericRepository<QuanLyThuVien.Models.NguoiDung>>();
builder.Services.AddScoped<IGenericRepository<QuanLyThuVien.Models.ChiTietPhieuMuon>, GenericRepository<QuanLyThuVien.Models.ChiTietPhieuMuon>>();

// Đăng ký các Service
builder.Services.AddScoped<ITheLoaiService, TheLoaiService>();
builder.Services.AddScoped<IDocGiaService, DocGiaService>();
builder.Services.AddScoped<ISachService, SachService>();
builder.Services.AddScoped<IPhieuMuonService, PhieuMuonService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<INguoiDungService, NguoiDungService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
