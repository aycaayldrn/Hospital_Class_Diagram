using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System.Models
{
    public abstract class Doctor : Staff
    {
        protected int Id { get; set; }
        protected string Name { get; set; }
        
        private Department _department;
        public Department Department
        {
            get { return _department; }
        }
        

        public Department? HeadedDepartment { get; private set; } // not all doctors are head of a department
       
        public Doctor(int id, string name,List<Shift> initialShifts) : base(id,name,"Doctor",initialShifts){}
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
            {
                throw new InvalidOperationException("Doctor must be part of the department to become head");
            }
            
            HeadedDepartment = department;
            
            department.assignHeadOfDepartment(this);
            
        }

        public void deleteDoctorFromBeingHead(Department department)
        {
            if (department == null)
            {
                throw new ArgumentException("Department cannot be null");
            }
            if(HeadedDepartment == null)
            {
                throw new InvalidOperationException("Department cannot be null");
            }

            if (department.GetHeadOfDepartment() != this)
            {
                throw new InvalidOperationException("Doctor is not the head of this department");
            }
            HeadedDepartment = null;
            department.removeHeadOfDepartment(this);
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



    
//===================================================================================================================
    }
}
