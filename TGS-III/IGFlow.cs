using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGS_III
{
    interface IGFlow
    {
        void Flow(int start, int stop, Int16?[][] _matrix);

        String report();

        int maxFlow();

        void debugMode(bool _debug);
    }
}
