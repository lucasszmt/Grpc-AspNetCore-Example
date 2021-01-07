using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using EcoSpider.Shared.Grpc;
using Grpc.Core;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var channel = GrpcChannel.ForAddress("http://localhost:5000",
                new GrpcChannelOptions() {HttpHandler = httpHandler});

            var client = new ProductSvc.ProductSvcClient(channel);
            try
            {
                var returnMessage = client.StoreProduct(new ProductData()
                {
                    Category = new CategoryData {Id = 1},
                    Name = "PS5",
                    Description = "Pleysteixon",
                    Price = -4500.20
                });
                Console.Write(returnMessage);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}