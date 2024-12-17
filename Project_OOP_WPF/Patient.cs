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

        #region Properties - Person info

        public override DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                DateTime minDate = DateTime.Today.AddYears(-60);
                DateTime maxDate = DateTime.Now;
                if (value < minDate || value > maxDate)
                    throw new ArgumentException(null, $"! DATE: Value out of range. The date must be between {minDate:dd.MM.yyyy} and {maxDate:dd.MM.yyyy}.");
                _birthDate = value;
            }
        }
        public override Hospital CurrentHospital { get; set; }
        #endregion

        #region Properties - Patient-specific fields
        private int nextID { get; set; }
        public List<MedicalRecord> MedicalHistory { get; private set; } = new();
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
            ID = IDManager.GenerateID();
        }

        public string GenerateCompositeID(DateTime? time = null)
        {
            DateTime currentTime = time ?? DateTime.Now;
            string datePart = currentTime.ToString("dd:MM:yyyy");

            if (!MedicalHistory.Any())
            {
                return $"{nextID++}-{datePart}";
            }

            string lastID = MedicalHistory.Last().ID;
            string[] lastKey = lastID.Split('-');

            if (lastKey.Length != 2 || !DateTime.TryParseExact(lastKey[1], "dd:MM:yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime lastDate))
            {
                throw new InvalidOperationException("Invalid ID format in MedicalHistory.");
            }

            if (lastKey[1] != datePart)
            {
                nextID = 0;
            }

            return $"{nextID++}-{datePart}";
        }


        public void AddMedicalRecord(List<string> diagnoses, List<string> treatments, List<string> medications, DateTime? time = null, List<Staff>? staff = null)
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
