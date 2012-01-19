using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TGS_III
{
    [Serializable]
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

        public void Serialize(String path)
        {
            Stream stream = File.Open(path, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, this);
            stream.Close();
        }

        public static SuperGraph Deserialize(String path)
        {
            SuperGraph objectToSerialize;
            Stream stream = File.Open(path, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (SuperGraph)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }
    }
}
