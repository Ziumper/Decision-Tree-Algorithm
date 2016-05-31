using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrzewaDecyzyjne
{
    class PodSystemDecyzyjny : SystemDecyzyjny
    {
        private int atrybut;
        private int cecha;
      

        public PodSystemDecyzyjny(List<ObiektDecyzyjny> zbiórObiektów, int cecha, decimal entropiaSystem, int atrybut,List<int> iloscAtrybutow)
        {
            this.zbiórObiektów = zbiórObiektów;
            this.cecha = cecha;
            this.entropiaSystem = entropiaSystem;
            this.atrybut = atrybut;
            this.zbiórObiektów = WyliczObiekty(zbiórObiektów, cecha,atrybut);
            this.iloscAtrybutow = iloscAtrybutow;
            iloscAtrybutow.Remove(atrybut);
            PoliczCzestoscDlaDecyzji(this.zbiórObiektów, this.czestoscDecyzja);
            PoliczEntropieDlaAtrybutow(this.iloscAtrybutow, entropieAtrybutów, czestoscAtrybutow, iloscRozwazanych,this.zbiórObiektów, czestoscDecyzja);
            PoliczZyskInformacyjnyDlaAtrybutow(entropieAtrybutów, this.entropiaSystem, iloscRozwazanych, this.iloscAtrybutow, this.zbiórObiektów, zyskInformacyjnyAtrybutow);
            PosortujZysk(zyskInformacyjnyAtrybutow);
            maksymalnyZyskAtrybut = PobierzMaksymalnyAtrybut(zyskInformacyjnyAtrybutow);
        }

        private List<ObiektDecyzyjny> WyliczObiekty(List<ObiektDecyzyjny> zbiórObiektów, int cecha, int atrybut)
        {
            List<ObiektDecyzyjny> obiekty = new List<ObiektDecyzyjny>();
            foreach(ObiektDecyzyjny obiekt in zbiórObiektów)
            {
                if (obiekt.atrybuty[atrybut] == cecha) obiekty.Add(obiekt);
            }
            return obiekty;
        }
    }
}
