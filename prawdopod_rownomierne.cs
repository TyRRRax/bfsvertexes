using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            double number = 0.0;
            int size = 4;
            int[,] tab = new int[size, size];
            double p = 0.5;


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    number = rnd.NextDouble();
                    Console.WriteLine(number);
                    if (number >= p) { tab[i, j] = 1; }
                    else { tab[i, j] = 0; }
                }
            }
            Console.WriteLine();
            for (int i=0; i< size; i++)
            {
                for(int j=0;j< size; j++)
                {
                    Console.Write(tab[i, j] + "  ");
                    if(j==size-1) { Console.WriteLine(); }
                }
            }
        }
    }
}
