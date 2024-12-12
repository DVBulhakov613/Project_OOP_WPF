using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public class Staff : IPerson
    {
        private int _id;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private DateTime _birthDate;
        private List<StaffRole> _roles;
        private List<Department> _departments;

        #region Properties - Person info
        public int ID { get; }
        public static IDManagement IDManager = new IDManagement();
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new FormatException("! NAME: The input string is empty.");
                if (!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                    throw new FormatException("! NAME: The input string is using symbols that do not belong to the latin alphabet.");
                _firstName = value;
            }
        }
        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new FormatException("! NAME: The input string is empty.");
                if (!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                    throw new FormatException("! NAME: The input string is using symbols that do not belong to the latin alphabet.");
                _middleName = value;
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new FormatException("! NAME: The input string is empty.");
                if (!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                    throw new FormatException("! NAME: The input string is using symbols that do not belong to the latin alphabet.");
                _lastName = value;
            }
        }
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                DateTime minDate = DateTime.Today.AddYears(-60);
                DateTime maxDate = DateTime.Today.AddYears(20);
                if (value < minDate || value > maxDate)
                    throw new ArgumentException(null, $"! DATE: Value out of range. The date must be between {minDate:dd.MM.yyyy} and {maxDate:dd.MM.yyyy}.");
            }
        }
        public Hospital CurrentHospital { get; set; }
        #endregion

        #region Properties - Staff-specific properties
        public List<StaffRole> Roles { get; set; }
        public List<Department> Departments { get; set; } = new();
        public AppointmentSchedule Schedule { get; set; } = new AppointmentSchedule();
        #endregion

        #region Methods - Staff-specific
        public Staff(string firstName, string middleName, string lastName, DateTime birthDate, List<StaffRole> roles, Hospital hospital, List<Department>? departments = null)
        { throw new NotImplementedException(); }
        public void ChangeRoles(List<StaffRole> roles)
        { throw new NotImplementedException(); }
        #endregion

        #region Methods - Inherited from the Person interface
        public string GetFullName() { return $"{FirstName} {MiddleName} {LastName}"; }

        public void ChangeInfo(string? firstName, string? middleName, string? lastName, DateTime? birthDate)
        {
            List<string> exceptions = new();

            try { FirstName = firstName ?? throw new ArgumentException("! STAFF: First name cannot be changed to nothing."); }
            catch(Exception ex) { exceptions.Add(ex.Message); }

            try { MiddleName = middleName ?? throw new ArgumentException("! STAFF: Middle name cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { LastName = lastName ?? throw new ArgumentException("! STAFF: Last name cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { BirthDate = birthDate ?? throw new ArgumentException("! STAFF: Birth date cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            if(exceptions.Count > 0)
                throw new ExceptionList(exceptions);
        }
        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            Staff other = obj as Staff;
            if (other != null)
                return ((IPerson)this).ID.CompareTo(((IPerson)other).ID);
            else
                throw new ArgumentException("! STAFF.COMPARE: Object is not a Staff instance.");
        }
        #endregion
    }
}
