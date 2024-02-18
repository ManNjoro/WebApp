using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Category
    {
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
