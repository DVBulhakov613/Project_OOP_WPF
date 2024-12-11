using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public class MedicalRecord
    {
        public string ID 
        { 
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); } 
        }
        public List<Staff> ParticipatingStaff
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public DateTime Date
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<string> Diagnoses
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<string> Treatments
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<string> Medications
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }

        public MedicalRecord(string id,List<string> diagnoses, List<string> treatments, List<string> medications, List<Staff>? staff = null)
        { throw new NotImplementedException(); }
        public override string ToString()
        { throw new NotImplementedException(); }
    }
}
