using DualUralCalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DualUralCalc.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts(string filePath);
    }

    public interface ISupplierOfferRepository
    {
        IEnumerable<SupplierOffer> GetAllOffers(string filePath);
    }

    public interface IWarehousePositionRepository
    {
        IEnumerable<StoragePosition> GetAllPositions(string filePath);
    }

    public class CsvProductRepository : IProductRepository
    {
        public IEnumerable<Product> GetAllProducts(string filePath)
        {
            return File.ReadAllLines(filePath)
                       .Skip(1)
                       .Select(line =>
                       {
                           var parts = line.Split(',');
                           return new Product
                           {
                               Id = int.Parse(parts[0]),
                               Name = parts[1]
                           };
                       });
        }
    }

    public class CsvSupplierRepository : ISupplierOfferRepository
    {
        public IEnumerable<SupplierOffer> GetAllOffers(string filePath)
        {
            return File.ReadAllLines(filePath)
                       .Skip(1)
                       .Select(line =>
                       {
                           var parts = line.Split(',');
                           return new SupplierOffer
                           {
                               ProductId = int.Parse(parts[0]),
                               SupplierId = int.Parse(parts[1]),
                               Price = double.Parse(parts[2].Replace('.', ',')),
                               Count = int.Parse(parts[3])
                           };
                       });
        }
    }

    public class CsvStorageRepository : IWarehousePositionRepository
    {
        public IEnumerable<StoragePosition> GetAllPositions(string filePath)
        {
            return File.ReadAllLines(filePath)
                       .Skip(1)
                       .Select(line =>
                       {
                           var parts = line.Split(',');
                           return new StoragePosition
                           {
                               ProductId = int.Parse(parts[0]),
                               Price = double.Parse(parts[1].Replace('.', ',')),
                               Count = int.Parse(parts[2])
                           };
                       });
        }
    }

}
