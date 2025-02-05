using System.Threading.Tasks;
using LibraryManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers{

    public class StudentController : Controller{
        private readonly DataContext _context;
        public StudentController(DataContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index(){
            return View(await _context.Students.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student model){
            _context.Students.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", "Student");
        }

        public async Task<IActionResult> Edit(int? id){
            if(id == null){
                return NotFound();
            }

            var std = await _context.Students.Include(s=>s.Reservations).ThenInclude(s=>s.Book).FirstOrDefaultAsync(s=>s.StudentID == id);

            if(std == null) {
                return NotFound();
            }
            return View(std);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(int id, Student model){
            if(id != model.StudentID){
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
                    if(_context.Students.Any(s=>s.StudentID == model.StudentID)){
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
            var std = await _context.Students.FindAsync(id);
            if(std == null){
                return NotFound();
            }
            return RedirectToAction("Index", std);
        }

        [HttpPost]
        public async Task<IActionResult>Delete([FromForm]int id){
            var std = await _context.Students.FindAsync(id);
            if(std == null){
                return NotFound();
            }

            _context.Students.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}