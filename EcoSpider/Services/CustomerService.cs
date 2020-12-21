using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using EcoGrpc.Customer;
using EcoSpider.Repositories;
using Google.Protobuf.WellKnownTypes;
using System.Collections.Generic;
using EcoSpider.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IO;
using Google.Protobuf;

namespace EcoSpider.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;
        private DataContext _context;

        public CustomerService(DataContext dataContext, ILogger<CustomerService> logger)
        {
            _logger = logger;
            _context = dataContext;
        }

        public override async Task GetCustomers(Empty request, IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            var customers = await _context.Customers.ToListAsync();
            foreach (var customer in customers)
            {
                await responseStream.WriteAsync(customer);
                //pra fingir que trabalha
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        public override Task<StatusMessage> InsertCustomer(CustomerModel request, ServerCallContext context)
        {
            _logger.LogInformation("################################\n Inserindo Um novo Customer...... \n################################");
            try
            {
                _context.Customers.Add(request);
                _context.SaveChanges();
                return Task.FromResult(new StatusMessage()
                {
                    Message = $"deu certo => {context.UserState.ToString()}, Status Code {context.Status.StatusCode}"
                });
            }
            catch (Exception e)
            {
                _logger.LogInformation("################################");
                _logger.LogInformation(e.Message);
                _logger.LogInformation("################################");
                var metadata = new Metadata
                {
                    { "error_message", e.Message }
                };
                throw new RpcException(new Status(StatusCode.Aborted, "Something fucked up man"), metadata);
            }
        }

        public override async Task<StatusMessage> UploadImages(IAsyncStreamReader<DataChunk> requestStream, ServerCallContext context)
        {
            try
            {
                while (!context.CancellationToken.IsCancellationRequested)
                {    
                    // await foreach (var image in requestStream.ReadAllAsync())
                    while(await requestStream.MoveNext())
                    {
                        var name = Guid.NewGuid().ToString().Substring(0, 5) + ".jpg";
                        // var data = image.Data.ToArray();
                        await File.WriteAllBytesAsync("/home/lucas.szeremeta/Downloads/Imagens Copiadas/" + name, requestStream.Current.Data.ToByteArray());
                        await Task.Delay(1000);
                        Console.WriteLine("Yes Baby");
                    }
                        Console.WriteLine("Thank You!");

                }
                return new StatusMessage { Message = "Finalizado com Sucesso" };

            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public override async Task<DataChunk> GetImage(RequestImage request, ServerCallContext context)
        {
            try
            {
                var data = await File.ReadAllBytesAsync(request.Path);
                var response = new DataChunk { Data = ByteString.CopyFrom(data) };
                return response;
            }
            catch (FileNotFoundException fe)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "File not found!"), new Metadata{
                    {"error_message", fe.Message}
                });
            }
            catch (Exception e)
            {
                throw new RpcException(new Status(StatusCode.Unknown, "Some Unexpected error has ocurred"), new Metadata{
                    {"error_message", e.Message}
                });
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override Task<CustomerModel> GetCustomerByLookUp(CustomerLookUpModel request, ServerCallContext context)
        {
            return base.GetCustomerByLookUp(request, context);
        }
    }
}