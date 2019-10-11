using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FocusManager
{

    class FocusManager
    //https://www.codeproject.com/Articles/2976/Detect-if-another-process-is-running-and-bring-it
    //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindowasync
    //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
    //https://www.codeproject.com/Articles/2976/Detect-if-another-process-is-running-and-bring-it
    //https://elanderson.net/2016/09/add-git-ignore-to-existing-visual-studio-project/
    {

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        //shows the window asyn, does not block and continues program eecution 
        //without needing the controol to come back

        //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindowasync
        /*
         * Remarks
This function posts a show-window event to the message queue of the given window. An application can use this function to avoid becoming nonresponsive while waiting for a nonresponsive application to finish processing a show-window event.
         * */
        [DllImport("USER32.DLL")]
        public static extern bool ShowWindowAsync(IntPtr hwd, int nCmdShowOption);

        private List<Process> procList;
        private bool continueToRun;
        private int switchingTimeIntervals;
        
        public FocusManager()
        {
            initalizeProcessList();
            continueToRun = false;
            setSwitchingIntervals(1);
        }

        public (bool, List<Process>) getProcessList()
        {
            List<Process> result = new List<Process>();
            foreach (Process proc in Process.GetProcesses())
            {
                //Console.WriteLine(proc.ProcessName);
                if (proc.ProcessName.Contains("elementclient") || proc.ProcessName.Contains("Monkey"))
                {
                    //change the priority

                    proc.PriorityClass = ProcessPriorityClass.High;
                    result.Add(proc);

                }
            }
            return (true, result);
        }

        private bool initalizeProcessList()
        {
            (bool wasSuccess, List<Process> processList) = getProcessList();
            if (!wasSuccess)
            {
                throw new Exception("Error getting the process List");
            }
            this.procList = processList;
            return true;
        }

        public bool setSwitchingIntervals(int switchIntervalsInSeconds)
        {
            this.switchingTimeIntervals = switchIntervalsInSeconds;
            return true;
        }

        public int getSwitchingTimeIntervalInSeconds()
        {
            return this.switchingTimeIntervals;
        }

        public bool setRunningStatus(bool continueRunning)
        {
            Console.WriteLine($"Setting running status to: {continueRunning}");
            if(continueToRun == false)
            {
                continueToRun = true;
                Task.Run( () =>this.RUN() ) ;
            }
            else
            {
                this.continueToRun = continueRunning;
            }
            
            
            return true;
        }

        public void RUN()
        {
            while (continueToRun)
            {
                foreach (Process proc in procList)
                {
                    Console.WriteLine($"Switching Processes to: {proc.Id}");
                    IntPtr windowPtr = proc.MainWindowHandle;
                    ShowWindowAsync(windowPtr, 9);
                    SetForegroundWindow(windowPtr);
                    var isResponding = proc.Responding;
                    //Console.WriteLine($"is resonding? {isResponding}");
                    Thread.Sleep(this.switchingTimeIntervals * 1000);
                }
            }
        }

    }
}
