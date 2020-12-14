using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EcoGrpc.Client.Customer;
using EcoSpider.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;

namespace EcoSpider.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<CustomerModel> _customers = new List<CustomerModel>()
            {
                new CustomerModel()
                {
                    Id = 1, Name = "Yuri Difusor", Address = "Matelândia 674",
                    EmailAddress = "YurinhoDifusor_2009gmail.com"
                },
                new CustomerModel()
                {
                    Id = 2,
                    Name = "Carlos Startupero", Address = "Visconde do Rio Branco 12345",
                    EmailAddress = "carlosnaopagosalgado@gmail.com"
                },
                new CustomerModel()
                {
                    Id = 3,
                    Name = "Lucas da Stradinha Rebaxada", Address = "Visconde do Rio Branco 447",
                    EmailAddress = "lucas_stradinha_2010@gmail.com"
                },
            };

            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions()
            {
                HttpHandler = httpHandler
            });

            // =======================================
            // var client = new Customer.CustomerClient(channel);
            // using var call = client.GetCustomers(new Empty());
            //
            // await foreach (var costumer in call.ResponseStream.ReadAllAsync())
            // {
            //     Console.WriteLine(costumer);
            // }
            // =======================================

            // var client = new Customer.CustomerClient(channel);
            // using var call = client.GetCustomersFromClient();
            // for (int i = 1; i < 4; i++)
            // {
            //     await call.RequestStream.WriteAsync(new CustomerLookUpModel() {Id = i});
            // }
            //
            // await call.RequestStream.CompleteAsync();
            // var response = await call;
            // Console.WriteLine(response);
            // =======================================

            // var client = new Customer.CustomerClient(channel);
            // using var call = client.GetCustomersBidirectional();
            //
            // var tarefa = Task.Run(async () =>
            // {
            //     await foreach (var response in call.ResponseStream.ReadAllAsync())
            //     {
            //         Console.WriteLine(response);
            //     }
            // });
            //
            // for (int i = 1; i < 4; i++)
            // {
            //     await call.RequestStream.WriteAsync(new CustomerLookUpModel(){Id = i});
            //     await Task.Delay(TimeSpan.FromSeconds(2));
            // }
            //
            // await call.RequestStream.CompleteAsync();
            // await tarefa;
            // Console.WriteLine("Tarefa Concluida!");

            var client = new Customer.CustomerClient(channel);
            var metadata = new Metadata() {new Metadata.Entry("user", "Yuri Difusor")};
            var response = await client.InsertCustomerAsync(
                new CustomerModel()
                {
                    // Id = 1,
                    Address = "lailuleilo",
                    Name = "Mr Potato",
                    EmailAddress = "ajhdjsa@gmail.com"
                }, metadata);
            Console.Write(response);
        }
    }
}