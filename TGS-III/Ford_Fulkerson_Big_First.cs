using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGS_III
{
    /// <summary>
    /// Zmodyfikowana wersja algorytmu Forda-Fulkersona.
    /// Ta wersja w pierwszej kolejności wybiera ścieżki dające największy 
    /// przepływ w danym momencie 
    /// </summary>
    class Ford_Fulkerson_Big_First : Ford_Fulkerson
    {

        /// <summary>
        /// Nadpisana wersja metody szukającej maksymalny przepływ.
        /// Ta wersja realizuje model zachłanny:
        /// analizuje wszystkie obecnie dostępne ścieżki i wybiera ta o największym przepływie 
        /// </summary>
        /// <param name="start">Punkt początkowy przepływu</param>
        /// <param name="stop">Punkt końcowy przepływu</param>
        /// <param name="_matrix">Graf na ktorym operujemy przedstawiony w postaci macierzy incydencji</param>
        override public void Flow(int start, int stop, Int16?[][] _matrix) {
            setFlowMatrix(_matrix);

            flow_val = 0;

            //wyjatkowy przypadek  
            if (start == stop)
            {
                raport_str.Append("Start == Stop: przepływ dowolny\n");
                return;
            }

            //ścieżkę reprezentujemy jako sekwencyjną listę liczb
            //które określają wierzchołki przez które trzeba przejść
            List<int> path = null;

            //wszystkie trasy
            List<List<int>> paths = findPaths(start, stop);

            //jeśli jest ścieżka o długości większej niż 0 
            while ((path = analyze(ref paths)).Count > 0)
            {
                int min = findMinFlow(path);
                flow_val += min;
                minimize(path, min);
            }

            raport_str.Append("Maksymalny przepływ wg. BigFirst to " + flow_val + "\n");
        }

        /// <summary>
        /// Analizuje wszystkie ścieżki dostępne dla obecnego grafu przejść
        /// i wybiera tą o największym przepływie.
        /// </summary>
        /// <param name="paths">Wszystkie ścieżki między punktem startowym a końcowym</param>
        /// <returns>Scieżka o największym przepływie przy obecnym stanie grafu</returns>
        protected List<int> analyze(ref List<List<int>> paths)
        {
            int best_flow = -1;
            List<int> best = new List<int>();

            removeUnavailable(ref paths);

            if (debug)
            {
                Console.WriteLine("Analyze: Ilość ścieżek przekazanych " + paths.Count);
            }

            for (int i = 0; i < paths.Count; i++)
            { 
                int min = findMinFlow(paths[i]);
                if (debug)
                {
                    Console.WriteLine("Analyze: Porównuję " + representPath(paths[i]) + ":" + min + " z " + representPath(best) + ":" + best_flow);
                }
                if (min > best_flow) {
                    best_flow = min;
                    best = paths[i];
                }
            }

            return best;
        }

        /// <summary>
        /// Usuwa niedrożne ścieżki z listy ścieżek
        /// </summary>
        /// <param name="paths">Referencja na listę ścieżek</param>
        private void removeUnavailable(ref List<List<int>> paths)
        {
            for (int i = 0; i < paths.Count; i++ )
            {
                if (findMinFlow(paths[i]) <= 0)
                {
                    paths.Remove(paths[i]);
                }
            }
        }

        /// <summary>
        /// Znajduje wszystkie ścieżki z start do stop
        /// </summary>
        /// <param name="start">Poczatek każdej z ścieżek, punkt startowy</param>
        /// <param name="stop">Koniec każdej z ścieżek, punkt końcowy</param>
        /// <returns></returns>
        private List<List<int>> findPaths(int start, int stop)
        {
            //wszystkie ścieżki z "start" do "stop" w obecnym "matrix"
            List<List<int>> paths = new List<List<int>>();

            //lista wierzcholkow przez ktore musimy przejsc by dojsc z start do stop
            //start i stop też są na tej liscie
            List<int> path = new List<int>();
            path.Add(start);

            //obecnie wybrany wierzchołek
            int element = -1;

            int found = -1;

            //ostatni wierzchołek na ścieżce
            int last = -1;

            while (true)
            {

                last = path.Count - 1;
                if (last < 0)
                {
                    break;
                }

                //znajdz wierzcholek polaczony dodatnia droga z ostatnim na sciezce, wiekszy niz element
                //lub zwroc -1 gdy nie ma takiego
                found = findWay(path[last], element);

                //jesli nie ma takiego przejscia to zdejmij wierzcholke z gory i szukaj dalej
                if (found == -1)
                {
                    if (element <= edges_num)
                    {
                        element++;
                    }
                    else
                    {
                        //cofnij sie o krok na ścieżce
                        element = path[last];
                        path.RemoveAt(last);
                    }
                }
                else
                {
                    if (!path.Contains(found))
                    {
                        path.Add(found);
                        element = -1;
                    }
                }

                last = path.Count - 1;
                if (last < 0)
                {
                    break;
                }

                if (path[last] == stop)
                {
                    raport_str.Append(representPath(path));
                    raport_str.Append("\n");
                    if (debug)
                    {
                        Console.WriteLine("FindPaths:Dodaje " + representPath(path));
                    }
                    paths.Add(new List<int>(path));
                    //cofnij sie o krok na ścieżce
                    element = path[last];
                    path.RemoveAt(last);
                }
            }

            //return path;
            paths.Add(path);
            return paths;
        }
    }
}
