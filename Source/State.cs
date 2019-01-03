using System;
using System.Linq;

namespace State
{
    public class StateMainApp
    {
        public static void Main(string[] args)
        {
            var appointment = new Appointment("John Smith");

            appointment.Proceed();
            appointment.ChangeState(new CheckedOut(appointment));
            appointment.Proceed();
            appointment.ChangeState(new Canceled(appointment));
            appointment.Proceed();
        }
    }

    public interface IAppointmentState
    {
        Appointment Appointment { get; set; }
        IAppointmentState NextState { get; }
        bool CanChangeTo(IAppointmentState newState);
    }

    public class Requested : IAppointmentState
    {
        public Appointment Appointment { get; set; }
        public IAppointmentState NextState => new Scheduled(Appointment);
        public Requested(Appointment appointment) => Appointment = appointment;

        public bool CanChangeTo(IAppointmentState newState)
        {
            var acceptedChangeTypes = new []{ typeof(Scheduled), typeof(Canceled) };
            return acceptedChangeTypes.Contains(newState.GetType());
        }
    }

    public class Scheduled : IAppointmentState
    {
        public Appointment Appointment { get; set; }
        public IAppointmentState NextState => new CheckedIn(Appointment);
        public Scheduled(Appointment appointment) => Appointment = appointment;

        public bool CanChangeTo(IAppointmentState newState)
        {
            var acceptedChangeTypes = new[] { typeof(CheckedIn), typeof(Canceled) };
            return acceptedChangeTypes.Contains(newState.GetType());
        }
    }

    public class CheckedIn : IAppointmentState
    {
        public Appointment Appointment { get; set; }
        public IAppointmentState NextState => new CheckedOut(Appointment);
        public CheckedIn(Appointment appointment) => Appointment = appointment;

        public bool CanChangeTo(IAppointmentState newState)
        {
            var acceptedChangeTypes = new[] { typeof(Scheduled), typeof(Canceled) };
            return acceptedChangeTypes.Contains(newState.GetType());
        }
    }

    public class CheckedOut : IAppointmentState
    {
        public Appointment Appointment { get; set; }
        public IAppointmentState NextState => null;
        public CheckedOut(Appointment appointment) => Appointment = appointment;

        public bool CanChangeTo(IAppointmentState newState)
        {
            var acceptedChangeTypes = new[] { typeof(CheckedIn), typeof(Canceled) };
            return acceptedChangeTypes.Contains(newState.GetType());
        }
    }

    public class Canceled : IAppointmentState
    {
        public Appointment Appointment { get; set; }
        public IAppointmentState NextState => null;
        public Canceled(Appointment appointment) => Appointment = appointment;

        public bool CanChangeTo(IAppointmentState newState)
        {
            var acceptedChangeTypes = new[] { typeof(Requested), typeof(Scheduled), typeof(CheckedIn), typeof(CheckedOut) };
            return acceptedChangeTypes.Contains(newState.GetType());
        }
    }

    public class Appointment
    {
        private IAppointmentState _State;
        public string PatientName { get; set; }        

        public Appointment(string patientName)
        {
            PatientName = patientName;
            _State = new Requested(this);
        }

        public void Proceed()
        {
            var nextState = _State.NextState;
            if (nextState == null)
                Console.WriteLine("This is the final step.");
            else _State = nextState;
        }

        public void ChangeState(IAppointmentState newState)
        {
            if (_State.CanChangeTo(newState))
                _State = newState;
            else Console.WriteLine("The current state cannot be changed to the specified state.");
        }
    }
}