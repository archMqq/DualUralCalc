using DualUralCalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DualUralCalc.Services
{
    public interface ICalculator
    {
        public CalculationResult Calculate(Product product, IEnumerable<SupplierOffer> offers, 
            IEnumerable<StoragePosition> positions);
    }
    public class ProductCalculator : ICalculator
    {
        private readonly IEnumerable<ICostFactor> _costFactors;
        private readonly IEnumerable<ICountFactor> _countFactors;

        public ProductCalculator(IEnumerable<ICostFactor> costFactors, IEnumerable<ICountFactor> countFactors)
        {
            _costFactors = costFactors;
            _countFactors = countFactors;
        }

        public CalculationResult Calculate(Product product, IEnumerable<SupplierOffer> offers, 
            IEnumerable<StoragePosition> positions)
        {
            double cost = _costFactors.Min(f => f.Calculate(product, offers, positions));
            int count = _countFactors.Sum(f => f.Calculate(product, offers,positions));

            return new CalculationResult
            {
                ProductId = product.Id,
                Cost = cost,
                Count = count
            };
        }
    }
}
