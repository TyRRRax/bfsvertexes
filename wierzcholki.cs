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
            int[] tabW = new int[size]; // Tablica, która posiada informacje jaką wartość ma dany wierzchołek
            double p = 0.5;
            int licz = 0;
            int sumaW = 1;


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // number = rnd.NextDouble();
                    number = rnd.Next(0,sumaW+1);
                    Console.WriteLine(number);
                    if (number >= p) { tab[i, j] = 1; tab[j, i] = 1; }
                    else { tab[i, j] = 0; tab[j, i] = 0; }

                    for (int x = 0; x < size; x++)
                    {
                        licz = 0;
                        for (int y = 0; y < size; y++)
                        {
                            if (tab[x, y] == 1) { licz++; }
                        }
                        tabW[x] = licz;                     // Liczenie wartościowości każdego wierzchołka
                        sumaW = sumaW + licz;
                    }
                    p = sumaW;

                }
            }

            /// WYSWIETLANIE /// 
             Console.WriteLine();
             for (int z = 0; z < size; z++)
             {
                 for (int u = 0; u < size; u++)
                 {
                     Console.Write(tab[z, u] + "  ");
                     if (u == size - 1) { Console.WriteLine(); }
                 }
             } 
             ///
             /// WYPISYWANIE WARTOŚCIOWOŚCI WIERZCHOŁKÓW /// 
            for (int w = 0; w < size; w++)
            { Console.Write(tabW[w]); sumaW = tabW[w] + sumaW; }
        }
    }
}
