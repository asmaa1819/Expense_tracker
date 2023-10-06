

namespace Expense_Tracker.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
              return _context.Categorys != null ? 
                          View(await _context.Categorys.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Categorys'  is null.");
        }

        

        // GET: Category/AddOrUpdate
        public IActionResult AddOrUpdate(int id=0)
        {
          if (id == 0)
                return View(new Category());
          else
                return View(_context.Categorys.Find(id));


        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrUpdate([Bind("CategoryId,Title,Icon,Type")] Category category)
        {
            if (ModelState.IsValid)
            {
                if(category.CategoryId==0)
                   _context.Add(category);
                else
                    _context.Update(category);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        
        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categorys == null)
            {
                return NotFound();
            }

            var category = await _context.Categorys
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categorys == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categorys'  is null.");
            }
            var category = await _context.Categorys.FindAsync(id);
            if (category != null)
            {
                _context.Categorys.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
