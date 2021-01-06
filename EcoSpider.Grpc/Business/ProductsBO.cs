using System;
using System.Linq;
using System.Threading.Tasks;
using EcoSpider.Grpc.Data;
using EcoSpider.Grpc.Models;
using EcoSpider.Shared.Grpc;
using Grpc.Core;

namespace EcoSpider.Grpc.Business
{
    public class ProductsBO
    {
        private StoreContext _context;

        public ProductsBO(StoreContext context)
        {
            _context = context;
        }

        public async Task StoreProduct(ProductData productData)
        {
            try
            {
                Category category = _context.Categories.FirstOrDefault(c => c.Id == productData.Category.Id);

                Product product = new Product("teste", "teste", 10, category);
                // Product product = new Product(
                //     productData.Name,
                //     productData.Description,
                //     Convert.ToDecimal(productData.Price),
                //     category
                // );
                // if (product.HasErrors())
                // {
                //     var e = new ArgumentException("Erro de validção");
                //     e.Data.Add("errors", product.Errors);
                //     throw e;
                // }

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}