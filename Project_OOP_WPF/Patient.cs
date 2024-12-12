using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public class Patient : IPerson
    {
        private int _id;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private DateTime _birthDate;

        #region Properties - Person info
        public int ID { get; }
        public static IDManagement IDManager = new();

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Hospital CurrentHospital { get; set; }
        #endregion

        #region Properties - Patient-specific fields
        private int nextID { get; set; }
        public List<MedicalRecord> MedicalHistory { get; private set; }
        public AppointmentSchedule Schedule { get; set; } = new AppointmentSchedule();
        #endregion

        #region Methods - Patient-specific methods
        public Patient(string firstName, string middleName, string lastName, DateTime birthDate, Hospital hospital, List<MedicalRecord>? medicalHistory = null)
        { throw new NotImplementedException(); }

        public string GenerateCompositeID(DateTime? time = null)
        {
            string res;

            if (MedicalHistory.Last().ID == ($"{nextID}-{time:dd:MM:yyyy}"))
                res = $"{nextID++}-{time:dd:MM:yyyy}";
            else res = $"{nextID}-{time:dd:MM:yyyy}";
            
            string[] lastKey = MedicalHistory.Last().ID.Split('-');
            if (lastKey[1] != $"{time:dd:MM:yyyy}")
                nextID = 0;

            return res;
        }

        public void AddMedicalRecord(List<Staff> staff, List<string> diagnoses, List<string> treatments, List<string> medications, DateTime? time = null)
        {
            DateTime timeActual = time ?? DateTime.Now;
            MedicalHistory.Add(new MedicalRecord(GenerateCompositeID(time), timeActual, diagnoses, treatments, medications, staff));
        }
        #endregion

        #region Methods - Inherited from Person interface
        public string GetFullName()
        { throw new NotImplementedException(); }

        public void ChangeInfo(string? firstName, string? middleName, string? lastName, DateTime? birthDate)
        {
            List<string> exceptions = new();

            try { FirstName = firstName ?? throw new ArgumentException("! STAFF: First name cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { MiddleName = middleName ?? throw new ArgumentException("! STAFF: Middle name cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { LastName = lastName ?? throw new ArgumentException("! STAFF: Last name cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { BirthDate = birthDate ?? throw new ArgumentException("! STAFF: Birth date cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            if (exceptions.Count > 0)
                throw new ExceptionList(exceptions);
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            Patient other = obj as Patient;
            if (other != null)
                return ((IPerson)this).ID.CompareTo(((IPerson)other).ID);
            else
                throw new ArgumentException("! PATIENT.COMPARE: Object is not a Patient instance.");
        }
        #endregion
    }
}
