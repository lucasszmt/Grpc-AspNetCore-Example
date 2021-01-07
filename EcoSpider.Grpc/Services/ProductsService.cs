using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EcoSpider.Grpc.Business;
using EcoSpider.Grpc.Data;
using EcoSpider.Shared.Grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;


namespace EcoSpider.Grpc.Services
{
    public class ProductsService : ProductSvc.ProductSvcBase
    {
        private readonly StoreContext _context;
        private readonly ProductsBO _productsBO;
        private readonly ILogger<ProductsService> _logger;

        public ProductsService(StoreContext context, ProductsBO productsBo, ILogger<ProductsService> logger)
        {
            _logger = logger;
            _context = context;
            _productsBO = productsBo;
        }

        public override async Task<ReturnMessage> StoreProduct(ProductData request, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation("Salvando Produto...");

                await _productsBO.StoreProduct(request);
                return new ReturnMessage()
                {
                    Success = true,
                    Message = "Produto Inserido com Sucesso"
                };
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e.Message);
                throw new RpcException(new Status(StatusCode.InvalidArgument, e.Message));
            }
        }

        public override Task GetProducts(Empty request, IServerStreamWriter<ProductData> responseStream, ServerCallContext context)
        {
            throw new NotImplementedException("numplementei");
        }
    }
}