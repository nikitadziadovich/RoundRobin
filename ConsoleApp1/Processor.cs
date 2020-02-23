using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleApp1
{
    public class Processor
    {
        private readonly Queue<Process> _goingQueue;
        private int _currentTime;
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
            _currentTime = Zero;
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
            _currentTime++;
        }

        private void CheckIsReady(Process process)
        {
            if (process.StartTime < _currentTime && process.Status == ProcessStatus.Waits)
            {
                _goingQueue.Enqueue(process);
                process.Status = ProcessStatus.Ready;
            }
        }

        private void MoveFirstProcessToTheEnd()
        {
            if (_goingQueue.TryDequeue(out Process firstProcess))
            {
                _goingQueue.Enqueue(firstProcess);
                firstProcess.Status = ProcessStatus.Ready;
                ResetQuantum();
            }
        }

        private void RunProcessFromQueue()
        {
            if (_goingQueue.TryPeek(out Process currentProcess))
            {
                currentProcess.Status = ProcessStatus.Going;
                currentProcess.DurationTime--;
                _leftQuanta--;
            }
        }

        private void CheckIsProcessEnded()
        {
            if (_goingQueue.TryPeek(out Process currentProcess))
            {
                if (currentProcess.DurationTime == Zero)
                {
                    _goingQueue.Dequeue();
                    _remainingProcesses.Remove(currentProcess);
                    currentProcess.Status = ProcessStatus.Done;
                    ResetQuantum();
                }
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