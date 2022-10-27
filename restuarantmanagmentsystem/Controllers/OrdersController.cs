

using restuarantmanagmentsystem.Models;

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

          
            var tableData = GetTableOrders(table.Id);
         
            HttpContext.Session.SetInt32(SessionKeyTableNumber, tableData.Result.TableNumber);

            var order = GetOrderTableNumber(tableData.Result.TableNumber);


            var itemPrice = 0;
            foreach (var o in order.Result)
            {

                ViewData["OrderInfo"] = new Order()
                {
                    TableNumber = table.TableNumber,

                };
                itemPrice = (int)(itemPrice + o.Total);
                HttpContext.Session.SetInt32(SessionKeyOrderTotal, itemPrice);
            }

            return View(order.Result);

        }

        // GET: Orders/Create
        public async Task<IActionResult> Create(Table table)
        {
            var tableData = GetTableOrders(table.Id);

            int staffID = (int)HttpContext.Session.GetInt32("_StaffID");
            

            ViewData["OrderInfo"] = new Order()
            {
                StaffID = staffID,
                TableNumber = tableData.Result.TableNumber,

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

                var table = await _context.Table
                   .FirstOrDefaultAsync(m => m.TableNumber == order.TableNumber);



             

                    table.IsAvailable = false;
                    _context.Update(table);
                    await _context.SaveChangesAsync();

              
                var staff = GetStaffID(staffID);

                    return RedirectToAction("Index", "Tables", staff.Result);
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


            var table = GetTableOrders((int)id);

            var order = await _context.Order
                    .FirstOrDefaultAsync(m => m.TableNumber == table.Result.TableNumber);

            
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

        
            var table = GetTableOrders((int)id);

        
            var order = GetOrderTableNumber(table.Result.TableNumber);

            if (order == null)
            {
                return NotFound();
            }

            return View(order.Result);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {
            var tableId = HttpContext.Session.GetInt32("_TableID");

           

            var table = GetTableOrders((int)tableId);

        

            var order = GetOrderTableNumber(table.Result.TableNumber);

            if (_context.Order == null)
            {
                return Problem("Entity set 'DataContext.Order'  is null.");
            }

            if (order != null)
            {
                foreach (var o in order.Result)
                {
                    _context.Order.Remove(o);
                }
            }

            table.Result.IsAvailable = true;
            _context.Update(table.Result);
            await _context.SaveChangesAsync();

            DayOfWeek wk = DateTime.Today.DayOfWeek;
            


            int orderTotal = (int)HttpContext.Session.GetInt32("_OrderTotal");

            var revenue = await _context.DayRevenue
                .FirstOrDefaultAsync(m => m.Day == wk.ToString());

            revenue.DayTotal = revenue.DayTotal + orderTotal;
            
            _context.Update(revenue);
            await _context.SaveChangesAsync();

            var staffID = HttpContext.Session.GetInt32("_StaffID");
          
            var staff = GetStaffID((int)staffID);

            return RedirectToAction("Index", "Tables", staff.Result);
        }

        public async Task<IActionResult> RemoveItem(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

          

            var order = GetOrderID((int)id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order.Result);
        }

        [HttpPost, ActionName("RemoveItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveItem(int id)
        {
            var order = GetOrderID(id);

            _context.Order.Remove(order.Result);
            await _context.SaveChangesAsync();


            var tableNo = HttpContext.Session.GetInt32("_TableID");

            var table = await _context.Table
                    .FirstOrDefaultAsync(m => m.Id == tableNo);
           

            return RedirectToAction("Details", "Orders", table);
        }
     

        public async Task<Table> GetTableOrders(int tableId)
        {
       
            var table = await _context.Table
                .Include(o => o.Orders)
                .FirstOrDefaultAsync(m => m.Id == tableId);

            return table;
        }

        public async Task<List<Order>> GetOrderTableNumber(int tableNumber)
        {
            var order = await _context.Order
                .Where(m => m.TableNumber == tableNumber)
                .ToListAsync();

            return order;
        }

        public async Task<Order> GetOrderID(int orderID)
        {
            var order = await _context.Order
                 .FirstOrDefaultAsync(m => m.ID == orderID);

            return order;
        }

        public async Task<Staff> GetStaffID(int staffID)
        {
            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.Id == staffID);

            return staff;
        }

        
    }

 
}
