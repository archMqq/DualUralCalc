using DualUralCalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DualUralCalc.Services
{
    public interface IFileSaver
    {
        public void Save(IEnumerable<CalculationResult> result, string filename);
    }

    public class CsvFileSaver : IFileSaver
    {
        public void Save(IEnumerable<CalculationResult> result, string filePath)
        {
            var lines = new List<string> { "ProductID, Cost, Count" };
            lines.AddRange(result.Select(r => $"{r.ProductId}, {r.Cost.ToString().Replace(",", ".")}, {r.Count}"));
            File.WriteAllLines(filePath, lines);
        }
    }
}
