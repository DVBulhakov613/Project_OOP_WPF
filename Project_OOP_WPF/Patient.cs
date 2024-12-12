using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public class Patient : Person
    {
        private DateTime _birthDate;

        #region Properties - Person info
        public static IDManagement IDManager = new();

        public override DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                DateTime minDate = DateTime.Today.AddYears(-60);
                DateTime maxDate = DateTime.Now;
                if (value < minDate || value > maxDate)
                    throw new ArgumentException(null, $"! DATE: Value out of range. The date must be between {minDate:dd.MM.yyyy} and {maxDate:dd.MM.yyyy}.");
            }
        }
        public override Hospital CurrentHospital { get; set; }
        #endregion

        #region Properties - Patient-specific fields
        private int nextID { get; set; }
        public List<MedicalRecord> MedicalHistory { get; private set; } = new();
        public override AppointmentSchedule Schedule { get; set; } = new AppointmentSchedule();
        #endregion

        #region Methods - Patient-specific methods
        public Patient(string firstName, string middleName, string lastName, DateTime birthDate, Hospital hospital, List<MedicalRecord>? medicalHistory = null)
            : base(firstName, middleName, lastName, hospital)
        {
            List<string> exceptions = new();

            try { BirthDate = birthDate; }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            if(medicalHistory != null) 
                try { MedicalHistory = medicalHistory.ToList(); }
                catch (Exception ex) { exceptions.Add(ex.Message); }

            if (exceptions.Count > 0)
                throw new ExceptionList(exceptions);
        }

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
        // everything shifted into the inherited person abstract
        #endregion
    }
}
