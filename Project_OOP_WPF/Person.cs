using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public abstract class Person : IPerson
    {
        protected string _firstName;
        protected string _middleName;
        protected string _lastName;
        protected DateTime _birthDate;
        public int ID { get; set; }
        public static IDManagement IDManager = new();
        public virtual string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new FormatException("! NAME: The input string is empty.");
                if (!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                    throw new FormatException("! NAME: The input string is using symbols that do not belong to the latin alphabet.");
                _firstName = value;
            }
        }
        public virtual string MiddleName
        {
            get => _middleName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new FormatException("! NAME: The input string is empty.");
                if (!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                    throw new FormatException("! NAME: The input string is using symbols that do not belong to the latin alphabet.");
                _middleName = value;
            }
        }
        public virtual string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new FormatException("! NAME: The input string is empty.");
                if (!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                    throw new FormatException("! NAME: The input string is using symbols that do not belong to the latin alphabet.");
                _lastName = value;
            }
        }
        public virtual DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                DateTime minDate = DateTime.Today.AddYears(-60);
                DateTime maxDate = DateTime.Today;
                if (value < minDate || value > maxDate)
                    throw new ArgumentException(null, $"! DATE: Value out of range. The date must be between {minDate:dd.MM.yyyy} and {maxDate:dd.MM.yyyy}.");
            }
        }
        public abstract Hospital CurrentHospital { get; set; }
        public abstract AppointmentSchedule Schedule { get; set; }

        public Person(string firstName, string middleName, string lastName, Hospital hospital)
        {
            List<string> exceptions = new();

            try { FirstName = firstName; }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { MiddleName = middleName; }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { LastName = lastName; }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { CurrentHospital = hospital; }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            if (exceptions.Count > 0)
                throw new ExceptionList(exceptions);

            ID = IDManager.GenerateID();
        }

        public virtual void ChangeInfo(string? firstName = null, string? middleName = null, string? lastName = null, DateTime? birthDate = null) 
        {
            List<string> exceptions = new();

            try { FirstName = firstName ?? throw new ArgumentException("! PERSON: First name cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { MiddleName = middleName ?? throw new ArgumentException("! PERSON: Middle name cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { LastName = lastName ?? throw new ArgumentException("! PERSON: Last name cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { BirthDate = birthDate ?? throw new ArgumentException("! PERSON: Birth date cannot be changed to nothing."); }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            if (exceptions.Count > 0)
                throw new ExceptionList(exceptions);
        }
        public virtual int CompareTo(object? obj)
        {
            if (obj == null) return 1;

            if (obj is Person other)
                return ID.CompareTo(other.ID);
            else
                throw new ArgumentException("! PERSON.COMPARE: Object is not a Person instance.");
        }

        public virtual string GetFullName() { return $"{FirstName} {MiddleName} {LastName}"; }
    }
}
