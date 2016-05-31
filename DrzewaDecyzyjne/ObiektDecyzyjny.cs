using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewaDecyzyjne
{
   public  class ObiektDecyzyjny
    {
        public List<int> atrybuty= new List<int>();
        public int decyzja;

        public ObiektDecyzyjny(string wiersz)
        {
            string[] miejsceParkingowe = wiersz.Split(' ');
            for (int i = 0; i < miejsceParkingowe.Length-1; i++)
            {
                atrybuty.Add(int.Parse(miejsceParkingowe[i]));
            }
            decyzja = int.Parse(miejsceParkingowe.Last<string>());
            
        }

       

        internal void PoliczCzestosc(Dictionary<int, int> czestoscDecyzja)
        {
            if (!czestoscDecyzja.Keys.Contains(decyzja)) czestoscDecyzja.Add(decyzja, 0);
            czestoscDecyzja[decyzja]++;

        }

        internal void PoliczCzestosc(Dictionary<int, Dictionary<int, Dictionary<int, int>>> czestosc, Dictionary<int, int>.KeyCollection keys)
        {
           foreach(int d in keys)
            {
                if(d == decyzja)
                {
                    for(int i = 0; i < atrybuty.Count; i++)
                    {
                        if (!czestosc[d][i].Keys.Contains(atrybuty[i])) czestosc[d][i].Add(atrybuty[i], 0);
                        czestosc[d][i][atrybuty[i]]++;
                    }
                }
            }
        }

        internal void PoliczCzestosc(Dictionary<int, int> czestoscWewnetrzna, int d, int i)
        {
            if (d == decyzja)
            {
                if (!czestoscWewnetrzna.Keys.Contains(atrybuty[i])) czestoscWewnetrzna.Add(atrybuty[i], 0);
                czestoscWewnetrzna[atrybuty[i]]++;
            }
        }

        internal void PoliczCzestosc(List<Dictionary<int, int>> czestoscAtrybutow)
        {
            throw new NotImplementedException();
        }

        internal void PoliczLiczbeRozwazanychObiektow(Dictionary<int, int> liczbaRozwazanychObiektow, int i)
        {
            if (!liczbaRozwazanychObiektow.Keys.Contains(atrybuty[i])) liczbaRozwazanychObiektow.Add(atrybuty[i], 0);
            liczbaRozwazanychObiektow[atrybuty[i]]++;
        }
    }
}
