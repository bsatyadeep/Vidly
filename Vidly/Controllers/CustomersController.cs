using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    //[Authorize]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        [Route("customers/index")]
        public ActionResult Index()
        {
            //var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            //return View(customers);
            return View();
        }
        [Route("customers/create")]
        public ActionResult Create()
        {
            var customer = new CustomerViewModel
            {
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View(customer);
        }
        [Route("customers/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.MembershipTypes = _context.MembershipTypes.ToList();
                return View(viewModel);
            }
            var customerExists =
                _context.Customers.FirstOrDefault(
                    c => c.Name == viewModel.Customer.Name && c.BirthDate == viewModel.Customer.BirthDate) != null;
            if (!customerExists)
            {
                _context.Customers.Add(new Customer { Name = viewModel.Customer.Name, BirthDate = viewModel.Customer.BirthDate, IsSubscribedToNewsletter = viewModel.Customer.IsSubscribedToNewsletter, MembershipTypeId = viewModel.Customer.MembershipTypeId });
                _context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("Name", "Name already exists.");
                viewModel.MembershipTypes = _context.MembershipTypes.ToList();
                return View(viewModel);
            }
            return RedirectToAction("index");
        }
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var edidCustomer = new CustomerViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View(edidCustomer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.MembershipTypes = _context.MembershipTypes.ToList();
                return View(viewModel);
            }
            var customer = _context.Customers.Find(viewModel.Customer.Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var customerExists =
                    _context.Customers.FirstOrDefault(
                        c => c.Name == viewModel.Customer.Name && c.BirthDate == viewModel.Customer.BirthDate) != null;
            if (!customerExists)
            {
                customer.Name = viewModel.Customer.Name;
                customer.BirthDate = viewModel.Customer.BirthDate;
                customer.IsSubscribedToNewsletter = viewModel.Customer.IsSubscribedToNewsletter;
                customer.MembershipTypeId = viewModel.Customer.MembershipTypeId;
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("Name", "Name already exists.");
                viewModel.MembershipTypes = _context.MembershipTypes.ToList();
                return View(viewModel);
            }

            return RedirectToAction("index");
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}