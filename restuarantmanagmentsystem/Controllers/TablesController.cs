﻿

using restuarantmanagmentsystem.Models;

namespace restuarantmanagmentsystem.Controllers
{
    public class TablesController : Controller
    {
        private readonly DataContext _context;

        public TablesController(DataContext context)
        {
            _context = context;
        }

        // GET: Tables
        public async Task<IActionResult> Index(Staff staff)
        {

            var order = GetOrder(staff.Id);
            var tables = GetTables(order.Result);


            return View(tables.Result);
        }

        // GET: Tables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TableNumber,IsAvailable")] Table table)
        {
            if (TableNoExists(table.TableNumber))
            {
                ModelState.AddModelError(string.Empty, "Table number already exists, try again");
                return View(table);
            }

            if (ModelState.IsValid)
            {
                _context.Add(table);

                await _context.SaveChangesAsync();
                
                return RedirectToAction("Admin", "Staffs");
            }
            return View(table);
        }

        private bool TableNoExists(int tableNo)
        {
            return (_context.Table?.Any(e => e.TableNumber == tableNo)).GetValueOrDefault();
        }
        private async Task<List<Order>> GetOrder(int staffID)
        {
            var order = await _context.Order
               .Where(m => m.StaffID == staffID)
               .ToListAsync();

            return order;
        }

        private async Task<List<Table>> GetTables(List<Order> order)
        {
            List<Table> staffTables = new();
            foreach (var o in order)
            {
                var tableNo = o.TableNumber;
                var table = await _context.Table
                    .FirstOrDefaultAsync(m => m.TableNumber == tableNo);

                staffTables.Add(table);
            }
        
            var tableDuplicates = staffTables.GroupBy(x => x)
                                            .Where(g => g.Count() > 1)
                                            .Select(x => x.Key);

            var allTables = staffTables.GroupBy(x => x)
                            .Where(g => g.Count() == 1)
                            .Select(x => x.Key);

            List<Table> tableList = tableDuplicates.ToList();

            tableList.AddRange(allTables);

            var availableTables = await _context.Table
                      .Where(m => m.IsAvailable == true)
                      .ToListAsync();

            tableList.AddRange(availableTables);

           

            return tableList;
        }





    }
}
