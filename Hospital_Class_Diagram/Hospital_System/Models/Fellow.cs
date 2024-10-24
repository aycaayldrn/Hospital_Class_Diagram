using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    [Serializable] 
    public class Fellow
    {
        private static List<Fellow> _fellowList = new List<Fellow>();
        public string? ResearchProject { get; set; }

        private string _specialization;
        
        
        public string Specialization
        {
            get => _specialization;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Specialization can't be empty");
                }
                _specialization = value;
            }
        }

        public Fellow(string specialization, string? researchProject = null) 
        {
            Specialization = specialization;
            ResearchProject = researchProject;
            addFellow(this);
        }
        public Fellow(){}
        
        
        private static void addFellow(Fellow fellow)
        {
            if (fellow== null)
            {
                throw new ArgumentException("Fellow cannot be null");
            }
           

            if (_fellowList.Exists(a=>a.Equals(fellow)))
            {
                throw new InvalidOperationException("Fellow already added");
            }
            _fellowList.Add(fellow);
        }
        
        
        private static void removeFellow(Fellow fellow)
        {
            if (fellow == null)
            {
                throw new ArgumentException("Fellow cannot be null");
            }

            if (!_fellowList.Contains(fellow))
            {
                throw new InvalidOperationException("Fellow not found!");
            }
            _fellowList.Remove(fellow);
        }
        
        
        public static IReadOnlyList<Fellow> GetFellows()
        {
            return _fellowList.AsReadOnly();
        }
        
        
        public override bool Equals(object? obj)
        {
            if (obj==null||!(obj is Fellow))
            {
                return false;
            }

            Fellow a = (Fellow)obj;

            return string.Equals(this.ResearchProject,a.ResearchProject,StringComparison.OrdinalIgnoreCase)&& 
                   string.Equals(this._specialization,a._specialization,StringComparison.OrdinalIgnoreCase);
        }
        
        
        
        
        public override int GetHashCode()
        {
            return HashCode.Combine(_specialization.ToLowerInvariant(),
                ResearchProject?.ToLowerInvariant() ?? string.Empty);
        }
        
        
        public override string ToString()
        {
            return "Specialization: " +" "+ _specialization +" "+ "Research project: " + ResearchProject;
        }

        public static void SetFellows(List<Fellow> Fellows)
        {
            _fellowList = Fellows ?? new List<Fellow>();
        }
    }
        
}
