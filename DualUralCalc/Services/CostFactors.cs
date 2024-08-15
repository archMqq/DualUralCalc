
using DualUralCalc.Models;

namespace DualUralCalc.Services
{
    public interface ICostFactor
    {
        public double Calculate(Product product, IEnumerable<SupplierOffer> offers, IEnumerable<StoragePosition> positions);
    }

    public class StoragePriceCostFactor : ICostFactor
    {
        public double Calculate(Product product, IEnumerable<SupplierOffer> offers, IEnumerable<StoragePosition> positions)
        {
            return positions.Where(p => p.ProductId == product.Id && p.Price > 0)
                .Min(p => p.Price);
        }
    }

    public class SupplierPriceCostFactor : ICostFactor
    {
        public double Calculate(Product product, IEnumerable<SupplierOffer> offers, IEnumerable<StoragePosition> positions)
        {
            return offers.Where(p => p.ProductId == product.Id && p.Price > 0)
                .Min(p => p.Price);
        }
    }
}
