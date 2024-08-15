using DualUralCalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DualUralCalc.Services
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Введите путь к файлу продуктов: ");
            var productFilePath = Console.ReadLine();
            Console.Write("Введите путь к файлу склада: ");
            var storageFilePath = Console.ReadLine();
            Console.Write("Введите путь к файлу поставщиков: ");
            var supplierFilePath = Console.ReadLine();
            Console.Write("Введите путь к конечному файлу: ");
            var resultFilePath = Console.ReadLine();

            var productRepository = new CsvProductRepository();
            var supplierOfferRepository = new CsvSupplierRepository();
            var warehousePositionRepository = new CsvStorageRepository();

            var products = productRepository.GetAllProducts(productFilePath);
            var positions = warehousePositionRepository.GetAllPositions(storageFilePath);
            var offers = supplierOfferRepository.GetAllOffers(supplierFilePath);

            var costFactors = new List<ICostFactor>
            {
            new StoragePriceCostFactor(),
            new SupplierPriceCostFactor()
            };

            var quantityFactors = new List<ICountFactor>
            {
            new StorageCountFactor(),
            new SupplierCountFactor()
            };

            var calculator = new ProductCalculator(costFactors, quantityFactors);

            var result = new List<CalculationResult>();
            foreach (var product in products)
            {
                var productOffers = offers.Where(o => o.ProductId == product.Id);
                var productPositions = positions.Where(p => p.ProductId == product.Id);
                var calcRes = calculator.Calculate(product, productOffers, productPositions);
                result.Add(calcRes);
            }

            var fileProcessor = new CsvFileSaver();
            fileProcessor.Save(result, resultFilePath);

            Console.WriteLine("Результаты сохранены в файл: " + resultFilePath);        
        }
    }
}
