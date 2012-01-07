using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGS_III
{
    class Ford_Fulkersons : IGFlow
    {
        //zbiera dane do późniejszego raportowania
        //każdy wpis kończy sie "\n"
        StringBuilder raport_str = new StringBuilder();

        //macierz identyczna jak w przypadku matrix z Graph
        Int16?[][] matrix = new Int16?[0][];

        public void Flow(int start, int stop, Int16?[][] _matrix)
        {
            setFlowMatrix(_matrix);

            //wyjatkowy przypadek  
            if (start == stop) {
                raport_str.Append("Start == Stop: przepływ dowolny\n");
                return;
            }

            //ścieżkę reprezentujemy jako sekwencyjną listę liczb
            //które określają wierzchołki przez które trzeba przejść
            List<int> path = null;
            
            //jeśli jest ścieżka o długości większej niż 0 
            while((path = findPath(start,stop)).Count > 0){
                int min = findMinFlow(path);
                minimize(path, min);
            }

        }

        /// <summary>
        /// Ustawia wartość wewnętrznej - w sensie skopiuj wartości
        /// chcemy operować na kopii
        /// </summary>
        /// <param name="_matrix">Nowa macierz przepływu</param>
        private void setFlowMatrix(Int16?[][] _matrix) {
            matrix = _matrix;
        }

        /// <summary>
        /// Zwraca wewnętrzną macierz przepływu
        /// domyślnie chodzi o macierz <PO> wykonaniu algorytmu 
        /// </summary>
        /// <returns>Ww. macierz, a raczej to co z niej pozostanie po wyzerowaniu niektórych krawędzi</returns>
        public Int16?[][] getFlowMatrix()
        {
            if (matrix != null)
            {
                return matrix;
            }
            else {
                throw new InvalidOperationException("Nie podano macierzy");
            }
        }

        /// <summary>
        /// Zmniejsza wartość przepływu krawędzi na ścieżce o wartość min
        /// </summary>
        /// <param name="path">Ścieżka zawierająca krawędzie których przepływy trzeba zmniejszyć</param>
        /// <param name="min"></param>
        private void minimize(List<int> path, int min)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Znajduje minimalny przeływ na podanej ścieżce i zwraca go
        /// </summary>
        /// <param name="path">Ścieżka na ktorej szukamy</param>
        /// <returns></returns>
        private int findMinFlow(List<int> path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Znajduje ścieżkę po której można przejść z start do stop
        /// warunkiem "przejścia" jest przepływ >0 na każdej pokonywanej krawędzi
        /// </summary>
        /// <param name="start">Wierzchołek początkowy</param>
        /// <param name="stop">Wierzchołek końcowy</param>
        /// <returns></returns>
        private List<int> findPath(int start, int stop)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Zwraca raport z procesu szukania największego przepływu 
        /// </summary>
        /// <returns>Raport z przepływu pod postacią obiektu kalsy String(zawiera wiele linii)</returns>
        public String raport()
        {
            return raport_str.ToString();
        }
    }
}
