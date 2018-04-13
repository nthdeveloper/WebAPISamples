using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPISamples.Models;

namespace WebAPISamples.Controllers
{
    public class CustomersController : ApiController
    {
        private static List<Customer> CustomerList;

        static CustomersController()
        {
            //Prepare in-memory customer list
            CustomerList = new List<Customer>()
            {
                new Customer(){Id=1, Name="Name 1", Surname="Surname 1"},
                new Customer(){Id=2, Name="Name 2", Surname="Surname 2"},
                new Customer(){Id=3, Name="Name 3", Surname="Surname 3"}                
            };
        }        

        public Customer[] Get()
        {
            return CustomerList.ToArray();
        }

        public HttpResponseMessage Get(int id)
        {
            var _customer = CustomerList.FirstOrDefault(x => x.Id == id);

            if (_customer == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            return Request.CreateResponse<Customer>(HttpStatusCode.OK, _customer);
        }

        public HttpResponseMessage Post(Customer customer)
        {
            if (this.ModelState.IsValid)
            {
                int _maxId = CustomerList.Max(x => x.Id);
                _maxId++;
                customer.Id = _maxId;

                CustomerList.Add(customer);

                var response = Request.CreateResponse<Customer>(HttpStatusCode.Created, customer);
                return response;
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Put(Customer customer)
        {
            if (this.ModelState.IsValid)
            {
                var _existingCustomer = CustomerList.FirstOrDefault(x => x.Id == customer.Id);

                if (_existingCustomer == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                _existingCustomer.Name = customer.Name;
                _existingCustomer.Surname = customer.Surname;

                return Request.CreateResponse<Customer>(HttpStatusCode.OK, _existingCustomer);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Delete(int id)
        {
            var _customer = CustomerList.FirstOrDefault(x => x.Id == id);
            if (_customer == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            CustomerList.Remove(_customer);

            return Request.CreateResponse<Customer>(HttpStatusCode.OK, _customer);
        }
    }
}
