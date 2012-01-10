﻿using System;
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
            ggg.flowAlg(new Ford_Fulkerson());

            //3. Uruchom przekazany algorytm z odpowiednimi parametrami
            ggg.findFlow(0, 4);
            ggg.report();

            Console.ReadKey();

            //1. Przygotuj graf
            Graph ggg2 = new Graph();
            ggg2.addEdge(0, 1, 2); 
            ggg2.addEdge(1, 3, 2);

            ggg2.addEdge(0, 2, 1);
            ggg2.addEdge(2, 3, 1);

            ggg2.addEdge(0, 3, 3);

            //2. Przygotuj algorytm i podaj go do obiektu klasy Graph
            ggg2.flowAlg(new Ford_Fulkerson());

            //3. Uruchom przekazany algorytm z odpowiednimi parametrami
            ggg2.findFlow(0, 3);
            ggg2.report();

            Console.ReadKey();

            //1. Przygotuj graf
            Graph ggg3 = new Graph();
            ggg3.addEdge(0, 1, 3); //od jedynki do dwójki długości 3
            ggg3.addEdge(0, 2, 3); //od jedynki do trójki długości 3
            ggg3.addEdge(0, 3, 1); //od jedynki do czwórki długości 1
            
            ggg3.addEdge(1, 2, 2); //od dwójki do trójki długości 2
            ggg3.addEdge(1, 3, 2); //od dwójki do czwórki długości 2

            ggg3.addEdge(2, 4, 4); //od trójki do piątki długości 4

            ggg3.addEdge(3, 4, 4); //od czwórki do piątki długości 4

            //2. Przygotuj algorytm i podaj go do obiektu klasy Graph
            ggg3.flowAlg(new Ford_Fulkerson_Big_First());

            //3. Uruchom przekazany algorytm z odpowiednimi parametrami
            ggg3.findFlow(0, 4);
            ggg3.report();
        }
    }
}
