using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewaDecyzyjne
{
    class Gałąź
    {
        public int wartoscGałęzi;//może to być decyzja bądź atrybut
        public List<Węzeł> węzły = new List<Węzeł>(); //lista węzłów wskazujące na inne gałęzie(korzenie) czy liście, jeżeli pusta to jest to decyzja, czyli liść.
       

        public Gałąź() {}


        public Gałąź(int atrybut) 
        {
            wartoscGałęzi = atrybut;
        }
    }
}
