using System;
using System.Threading;

namespace TimeTinkering
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var timeTinker = new TimeTinker();

            var startTime = new TimeSpan(18, 10, 00);
            var endTime = new TimeSpan(18, 11, 00);

            timeTinker.Start(startTime, endTime, 5);

            while (true)
            {

            }
        }
    }

    internal class TimeTinker
    {
        private Timer _workTimer; 
        private TimeSpan _startTimeOfDay;
        private TimeSpan _endTimeOfDay;
        private bool _inWorkingHours;

        public void Start(TimeSpan startTimeOfDay, TimeSpan endTimeOfDay, int secondsInterval)
        {
            _startTimeOfDay = startTimeOfDay;
            _endTimeOfDay = endTimeOfDay;

            _workTimer = new Timer(Control);
            _workTimer.Change(0, secondsInterval * 1000);
        }

        public void Control(object sender)
        {
            Console.WriteLine("Checking time...");

            var timeNow = DateTime.Now;

            var startTime = DateTime.Today.Add(_startTimeOfDay);
            var endTime = DateTime.Today.Add(_endTimeOfDay);

            switch (_inWorkingHours)
            {
                case false when timeNow > startTime && timeNow < endTime:
                    StartWork();
                    break;
                case true when timeNow > endTime:
                    StopWork();
                    break;
            }

            if(_inWorkingHours)
                Work(sender);
        }

        private void StartWork()
        {
            _inWorkingHours = true;
            Console.WriteLine("Start Working!");
        }

        private void StopWork()
        {
            _inWorkingHours = false;
            Console.WriteLine("Stop Working!");
        }

        private void Work(object sender)
        {
            Console.WriteLine("Working!");
        }
    }
}
