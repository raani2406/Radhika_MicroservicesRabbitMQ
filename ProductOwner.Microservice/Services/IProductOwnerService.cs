using ProductOwner.Microservice.Models;

namespace ProductOwner.Microservice.Services
{
    public interface IProductOwnerService
    {
        public Task<IEnumerable<Products>> GetProductsListAsync();

        public bool SendProductOffer(ProductOfferDetail productOfferDetails);
    }
}
