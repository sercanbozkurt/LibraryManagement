using System.Threading.Tasks;
using LibraryManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{

    public class ReservationController : Controller
    {

        private readonly DataContext _context;
        public ReservationController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var reservations = await _context
                                .Reservations
                                .Include(r => r.Student)
                                .Include(r => r.Book)
                                .ToListAsync();
            return View(reservations);
        }

        public async Task<IActionResult> Create()
        {

            ViewBag.Students = new SelectList(await _context.Students.Where(s => s.IsActive).ToListAsync(), "StudentID", "AdSoyad");
            ViewBag.Books = new SelectList(await _context.Books.Where(b => b.IsActive).ToListAsync(), "BookID", "BookInfo");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservation model)
        {

            model.Registration = DateTime.Now;
            _context.Reservations.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", "Reservation");

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewBag.Students = new SelectList(await _context.Students.Where(s => s.IsActive).ToListAsync(), "StudentID", "AdSoyad");
            ViewBag.Books = new SelectList(await _context.Books.Where(b => b.IsActive).ToListAsync(), "BookID", "BookInfo");

            return View(reservation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reservation model)
        {
            if (id != model.ReservationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingReservation = await _context.Reservations.FindAsync(id);
                    if (existingReservation == null)
                    {
                        return NotFound();
                    }

                    // Değişiklikleri manuel olarak uygulayın
                    existingReservation.StudentID = model.StudentID;
                    existingReservation.BookID = model.BookID;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    
                    Console.WriteLine($"Hata: {ex.Message}");
    ModelState.AddModelError(string.Empty, "Bu kayıt başka bir kullanıcı tarafından değiştirildi.");
                    if (!_context.Reservations.Any(s => s.ReservationID == model.ReservationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            // ModelState geçerli değilse, ViewBag'leri yeniden doldur ve formu tekrar göster
            ViewBag.Students = new SelectList(await _context.Students.Where(s => s.IsActive).ToListAsync(), "StudentID", "AdSoyad");
            ViewBag.Books = new SelectList(await _context.Books.Where(b => b.IsActive).ToListAsync(), "BookID", "BookInfo");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }






    }

}