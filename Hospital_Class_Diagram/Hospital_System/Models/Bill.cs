using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Bill
    {
        private static List<Bill>_billList = new List<Bill>();
        public int Number {  get; set; }
        private static double _taxRate = 0.15;
        private double _totalCost;
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
            addBill(this);
        }
        public Bill(){}
        
        
        private static void addBill(Bill bill)
        {
            if (bill== null)
            {
                throw new ArgumentException("Bill cannot be null");
            }
           

            if (_billList.Exists(a=>a.Equals(bill)))
            {
                throw new InvalidOperationException("Bill already added");
            }
            _billList.Add(bill);
        }
        
        
        
        
        
        private static void removeBill(Bill bill)
        {
            if (bill == null)
            {
                throw new ArgumentException("Bill cannot be null");
            }

            if (!_billList.Contains(bill))
            {
                throw new InvalidOperationException("Bill not found!");
            }
            _billList.Remove(bill);
        }
        
        
        public static IReadOnlyList<Bill> GetBills()
        {
            return  _billList.AsReadOnly();
        }
        
        
        
        
        public override bool Equals(object? obj)
        {
            if (obj==null||!(obj is Bill))
            {
                return false;
            }

            Bill a = (Bill)obj;

            return this.Number == a.Number && this._totalCost == a._totalCost;
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode()^_totalCost.GetHashCode();
        }

        public override string ToString()
        {
            return "Bill: " + Number + " " + "Total cost: " + _totalCost+" "+"Final cost: "+FinalCost;
        }

        public static void SetBills(List<Bill> bills)
        {
            _billList = bills ?? new List<Bill>();
        }
    }
}
