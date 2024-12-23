﻿//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using System.Windows.Controls;
//using System.Xml.Linq;

//namespace Project_OOP_WPF
//{
//    public class Department
//    {
//        private string _name;
//        private Staff _headOfDepartment;
//        #region Properties
//        // an event made in case of department deletion to simplify the process
//        public event Action<Department> DepartmentRemoved;
//        public string Name
//        {
//            get => _name;
//            set
//            {
//                _name = ChangeName(value);
//            }
//        }
//        public Staff HeadOfDepartment 
//        {
//            get => _headOfDepartment;
//            set 
//            {
//                if (value.CurrentHospital != ParentHospital)
//                    throw new ArgumentException("! DEPARTMENT: Cannot assign staff from a different hospital.");
//                _headOfDepartment = value;
//            }
//        }
//        public Dictionary<int, Staff> DepartmentStaff { get; set; } = new();
//        public List<int> DepartmentRooms { get; set; }
//        public Hospital ParentHospital { get; set; }

//        #endregion

//        #region Methods
//        public Department(Hospital parent, string name, List<int> rooms, Staff? head = null, List<Staff>? staffList = null)
//        {
//            // assign parent hospital first for reference
//            List<string> exceptions = new();
//            ParentHospital = parent;

//            // check rooms
//            try { 
//                if (rooms.Any(a => !parent.Rooms.Contains(a) || a < 1)) 
//                    throw new ArgumentException("! DEPARTMENT: Room ID list contains invalid room IDs.");
//                DepartmentRooms = rooms.ToList();
//            }
//            catch (Exception ex) { exceptions.Add(ex.Message); }

//            // check head
//            if (head != null)
//            {
//                try {
//                    if (head.CurrentHospital != parent)
//                        throw new ArgumentException("! DEPARTMENT: Cannot assign staff from a different hospital.");
//                    HeadOfDepartment = head;
//                }
//                catch (Exception ex) { exceptions.Add(ex.Message); }
//            }

//            // check the staff list
//            if (staffList != null)
//            {
//                try { 
//                    if (staffList.Any(a => a.CurrentHospital != parent))
//                        throw new ArgumentException("! DEPARTMENT: Cannot assign staff from a different hospital.");
//                    foreach(Staff staff in staffList)
//                        AddStaff(staff);
//                }
//                catch (Exception ex) { exceptions.Add(ex.Message); }
//            }

//            if(exceptions.Count > 0) throw new ExceptionList(exceptions);

//            // assign the name last
//            Name = name;
//        }


//        // change the head
//        public void ChangeHead(int newHeadID)
//        {
//            if (HeadOfDepartment != null && HeadOfDepartment.ID == newHeadID)
//                throw new ArgumentException("! DEPARTMENT: Cannot assign Staff to the same position.");
//            try { HeadOfDepartment = ParentHospital.ActiveStaff.First(a => a.ID == newHeadID); }
//            catch (Exception ex) { throw new ArgumentException("! DEPARTMENT: Staff does not belong to parent hospital."); } ; 
//        }


//        // change the name
//        public string ChangeName(string newName) 
//        {
//            if (string.IsNullOrWhiteSpace(newName))
//                throw new NullReferenceException("! DEPARTMENT: Cannot have an empty name.");
//            // trim the input
//            newName = newName.Trim();
//            if (newName.Length > 50)
//                throw new ArgumentException("! DEPARTMENT: Cannot have a name that is longer than 50 symbols.");
//            // use regex to check for invalid symbols
//            Regex regex = new(@"^[a-zA-Z '-]+$");
//            if (!regex.IsMatch(newName))
//                throw new ArgumentException("! DEPARTMENT: Illegal symbols detected.");

//            return newName;
//        }


//        public void AddStaff(int ID) 
//        {
//            if (ID < 0) throw new ArgumentException("! DEPARTMENT: ID cannot be less than 0.");
//            if(DepartmentStaff.ContainsKey(ID))  
//                throw new ArgumentException("! DEPARTMENT: This Department already contains a Staff member with this ID.");
//            ParentHospital.ActiveStaff.Select(t => t.ID == ID);
//            DepartmentStaff.Add(ID, ParentHospital.ActiveStaff.First(t => t.ID == ID)); 
//        }

//        public void AddStaff(Staff staff)
//        {
//            if (DepartmentStaff.ContainsKey(staff.ID))
//                throw new ArgumentException("! DEPARTMENT: This Department already contains a Staff member with this ID.");
//            if (staff.CurrentHospital != ParentHospital)
//                throw new ArgumentException("! DEPARTMENT: This Staff member is not part of the same hospital as this Department.");
//            DepartmentStaff.Add(staff.ID, staff);
//            DepartmentRemoved += staff.OnDepartmentRemoval;
//        }
//        // importantly, this ID is for the DICTIONARY of the department
//        // it should still line up with staff ID's though
//        public void RemoveStaff(int ID) 
//        {
//            if (!DepartmentStaff.ContainsKey(ID)) 
//                throw new ArgumentException("! DEPARTMENT: Department does not contain a Staff member with this ID.");
            
//            DepartmentRemoved -= DepartmentStaff[ID].OnDepartmentRemoval;
//            DepartmentStaff.Remove(ID);
//        }
//        // ID is for the DICTIONARY of the department
//        // still should line up with staff ID's
//        public void TransferStaff(int ID, Department dep, bool transfer = false) 
//        {
//            if (!DepartmentStaff.ContainsKey(ID))
//                throw new ArgumentException("! DEPARTMENT: Department does not contain a Staff member with this ID.");
//            if (dep.DepartmentStaff.ContainsKey(ID))
//                throw new ArgumentException("! DEPARTMENT: The target department already contains this Staff instance.");

//            if (!transfer && ParentHospital != dep.ParentHospital)
//                throw new ArgumentException("! DEPARTMENT: Cannot transfer staff to a department in a different hospital when initiated inside the department.");
//            dep.AddStaff(DepartmentStaff[ID]);
//            RemoveStaff(ID);
//        }

//        // event that must be fired on department removal; important !!
//        public void Remove()
//        {
//            DepartmentRemoved?.Invoke(this);
//            DepartmentRemoved = null;
//            DepartmentStaff.Clear();
//        }
//        //public string StaffInfo() 
//        //{
            
//        //}
        
//        #endregion
//    }
//}
