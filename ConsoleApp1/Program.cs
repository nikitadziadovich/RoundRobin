using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        private static int _processesCount;
        
        static void Main(string[] args)
        {
            InitializeProcesses(out var processes);
            _processesCount = processes.Count;
            
            Processor processor = new Processor(3, processes);
            processor.Work();
            
            OutPutMatrix(processor.ResultMatrix);
        }

        private static void InitializeProcesses(out List<Process> processes)
        {
            processes = new List<Process>();
            processes.Add(new Process(-1, 5));
            processes.Add(new Process(2, 5));
            processes.Add(new Process(5, 1));
            processes.Add(new Process(7, 3));
            processes.Add(new Process(15, 2));
            processes.Add(new Process(16, 4));
        }

        private static void OutPutMatrix(List<char[]> result)
        {
            Console.Write(CellTemplate, "");
            for (int i = 0; i < result.Count; i++)
            {
                Console.Write(CellTemplate, i);
            }
            Console.WriteLine();
            for (int j = 0; j < _processesCount; j++)
            {
                Console.Write(CellTemplate, j + 1);
                foreach (var state in result)
                {
                    Console.Write(CellTemplate, state[j]);
                }
                Console.WriteLine();
            }
        }
        
        private const string CellTemplate = "{0,3}";
    }
}