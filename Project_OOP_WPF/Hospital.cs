using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public class Hospital
    {
        #region Properties
        public int ID
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public IDManagement IDManager = new IDManagement();
        public string Name
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public string Location
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public int Capacity
        {
            get { throw new NotImplementedException(); }
        }
        public List<Department> Departments
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<int> Rooms
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<Patient> Patients
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<Staff> ActiveStaff
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        #endregion

        #region Methods
        public Hospital(string name, string location, List<int> rooms, List<Department>? departments = null, List<Patient>? patient = null, List<Staff>? activeStaff = null)
        { throw new NotImplementedException(); }
        public void ChangeName(string newName)
        { throw new NotImplementedException(); }
        #endregion

        #region Methods - Room-related
        public void AddRoom(int roomID)
        { throw new NotImplementedException(); }
        public void AddRooms(List<int> rooms)
        { throw new NotImplementedException(); }
        #endregion

        #region Methods - Department-related
        public void AddDepartment(string name, List<int> rooms, Staff? head = null, List<Staff>? staff = null)
        { throw new NotImplementedException(); }

        public void RemoveDepartment(string name)
        { throw new NotImplementedException(); }
        #endregion

        #region Methods - Patient-related
        public void AddPatient(string firstName, string middleName, string lastName, DateTime birthDate, List<MedicalRecord>? medicalHistory = null)
        { throw new NotImplementedException(); }
        public void AddPatient(Patient patient)
        { throw new NotImplementedException(); }
        public void TransferPatient(Patient patient, Hospital target)
        { throw new NotImplementedException(); }
        #endregion

        #region Methods - Staff-related
        public void AddStaff(string firstName, string middleName, string lastName, DateTime birthDate, List<StaffRole> roles, List<Department>? departments = null)
        { throw new NotImplementedException(); }
        public void TransferStaff(int ID, Hospital targetHospital, Department targetDepartment)
        { throw new NotImplementedException(); }
        public void RemoveStaff(int ID)
        { throw new NotImplementedException(); }
        #endregion
    }
}
