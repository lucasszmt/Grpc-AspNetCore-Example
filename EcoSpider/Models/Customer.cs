using System.ComponentModel.DataAnnotations;

namespace EcoSpider.Models
{
    public class Customer
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "O nome não pode possuir menos que 3 caracteres")]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}