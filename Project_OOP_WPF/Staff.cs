using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public class Staff : Person
    {
        public override DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                DateTime minDate = DateTime.Today.AddYears(-60);
                DateTime maxDate = DateTime.Today.AddYears(-18);
                if (value < minDate || value > maxDate)
                    throw new ArgumentException(null, $"! DATE: Value out of range. The date must be between {minDate:dd.MM.yyyy} and {maxDate:dd.MM.yyyy}.");
                _birthDate = value;
            }
        }
        private List<StaffRole> _roles;
        //private List<Department> _departments;

        #region Properties - Person info
        public override Hospital CurrentHospital { get; set; }
        #endregion

        #region Properties - Staff-specific properties
        public List<StaffRole> Roles 
        {
            get => _roles; 
            set
            {
                if (value == null || value.Count == 0)
                    throw new ArgumentException("! STAFF: Any staff member must have at least one role.");
                _roles = value;
            }
        }
        //public List<Department> Departments { get; set; } = new();
        #endregion

        #region Methods - Staff-specific
        public Staff(string firstName, string middleName, string lastName, DateTime birthDate, List<StaffRole> roles, Hospital hospital)//, List<Department>? departments = null)
            :base (firstName, middleName, lastName, hospital)
        {
            List<string> exceptions = new();

            try { BirthDate = birthDate; }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { Roles = roles.ToList(); }
            catch (Exception ex) { exceptions.Add(ex.Message); }
            
            //if(departments != null)
            //    try { Departments = departments.ToList(); }
            //    catch (Exception ex) { exceptions.Add(ex.Message); }

            if (exceptions.Count > 0)
                throw new ExceptionList(exceptions);

            ID = IDManager.GenerateID();
        }

        //public void OnDepartmentRemoval(Department department)
        //{
            
        //    Departments?.Remove(department);
        //}
        #endregion

        #region Methods - Inherited from the Person interface
        // actually yeah i can just use the changeinfo from the thing
        #endregion
    }
}
