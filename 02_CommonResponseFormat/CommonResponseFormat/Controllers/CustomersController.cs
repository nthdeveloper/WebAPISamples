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

        public HttpResponseMessage Get()
        {
            ResponseMessage _response = new ResponseMessage()
            {
                IsSuccess = true,
                HttpStatusCode = (int)HttpStatusCode.OK,
                Content = CustomerList.ToArray()
            };

            return Request.CreateResponse<ResponseMessage>(HttpStatusCode.OK, _response);
        }

        public HttpResponseMessage Get(int id)
        {
            var _customer = CustomerList.FirstOrDefault(x => x.Id == id);

            if (_customer == null)
            {
                return Request.CreateResponse<ResponseMessage>(
                    HttpStatusCode.NotFound,
                    new ResponseMessage()
                    {
                        IsSuccess = false,
                        HttpStatusCode = (int)HttpStatusCode.NotFound,
                        Message = $"Customer with id {id} does not exist."
                    });
            }

            return Request.CreateResponse<ResponseMessage>(
                HttpStatusCode.OK,
                new ResponseMessage()
                {
                    IsSuccess = true,
                    HttpStatusCode = (int)HttpStatusCode.OK,
                    Content = _customer
                });
        }

        public HttpResponseMessage Post(Customer customer)
        {
            if (this.ModelState.IsValid)
            {
                int _maxId = CustomerList.Max(x => x.Id);
                _maxId++;
                customer.Id = _maxId;

                CustomerList.Add(customer);

                return Request.CreateResponse<ResponseMessage>(
                    HttpStatusCode.Created,
                    new ResponseMessage()
                    {
                        IsSuccess = true,
                        HttpStatusCode = (int)HttpStatusCode.Created,
                        Content = customer
                    });
            }

            return Request.CreateResponse<ResponseMessage>(
                    HttpStatusCode.BadRequest,
                    new ResponseMessage()
                    {
                        IsSuccess = false,
                        HttpStatusCode = (int)HttpStatusCode.BadRequest,
                        Message="Invalid customer data."
                    });           
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
            {
                return Request.CreateResponse<ResponseMessage>(
                    HttpStatusCode.NotFound,
                    new ResponseMessage()
                    {
                        IsSuccess = false,
                        HttpStatusCode = (int)HttpStatusCode.NotFound,
                        Message = $"Customer with id {id} does not exist."
                    });
            }               

            CustomerList.Remove(_customer);

            return Request.CreateResponse<ResponseMessage>(
                    HttpStatusCode.OK,
                    new ResponseMessage()
                    {
                        IsSuccess = true,
                        HttpStatusCode = (int)HttpStatusCode.OK,
                        Message = "Customer deleted.",
                        Content = _customer
                    });            
        }
    }
}
