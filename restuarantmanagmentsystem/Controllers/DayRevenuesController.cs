


namespace restuarantmanagmentsystem.Controllers
{
    public class DayRevenuesController : Controller
    {
        private readonly DataContext _context;

        public DayRevenuesController(DataContext context)
        {
            _context = context;
        }

        // GET: Revenues
        public async Task<IActionResult> Index()
        {
            return _context.DayRevenue != null ?
                        View(await _context.DayRevenue.ToListAsync()) :
                        Problem("Entity set 'DataContext.Revenue'  is null.");
        }
    }
}
  