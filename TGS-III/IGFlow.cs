using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGS_III
{
    interface IGFlow
    {
        public void Flow(int start, int stop, Int16?[][] _matrix);

        public String raport();
    }
}
