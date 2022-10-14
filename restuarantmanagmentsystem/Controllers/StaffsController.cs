

namespace restuarantmanagmentsystem.Controllers
{
    public class StaffsController : Controller
    {
        private readonly DataContext _context;
        public const string SessionKeyStaffID = "_StaffID";
        public const string SessionKeyLogin = "_Login";
        public const string SessionKeyName = "_Name";
        public StaffsController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(int passcode)
        {

            var staff = await _context.Staff
              .FirstOrDefaultAsync(m => m.Passcode == passcode);

            if (staff != null)
            {

                HttpContext.Session.SetInt32(SessionKeyStaffID, staff.Id);
                HttpContext.Session.SetString(SessionKeyName, staff.Name);

                if (staff.Passcode == 1000)
                {
                    return RedirectToAction("Admin", "Staffs", staff);
                }
                else
                {
                    return RedirectToAction("Index", "Tables", staff);
                }

                
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt, try again");
                return View();
            }

}

        public async Task<IActionResult> Admin()
        {

            return View();
        }


            // GET: Staffs
        public async Task<IActionResult> Index()
        {
              return _context.Staff != null ? 
                          View(await _context.Staff.ToListAsync()) :
                          Problem("Entity set 'DataContext.Staff'  is null.");
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Passcode")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Staff == null)
            {
                return Problem("Entity set 'DataContext.Staff'  is null.");
            }
            var staff = await _context.Staff.FindAsync(id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
