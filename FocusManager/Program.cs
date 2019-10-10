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

        
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        



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

            while (true)
            {
                foreach(Process proc in procList)
                {
                    IntPtr windowPtr = proc.MainWindowHandle;
                    SetForegroundWindow(windowPtr);
                    var isResponding = proc.Responding;
                    //Console.WriteLine($"is resonding? {isResponding}");
                    Thread.Sleep(500);
                }
            }
               
        }
    }
}
