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
        private string _name;
        private string _location;
        private SortedSet<int> _rooms = new();
        #region Properties
        public int ID { get; private set; }
        public static IDManagement IDManager = new IDManagement();
        public string Name 
        {
            get => _name;
            set
            {
                _name = ChangeNameOrLocation(value);
            } 
        }
        public string Location
        {
            get => _location;
            set
            {
                _location = ChangeNameOrLocation(value);
            }
        }

        //public List<Department> Departments { get; private set; } = new List<Department>();
        public SortedSet<int> Rooms 
        { 
            get => _rooms;
            set 
            { 
                AddRooms(value);
            }
        }
        public List<Person> People { get; set; } = new(); // both patients and staff should be stored here
        // 
        public List<Patient> Patients => People.OfType<Patient>().ToList();
        public List<Staff> ActiveStaff => People.OfType<Staff>().ToList();

        public int TotalRooms => Rooms?.Count ?? 0;
        public int TotalStaff => ActiveStaff?.Count ?? 0;
        public int TotalPatients => Patients?.Count ?? 0;
        #endregion

        #region Methods
        public Hospital(string name, string location, SortedSet<int> rooms) //, List<Department>? departments = null, List<Patient>? patients = null, List<Staff>? activeStaff = null)
        {
            List<string> exceptions = new();

            try { Name = name; }
            catch(Exception ex) { exceptions.Add(ex.Message); }

            try { Location = location; }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try 
            {
                if (rooms.Count() < 1) throw new ArgumentException("! HOSPITAL: Hospitals must have at least 1 room."); 
                Rooms = rooms; 
            }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            //try
            //{   foreach (Department dep in departments)
            //    {
            //        if (dep.DepartmentRooms.Any(t => !Rooms.Contains(t)))
            //            throw new ArgumentException($"! HOSPITAL: Department {dep.Name} has rooms that are not part of this Hospital.");
            //        dep.ParentHospital = this;
            //    } }
            //catch (Exception ex) { exceptions.Add(ex.Message); }

            //if (patients != null)
            //{
            //    foreach (Patient pat in patients)
            //        if (pat.CurrentHospital != this)
            //            pat.CurrentHospital = this;
            //        else throw new ArgumentException($"Patient {pat.ID} is already assigned to this Hospital.");
            //    People.AddRange(patients); 
            //}

            //try
            //{   if (activeStaff != null)
            //    {   foreach (Staff staff in activeStaff)
            //        {
            //            if (staff.CurrentHospital != this)
            //                staff.CurrentHospital = this;
            //            else throw new ArgumentException($"Staff {staff.ID} is already assigned to this Hospital.");
            //        }
            //        People.AddRange(patients); } }
            //catch (Exception ex) { exceptions.Add(ex.Message); }

            if(exceptions.Count > 0)
                throw new ExceptionList(exceptions);

            ID = IDManager.GenerateID();
        }
        public string ChangeNameOrLocation(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new NullReferenceException("! HOSPITAL: Cannot have an empty value.");
            // trim the input
            newName = newName.Trim();
            if (newName.Length > 100)
                throw new ArgumentException("! HOSPITAL: Cannot have a value that is longer than 100 symbols.");
            // use regex to check for invalid symbols
            if (double.TryParse(newName, out _))
                throw new ArgumentException("! HOSPITAL: Cannot have a value that is just a number.");
            Regex regex = new(@"^[a-zA-Z0-9 '-]+$");
            if (!regex.IsMatch(newName))
                throw new ArgumentException("! HOSPITAL: Illegal symbols detected.");
            return newName;
        }
        #endregion

        #region Methods - Room-related
        public void AddRoom(int id)
        {
            if (id < 1) throw new ArgumentException($"! HOSPITAL: This hospital already contains room {id}.");
            else if (!_rooms.Add(id)) throw new ArgumentException($"! HOSPITAL: This hospital already contains room {id}.");
        }

        public void AddRooms(SortedSet<int> rooms)
        {
            foreach (int id in rooms)
                if (id < 1) throw new ArgumentException($"! HOSPITAL: This hospital already contains room {id}.");
                else if (!_rooms.Add(id)) throw new ArgumentException($"! HOSPITAL: This hospital already contains room {id}.");
        }
        #endregion

        #region Methods - Department-related
        //public void AddDepartment(string name, List<int> rooms, Staff? head = null, List<Staff>? staff = null)
        //{
        //    Departments.Add(new Department(this, name, rooms, head, staff));
        //}

        //public void RemoveDepartment(int id)
        //{
        //    if (id < 0 || id > Departments.Count - 1)
        //        throw new ArgumentException("! HOSPITAL: Cannot remove a department that does not exist.");
            
        //}
        #endregion

        #region Methods - Patient-related
        public void AddPatient(string firstName, string middleName, string lastName, DateTime birthDate, List<MedicalRecord>? medicalHistory = null)
        {
            People.Add(new Patient(firstName, middleName, lastName, birthDate, this, medicalHistory));
        }
        public void AddPatient(Patient patient)
        {
            if (People.Contains(patient))
                throw new ArgumentException("! HOSPITAL: This patient already exists in the hospital.");

            // Ensure the patient is reassigned to this hospital
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
        public void AddStaff(string firstName, string middleName, string lastName, DateTime birthDate, List<StaffRole> roles)//, List<Department>? departments = null)
        {
            if (roles.Count == 0) throw new NullReferenceException("! HOSPITAL: Staff must always have roles!");
            People.Add(new Staff(firstName, middleName, lastName, birthDate, roles, this));//, departments));
        }

        public void RemovePerson(int ID)
        {
            try { 
                Person person = People.First(p => p.ID == ID);
                foreach (int key in person.Schedule.Appointments.Keys)
                    person.Schedule.CancelAppointment(key);

                People.Remove(person);
            }
            catch (Exception ex) { throw new ArgumentException($"! HOSPITAL: ID {ID} does not exist within this Hospital."); }
        }

        public void RemovePerson(Person person)
        {
            if (!People.Any(p => p.ID == person.ID)) throw new ArgumentException($"! HOSPITAL: ID {person.ID} does not exist within this Hospital.");
            foreach (int key in person.Schedule.Appointments.Keys)
                person.Schedule.CancelAppointment(key);

            People.Remove(person);
        }

        public T FindPerson<T>(int ID) where T : Person
        {
            if (!People.Any(p => p.ID == ID && p is T))
                throw new ArgumentException($"! HOSPITAL: {typeof(T).Name} with ID {ID} does not exist within this Hospital.");

            return (T)People.First(p => p.ID == ID && p is T);
        }

        public void TransferPerson<T>(int ID, Hospital targetHospital) where T : Person
        {
            T person = FindPerson<T>(ID);
            if (person is Staff staff && staff.CurrentHospital == targetHospital)
                throw new ArgumentException("! HOSPITAL: Cannot add duplicate instances of Staff.");

            RemovePerson(person);
            person.CurrentHospital = targetHospital;

            targetHospital.People.Add(person);
        }


        //public void TransferStaff(int ID, Hospital targetHospital)// , Department targetDepartment)
        //{
        //    Staff staff = FindStaff(ID);
        //    if (staff.CurrentHospital == targetHospital) throw new ArgumentException("! HOSPITAL: Cannot add duplicate instances of Staff.");
        //    RemoveStaff(staff);

        //    staff.CurrentHospital = targetHospital;
        //    targetHospital.People.Add(staff);
        //}
        //public void RemoveStaff(int ID)
        //{
        //    Staff staff = FindStaff(ID);
        //    foreach (int key in staff.Schedule.Appointments.Keys)
        //        staff.Schedule.CancelAppointment(key);
        //    People.Remove(staff);
        //}

        //public void RemoveStaff(Staff staff)
        //{
        //    foreach (int key in staff.Schedule.Appointments.Keys)
        //        staff.Schedule.CancelAppointment(key);
        //    People.Remove(staff);
        //}

        //public Staff FindStaff(int ID)
        //{
        //    if (!People.Any(t => t.ID == ID)) throw new ArgumentException($"! HOSPITAL: Staff with ID {ID} does not exist within this Hospital.");
        //    return ActiveStaff.First(x => x.ID == ID);
        //}
        #endregion
    }
}
