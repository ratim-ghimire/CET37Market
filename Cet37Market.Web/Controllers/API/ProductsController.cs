using Cet37Market.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cet37Market.Web.Controllers.API
{
    //vai controllar API que vai ser usado pelo Mobile
    [Route("api/[Controller]")] //para chamar o controlador atravs es do mobile
    public class ProductsController:Controller
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(this.productRepository.GetAll());
        }

    }
}
