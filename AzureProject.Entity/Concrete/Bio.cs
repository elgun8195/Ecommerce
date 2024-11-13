using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureProject.Entity.Concrete
{
    public class Bio:BaseEntity
    {
        public string Adress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Tiktok { get; set; }
        public string Pinterest { get; set; }

    }
}
