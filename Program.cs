using System;
using System.Diagnostics;
using System.IO;

namespace UCIHybrid3
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Process stockfish = new Process();
            ProcessStartInfo si = new ProcessStartInfo()
            {
                FileName = "stockfish.exe",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };

            stockfish = new Process();
            stockfish.StartInfo = si;
            try
            {
             
                stockfish.PriorityClass = ProcessPriorityClass.Normal;
            }
            catch { }

            stockfish.OutputDataReceived += new DataReceivedEventHandler(communicator.myProcess_OutputDataReceived);

            stockfish.Start();
            stockfish.BeginErrorReadLine();
            stockfish.BeginOutputReadLine();



            Process lc0 = new Process();
            ProcessStartInfo si2 = new ProcessStartInfo()
            {
                FileName = "lc0.exe",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };

            lc0 = new Process();
            lc0.StartInfo = si2;
            try
            {
             
                lc0.PriorityClass = ProcessPriorityClass.Normal;
            }
            catch { }

            lc0.OutputDataReceived += new DataReceivedEventHandler(communicator.myProcess_OutputDataReceived_lc0);

            lc0.Start();
            lc0.BeginErrorReadLine();
            lc0.BeginOutputReadLine();
            Console.WriteLine("MeowFusion v. 1.0.0-0013");
            Console.WriteLine("The combination between Stockfish NNUE and Leela Chess Zero");
            UCI.Start(stockfish, lc0);
        }

       
    }
    public class communicator
    {
        public static void SendLine(string command, Process process)
        {
            process.StandardInput.WriteLine(command);
            process.StandardInput.Flush();
        }

        public static void myProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            string text = e.Data;
            if (!text.Contains("Stockfish"))
            {
                Console.WriteLine(text);
            }
        }

        public static void myProcess_OutputDataReceived_lc0(object sender, DataReceivedEventArgs e)
        {
            string text = e.Data;
            Console.WriteLine(text);
        }
    }
}
