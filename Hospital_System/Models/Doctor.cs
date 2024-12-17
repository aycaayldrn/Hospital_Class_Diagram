using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    public abstract class Doctor 
    {
        protected int Id { get; set; }
        protected string Name { get; set; }
        
        private Department _department;
        public Department Department
        {
            get { return _department; }
        }
        private List<Patient> _doctorsPatients = new List<Patient>();
       
        public Doctor(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public Doctor(){}
        
        
//==================================================================================================================
//Associations: Agregation Doctor-head of-Department
        public void becomeHeadOfDepartment(Department department)
        {
            if (department==null)
            {
                throw new ArgumentException("Department cannot be null");
                
            }
            if (_department != department)
                throw new InvalidOperationException("Doctor must be part of the department to become head");
            department.assignHeadOfDepartment(this);
            
        }

        public void deleteDoctorFromBeingHead(Department department)
        {
            if (department == null)
            {
                throw new ArgumentException("Department cannot be null");
            }

            if (department.GetHeadOfDepartment() != this)
            {
                throw new InvalidOperationException("Doctor is not the head of this department");
            }

            department.removeHeadOfDepartment();

        }




//==================================================================================================================
//Associations: Agregation Doctor-patient
        public void addPatientToDoctor(Patient patient)
        {
            if (patient==null)
            {
                throw new ArgumentException("Patient can't be null");
            }

            if (_doctorsPatients.Contains(patient))
            {
                throw new InvalidOperationException("Patient is already assigned to this department");
                
            }
            _doctorsPatients.Add(patient);
        }    
        
        public void removePatientFromDoctor(Patient patient)
        {
            if (patient==null)
            {
                throw new ArgumentException("Patient can't be null");
            }

            if (!_doctorsPatients.Contains(patient))
            {
                throw new InvalidOperationException("No such patient in list");
                
            }
            _doctorsPatients.Remove(patient);
        }
        public IReadOnlyList<Patient> GetPatients()
        {
            return _doctorsPatients.AsReadOnly();
        }
//==================================================================================================================
//Associations: Agregation Doctor-department
        public void asssignDoctorToDepartment(Department department)
        {
            if (department==null)
            {
                throw new ArgumentException("department cannot be null");
            }
            
            if (_department!= null)
            {
                throw new InvalidOperationException("Doctor already assigned to department");
            }

            _department = department;
            if (!department.GetDoctors().Contains(this))
            {
                department.addDoctorToDepartment(this);
            }
        }
        
        
        public void changeDepartment(Department difrentDepartment)
        {
            if (difrentDepartment== null)
            {
                throw new ArgumentException("department cannot be null");
            }

            if (_department==difrentDepartment)
            {
                throw new InvalidOperationException("Departments are the same!");
            }

            if (_department!=null)
            {
                _department.removeDoctorFromDepartment(this);
            }
            difrentDepartment.addDoctorToDepartment(this);
            _department = difrentDepartment;
        }
        
        
        public void deleteDoctor()
        {
            if (_department != null && _department.GetDoctors().Contains(this))
            {
                _department.removeDoctorFromDepartment(this);
            }
            _department = null;

           
        }



    
//=======
    }
}
