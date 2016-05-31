using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewaDecyzyjne
{
    class Entropia
    {
        
        public static decimal PoliczEntropie(Dictionary<int, int> czestosc, int count)
         {
            //PRZYPADEK NR 1
            //wszystkie są albo pozytywne albo negatywne (istnieje jedna)
            if (czestosc.Count == 1) return 0;


            //PRZYPADEK NR 2     
            //wszystkie są równe
            bool flaga = true; //zakładam, że wszystkie są równe
            foreach (var c in czestosc.Values)
            {
                if (c != czestosc.Values.Last<int>()) {//wystarczy, że jeden nie jest równy 
                    flaga = false;
                    break;
                }
            }
            if (flaga) return 1; //wszystkie są równe

            //PRZYPADEK NR 3 
            //Liczę Entropie ze wzoru
            List<double> listap = new List<double>();
            foreach (var k  in czestosc)
            {
                double p = (double) k.Value / count;
                p = Math.Pow(p, p);
                listap.Add(p);
            }

            double iloczyn = listap[0];//weź pierwszy element

            for(int i = 1; i < listap.Count; i++)
            {
                iloczyn *= listap[i];//wymnóż
            }
 
            double wynik = -1* Math.Log(iloczyn, 2); 

            return decimal.Round((decimal)wynik, 3);
        }

        public static decimal PoliczZyskInformacyjny(Dictionary<int, decimal> entropie, decimal entropiaSystemu, Dictionary<int, int> iloscRozwazanych, int ilosc)
        {
            double różnica = 0;
            foreach(var e in entropie)
            {
                double ułamek =(double)iloscRozwazanych[e.Key] / ilosc;
                różnica -= (double)e.Value * ułamek;
            }
            decimal wynik = entropiaSystemu + (decimal)różnica;
            return decimal.Round(wynik,3);
        }
    }
}
