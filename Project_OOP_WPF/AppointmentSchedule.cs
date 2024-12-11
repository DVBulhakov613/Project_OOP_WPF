using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public class AppointmentSchedule
    {
        public IDManagement IDManager = new IDManagement();
        public Dictionary<int, Appointment> Appointments 
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); } 
        }
        #region Methods
        // DO NOT FORGET:
        // 1. MUST check for overlap in ALL participant schedules
        public void CreateAppointment(int room, DateTime startTime, DateTime endTime, List<Staff> staffInvolved, Patient appointee, AppointmentPurpose purpose)
        { throw new NotImplementedException(); }

        public string CancelAppointment(int appID)
        { throw new NotImplementedException(); }

        public Appointment GetAppointment(int appID)
        { throw new NotImplementedException(); }

        public override string ToString()
        { throw new NotImplementedException(); }
        #endregion
    }
}
