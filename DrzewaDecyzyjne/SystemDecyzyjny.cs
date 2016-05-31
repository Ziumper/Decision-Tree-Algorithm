using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewaDecyzyjne
{
    public class SystemDecyzyjny
    {
      
      
        public List<ObiektDecyzyjny> zbiórObiektów = new List<ObiektDecyzyjny>();
        public Dictionary<int, int> czestoscDecyzja = new Dictionary<int, int>();
        public Dictionary<int, Dictionary<int, Dictionary<int, int>>> czestoscAtrybutow = new Dictionary<int, Dictionary<int, Dictionary<int, int>>>();
        public Dictionary<int, Dictionary<int, int>> iloscRozwazanych = new Dictionary<int, Dictionary<int, int>>();
        public decimal entropiaSystem;
        public List<int> iloscAtrybutow = new List<int>();
        public Dictionary<int, Dictionary<int, decimal>> entropieAtrybutów = new Dictionary<int, Dictionary<int, decimal>>();
        public List<KeyValuePair<int, decimal>> zyskInformacyjnyAtrybutow = new List<KeyValuePair<int, decimal>>();
        public int maksymalnyZyskAtrybut;
        
        public SystemDecyzyjny()
        {

        }

        public SystemDecyzyjny(string sciezkaDoPliku)
        {
            var daneZPliku = System.IO.File.ReadAllLines(sciezkaDoPliku);
            foreach (var wiersz in daneZPliku)
                if (wiersz.Trim() != "")
                    zbiórObiektów.Add(new ObiektDecyzyjny(wiersz));
            
            for(int i = 0; i < zbiórObiektów[0].atrybuty.Count; i++)
            {
                iloscAtrybutow.Add(i);
            }
            //Inicjalizacja czestosci dla decyzji
            PoliczCzestoscDlaDecyzji(zbiórObiektów,czestoscDecyzja);
           
            //Liczenie Entropii
            entropiaSystem = Entropia.PoliczEntropie(czestoscDecyzja,zbiórObiektów.Count);

            PoliczEntropieDlaAtrybutow(iloscAtrybutow, entropieAtrybutów, czestoscAtrybutow, iloscRozwazanych, zbiórObiektów, czestoscDecyzja);
            PoliczZyskInformacyjnyDlaAtrybutow(entropieAtrybutów,entropiaSystem,iloscRozwazanych,iloscAtrybutow,zbiórObiektów,zyskInformacyjnyAtrybutow);
            PosortujZysk(zyskInformacyjnyAtrybutow);
            
            maksymalnyZyskAtrybut = PobierzMaksymalnyAtrybut(zyskInformacyjnyAtrybutow);
        }

        public int PobierzMaksymalnyAtrybut(List<KeyValuePair<int, decimal>> zyskInformacyjnyAtrybutow)
        {
            KeyValuePair<int, decimal> maksymalnyZyskInformacyjny = zyskInformacyjnyAtrybutow.Last<KeyValuePair<int, decimal>>();
            return maksymalnyZyskInformacyjny.Key;
        }

        public static void PosortujZysk(List<KeyValuePair<int, decimal>> zyskInformacyjnyAtrybutow)
        {
            zyskInformacyjnyAtrybutow.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
        }

        public static void PoliczZyskInformacyjnyDlaAtrybutow(Dictionary<int, Dictionary<int, decimal>> entropieAtrybutów, decimal entropia, Dictionary<int, Dictionary<int, int>> iloscRozwazanych, List<int> iloscAtrybutow, List<ObiektDecyzyjny> zbiórObiektów, List<KeyValuePair<int, decimal>> zyskInformacyjnyAtrybutow)
        {

            foreach (int atrybut in iloscAtrybutow)
            {
                decimal zysk = Entropia.PoliczZyskInformacyjny(entropieAtrybutów[atrybut], entropia,iloscRozwazanych[atrybut],zbiórObiektów.Count);
                zyskInformacyjnyAtrybutow.Add(new KeyValuePair<int, decimal>(atrybut, zysk));
            }
        }

        public  void PoliczEntropieDlaAtrybutow(List<int> iloscAtrybutow, Dictionary<int, Dictionary<int, decimal>> entropieAtrybutów, Dictionary<int, Dictionary<int, Dictionary<int, int>>> czestoscAtrybutow, Dictionary<int, Dictionary<int, int>> iloscRozwazanych, List<ObiektDecyzyjny> zbiórObiektów, Dictionary<int, int> czestoscDecyzja)
        {

            foreach(int atrybut in iloscAtrybutow)
            {
                if (!entropieAtrybutów.Keys.Contains(atrybut)) entropieAtrybutów.Add(atrybut, new Dictionary<int, decimal>());
                if (!czestoscAtrybutow.Keys.Contains(atrybut)) czestoscAtrybutow.Add(atrybut, new Dictionary<int, Dictionary<int, int>>());
                if (!iloscRozwazanych.Keys.Contains(atrybut)) iloscRozwazanych.Add(atrybut, new Dictionary<int, int>());
                Dictionary <int, int> liczbaRozwazanychObiektow = PoliczLiczbeRozważanychObiektow(atrybut, zbiórObiektów);
                foreach (var d in czestoscDecyzja.Keys)
                {
                    Dictionary<int, int> czestoscWewnetrzna = new Dictionary<int, int>();
                    foreach (ObiektDecyzyjny obiekt in zbiórObiektów)
                    {
                        obiekt.PoliczCzestosc(czestoscWewnetrzna,d, atrybut);
                    }
                   
                    if (!czestoscAtrybutow[atrybut].Keys.Contains(d)) czestoscAtrybutow[atrybut].Add(d, czestoscWewnetrzna);
                }
                foreach(var c in liczbaRozwazanychObiektow)
                {
                    Dictionary<int, int> czestoscE = new Dictionary<int, int>();
                    foreach(var d in czestoscAtrybutow[atrybut].Keys)
                    {
                        {
                            if (!czestoscAtrybutow[atrybut][d].Keys.Contains(c.Key)) continue;
                            czestoscE[d] = czestoscAtrybutow[atrybut][d][c.Key];
                        }
                    }
                    //Policz Entropie
                    entropieAtrybutów[atrybut][c.Key] = Entropia.PoliczEntropie(czestoscE, c.Value);
                }
                iloscRozwazanych[atrybut] = liczbaRozwazanychObiektow;
            }
        }

        public  Dictionary<int, int> PoliczLiczbeRozważanychObiektow(int i,List<ObiektDecyzyjny> zbiórObiektów)
        {
            Dictionary<int, int> liczbaRozwazanychObiektow = new Dictionary<int, int>();
            foreach (ObiektDecyzyjny obiekt in zbiórObiektów)
            {
                obiekt.PoliczLiczbeRozwazanychObiektow(liczbaRozwazanychObiektow, i);
            }
            return liczbaRozwazanychObiektow;
        }

        public void PoliczCzestoscDlaDecyzji(List<ObiektDecyzyjny> zbiórObiektów, Dictionary<int, int> czestoscDecyzja)
        {
            foreach (ObiektDecyzyjny obiekt in zbiórObiektów)
            {
                obiekt.PoliczCzestosc(czestoscDecyzja);
            }
        }

    }
}
