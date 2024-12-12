using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public class AppointmentSchedule
    {
        private IDManagement IDManager = new IDManagement();
        public Dictionary<int, Appointment> Appointments = new();

        #region Methods
        // DO NOT FORGET:
        // 1. MUST check for overlap in ALL participant schedules
        public void CreateAppointment(int room, DateTime startTime, DateTime endTime, List<Staff> staffInvolved, Patient appointee, AppointmentPurpose purpose)
        {
            
        }

        public string CancelAppointment(int appID)
        { throw new NotImplementedException(); }

        public Appointment GetAppointment(int appID)
        { throw new NotImplementedException(); }

        public override string ToString()
        { throw new NotImplementedException(); }
        #endregion
    }
}
