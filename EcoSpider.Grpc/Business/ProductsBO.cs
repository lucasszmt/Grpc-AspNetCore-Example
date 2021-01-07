using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EcoSpider.Grpc.Data;
using EcoSpider.Grpc.Models;
using EcoSpider.Shared.Grpc;
using Grpc.Core;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EcoSpider.Grpc.Business
{
    public class ProductsBO
    {
        private StoreContext _context;

        public ProductsBO(StoreContext context)
        {
            _context = context;
        }

        public List<ProductData> ListarProduos()
        {
            return _context.Products.Select(
                product => new ProductData
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = (double) product.Price,
                    Description = product.Description,
                    Category = new CategoryData
                    {
                        Id = product.Category.Id, 
                        Name = product.Category.Name
                    }
                }
            ).ToList();
        }

        public async Task StoreProduct(ProductData productData)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == productData.Category.Id);

            Product product = new Product(
                productData.Name,
                productData.Description,
                Convert.ToDecimal(productData.Price),
                category
            );

            if (product.HasErrors())
            {
                var e = new ArgumentException("Erro de validação: " + product.ErrorsList);
                e.Data.Add("errors", product.Errors);
                throw e;
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
    }
}