using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public class MedicalRecord : IComparable, ICloneable
    {
        public string ID { get; private set; }
        public List<Staff>? ParticipatingStaff { get; private set; } = null;
        public DateTime Date { get; private set; }
        public List<string> Diagnoses { get; private set; }
        public List<string> Treatments { get; private set; }
        public List<string> Medications { get; private set; }

        public MedicalRecord(string id, DateTime time, List<string> diagnoses, List<string> treatments, List<string> medications, List<Staff>? staff = null)
        {
            if (!id.Contains('-'))
                throw new FormatException("Incorrect format used for Medical Record ID's!");
            var check = id.Split('-');
            try { DateTime check2 = DateTime.ParseExact(check[1], "dd:MM:yyyy", System.Globalization.CultureInfo.InvariantCulture); }
            catch (Exception ex) { throw new FormatException("Incorrect format used for Medical Record ID's!"); }

            ID = id;
            if (time > DateTime.Now) throw new ArgumentException("Cannot create a medical record in the future!");
            Date = time;

            if (diagnoses.Count() == 0) throw new ArgumentException("Diagnoses cannot be empty!");
            foreach (string diagnose in diagnoses)
                if (string.IsNullOrWhiteSpace(diagnose)) throw new NullReferenceException("Empty diagnoses are not permitted!");
            Diagnoses = diagnoses.ToList();

            if (treatments.Count() == 0) throw new ArgumentException("Diagnoses cannot be empty!");
            foreach (string treatment in treatments)
                if (string.IsNullOrWhiteSpace(treatment)) throw new NullReferenceException("Empty diagnoses are not permitted!");
            Treatments = treatments.ToList();

            if (medications.Count() == 0) throw new ArgumentException("Diagnoses cannot be empty!");
            foreach (string medicament in medications)
                if (string.IsNullOrWhiteSpace(medicament)) throw new NullReferenceException("Empty diagnoses are not permitted!");
            Medications = medications.ToList();
            if(staff != null && staff.Count() != 0)
                ParticipatingStaff = staff.ToList();
        }
        int IComparable.CompareTo(object obj)
        {
            if (obj == null) return 1;
            MedicalRecord other = obj as MedicalRecord;
            if (other != null)
            {
                var thisPartial = ID.Split('-');
                var otherPartial = other.ID.Split("-");

                int thisID = int.Parse(thisPartial[0]);
                DateTime thisDate = DateTime.ParseExact(thisPartial[1], "dd:MM:yyyy", null);

                int otherID = int.Parse(thisPartial[0]);
                DateTime otherDate = DateTime.ParseExact(thisPartial[1], "dd:MM:yyyy", null);

                int idComparison = thisID.CompareTo(otherID);
                if(idComparison != 0) { return idComparison; }
                return thisDate.CompareTo(otherDate);
            }
            else
                throw new ArgumentException("! MEDREC.COMPARE: Object is not a Medical Record instance.");
        }
        // allows cloning of the object
        // should test if it will give me an error on parsing a null value with the staff
        object ICloneable.Clone() => new MedicalRecord(
                ID,
                Date,
                Diagnoses.ToList(),
                Treatments.ToList(),
                Medications.ToList(),
                ParticipatingStaff.ToList()) as object;
        // not sure about this one..
        //public override string ToString()
        //{ throw new NotImplementedException(); }
    }
}
