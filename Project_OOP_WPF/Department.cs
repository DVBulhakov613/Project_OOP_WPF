using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public class Department
    {
        #region Properties
        
        public string Name
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public Staff HeadOfDepartment
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<Staff> DepartmentStaff
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<int> DepartmentRooms
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public Hospital ParentHospital
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }

        #endregion

        #region Methods
        public Department(Hospital parent, string name, List<int> rooms, Staff? head = null, List<Staff>? staff = null)
        { throw new NotImplementedException(); }

        public void ChangeHead(int newHeadID)
        { throw new NotImplementedException(); }
        public void ChangeName(string newName) 
        { throw new NotImplementedException(); }
        public void AddStaff(int ID) 
        { throw new NotImplementedException(); }
        public void RemoveStaff(int ID) 
        { throw new NotImplementedException(); }
        public void TransferStaff(int ID, Department dep) 
        { throw new NotImplementedException(); }
        public string StaffInfo() 
        { throw new NotImplementedException(); }
        
        #endregion
    }
}
