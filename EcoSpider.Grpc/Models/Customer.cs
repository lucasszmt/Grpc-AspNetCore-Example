using System.ComponentModel.DataAnnotations;

namespace EcoSpider.Grpc.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}