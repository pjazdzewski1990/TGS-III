using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGS_III
{
    class Program
    {
        static void Main(string[] args)
        {
            //Przykład 1
            //1. Przygotuj graf
            Graph ggg = new Graph();

            ggg.addEdge(0, 1, 3); //od jedynki do dwójki długości 3
            ggg.addEdge(0, 2, 3); //od jedynki do trójki długości 3
            ggg.addEdge(0, 3, 1); //od jedynki do czwórki długości 1
            
            ggg.addEdge(1, 2, 2); //od dwójki do trójki długości 2
            ggg.addEdge(1, 3, 2); //od dwójki do czwórki długości 2

            ggg.addEdge(2, 4, 4); //od trójki do piątki długości 4

            ggg.addEdge(3, 4, 4); //od czwórki do piątki długości 4
            
            /*
            //2. Przygotuj algorytm i podaj go do obiektu klasy Graph
            ggg.flowAlg(new Ford_Fulkerson());
            //3. Uruchom przekazany algorytm z odpowiednimi parametrami
            ggg.debugMode(true);
            ggg.findFlow(0, 4);
            ggg.report();
            //Console.ReadKey();
            */

            /*
            //alternatywny mechanizm szukania ścieżki
            //2. Przygotuj algorytm i podaj go do obiektu klasy Graph
            ggg.flowAlg(new Ford_Fulkerson_Big_First());
            //3. Uruchom przekazany algorytm z odpowiednimi parametrami
            ggg.debugMode(true);
            ggg.findFlow(0, 4);
            ggg.report();
            //Console.ReadKey();
            */

            /*
            //alternatywny mechanizm szukania ścieżki
            //2. Przygotuj algorytm i podaj go do obiektu klasy Graph
            ggg.flowAlg(new Edmonds_Karp());
            //3. Uruchom przekazany algorytm z odpowiednimi parametrami
            ggg.debugMode(true);
            //ggg.findFlow(0, 4);
            //ggg.report();
            //Console.ReadKey();
            */
 
            /*
            //alternatywny mechanizm szukania ścieżki
            //2. Przygotuj algorytm i podaj go do obiektu klasy Graph
            ggg.flowAlg(new Ford_Fulkerson_Big_First());
            //3. Uruchom przekazany algorytm z odpowiednimi parametrami
            ggg.debugMode(true);
            ggg.findFlow(0, 4);
            ggg.report();
            Console.ReadKey();
            */

            int choice = -1;
            IGFlow alg = new Ford_Fulkerson();
            while (choice != 4) {
                Console.WriteLine("Wybierz algorytm szukania ścieżki ");
                Console.WriteLine("1.FF ");
                Console.WriteLine("2.FF Big First ");
                Console.WriteLine("3.EK ");
                Console.WriteLine("4.Wyjdź ");

                choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 4) { break; }

                switch(choice){
                    case 1:
                        alg = new Ford_Fulkerson();
                        break;
                    case 2:
                        alg = new Ford_Fulkerson_Big_First();
                        break;
                    case 3:
                        alg = new Edmonds_Karp();
                        break;
                }

                ggg.flowAlg(alg);

                Console.WriteLine("Podaj punkt startowy");
                int start = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj punkt końcowy");
                int stop = Convert.ToInt32(Console.ReadLine());

                ggg.findFlow(start, stop);
                ggg.report();
            }

            //Przykład 3. super przepływ
            SuperGraph sg = new SuperGraph();

            sg.addEdge(0, 1, 1);
            sg.addEdge(0, 2, 3);

            sg.addEdge(1, 2, 4);

            sg.addEdge(2, 3, 2);
            sg.addEdge(2, 4, 3);

            sg.addEdge(3, 5, 2);

            sg.addEdge(4, 5, 1);
            sg.addEdge(4, 6, 2);

            IGSuperFlow super_alg = new Edmonds_Karp_Super_Flow_naive();
            while (choice != 3)
            {
                Console.WriteLine("Wybierz algorytm szukania ścieżki ");
                Console.WriteLine("1.EG naive");
                Console.WriteLine("2.EG");
                Console.WriteLine("3.Wyjdź");

                choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 3) { break; }

                switch (choice)
                {
                    case 1:
                        super_alg = new Edmonds_Karp_Super_Flow_naive();
                        break;
                    case 2:
                        super_alg = new Edmonds_Karp_Super_Flow();
                        break;
                }

                sg.flowAlg(super_alg);

                Console.WriteLine("Podaj punkty startowe");
                List<int> start = fillList();
                Console.WriteLine("Podaj punkty końcowe");
                List<int> stop = fillList(); ;

                sg.findFlow(start, stop);
                sg.super_report();
            }

            //zapisz graf dwa do pliku
            String path = @"C:\TGIS.bin";
            sg.Serialize(@path);
            SuperGraph from = SuperGraph.Deserialize(@path);
            Console.WriteLine("");

            /*
            //Przykład 2 
            //1. Przygotuj graf
            Graph ggg3 = new Graph();

            ggg3.addEdge(0, 1, 2); 
            ggg3.addEdge(1, 3, 2);

            ggg3.addEdge(0, 2, 1);
            ggg3.addEdge(2, 3, 1);

            ggg3.addEdge(0, 3, 3);

            //2. Przygotuj algorytm i podaj go do obiektu klasy Graph
            ggg3.flowAlg(new Ford_Fulkerson());

            //3. Uruchom przekazany algorytm z odpowiednimi parametrami
            ggg3.debugMode(true);
            ggg3.findFlow(0, 3);
            ggg3.report();
            Console.ReadKey();
            */
        }

        private static List<int> fillList()
        {
            string[] str = Console.ReadLine().Split(' ');
            List<int> l = new List<int>();

            foreach (string c in str) {
                l.Add(int.Parse(c));
            }

            return l;
        }
    }
}
