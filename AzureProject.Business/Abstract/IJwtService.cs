using AzureProject.Entity.Concrete;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureProject.DataAccess.Abstract
{
    public interface IJwtService
    {
        string Genereate(AppUser user, IList<string> roles, IConfiguration configuration);
    }
}
