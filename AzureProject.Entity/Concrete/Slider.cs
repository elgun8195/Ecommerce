using Microsoft.AspNetCore.Http; 
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AzureProject.Entity.Concrete
{
    public class Slider:BaseEntity
    { 
        public string? Title { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public string? ImageUrl { get; set; }
        [JsonIgnore]
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
