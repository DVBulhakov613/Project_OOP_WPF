using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public class Appointment
    {
        # region Properties
        public int RoomID
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public DateTime StartTime
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public DateTime EndTime
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<Staff> Staff
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public Patient Appointee
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public AppointmentPurpose Purpose
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public AppointmentState State
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        
        #endregion

        # region Methods
        public Appointment(int room, DateTime startTime, DateTime endTime, List<Staff> Staff, Patient appointee, AppointmentPurpose purpose) 
        { throw new NotImplementedException(); }

        public void ChangeState(AppointmentState newState)
        { throw new NotImplementedException(); }
        
        public override string ToString()
        { throw new NotImplementedException(); }
        # endregion
    }
}
