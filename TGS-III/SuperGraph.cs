using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGS_III
{
    class SuperGraph : Graph
    {
        //algorytm szukania przepływu
        protected IGSuperFlow super_alg;

        public void flowAlg(IGSuperFlow _alg)
        {
            super_alg = _alg;
        }

        public void findFlow(List<int> start, List<int> stop)
        {
            Int16?[][] new_matrix = copyMatrix(matrix);
            super_alg.Flow(start, stop, new_matrix);
        }

        /// <summary>
        /// Wypisuje raport z poszukiwania przepływu na konsolę
        /// </summary>
        public void super_report()
        {
            Console.WriteLine(super_alg.report());
        }
    }
}
