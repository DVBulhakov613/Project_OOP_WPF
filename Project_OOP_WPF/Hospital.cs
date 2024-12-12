using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
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
        public string Name { get; set; }
        public string Location { get; set; }

        public List<Department> Departments
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public SortedSet<int> Rooms { get; set; } = new();
        public List<Person> People { get; set; } // both patients and staff should be stored here
        // 
        public List<Patient> Patients { get { return People.OfType<Patient>().ToList(); } }
        public List<Staff> ActiveStaff { get { return People.OfType<Staff>().ToList(); } }
        #endregion

        #region Methods
        public Hospital(string name, string location, List<int> rooms, List<Department>? departments = null, List<Patient>? patient = null, List<Staff>? activeStaff = null)
        { throw new NotImplementedException(); }
        public void ChangeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new NullReferenceException("! HOSPITAL: Cannot have an empty name.");
            // trim the input
            newName = newName.Trim();
            if (newName.Length > 50)
                throw new ArgumentException("! HOSPITAL: Cannot have a name that is longer than 50 symbols.");
            // use regex to check for invalid symbols
            Regex regex = new(@"^[a-zA-Z '-]+$");
            if (!regex.IsMatch(newName))
                throw new ArgumentException("! HOSPITAL: Illegal symbols detected.");
        }
        #endregion

        #region Methods - Room-related
        private bool AddRoom(int roomID)
        {
            if (roomID < 1) throw new ArgumentException("! HOSPITAL: Room ID cannot be lower than 1.");
            if(!Rooms.Add(roomID)) throw new ArgumentException($"! HOSPITAL: Room {roomID} already exists.");
            return true;
        }
        public void AddRooms(List<int> rooms)
        {
            foreach(int roomID in rooms)
                AddRoom(roomID);
        }
        #endregion

        #region Methods - Department-related
        public void AddDepartment(string name, List<int> rooms, Staff? head = null, List<Staff>? staff = null)
        {
            Departments.Add(new Department(this, name, rooms, head, staff));
        }

        public void RemoveDepartment(int id)
        {
            if (id < 0 || id > Departments.Count - 1)
                throw new ArgumentException("! HOSPITAL: Cannot remove a department that does not exist.");
            
        }
        #endregion

        #region Methods - Patient-related
        public void AddPatient(string firstName, string middleName, string lastName, DateTime birthDate, List<MedicalRecord>? medicalHistory = null)
        {
            People.Add(new Patient(firstName, middleName, lastName, birthDate, this, medicalHistory));
        }
        public void AddPatient(Patient patient)
        {
            patient.CurrentHospital = this;
            People.Add(patient);
        }
        public void TransferPatient(Patient patient, Hospital target)
        {
            if (patient == null) throw new NullReferenceException("! HOSPITAL: Cannot transfer nothing.");
            if (target == null) throw new NullReferenceException("! HOSPITAL: Cannot transfer to nothing.");
            if (patient.CurrentHospital == target) throw new ArgumentException("! HOSPITAL: Cannot transfer to the same Hospital.");

            patient.CurrentHospital = target;
            target.AddPatient(patient);
            People.Remove(patient);
        }
        #endregion

        #region Methods - Staff-related
        public void AddStaff(string firstName, string middleName, string lastName, DateTime birthDate, List<StaffRole> roles, List<Department>? departments = null)
        {
            People.Add(new Staff(firstName, middleName, lastName, birthDate, roles, this, departments));
        }
        public void TransferStaff(int ID, Hospital targetHospital, Department targetDepartment)
        { throw new NotImplementedException(); }
        public void RemoveStaff(int ID)
        { throw new NotImplementedException(); }
        #endregion
    }
}
