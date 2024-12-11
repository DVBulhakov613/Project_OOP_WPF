using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public interface IPerson
    {
        public int ID { get; set; }
        static IDManagement IDManager = new IDManagement();
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public Hospital CurrentHospital { get; set; }

        public string GetFullName();
        public void ChangeInfo(string? firstName = null, string? middleName = null, string? lastName = null, DateTime? birthDate = null);
        public string ToString();
    }
}
