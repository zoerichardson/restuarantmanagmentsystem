

namespace restuarantmanagmentsystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly DataContext _context;
        public const string SessionKeyTableNumber = "_TableNumber";
        public const string SessionKeyTableDetails = "_TableID";
        public const string SessionKeyOrderTotal = "_OrderTotal";
        public const string SessionKeyOrderID = "_OrderID";
        public OrdersController(DataContext context)
        {
            _context = context;
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Table table)
        {

            if (table.Id == null || _context.Order == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetInt32(SessionKeyTableDetails, table.Id);

            var tableData = await _context.Table
                 .Include(o => o.Orders)
                .FirstOrDefaultAsync(m => m.Id == table.Id);

            HttpContext.Session.SetInt32(SessionKeyTableNumber, tableData.TableNumber);

            var order = await _context.Order
                .Where(m => m.TableNumber == tableData.TableNumber)
                .ToListAsync();


            var itemPrice = 0;
            foreach (var o in order)
            {

                ViewData["OrderInfo"] = new Order()
                {
                    TableNumber = table.TableNumber,

                };
                itemPrice = (int)(itemPrice + o.Total);
                HttpContext.Session.SetInt32(SessionKeyOrderTotal, itemPrice);
            }

            return View(order);

        }

        // GET: Orders/Create
        public async Task<IActionResult> Create(Table tableId, int id)//staff staff
        {

            var tableInfo = await _context.Table
    .FirstOrDefaultAsync(m => m.Id == tableId.Id);

            int staffID = (int)HttpContext.Session.GetInt32("_StaffID");
            

            ViewData["OrderInfo"] = new Order()
            {
                StaffID = staffID, //staff.id
                TableNumber = tableInfo.TableNumber,

            };

            var starter = await _context.Menu
                .Where(m => m.CategoryType == 1)
                .ToListAsync();
            ViewData["Starter"] = new SelectList(starter, "Name", "Name");

            var main = await _context.Menu
                .Where(m => m.CategoryType == 2)
                .ToListAsync();

            ViewData["Main"] = new SelectList(main, "Name", "Name");


            var dessert = await _context.Menu
                .Where(m => m.CategoryType == 3)
                .ToListAsync();
            ViewData["Dessert"] = new SelectList(dessert, "Name", "Name");


            var drinks = await _context.Menu
                .Where(m => m.CategoryType == 4)
                .ToListAsync();
            ViewData["Drinks"] = new SelectList(drinks, "Name", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order, List<string> selectedOptions)
        {
            //if (selectedOptions.Count == 0)
            //{
            //    // ModelState.AddModelError(string.Empty, "Select items to create an order.");
            //    // return RedirectToAction("Create", "Orders", 101);
            //    //  return View(order);
            //    return Problem("Select items to proceed with order");
            //}

            //Order tableOrder = new Order();
            //tableOrder.Items = viewModel.

            
            var staffID = (int)HttpContext.Session.GetInt32("_StaffID");

            if (ModelState.IsValid)
            {

                var count = selectedOptions.Count;
                    foreach (var item in selectedOptions)
                    {
                        var menu = await _context.Menu
                        .FirstOrDefaultAsync(m => m.Name == item);

                        var itemPrice = menu.Price;
                        order.Total = itemPrice;
                        order.StaffID = staffID;
                        order.Items = menu.Name;
                        _context.Add(order);
                        await _context.SaveChangesAsync();

                        if (count > 1)
                        {
                            order.ID = 0;
                            count -= 1;
                        }
                    }

                    HttpContext.Session.SetInt32(SessionKeyOrderID, order.ID);

                    var tableInfo = await _context.Table
                        .FirstOrDefaultAsync(m => m.TableNumber == order.TableNumber);

                    tableInfo.IsAvailable = false;
                    _context.Update(tableInfo);
                    await _context.SaveChangesAsync();

                    var staff = await _context.Staff
                        .FirstOrDefaultAsync(m => m.Id == staffID);

                    return RedirectToAction("Index", "Tables", staff);
                }
            else
            {
                ModelState.AddModelError(string.Empty, "Select items to create order");
                ViewData["StaffID"] = new SelectList(_context.Staff, "Id", "Id", order.StaffID);

                return View(order);
            }
        }
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var starter = await _context.Menu
                    .Where(m => m.CategoryType == 1)
                    .ToListAsync();
            ViewData["Starter"] = new SelectList(starter, "Name", "Name");

            var main = await _context.Menu
                 .Where(m => m.CategoryType == 2)
                 .ToListAsync();
            ViewData["Main"] = new SelectList(main, "Name", "Name");


            var dessert = await _context.Menu
                .Where(m => m.CategoryType == 3)
                .ToListAsync();
            ViewData["Dessert"] = new SelectList(dessert, "Name", "Name");


            var drinks = await _context.Menu
                .Where(m => m.CategoryType == 4)
                .ToListAsync();
            ViewData["Drinks"] = new SelectList(drinks, "Name", "Name");

            var table = await _context.Table
                .FirstOrDefaultAsync(m => m.Id == id);

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.TableNumber == table.TableNumber);

            ViewData["OrderInfo"] = order;

            return View();
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var table = await _context.Table
    .FirstOrDefaultAsync(m => m.Id == id);

            var order = await _context.Order
                 .Where(m => m.TableNumber == table.TableNumber)
                 .ToListAsync();

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {
            var tableNo = HttpContext.Session.GetInt32("_TableID");

            var table = await _context.Table
                .Include(o => o.Orders)
                .FirstOrDefaultAsync(m => m.Id == tableNo);

            var order = await _context.Order
                .Where(m => m.TableNumber == table.TableNumber)
                .ToListAsync();


            if (_context.Order == null)
            {
                return Problem("Entity set 'DataContext.Order'  is null.");
            }

            if (order != null)
            {
                foreach (var o in order)
                {
                    _context.Order.Remove(o);
                }
            }

            table.IsAvailable = true;
            _context.Update(table);
            await _context.SaveChangesAsync();

            DayOfWeek wk = DateTime.Today.DayOfWeek;
            


            int orderTotal = (int)HttpContext.Session.GetInt32("_OrderTotal");

            var revenue = await _context.DayRevenue
                .FirstOrDefaultAsync(m => m.Day == wk.ToString());

            revenue.DayTotal = revenue.DayTotal + orderTotal;
            
            _context.Update(revenue);
            await _context.SaveChangesAsync();

            var staffID = HttpContext.Session.GetInt32("_StaffID");
            var staff = await _context.Staff
            .FirstOrDefaultAsync(m => m.Id == staffID);

            return RedirectToAction("Index", "Tables", staff);
        }

        public async Task<IActionResult> RemoveItem(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                 .FirstOrDefaultAsync(m => m.ID == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("RemoveItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveItem(int id)
        {
            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.ID == id);

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();


            var tableNo = HttpContext.Session.GetInt32("_TableID");

            var table = await _context.Table
                    .FirstOrDefaultAsync(m => m.Id == tableNo);

            return RedirectToAction("Details", "Orders", table);
        }
     
    }
}
