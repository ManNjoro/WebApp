using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Category
    {
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss zzz}", ConvertEmptyStringToNull = true, NullDisplayText = "")]
        public DateTime CreatedAt { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time"));
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss zzz}", ConvertEmptyStringToNull = true, NullDisplayText = "")]
        public DateTime UpdatedAt { get; set;}
        // Mark the UpdatedAt property for ignoring during database updates
        [NotMapped]
        public bool IsUpdated { get; set; }

        public Category()
        {
            UpdatedAt = CreatedAt;
        }

        // Method to update the UpdatedAt property when properties are changed
        public void UpdateTimestamp()
        {
            if (IsUpdated)
            {
                UpdatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time"));
            }
        }
    }
}
