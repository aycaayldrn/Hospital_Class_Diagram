using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    public class Surgeon
    {
        public List<string> Surgeries { get; set; }
        public static readonly int MaxSurgeriesPerShift = 2;

        public Surgeon(List<string> surgeries = null)
        {
            Surgeries = surgeries ?? new List<string>(); 
        }

        public void PerformSurgery (string surgery)
        {
            if(Surgeries.Count >= MaxSurgeriesPerShift)
            {
                throw new InvalidOperationException("Cannot perform more than 2 surgeries per shift");         
            }
            Surgeries.Add(surgery);
        }
    }
}
