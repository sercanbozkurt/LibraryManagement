using System.Linq;
using System.Threading.Tasks;
using LibraryManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Kartlar için verileri al
            var toplamKitapSayisi = await _context.Books.CountAsync();
            var toplamAktifKitapSayisi = await _context.Books.CountAsync(b => b.IsActive);
            var toplamOgrenciSayisi = await _context.Students.CountAsync();
            var toplamAktifOgrenciSayisi = await _context.Students.CountAsync(s => s.IsActive);

            // Öğrenciye göre rezervasyon sayılarını al ve büyükten küçüğe sırala
            var ogrenciSiralamasi = await _context.Reservations
                .GroupBy(r => r.StudentID)
                .Select(g => new
                {
                    StudentID = g.Key,
                    AdSoyad = _context.Students.Where(s => s.StudentID == g.Key).Select(s => s.AdSoyad).FirstOrDefault(),
                    KitapSayisi = g.Count()
                })
                .OrderByDescending(g => g.KitapSayisi)
                .ToListAsync();

            // Kitaba göre kaç farklı öğrenci tarafından okunduğunu al ve büyükten küçüğe sırala
            var kitapSiralamasi = await _context.Reservations
                .GroupBy(r => r.BookID)
                .Select(g => new
                {
                    BookID = g.Key,
                    KitapBilgi = _context.Books.Where(b => b.BookID == g.Key).Select(b => b.BookInfo).FirstOrDefault(),
                    OgrenciSayisi = g.Select(r => r.StudentID).Distinct().Count()
                })
                .OrderByDescending(g => g.OgrenciSayisi)
                .ToListAsync();

            ViewBag.ToplamKitapSayisi = toplamKitapSayisi;
            ViewBag.ToplamAktifKitapSayisi = toplamAktifKitapSayisi;
            ViewBag.ToplamOgrenciSayisi = toplamOgrenciSayisi;
            ViewBag.ToplamAktifOgrenciSayisi = toplamAktifOgrenciSayisi;
            ViewBag.OgrenciSiralamasi = ogrenciSiralamasi;
            ViewBag.KitapSiralamasi = kitapSiralamasi;

            return View();
        }
    }
}
