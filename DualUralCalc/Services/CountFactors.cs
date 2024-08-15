using DualUralCalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DualUralCalc.Services
{
    public interface ICountFactor
    {
        public int Calculate(Product product, IEnumerable<SupplierOffer> offers, IEnumerable<StoragePosition> positions);
    }

    public class StorageCountFactor : ICountFactor
    {
        public int Calculate(Product product, IEnumerable<SupplierOffer> offers, IEnumerable<StoragePosition> positions)
        {
            return positions.Where(p => p.ProductId == product.Id)
                .Sum(p => p.Count);
        }
    }

    public class SupplierCountFactor : ICountFactor
    {
        public int Calculate(Product product, IEnumerable<SupplierOffer> offers, IEnumerable<StoragePosition> positions)
        {
            return positions.Where(p => p.ProductId == product.Id)
                .Sum(p => p.Count);
        }
    }
}
