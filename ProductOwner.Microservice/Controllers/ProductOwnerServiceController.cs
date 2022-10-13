using Microsoft.AspNetCore.Mvc;
using ProductOwner.Microservice.Models;
using ProductOwner.Microservice.Services;

namespace ProductOwner.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOwnerServiceController : Controller
    {
        private IProductOwnerService productOwnerService;

        public ProductOwnerServiceController(IProductOwnerService _productOwnerService)
        {
            productOwnerService = _productOwnerService;
        }

        ////[HttpGet("Productslist")]
        ////public async Task<IEnumerable<Products>> GetProductsList(Products products)
        ////{
        ////    return await productOwnerService.ProductsListAsync.ToListAsync();
        ////}

        [HttpPost("SendOfferDetails")]
        public bool SendProductOfferDetails(ProductOfferDetail productofferdetail)
        {
            bool isSent = false;
            if (productofferdetail != null)
            {
                isSent = productOwnerService.SendProductOffer(productofferdetail);
                return isSent;
            }
            return isSent;
        }
    }
}

