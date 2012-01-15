using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGS_III
{
    interface IGSuperFlow : IGFlow
    {
        void Flow(List<int> start, List<int> stop, Int16?[][] _matrix);
    }
}
