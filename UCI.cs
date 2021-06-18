using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace UCIHybrid3
{
    class UCI
    {
        public static NeuralNetwork nn;
        public static void Start(Process sf, Process lc0)
        {
            nn = new NeuralNetwork(File.ReadAllText("nn.txt"));
            int move = 1;
            while (true)
            {


                string cmd = Console.ReadLine();
                move++;
                if(cmd=="position")
                {
                    communicator.SendLine(cmd, sf);
                    communicator.SendLine(cmd, lc0);
                    nn.sf_or_not(cmd, move); //Update internal state
                }
                
                if(cmd=="uci")
                {
                    Console.WriteLine("id name CATFusion");
                    Console.WriteLine("id author SF and LC0 team, Raberger Raphael");
                    Console.WriteLine("opiton name Threads type spin default 4 min 1 max 256");
                    Console.WriteLine("option name Hash type spin defaukt 128 min 4 max 50000");
                    Console.WriteLine("uciok");
                    
                }

                

                if(cmd.StartsWith("setoption name"))
                {
                    communicator.SendLine(cmd, sf);
                    communicator.SendLine(cmd, lc0);
                }

                if(cmd=="stop")
                {
                    communicator.SendLine(cmd, sf);
                    communicator.SendLine(cmd, lc0);
                }
                if(cmd.StartsWith("go"))
                {
                    if(nn.sf_or_not(cmd, move))
                    {
                        communicator.SendLine(cmd, sf);
                    }
                    else
                    {
                        communicator.SendLine(cmd, lc0);
                    }
                    

                }
                if(cmd=="ucinewgame")
                {
                    communicator.SendLine(cmd, sf);
                    communicator.SendLine(cmd, lc0);
                    nn.Reset();
                    move = 1;
                }
                if(cmd=="isready")
                {
                    Console.WriteLine("readyok");
                }
                File.Delete("nn.txt");
                File.WriteAllText("nn.txt", nn.ToString());
            }
        }


    }
}
