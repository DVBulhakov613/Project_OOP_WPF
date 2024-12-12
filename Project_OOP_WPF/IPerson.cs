using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public interface IPerson : IComparable
    {
        public int ID { get; }
        static IDManagement IDManager = new IDManagement();
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public Hospital CurrentHospital { get; set; }

        public string GetFullName();
        public void ChangeInfo(string? firstName = null, string? middleName = null, string? lastName = null, DateTime? birthDate = null);
    }
}
