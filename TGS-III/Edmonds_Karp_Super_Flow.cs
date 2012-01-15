using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGS_III
{
    /// <summary>
    /// Algorytm Edmondsa i Karpa dla sieci o wielu źródłach i wielu ujściach
    /// Wykorzystuje technikę superźródeł i superujść
    /// </summary>
    class Edmonds_Karp_Super_Flow : Edmonds_Karp, IGSuperFlow
    {
        public void Flow(List<int> starting_points, List<int> stoping_points, short?[][] _matrix)
        {
            setFlowMatrix(_matrix);

            //stwórz superźródło
            int start = createSuperStart(starting_points);
            //stwórz superujście
            int stop = createSuperStop(stoping_points);

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

            //wszystkie trasy możliwe w grafie
            //w wyniku działania algorytmu tras może co najwyżej ubyć 
            // dlatego opłaca się znaleźć je tylko raz i pytem tylko pozbywać się tych już nieużytecznych
            List<List<int>> paths = findPaths(start, stop);

            //jeśli jest ścieżka o długości większej niż 0 
            while ((path = analyze(ref paths)).Count > 0)
            {
                int min = findMinFlow(path);
                flow_val += min;
                minimize(path, min);
            }

            raport_str.Append("Maksymalny przepływ wg. Forda i Fulkersona(super flow) to " + flow_val + "\n");
        }

        private int createSuperStop(List<int> stoping_points)
        {
            //przygotuj macierz na nowe przejście i wirzchołek, zwróć index nowego wierzchołka
            int index = prepareMatrix();
            foreach (int stop in stoping_points)
            {
                //ile czynnika maksymalnie moze wpływać do danego punktu startowego
                int flow_in = input(stop);
                //nowy wierzchołek jest połączony z punktem startowym x krawedzia\
                // o przeplywie równym sumie wypływu z danego punktu 
                if (debug) {
                    Console.WriteLine("Do wierzcholka " + stop + " wplywa " + flow_in);
                }
                add_transition(stop, index, (short?)flow_in);
            }

            //zwroc index nowego wierzchołka
            return index;
        }

        private int input(int stop)
        {
            int? sum = 0;
            foreach (short?[] trans in matrix)
            {
                //znajdź wszystkie przejścia, które kończą się w stop
                if (stop < trans.Length && trans[stop] != null && trans[stop] == -1)
                {
                    //znajdź wartość tego przejścia
                    foreach (int? i in trans) {
                        if (i > 0) {
                            sum += i;
                            break;
                        }
                    }
                }
            }
            return (int)sum;
        }

        private int createSuperStart(List<int> starting_points)
        {
            //przygotuj macierz na nowe przejście i wirzchołek, zwróć index nowego wierzchołka
            int index = prepareMatrix();
            foreach (int start in starting_points) {
                //ile czynnika wyplywać moze z danego punktu startowego
                int flow_out = output(start);
                //nowy wierzchołek jest połączony z punktem startowym x krawedzia\
                // o przeplywie równym sumie wypływu z danego punktu 
                if (debug)
                {
                    Console.WriteLine("Do wierzcholka " + start + " wplywa " + flow_out);
                }
                add_transition(index, start, (short?)flow_out);
            }

            //zwroc index nowego wierzchołka
            return index;
        }

        private void add_transition(int from, int to, short? flow)
        {
            ++edges_num;

            //najpierw rozszerz "w szerz"
            int num_vertices = Math.Max(from, to) + 1;

            //rozszerz macierz w wzdłuż by móc zmieścić dodatkowe przejście
            height();

            matrix[edges_num] = new Int16?[num_vertices];
            matrix[edges_num][from] = flow;
            matrix[edges_num][to] = -1;
        }

        /// <summary>
        /// Rozszerza macierz incydencji matrix
        /// tak by można byłoy dodać nową krawędź
        /// </summary>
        private void height()
        {
            Array.Resize<Int16?[]>(ref matrix, edges_num + 1);
        }

        private int prepareMatrix()
        {
            //wystarczy zwrócić potencjalny index nowego wierzchołka
            return matrix[matrix.Length - 1].Length;
        }

        private int output(int start)
        {
            int? sum = 0;
            foreach (short?[] trans in matrix) {
                if (trans[start] != null && trans[start] > 0) {
                    sum = sum + trans[start];
                }
            }
            return (int)sum;
        }
    }
}
