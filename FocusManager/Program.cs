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
        

       public static int IncrementCurrentNum(int currentNum)
        {
            //increment by 3
            return currentNum + 2;
        }

        static void Main(string[] args)
        {
            /*
               //get the processes
               List<Process> procList = new List<Process>();
               foreach (Process proc in Process.GetProcesses())
               {
                   //Console.WriteLine(proc.ProcessName);
                   if (proc.ProcessName.Contains("elementclient") || proc.ProcessName.Contains("Monkey"))
                   {
                       //change the priority

                       proc.PriorityClass = ProcessPriorityClass.High;
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
                       Console.WriteLine($"Switching Processes to: {proc.Id}");
                       IntPtr windowPtr = proc.MainWindowHandle;
                       SetForegroundWindow(windowPtr);
                       var isResponding = proc.Responding;
                       //Console.WriteLine($"is resonding? {isResponding}");
                       Thread.Sleep(3000);
                   }
               }
           */

            FocusManager fm = new FocusManager();
            fm.setRunningStatus(true);
            //Task.Run(()=> fm.RUN());

   
            while (true)
            {
                Console.WriteLine("Press 'Y' to continue running or 'N' to stop");
                ConsoleKey input = Console.ReadKey().Key;
                if(input == ConsoleKey.Y)
                {
                    fm.setRunningStatus(true);
                }
                if(input == ConsoleKey.N)
                {
                    fm.setRunningStatus(false);
                }
                if(input == ConsoleKey.I)
                {
                    //incrementtime
                    var cur = fm.getSwitchingTimeIntervalInSeconds();
                    Console.WriteLine($"Current Value: {cur}");

                    var setThisAmount = IncrementCurrentNum(cur);
                    
                    fm.setSwitchingIntervals(setThisAmount);
                }

                if(input == ConsoleKey.D)
                {
                    var cur = fm.getSwitchingTimeIntervalInSeconds();
                    Console.WriteLine($"Current Value: {cur}");
                    var setThisAmount = cur - 1;
                    if(setThisAmount > 0)
                    {
                        fm.setSwitchingIntervals(setThisAmount);
                    }
             
                }
            }
            Console.WriteLine("DFDFSD");
        
        
        }

    }
}
