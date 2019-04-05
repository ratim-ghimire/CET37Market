using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cet37Market.Web.Data.Entities
{
    public interface IEntity
    {
        int Id { get; set; }

        //to know that the data was deleted
        //bool WasDelete { get; set; }
    }
}
