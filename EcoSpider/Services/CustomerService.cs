using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using EcoGrpc.Customer;
using EcoSpider.Repositories;
using Google.Protobuf.WellKnownTypes;
using System.Collections.Generic;

namespace EcoSpider.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private CustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }

        public override async Task<CustomerModel> GetCustomerByLookUp(CustomerLookUpModel request,
            ServerCallContext context)
        {
            var customers = await _customerRepository.GetCustomers();
            CustomerModel response = customers.First(customer => customer.Id == request.Id);
            return response;
        }

        public override async Task GetCustomers(Empty request, IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            var customers = await _customerRepository.GetCustomers();
            foreach (CustomerModel customer in customers)
            {
                await responseStream.WriteAsync(customer);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        public override async Task<CustomerModel> GetCustomersFromClient(
            IAsyncStreamReader<CustomerLookUpModel> requestStream, ServerCallContext context)
        {
            List<CustomerModel> customers = await _customerRepository.GetCustomers();
            List<CustomerModel> requestedCustomers = new List<CustomerModel>();

            await foreach (CustomerLookUpModel customerLookUpModel in requestStream.ReadAllAsync())
            {
                Console.WriteLine(customerLookUpModel);
                requestedCustomers.Add(customers.First(customer => customer.Id == customerLookUpModel.Id));
            }

            return requestedCustomers.First();
        }

        public override async Task GetCustomersBidirectional(IAsyncStreamReader<CustomerLookUpModel> requestStream,
            IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            var customers = _customerRepository.GetCustomers().Result;
            await foreach (CustomerLookUpModel lookUpModel in requestStream.ReadAllAsync())
            {
                var customer = customers.FirstOrDefault(cust => cust.Id == lookUpModel.Id);
                Console.WriteLine($"Vindo da requisição talkey? : => {customer}");
                await responseStream.WriteAsync(customer);
            }
        }
    }
}