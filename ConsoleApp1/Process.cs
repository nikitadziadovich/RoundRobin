namespace ConsoleApp1
{
    public class Process
    {
        private readonly int _startTime;
        private ProcessStatus _status;
        
        public int DurationTime { get; set; }

        public Process(int startTime, int durationTime)
        {
            _startTime = startTime;
            DurationTime = durationTime;
            _status = ProcessStatus.Waits;
        }

        public void Start()
        {
            _status = ProcessStatus.Going;
        }

        public void GetReady()
        {
            _status = ProcessStatus.Ready;
        }

        public void End()
        {
            _status = ProcessStatus.Done;
        }

        public bool IsReadyToStart(int currentQuantum)
        {
            return _startTime < currentQuantum && _status == ProcessStatus.Waits;
        }

        public override string ToString()
        {
            switch (_status)
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