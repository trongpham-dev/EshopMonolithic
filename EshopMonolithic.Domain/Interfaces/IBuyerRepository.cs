using EshopMonolithic.Domain.AggregatesModel.BuyerAggregate;
using EshopMonolithic.Domain.SeedWork;

namespace EshopMonolithic.Domain.Interfaces
{
    public interface IBuyerRepository : IRepository<Buyer>
    {
        Buyer Add(Buyer buyer);
        Buyer Update(Buyer buyer);
        Task<Buyer> FindAsync(string BuyerIdentityGuid);
        Task<Buyer> FindByIdAsync(int id);
    }
}
