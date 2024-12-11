using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public class Staff : IPerson
    {
        #region Properties - Person info
        public int ID
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public static IDManagement IDManager = new IDManagement();
        public string FirstName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public string MiddleName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public string LastName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public DateTime BirthDate
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public Hospital CurrentHospital
        { 
            get { throw new NotImplementedException(); } 
            set { throw new NotImplementedException(); }
        }
        #endregion

        #region Properties - Staff-specific properties
        public List<StaffRole> Roles
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<Department> Departments
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public AppointmentSchedule Schedule
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        #endregion

        #region Methods - Staff-specific
        public Staff(string firstName, string middleName, string lastName, DateTime birthDate, List<StaffRole> roles, Hospital hospital, List<Department>? departments = null)
        { throw new NotImplementedException(); }
        public void ChangeRoles(List<StaffRole> roles)
        { throw new NotImplementedException(); }
        #endregion

        #region Methods - Inherited from the Person interface
        public string GetFullName()
        { throw new NotImplementedException(); }
        public void ChangeInfo(string? firstName = null, string? middleName = null, string? lastName = null, DateTime? birthDate = null)
        { throw new NotImplementedException(); }
        public override string ToString()
        { throw new NotImplementedException(); }
        #endregion
    }
}
