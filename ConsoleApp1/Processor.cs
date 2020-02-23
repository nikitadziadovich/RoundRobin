using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class Processor
    {
        private readonly Queue<Process> _goingQueue;
        private int _currentQuantum;
        private int _leftQuanta;
        private readonly int _quantaPerProcess;
        private readonly List<Process> _allProcesses;
        private readonly List<Process> _remainingProcesses;
        private readonly int _processesCount;

        public readonly List<char[]> ResultMatrix;

        public Processor(int quantaPerProcess, List<Process> processes)
        {
            _quantaPerProcess = quantaPerProcess;
            _allProcesses = new List<Process>(processes);
            _remainingProcesses = new List<Process>(processes);
            _currentQuantum = Zero;
            _processesCount = processes.Count;
            _goingQueue = new Queue<Process>();
            ResultMatrix = new List<char[]>();
        }

        public void Work()
        {
            ResetQuantum();
            while (_remainingProcesses.Count > 0)
            {
                foreach (Process process in _allProcesses) CheckIsReady(process);
                CheckIsProcessEnded();
                if (_remainingProcesses.Count == 0) break;
                if (_leftQuanta == Zero) MoveFirstProcessToTheEnd();
                RunProcessFromQueue();
                SaveStateInMatrix();
                Tact();
            }
        }

        private void ResetQuantum()
        {
            _leftQuanta = _quantaPerProcess;
        }

        private void Tact()
        {
            _currentQuantum++;
        }

        private void CheckIsReady(Process process)
        {
            if (process.IsReadyToStart(_currentQuantum))
            {
                _goingQueue.Enqueue(process);
                process.GetReady();
            }
        }

        private void MoveFirstProcessToTheEnd()
        {
            if (_goingQueue.TryDequeue(out Process firstProcess))
            {
                _goingQueue.Enqueue(firstProcess);
                firstProcess.GetReady();
                ResetQuantum();
            }
        }

        private void RunProcessFromQueue()
        {
            if (_goingQueue.TryPeek(out Process currentProcess))
            {
                currentProcess.Start();
                currentProcess.DurationTime--;
                _leftQuanta--;
            }
        }

        private void CheckIsProcessEnded()
        {
            if (_goingQueue.TryPeek(out Process currentProcess) && currentProcess.DurationTime == Zero)
            {
                _goingQueue.Dequeue();
                _remainingProcesses.Remove(currentProcess);
                currentProcess.End();
                ResetQuantum();
            }
        }

        private void SaveStateInMatrix()
        {
            char[] state = new char[_processesCount];
            for (int i = 0; i < _processesCount; i++)
            {
                state[i] = _allProcesses[i].ToString().ToCharArray().First();
            }
            ResultMatrix.Add(state);
        }

        private const int Zero = 0;
    }
}