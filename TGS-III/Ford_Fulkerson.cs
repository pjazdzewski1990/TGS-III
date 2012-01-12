using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGS_III
{
    class Ford_Fulkerson : IGFlow
    {
        //zbiera dane do późniejszego raportowania
        //każdy wpis kończy sie "\n"
        protected StringBuilder raport_str = new StringBuilder();

        //macierz identyczna jak w przypadku matrix z Graph
        protected Int16?[][] matrix = new Int16?[0][];

        //liczba wierzcholkow - wnioskowana z macierz po jej zaladowaniu
        protected int edges_num = -1;

        //flow val to sumaryczny przepływ z wszystkich ścieżek
        protected int flow_val = 0;

        protected bool debug = false;

        /// <summary>
        /// Metoda szukająca maksymalny przepływ.
        /// </summary>
        /// <param name="start">Punkt początkowy przepływu</param>
        /// <param name="stop">Punkt końcowy przepływu</param>
        /// <param name="_matrix">Graf na ktorym operujemy przedstawiony w postaci macierzy incydencji</param>
        virtual public void Flow(int start, int stop, Int16?[][] _matrix)
        {
            setFlowMatrix(_matrix);

            flow_val = 0;

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
                flow_val += min;
                minimize(path, min);
            }

            raport_str.Append("Maksymalny przepływ to " + flow_val + "\n");
        }

        /// <summary>
        /// Ustawia wartość wewnętrznej - w sensie skopiuj wartości
        /// chcemy operować na kopii
        /// </summary>
        /// <param name="_matrix">Nowa macierz przepływu</param>
        protected void setFlowMatrix(Int16?[][] _matrix)
        {
            matrix = _matrix;

            edges_num = findEdgesNum(matrix);
        }

        /// <summary>
        /// Określa maksymalną ilość wierzchołków w macierzy matrix
        /// </summary>
        /// <param name="matrix">Macierz z danymi nt. przejść w grafie</param>
        /// <returns>Maksymalna liczba wierzchołków</returns>
        protected int findEdgesNum(short?[][] matrix)
        {
            int num = -1;
            for(int i= 0; i< matrix.Length; i++){
                if (num < matrix[i].Length) {
                    num = matrix[i].Length;
                }
            }
            return num;
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
        /// <param name="min">O ile zmniejszyć wartość przejścia</param>
        protected void minimize(List<int> path, int min)
        {
            for (int i = 0; i < path.Count - 1; i++) {
                transition(path[i], path[i + 1], (short)min);
            }
        }

        /// <summary>
        /// Znajduje minimalny przeływ na podanej ścieżce i zwraca go
        /// </summary>
        /// <param name="path">Ścieżka na ktorej szukamy</param>
        /// <returns>Minimalna wartość przepływu na zadanej ścieżce</returns>
        protected int findMinFlow(List<int> path)
        {
            int? min = null;
            int f = -1;
            for (int i = 0; i < path.Count - 1; i++)
            {
                f = transition(path[i],path[i+1]);
                if ((min == null) || (min > f && f >= 0 )) {
                    min = f;
                }
            }

            raport_str.Append("Przepływ na ścieżce wynosi " + min + "\n");
            if (debug)
            {
                Console.WriteLine("Przepływ na ścieżce wynosi " + min + "\n");
            }

            if (min == null)
            {
                return 0;
            }
            else
            {
                return (int)min;
            }
        }

        /// <summary>
        /// Znajduje wartość przepływu pomiędzy start i end o ile jest większy niż 0
        /// jeśli jest wiele takich przejść to znajduje pierwsze
        /// zmniejsza ten przepływ o val
        /// </summary>
        /// <param name="start">Wierzchołek startowy</param>
        /// <param name="end">wierzchołek końcowy</param>
        /// <param name="val">wierzchołek końcowy</param>
        /// <returns>Wartość przejścia pomiędzy start i end po zmniejszeniu</returns>
        protected int transition(int start, int end, short val)
        {
            short?[] arr;

            for (int i = 0; i < matrix.Length; i++)
            {
                arr = matrix[i];
                try
                {
                    if (arr[end] == -1 && arr[start] > 0)
                    {
                        if (arr[start] != null)
                        {
                            if (arr[start] - val < 0)
                            {
                                arr[start] = 0;
                            }
                            else
                            {
                                arr[start] -= val;
                            }
                        }
                        return (int)arr[start];
                    }
                }
                catch (Exception e)
                {

                }
            }
            return 0;
            //throw new ArgumentException("Transition: nie ma ścieżki pomiędzy " + start + " a " + end);
        }

        protected int transition(int start, int end)
        {
            return transition(start, end, 0);
        }

        /// <summary>
        /// Znajduje ścieżkę po której można przejść z start do stop
        /// warunkiem "przejścia" jest przepływ >0 na każdej pokonywanej krawędzi
        /// </summary>
        /// <param name="start">Wierzchołek początkowy</param>
        /// <param name="stop">Wierzchołek końcowy</param>
        /// <returns>Lista int'ów jako kolejnych wierzchołków od przejścia</returns>
        protected List<int> findPath(int start, int stop)
        {
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
                    if(element <= edges_num){
                        element++;
                    }else{
                        //cofnij sie o krok na ścieżce
                        element = path[last];
                        path.RemoveAt(last);
                    }
                }
                else {
                    if (!path.Contains(found))
                    {
                        path.Add(found);
                        element = -1;
                    }
                }

                last = path.Count - 1;
                if (last < 0) {
                    break;
                }

                if (path[last] == stop)
                {
                    raport_str.Append(representPath(path));
                    raport_str.Append("\n");
                    if (debug)
                    {
                        Console.WriteLine(representPath(path));
                    }
                    return path;
                }
            }

            return path;
        }

        /// <summary>
        /// Prezentuje ścieżkę w grafie w postaci czytelnej dla człowieka
        /// </summary>
        /// <param name="path">Ścieżka którą chcemy zaprezentować</param>
        /// <returns>Czytelna dla człowieka reprezentacja</returns>
        protected String representPath(List<int> path)
        {
            StringBuilder sb = new StringBuilder("Ścieżka: ");

            foreach(int edge in path){
                sb.Append(edge);
                sb.Append("->");
            }
            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }

        /// <summary>
        /// Znajduje wierzchołek połączony dodatnią droga z p, większy niż element
        /// lub zwróć -1 gdy taki nie istnieje
        /// </summary>
        /// <param name="p">Numer wierzchołka w którym jesteśmy</param>
        /// <param name="element">Numer wierzchołka ma być większy niż</param>
        /// <returns>Zwraca numer wierzchołka lub -1 jeśli go nie ma</returns>
        protected int findWay(int p, int element)
        {
            for (int i = 0; i < matrix.Length; i++) {
                try
                {
                    if (matrix[i][p] != null && matrix[i][p] > 0)
                    {
                        for (int index = element + 1; index<matrix[i].Length; index++) {
                            try
                            {
                                if (matrix[i][index] == -1)
                                {
                                    return index;
                                }
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
                catch (Exception e) {  
                }
            }

            return -1;
        }

        /// <summary>
        /// Zwraca raport z procesu szukania największego przepływu 
        /// </summary>
        /// <returns>Raport z przepływu pod postacią obiektu kalsy String(zawiera wiele linii)</returns>
        public String report()
        {
            return raport_str.ToString();
        }

        /// <summary>
        /// Zwróć maksymalny przepływ jako liczbę całkowitą
        /// </summary>
        /// <returns>Liczba całkowita reprezentująca maksymalny przepływ uzyskany w wyniku poprzedniego sprawdzania</returns>
        public int maxFlow() {
            return flow_val;
        }

        /// <summary>
        /// Ustala czy użytkownik życzy sobie otrzymywać komunikaty na ekran
        /// </summary>
        /// <param name="_debug">Wartość logiczna: czy drukować komunikaty?</param>
        public void debugMode(bool _debug) {
            this.debug = _debug;
        }
    }
}
