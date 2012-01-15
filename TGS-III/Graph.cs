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
        protected Int16?[][] matrix = new Int16?[0][];

        //ilość krawędzi - może być większa niż rzeczywista liczba
        protected int num_edges = 0;
        //ilość krawędzi jakie może pomieścić macierz
        protected int max_edges = 0;
        //ilość wierzchołków - dla oszczędności dokładnie odpowiada liczbie wierzchołków 
        protected int num_vertices = 0;

        //algorytm szukania przepływu
        protected IGFlow alg;

        /// <summary>
        /// Dodaj krawędź do grafu
        /// </summary>
        /// <param name="from">Startowy wierzchołek</param>
        /// <param name="to">Końcowy wierzchołek</param>
        /// <param name="flow">Przepływ pomiędzy wierzchołkami</param>
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
        protected void width()
        {
            //do nothin'
        }

        /// <summary>
        /// Rozszerza macierz incydencji matrix
        /// tak by można byłoy dodać nową krawędź
        /// </summary>
        protected void height()
        {
            max_edges += 1;
            Array.Resize<Int16?[]>(ref matrix, max_edges);
        }

        /// <summary>
        /// Analizuje przepływ korzystając z algorytmu this.alg
        /// implementującego metodę flow interfejsu IGFlow
        /// </summary>
        /// <param name="start">Wierzchołek początkowy</param>
        /// <param name="stop">Wierzchołek końcowy</param>
        public void findFlow(int start, int stop) {
            Int16?[][] new_matrix = copyMatrix(matrix);
            alg.Flow(start, stop, new_matrix);
        }

        /// <summary>
        /// Tworzy głęboką kopię macierzy matrix
        /// </summary>
        /// <param name="matrix">Macierz kwadratowa do skopiowania</param>
        /// <returns>Deep copy argumentu matrix</returns>
        protected short?[][] copyMatrix(short?[][] matrix)
        {
            short?[][] newOne = new short?[matrix.Length][];
            
            for(int i = 0; i<matrix.Length; i++){
                newOne[i] = new short?[matrix[i].Length];
                for(int j=0; j<newOne[i].Length; j++){
                    newOne[i][j] = matrix[i][j];
                }
            }

            return newOne;
        }

        /// <summary>
        /// Ustawia algorytm szuykania przepływu
        /// </summary>
        /// <param name="_alg">Algorytm szukania przepływu implementujący interfejs IGFlow</param>
        public void flowAlg(IGFlow _alg)
        {
            alg = _alg;
        }

        /// <summary>
        /// Wypisuje raport z poszukiwania przepływu na konsolę
        /// </summary>
        public void report()
        {
            Console.WriteLine(alg.report());
        }

        /// <summary>
        /// Ustala czy użytkownik życzy sobie otrzymywać komunikaty na ekran
        /// </summary>
        /// <param name="_debug">Wartość logiczna: czy drukować komunikaty?</param>
        public void debugMode(bool _debug)
        {
            if (alg != null)
            {
                alg.debugMode(_debug);
            }
            else {
                throw new InvalidOperationException("Brak zdefiniowanego algorytmu. Nie można aktywować Debug Mode");
            }
        }
    }
}
