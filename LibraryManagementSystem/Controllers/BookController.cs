using System.Threading.Tasks;
using LibraryManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers{

    public class BookController : Controller{
        private readonly DataContext _context;
        public BookController(DataContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index(){
            return View(await _context.Books.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book model){
            _context.Books.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", "Book");
        }

        public async Task<IActionResult> Edit(int? id){
            if(id == null){
                return NotFound();
            }

            var book = await _context.Books.Include(b=>b.Reservations).ThenInclude(b=>b.Student).FirstOrDefaultAsync(b=>b.BookID == id);

            if(book == null) {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(int id, Book model){
            if(id != model.BookID){
                return NotFound();
            }

            if(ModelState.IsValid){
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(_context.Books.Any(s=>s.BookID == model.BookID)){
                        return NotFound();
                    }
                    else {
                    throw;
                    }
                    
                }
                return RedirectToAction("Index"); 
            }
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult>Delete(int? id){
            if(id == null){
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if(book == null){
                return NotFound();
            }
            return RedirectToAction("Index", book);
        }

        [HttpPost]
        public async Task<IActionResult>Delete([FromForm]int id){
            var book = await _context.Books.FindAsync(id);
            if(book == null){
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}