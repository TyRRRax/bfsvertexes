using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication12
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace ConsoleApplication12
    {
        class Program
        {
            class Vertex
            {
                private static readonly Random rnd = new Random();
                public static double GetRandomNumber()
                {
                    lock (rnd)
                    {
                        return rnd.NextDouble();
                    }
                }
                public double value = 0.0;
                public string color = "white";
                public double sourceDistance = double.PositiveInfinity;
                public Vertex()
                {
                    value = GetRandomNumber(); // Wylosowane prawdopodobieństwo od 0-1.
                    Console.WriteLine(value);
                } // Konstruktor, który dla obiektu generuje prawdopodobieństwo
                public static void CreateVertexes(Vertex[] tabVert, int size)
                {
                    for (int i = 2; i < size; i++)
                    {
                        tabVert[i] = new Vertex();    // Tworzenie potrzebnej liczby wierzchołków(z określonym prawdopodobieństwem)          
                    }
                }
                public static int CheckIfEmpty(Vertex[] queue,int size)
                {
                    int value = -1;
                    for(int i=0;i<size;i++)
                    {
                        if (queue[i] != null) value++;
                    }
                    return value;
                }
                public static void AddToQueue(int[,] tab, Vertex[] tabVert,Vertex[] queue, int size, int y)
                {
                    int x = Vertex.CheckIfEmpty(queue, size); // Sprawdza które miejsce jako ostatnie jest zajęte w tablicy kolejki
                    x++; // Miejsce w które wstawić możemy coś do kolejki nie nadpisująć niczego, co nas interesuje
                    for (int i = y; i < size; i++)
                    {
                        if((tab[y,i] == 1) && (tabVert[i].color =="white")) // informacja o tym, że wezel byl nie ruszony
                        {
                            tabVert[i].color = "gray";
                            tabVert[i].sourceDistance = y + 1;
                            queue[x] = tabVert[i];
                            x++;
                        }
                    }
                    tabVert[y].color = "black"; // Informacja o skończonej pracy na danym węźle
                    
                }
                public static void RepairIndexOfQueue(Vertex[] queue, int size)
                {
                   if((queue[0] == null) && (queue[1] != null))
                   {
                        for (int i = 0; i < size; i++)
                        {
                            if (i == size-1) { }
                            else queue[i] = queue[i + 1];
                        }         
                   }
                }
            }

            public static void Show(int size, int[,] tab)
            {
                Console.WriteLine();
                for (int z = 0; z < size; z++)
                {
                    for (int u = 0; u < size; u++)
                    {
                        Console.Write(tab[z, u] + "  ");
                        if (u == size - 1) { Console.WriteLine(); }
                    }
                }
            } // Funkcja wyświetlająca tablicę
            public static int[] VertexesValue(int[,] tab, int size)  // Funkcja liczy ile połączeń ma każdy z wierzchołków
            {
                int[] tabW = new int[size];
                for (int i = 0; i < size; i++)
                {
                    int counter = 0;
                    for (int j = 0; j < size; j++)
                    {
                        if (tab[i, j] == 1) { counter++; }
                    }
                    tabW[i] = counter;
                }
                return tabW;
            }
            public static int Sum(int[] tabW, int size)
            {
                int sumaW = 0;
                for (int i = 0; i < size; i++)
                {
                    sumaW = sumaW + tabW[i];
                }
                return sumaW;
            }
            public static double[] ProbValue(int[] tabW, double p, int size)
            {
                double[] tabP = new double[size]; // Tablica prawdopodobieństwas
                double lastvalue = 0.0;
                for (int x = 1; x <= size; x++)
                {
                    tabP[x - 1] = lastvalue + tabW[x - 1] * p; // Wrzucenie do tablicy prawdopodobieństwa, przedziału(p) * tabW[x] (wart. wierzch.)
                    lastvalue = tabP[x - 1];
                    Console.Write(lastvalue + " ");
                }
                Console.WriteLine("");
                return tabP;
            }

            static void Main(string[] args)
            {
                int size = 4; // liczba wierzcholkow
                int[,] tab = new int[size, size]; // tablica zawierajaca polaczenia miedzywierzcholkowe
                int[] tabW = new int[size]; // Tablica, która posiada informacje jaką wartość ma dany wierzchołek
                double[] tabP = new double[size]; // Tablica granic prawdopodobieństwa
                Vertex[] tabVert = new Vertex[size]; // Tablica obiektów

                // Okreslenie pierwszego połączenia //
                Console.WriteLine("Wartosci prawdopodobienstw wierzcholkow: ");
                Vertex vert1 = new Vertex(); // Wierzchołek nr 0
                tab[0, 0] = 0;
                tabVert[0] = vert1;
                Vertex vert2 = new Vertex(); // Wierzchołek nr 1
                tabVert[1] = vert2;
                tab[0, 1] = 1;
                tab[1, 0] = 1;
                // ###### // 
                Console.WriteLine("Te wartości są istotne: ");
                Vertex.CreateVertexes(tabVert, size); // Tworzy pozostałe wierzchołki
                Console.WriteLine();
                // Wypełnienie przekątnej zerami, żeby był graf nieskierowany
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (i == j) { tab[i, j] = 0; }
                    }
                }
                // ###### // 
                double p = 0;
                int y = 0, sumaW = 0;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if ((i == j) || (i == 0 && j == 1) || (i == 1 && j == 0)) { } // Ten if przetrzymuje iteracje,
                                                                                      //żeby pominąć te pola, dla których wartości są zdefiniowane statycznie
                        else
                        {
                            tabW = VertexesValue(tab, size); // Wywołanie metody, która wrzuci nam w tablice tabW (wartościowość wierzchołków)
                            sumaW = Sum(tabW, size); // Otrzymanie sumy wartościowości wierzchołków 
                            p = 1.0 / sumaW; // przedział dzielący przez wartościowości wierzchołków
                            tabP = ProbValue(tabW, p, size); // Da to tablice prawdopodobieństwa (wartości graniczne do przypinania)
                            if (j == 0)
                            {
                                if (tabVert[y].value < tabP[j]) { tab[i, j] = 1; tab[j, i] = 1; }
                            }
                            else
                            {
                                if (tabVert[y].value < tabP[j] && tabVert[y].value >= tabP[j - 1]) { tab[i, j] = 1; tab[j, i] = 1; break; }
                            }
                        }
                    }
                    y++;
                }
                Show(size, tab);             /// WYSWIETLANIE 
                Console.WriteLine("-----------------------------------------");
                /// ####### CZĘŚĆ ODPOWIADAJĄCA ZA ALGORYTM BFS ######### /// 

                tabVert[0].color = "gray"; // odwiedzenie źródła
                tabVert[0].sourceDistance = 0.0; // dystans do węzła źródłowego
                Vertex[] queue = new Vertex[size]; // utworzenie kolejki wielkości ilości wierzchołków
                y = 0; // informacja ktory wezel obsługuje
                Vertex.AddToQueue(tab, tabVert, queue, size, y);
                y = 1;

                 while(queue[0] != null)
                {
                    if (y < size)
                    {
                        Vertex.AddToQueue(tab, tabVert, queue, size, y); // tworzy kolejke, oraz obsługuje czarny wezel 
                        Vertex.RepairIndexOfQueue(queue, size); // naprawia indeks kolejki, gdy jakis wezel juz zostanie obsluzony
                    }
                    for (int i = 0; i < size; i++)
                    {
                        Console.WriteLine(tabVert[i].color);
                    }
                    y++;
                }
            }
        }
    }

}
