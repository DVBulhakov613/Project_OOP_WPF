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
        private DateTime _birthDate;
        public override DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                DateTime minDate = DateTime.Today.AddYears(-60);
                DateTime maxDate = DateTime.Today.AddYears(-18);
                if (value < minDate || value > maxDate)
                    throw new ArgumentException(null, $"! DATE: Value out of range. The date must be between {minDate:dd.MM.yyyy} and {maxDate:dd.MM.yyyy}.");
            }
        }
        private List<StaffRole> _roles;
        private List<Department> _departments;

        #region Properties - Person info
        public static IDManagement IDManager = new IDManagement();

        public override Hospital CurrentHospital { get; set; }
        #endregion

        #region Properties - Staff-specific properties
        public List<StaffRole> Roles { get; set; }
        public List<Department> Departments { get; set; } = new();
        public override AppointmentSchedule Schedule { get; set; } = new AppointmentSchedule();
        #endregion

        #region Methods - Staff-specific
        public Staff(string firstName, string middleName, string lastName, DateTime birthDate, List<StaffRole> roles, Hospital hospital, List<Department>? departments = null)
            :base (firstName, middleName, lastName, hospital)
        {
            List<string> exceptions = new();

            try { BirthDate = birthDate; }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { Roles = roles.ToList(); }
            catch (Exception ex) { exceptions.Add(ex.Message); }
            
            if(departments != null)
                try { Departments = departments.ToList(); }
                catch (Exception ex) { exceptions.Add(ex.Message); }

            if (exceptions.Count > 0)
                throw new ExceptionList(exceptions);

            ID = IDManager.GenerateID();
        }

        public void OnDepartmentRemoval(Department department)
        {
            
            Departments?.Remove(department);
        }
        #endregion

        #region Methods - Inherited from the Person interface
        public override void ChangeInfo(string? firstName, string? middleName, string? lastName, DateTime? birthDate)
        {
            List<string> exceptions = new();

            try { FirstName = firstName ?? throw new ArgumentException("! PERSON: First name cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { MiddleName = middleName ?? throw new ArgumentException("! PERSON: Middle name cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { LastName = lastName ?? throw new ArgumentException("! PERSON: Last name cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { BirthDate = birthDate ?? throw new ArgumentException("! PERSON: Birth date cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            if (exceptions.Count > 0)
                throw new ExceptionList(exceptions);
        }
        #endregion
    }
}
