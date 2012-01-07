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
            //1. Przygotuj graf
            Graph ggg = new Graph();
            ggg.addEdge(0, 1, 3); //od jedynki do dwójki długości 3
            ggg.addEdge(0, 2, 3); //od jedynki do trójki długości 3
            ggg.addEdge(0, 3, 1); //od jedynki do czwórki długości 1
            
            ggg.addEdge(1, 2, 2); //od dwójki do trójki długości 2
            ggg.addEdge(1, 3, 2); //od dwójki do czwórki długości 2

            ggg.addEdge(2, 4, 4); //od trójki do piątki długości 4

            ggg.addEdge(3, 4, 4); //od czwórki do piątki długości 4

            //2. Przygotuj algorytm i podaj go do obiektu klasy Graph
            ggg.flowAlg(new Ford_Fulkersons());

            //3. Uruchom przekazany algorytm z odpowiednimi parametrami
            ggg.findFlow(0, 4);

            Console.ReadKey();
        }
    }
}
