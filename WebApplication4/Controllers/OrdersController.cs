using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication4.DataAccess;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class OrdersController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customer);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Discount,Address,CustomerId,IsCurrent")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var order = db.Orders.Include(x => x.OrderPositions)
                .Include(x => x.OrderPositions.Select(o => o.Product))
                .FirstOrDefault(x => x.Id == id);

            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Discount,Address,CustomerId,IsCurrent")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Cart()
        {
            if (!User.Identity.IsAuthenticated)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var customer = db.Customers.FirstOrDefault(x => x.Login == User.Identity.Name);

            if (customer == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var order =
                db.Orders
                .Include(x => x.OrderPositions)
                .Include(x => x.OrderPositions.Select(o => o.Product))
                .FirstOrDefault(x => x.CustomerId == customer.Id && x.IsCurrent);

            if (order == null)
            {
                order = new Order
                {
                    Customer = customer,
                    IsCurrent = true,
                    OrderPositions = new List<OrderPosition>()
                };
                db.Orders.Add(order);
                db.SaveChanges();
            }

            return View(order);
        }

        public ActionResult RemoveFromCart(int id)
        {
            var orderPosition = db.OrderPositions.Include(x => x.Product).FirstOrDefault(x => x.Id == id);

            if (orderPosition == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (orderPosition.Count > 0)
            {
                orderPosition.Count--;
                db.SaveChanges();
            }

            return Content(orderPosition.Count.ToString());
        }

        public ActionResult AddToCart(int id)
        {
            var orderPosition = db.OrderPositions.Include(x => x.Product).FirstOrDefault(x => x.Id == id);

            if (orderPosition == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (orderPosition.IsCanAddCount())
            {
                orderPosition.Count++;
                db.SaveChanges();
                return Content(orderPosition.Count.ToString());
            }
            else
            {
                return Content($"К сожалению, продукта {orderPosition.Product.Name} больше нет :(");

            }
        }

        public ActionResult RemovePositionFromCart(int id)
        {
            var orderPosition = db.OrderPositions.FirstOrDefault(x => x.Id == id);
            if (orderPosition == null)
                return RedirectToAction("Cart");

            db.OrderPositions.Remove(orderPosition);
            db.SaveChanges();

            return RedirectToAction("Cart");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
