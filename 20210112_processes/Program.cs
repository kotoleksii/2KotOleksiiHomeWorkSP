using System;

namespace _20210112_processes
{
    class Program
    {
        static void Main(string[] args)
        {        
            try
            {
                string comm = ProcessManager.getCommand();

                ProcessManager.runCommand(comm);

                if (comm != "0")
                    Main(null);
            }
            catch (Exception ex)
            {
                ProcessManager.colorInfoMsg("", ConsoleColor.Red, ex.Message);
                Main(null);
            }         
        }       
    }
}