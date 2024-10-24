using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    internal class Bill
    {
        public int Number {  get; set; }
        private static double _taxRate = 0.15;
        public static double TaxRate
        {
            get => _taxRate;
            set
            {
                if(value < 0 || value > 1)
                {
                    throw new ArgumentException("Tax rate must between 0 and 1");
                }
                _taxRate = value;
            }
        }

        private double _totalCost;
        public double TotalCost
        {
            get => _totalCost;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Total cost can't be negative");
                }
                _totalCost = value;
            }
        }

        public double FinalCost => CalculateFinalCost();

        private double CalculateFinalCost()
        {
            double taxRate = TotalCost * TaxRate;
            return TotalCost + taxRate;
        }

        public Bill(int number, double totalCost)
        {
            Number = number;
            TotalCost = totalCost;
        }
    }
}
