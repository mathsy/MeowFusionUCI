using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace UCIHybrid3
{
    class NeuralNetwork
    {
        //Input: 64 char values + 1 previous state double
        //Output: 2 probabilities + 1 previous state double

        static double previous_state = 0;
        static int inputsize = 67;
        static double[] weights = new double[inputsize * 3];

        public void Reset()
        {
            previous_state = 0;
        }

       public bool sf_or_not(string fen, int move)
        {
            int i = 0;
            double[] input = new double[inputsize];
            double[] output = new double[3];

            foreach(double d in input)
            {
                input[i] = 0;
                i++;
            }
            i = 0;
            input[64] = previous_state;
            input[65] = fen.Length;
            input[66] = move;
            foreach(char letter in fen)
            {
                input[i] = Math.Sqrt((int)letter);
        
                i++;
            }

            i = 0;
            foreach(double result in output)
            {
                double inp = 0;
                int inputnum = 0;
                for(int w = i*inputsize; w<=(i+1)*inputsize-1; w++)
                {
                     inp += input[inputnum] * weights[w];
                   // Console.WriteLine(inp + " " + w);
                    inputnum++;
                }
                output[i] = Math.Sqrt(inp);
                i++;
            }
            // output[2] = previous_state;
            previous_state = Math.Sqrt(output[0]);

            double sum = output[0] + output[1];
            double probsf = output[0] / sum;
            Console.WriteLine("info string SF_nn: " + probsf + "      LC0_nn: " + (1-probsf) + "      internal state: " + previous_state + "     sum: " + sum);
            
            if(output[0]>output[1]) { return true; }
            else { return false; }

        }

        public override string ToString()
        {
            string o = "";
            foreach(double d in weights)
            {
                o += d + " ";
            }

            return o;
        }

        public NeuralNetwork(bool random = true)
        {
            Random random1 = new Random();

            int i = 0;
            foreach (double d in weights)
            {
                weights[i] = random1.NextDouble();
                i++;
            }

        }
        public NeuralNetwork(string weight)
        {
            int i = 0;
            foreach(string s in weight.Split(' '))
            {
                try
                {
                    weights[i] = Convert.ToDouble(s);
                  //  Console.WriteLine("Reading: " + i + "=" + weights[i]);
                }
                catch
                {
                    try
                    {
                        weights[i] = 0;
                    }
                    catch
                    {

                    }

                  

                }
                i++;
            }
        }


    }
}
