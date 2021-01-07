using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoSpider.Grpc.Models
{
    public class Product : Base
    {
        [Key] public int Id { get; private set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }

        [Required] public Decimal Price { get; set; }
        public Category Category { get; set; }
        
        [NotMapped] public byte[] Image { get; set; }

        public Product(string name, string description, Decimal price, Category category) : base()
        {
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            if (price < 0)
            {
                _errors.Add("Invalid Price", "The price value can't be negative!");
                _errors.Add("Invalid Another teste", "The price value can't be negative!");
            }
        }

        protected Product()
        {
        }
    }
}