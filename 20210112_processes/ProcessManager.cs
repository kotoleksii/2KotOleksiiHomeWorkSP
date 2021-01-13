using System;
using System.Linq;
using System.Diagnostics;

namespace _20210112_processes
{
    class ProcessManager
    {
        public static string getCommand()
        {
            Console.WriteLine("1 - All processes");
            Console.WriteLine("2 - Find process by PID");
            Console.WriteLine("3 - Threads info");
            Console.WriteLine("4 - Included modules");
            Console.WriteLine("5 - Run process");
            Console.WriteLine("6 - Kill process");
            Console.WriteLine("0 - Exit");

            Console.Write(">>> ");
            return Console.ReadLine();
        }

        public static void runCommand(string comm)
        {
            switch (comm)
            {
                case "0":
                    break;
                case "1":
                    getAllProcesses();
                    break;
                case "2":
                    findByPid();
                    break;
                case "3":
                    getThreads();
                    break;
                case "4":
                    getModules();
                    break;
                case "5":
                    runProcess();
                    break;
                case "6":
                    killProcess();
                    break;
                default:
                    colorInfoMsg("", ConsoleColor.Red, "Invalid command");
                    break;
            }
        }

        private static void getAllProcesses()
        {
            Console.WriteLine("\n--- All processes --- ");
            var allProcesses = from proc in Process.GetProcesses()
                               orderby proc.ProcessName
                               select proc;

            foreach (var p in allProcesses)
                Console.WriteLine($"{p.Id}: {p.ProcessName}");

            Console.WriteLine();
        }

        private static void findByPid()
        {
            Process p = getProcessByPid();

            Console.WriteLine($"\nPID: {p.Id}\tName: {p.ProcessName}\n");
        }

        private static void getThreads()
        {
            Process p = getProcessByPid();

            var threads = p.Threads;
            foreach (ProcessThread t in threads)
                Console.WriteLine($"ID: {t.Id}\tTime: {t.StartTime.ToShortTimeString()}\tPriority: {t.PriorityLevel}");
            Console.WriteLine();
        }

        private static void getModules()
        {
            Process p = getProcessByPid();

            var modules = p.Modules;
            foreach (ProcessModule m in modules)
                Console.WriteLine($"{m.ModuleName}");
            Console.WriteLine();
        }

        private static void runProcess()
        {
            Console.Write("Specify the name of the process: ");

            Process p = new Process();
            p.StartInfo.FileName = Console.ReadLine();
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.Start();

            colorInfoMsg(p.StartInfo.FileName, ConsoleColor.Green, "running");
        }

        private static void killProcess()
        {
            getAllProcesses();

            Process p = getProcessByPid();
            p.Kill();

            colorInfoMsg(p.ProcessName, ConsoleColor.Red, "killed");
        }

        private static Process getProcessByPid()
        {
            Console.Write("Finded PID: ");
            int pid = Int32.Parse(Console.ReadLine());

            return Process.GetProcessById(pid);
        }

        public static void colorInfoMsg(string processName, ConsoleColor color, string action)
        {
            if (processName == String.Empty)
            {
                Console.Write($"\n{processName}");
                Console.ForegroundColor = color;
                Console.WriteLine($"{action}\n");
                Console.ResetColor();
            }
            else
            {
                Console.Write($"\n{processName} ");
                Console.ForegroundColor = color;
                Console.WriteLine($"{action}\n");
                Console.ResetColor();
            }
        }
    }
}