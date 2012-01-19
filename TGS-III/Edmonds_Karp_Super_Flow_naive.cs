using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGS_III
{
    /// <summary>
    /// Algorytm Edmondsa i Karpa dla sieci o wielu źródłach i wielu ujściach
    /// Podejście naiwne
    /// </summary>
    [Serializable]
    class Edmonds_Karp_Super_Flow_naive : Edmonds_Karp, IGSuperFlow
    {
        public void Flow(List<int> start, List<int> stop, Int16?[][] _matrix) {
            setFlowMatrix(_matrix);

            flow_val = 0;

            //wyjątkowy przypadek  
            if (start.FindAll(x=> stop.Contains(x)).Count > 0)
            {
                raport_str.Append("Start == Stop: przepływ dowolny\n");
                return;
            }

            //ścieżkę reprezentujemy jako sekwencyjną listę liczb
            //które określają wierzchołki przez które trzeba przejść
            List<int> path = null;

            //wszystkie trasy możliwe w grafie
            //w wyniku działania algorytmu tras może co najwyżej ubyć 
            // dlatego opłaca się znaleźć je tylko raz i pytem tylko pozbywać się tych już nieużytecznych
            List<List<int>> paths = findMultiplepaths(start, stop);

            //jeśli jest ścieżka o długości większej niż 0 
            while ((path = analyze(ref paths)).Count > 0)
            {
                int min = findMinFlow(path);
                flow_val += min;
                minimize(path, min);
            }

            raport_str.Append("Maksymalny przepływ to wg. Edmondsa i Karpa(naive super flow) to " + flow_val + "\n");
        }

        protected List<List<int>> findMultiplepaths(List<int> start, List<int> stop)
        {
            List<List<int>> paths = new List<List<int>>();
            foreach (int start_n in start)
            {
                foreach (int stop_n in stop)
                {
                    List<List<int>> p = findPaths(start_n, stop_n);
                    foreach (List<int> _p in p)
                    {
                        paths.Add(_p);
                    }
                }
            }
            return paths;
        }
    }
}
