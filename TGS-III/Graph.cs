using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGS_III
{
    class Graph
    {
        //macierz incydencji
        //matrix[1][]   oznacza dostep do drugiej krawedzi
        //matrix[2][1]  oznacza sprawdzenie czy trzecia krawędź ma połączenie z 2 wierzcholkem
        //
        //z kolei kolumny wskazują skąd dokąd mamy dostęp, tj.
        // liczby większe niż 0 w kolumnie o indeksie x mówią dokąd możemy się dostać z x
        Int16?[][] matrix = new Int16?[0][];

        //ilość krawędzi - może być większa niż rzeczywista liczba
        int num_edges = 0;
        //ilość krawędzi jakie może pomieścić macierz
        int max_edges = 0;
        //ilość wierzchołków - dla oszczędności dokładnie odpowiada liczbie wierzchołków 
        int num_vertices = 0;

        //algorytm szukania przepływu
        IGFlow alg;

        /// <summary>
        /// Dodaj krawędź do grafu
        /// </summary>
        /// <param name="from">startowy wierzchołek</param>
        /// <param name="to">końcowy wierzchołek</param>
        public void addEdge(int from, int to, Int16? flow) {
            
            //najpierw rozszerz "w szerz"
            if (from >= num_vertices || to >= num_vertices)
            {
                num_vertices = Math.Max(from, to)+1;
                width();
            }

            if (num_edges >= max_edges)
            {
                height();
            }

            matrix[num_edges] = new Int16?[num_vertices];
            matrix[num_edges][from] = flow;
            matrix[num_edges][to] = -1;

            ++num_edges;
        }

        /// <summary>
        /// Rozszerza macierz incydencji matrix "w szerz"
        /// tak by uwzględnić nowe wierzchołki
        /// </summary>
        private void width()
        {
            //do nothin'
        }

        /// <summary>
        /// Rozszerza macierz incydencji matrix
        /// tak by można byłoy dodać nową krawędź
        /// </summary>
        private void height()
        {
            max_edges += 10;
            Array.Resize<Int16?[]>(ref matrix, max_edges);
        }

        /// <summary>
        /// Analizuje przepływ korzystając z algorytmu this.alg
        /// implementującego metodę flow interfejsu IGFlow
        /// </summary>
        /// <param name="start">Wierzchołek początkowy</param>
        /// <param name="stop">Wierzchołek końcowy</param>
        public void findFlow(int start, int stop) {
            alg.Flow(start, stop, matrix);
        }

        /// <summary>
        /// Ustawia algorytm szuykania przepływu
        /// </summary>
        /// <param name="_alg">Algorytm szukania przepływu implementujący interfejs IGFlow</param>
        public void flowAlg(IGFlow _alg)
        {
            alg = _alg;
        }

    }
}
