using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public class Appointment //: IDisposable
    {
        private int _roomId;
        private DateTime _startTime;
        private DateTime _endTime;
        private List<Staff> _staff;
        private Patient _appointee;
        private AppointmentPurpose _purpose;
        private AppointmentState _state;
        private bool disposedValue;

        #region Properties
        public int RoomID
        {
            get => _roomId;
            private set
            {
                if (value < 1)
                    throw new ArgumentException("! APPOINTMENT: Rooms with ID below 0 are not possible.");
                _roomId = value;
            }
        }

        public DateTime StartTime
        {
            get => _startTime;
            private set
            {
                //if (value >= EndTime)
                //    throw new ArgumentException("Cannot schedule for a negative amount of time!");
                if (value > DateTime.Now.AddMonths(2))
                    throw new ArgumentException("! APPOINTMENT: Cannot schedule further than 2 months ahead.");
                if (value < DateTime.Now.AddDays(1))
                    throw new ArgumentException("! APPOINTMENT: Cannot schedule earlier than tomorrow.");
                if (value < DateTime.Now)
                    throw new ArgumentException("! APPOINTMENT: Cannot schedule an appointment in the past.");
                _startTime = value;
            }
        }

        public DateTime EndTime
        {
            get => _endTime;
            private set
            {
                if (value <= StartTime)
                    throw new ArgumentException("! APPOINTMENT: End time must be greater than the start time.");
                if (value > DateTime.Now.AddMonths(2).AddHours(2))
                    throw new ArgumentException("! APPOINTMENT: Cannot schedule further than 2 months ahead.");
                if (value < DateTime.Now.AddDays(1))
                    throw new ArgumentException("! APPOINTMENT: Cannot schedule earlier than tomorrow.");
                if (value < DateTime.Now)
                    throw new ArgumentException("! APPOINTMENT: Cannot schedule an appointment in the past.");
                _endTime = value;
            }
        }

        public List<Staff> Staff
        {
            get => _staff;
            private set
            {
                if (value == null || value.Count == 0)
                    throw new NullReferenceException("! APPOINTMENT: Staff list cannot be non-existent or empty.");
                _staff = value;
            }
        }

        public Patient Appointee
        {
            get => _appointee;
            private set
            {
                if (value == null)
                    throw new NullReferenceException("! APPOINTMENT: Appointee cannot be non-existent.");
                _appointee = value;
            }
        }

        public AppointmentPurpose Purpose
        {
            get => _purpose;
            private set => _purpose = value; // there wont be any validation here tbh its not needed
        }

        public AppointmentState State
        {
            get => _state;
            set => _state = value; // there wont be any validation here tbh its not needed
        }
        #endregion

        #region Methods
        public Appointment(int room, DateTime startTime, DateTime endTime, List<Staff> staff, Patient appointee, AppointmentPurpose purpose) 
        {
            List<string> exceptions = new();

            // make a check for DAYS, not total time
            if (startTime >= endTime) exceptions.Add("! APPOINTMENT: The end time of an appointment must be greater than its start time.");
            else
                try { StartTime = startTime; }
                catch(Exception ex) { exceptions.Add(ex.Message); }

            try { EndTime = endTime; }
            catch (Exception ex) { exceptions.Add(ex.Message); }
            // compare the start and end time between each other to
            // not allow appointments that last for days
            try 
            { 
                Staff = staff;
                if (staff.Any(t => t.CurrentHospital != appointee.CurrentHospital))
                    throw new ArgumentException("! APPOINTMENT: Cannot assign appointments across different Hospitals.");
            }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try 
            { 
                Appointee = appointee;
                if (!appointee.CurrentHospital.Rooms.Contains(room))
                    throw new ArgumentException("! APPOINTMENT: Cannot assign appointments to rooms that do not exist within the current Hospital.");
            }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            try { RoomID = room; }
            catch (Exception ex) { exceptions.Add(ex.Message); }

            if (exceptions.Count > 0)
                throw new ExceptionList(exceptions);

            Purpose = purpose;
            State = AppointmentState.Scheduled;
        }

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            // TODO: dispose managed state (managed objects)
        //        }

        //        // TODO: free unmanaged resources (unmanaged objects) and override finalizer
        //        // TODO: set large fields to null
        //        disposedValue = true;
        //    }
        //}

        //// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        //// ~Appointment()
        //// {
        ////     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        ////     Dispose(disposing: false);
        //// }

        //public void Dispose()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //    Dispose(disposing: true);
        //    GC.SuppressFinalize(this);
        //}
        #endregion
    }
}
