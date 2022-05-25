using System;

namespace TrenchesRTS.Controllers
{
    public interface IController
    {
    }

    public interface ICommand
    {
        public void Execute();
    }

    public class PlayerController : IController
    {

    }

    public class KeyPress
    {
        private TimeSpan startTimeSpan;
        private TimeSpan _elapsedTimeSpan;
        private TimeSpan _endTimeSpan;
    }
}
