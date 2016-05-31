using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewaDecyzyjne
{
    class Węzeł
    {
        public int cecha;
        public Gałąź gałąźNad;//wskazuje na konkretną gałąź
        public Gałąź gałąźPod;
        

        public Węzeł(int cecha, Gałąź gałąźNad)
        {
            this.cecha = cecha;
            this.gałąźNad = gałąźNad;
        }
    }
}
