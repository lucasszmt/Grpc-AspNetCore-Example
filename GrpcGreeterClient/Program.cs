using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using EcoSpider.Shared.Grpc;

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

            client.StoreProduct(new ProductData()
            {
                Category = new CategoryData {Id = 1},
                Name = "PS5",
                Description = "Pleysteixon",
                Price = 4500.20
            });
        }
    }
}