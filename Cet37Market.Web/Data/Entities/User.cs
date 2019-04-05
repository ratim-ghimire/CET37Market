using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cet37Market.Web.Data.Entities
{
    public class User:IdentityUser //Herda da classe Identity dot net core
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}
