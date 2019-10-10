using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace FocusManager
{
    class Program
    {

        static bool RUNNING = true;
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        
        public bool continueRunning;
        List<Process> processList = new List<Process>();
        public Program()
        {
            continueRunning = true;
            setMonkeyProcessList();

        }
        private bool setMonkeyProcessList()
        {
            foreach (Process proc in Process.GetProcesses())
            {
                Console.WriteLine(proc.ProcessName);
                if (proc.ProcessName.Contains("elementclient"))
                {

                    processList.Add(proc);
                    //proc.WaitForInputIdle();

                }
            }
            return true;
        }

        public bool setFunningFlag(bool continueToRun)
        {
            this.continueRunning = continueToRun;
            return true;
        }

        public async Task Run()
        {
            while (this.continueRunning)
            {
                foreach (Process proc in this.processList)
                {
                    IntPtr windowPtr = proc.MainWindowHandle;
                    SetForegroundWindow(windowPtr);
                    var isResponding = proc.Responding;
                    //Console.WriteLine($"is resonding? {isResponding}");
                    Thread.Sleep(1000);
                }
            }
        }


        static void Main(string[] args)
        {
          
            List<Process> procList = new List<Process>();
            foreach (Process proc in Process.GetProcesses())
            {
                //Console.WriteLine(proc.ProcessName);
                if (proc.ProcessName.Contains("elementclient") || proc.ProcessName.Contains("Monkey"))
                {

                    procList.Add(proc);
                    //proc.WaitForInputIdle();
                    IntPtr monkeyPtr = proc.MainWindowHandle;
                    SetForegroundWindow(monkeyPtr);
                  
                }
            }
            Console.WriteLine($"There exist {procList.Count} Clients");
            ConsoleKey UserInput = ConsoleKey.Y;
            while (true)
            {
                foreach(Process proc in procList)
                {
                    IntPtr windowPtr = proc.MainWindowHandle;
                    SetForegroundWindow(windowPtr);
                    var isResponding = proc.Responding;
                    //Console.WriteLine($"is resonding? {isResponding}");
                    Thread.Sleep(1000);
                }
            }
               
        }
    }
}
