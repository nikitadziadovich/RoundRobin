using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Process
    {
        public int StartTime { get; }
        public int DurationTime { get; set; }
        public ProcessStatus Status { get; set; }

        public Process(int startTime, int durationTime)
        {
            this.StartTime = startTime;
            this.DurationTime = durationTime;
            this.Status = ProcessStatus.Waits;
        }

        public override string ToString()
        {
            switch (Status)
            {
                case ProcessStatus.Ready: return "r"; 
                case ProcessStatus.Going: return "g";
                default: return "-";
            }
        }
    }

    public enum ProcessStatus
    {
        Waits,
        Ready,
        Going,
        Done
    }
}