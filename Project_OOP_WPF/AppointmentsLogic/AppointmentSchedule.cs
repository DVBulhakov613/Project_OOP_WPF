using System;
using System.Collections;
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
            if (staffInvolved == null || staffInvolved.Count == 0)
                throw new ArgumentException("! SCHEDULE: At least one staff member must be involved.");
            if (appointee == null)
                throw new NullReferenceException("! SCHEDULE: Appointments must have appointees.");

            List<string> exceptions = new();
            foreach(Staff staff in staffInvolved)
                if (staff.Schedule.Appointments.Values.Any(a => (startTime < a.EndTime && endTime > a.StartTime)))
                    exceptions.Add($"! SCHEDULE: Staff member {staff.GetFullName}; ID {staff.ID} has a schedule planned already.");

            if (appointee.Schedule.Appointments.Values.Any(a => startTime < a.EndTime && endTime > a.StartTime && (a.State != AppointmentState.Ended || a.State != AppointmentState.Cancelled)))
                exceptions.Add($"! SCHEDULE: Patient member {appointee.GetFullName}; ID {appointee.ID} has a schedule planned already.");

            if(exceptions.Count > 0) throw new ExceptionList(exceptions);
            
            exceptions.Clear();
            try
            {
                Appointment app = new Appointment(room, startTime, endTime, staffInvolved, appointee, purpose);
                foreach (Staff staff in staffInvolved)
                    staff.Schedule.AddAppointment(app);
                appointee.Schedule.AddAppointment(app);
            }
            catch (Exception ex) { exceptions.Add(ex.Message); }
            if (exceptions.Count > 0) throw new ExceptionList(exceptions);
        }

        private void AddAppointment(Appointment appointment)
        { Appointments.Add(IDManager.GenerateID(), appointment); }

        public void CancelAppointment(int appID)
        {
            if (!Appointments.ContainsKey(appID))
                throw new ArgumentException("! SCHEDULE: Appointment does not exist for this member!");

            Appointments[appID].State = AppointmentState.Cancelled;
        }

        //public Appointment GetAppointment(int appID)
        //{ throw new NotImplementedException(); }

        //public override string ToString()
        //{ throw new NotImplementedException(); }
        #endregion
    }
}
