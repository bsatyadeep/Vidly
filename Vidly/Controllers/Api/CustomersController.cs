using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Vidly.Models;
using Vidly.Models.Dtos;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //GET api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);
        }
        //GET api/customers/1
        public IHttpActionResult GetCustomers(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

            //if (customer == null)
            //    throw new HttpResponseException(HttpStatusCode.NotFound);
            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
            //return Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto);
        }
        //POST api/customers
        [HttpPost]
        //public CustomerDto CreateCustomer(CustomerDto customerDto)
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            //if (!ModelState.IsValid)
            //    throw new HttpResponseException(HttpStatusCode.BadRequest);
            if (!ModelState.IsValid)
                return BadRequest();
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            //var newCustomer = Mapper.Map<Customer, CustomerDto>(customer);
            return Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto);
        }
        //POST api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customerInDb = _context.Customers.Find(id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);

            //var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            //customerInDb.Name = customer.Name;
            //customerInDb.BirthDate = customer.BirthDate;
            //customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            //customerInDb.MembershipTypeId = customer.MembershipTypeId;

            _context.Entry(customerInDb).State = EntityState.Modified;
            _context.SaveChanges();
        }
        //DELETE api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerIdDb = _context.Customers.Find(id);
            if (customerIdDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerIdDb);
            _context.SaveChanges();
        }
    }
}
