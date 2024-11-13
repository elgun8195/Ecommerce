using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzureProject.Entity.Concrete
{
    public class BaseEntity
    {
        [JsonIgnore]
        public  int Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedTime { get; set; }= DateTime.Now;
        [JsonIgnore]
        public DateTime DeletedTime { get; set; }
        [JsonIgnore]
        public DateTime UpdatedTime { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
    }
}
