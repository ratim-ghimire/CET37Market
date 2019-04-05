using Cet37Market.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cet37Market.Web.Data
{
    public class CountryRepository:GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(DataContext context):base(context)
        {

        }
    }
}
