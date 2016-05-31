using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrzewaDecyzyjne
{
    public partial class Drzewa : Form
    {
        public Drzewa()
        {
            InitializeComponent();
            btnBudujDrzewo.Enabled = false;
        }

        private void btnWczytaj_Click(object sender, EventArgs e)
        {
            var r = ofdSystemDecyzyjny.ShowDialog();
            if (r != DialogResult.OK) return;
            rtbSystemDecyzyjny.Text = System.IO.File.ReadAllText(ofdSystemDecyzyjny.FileName);
            btnBudujDrzewo.Enabled = true;
        }

        private void btnBudujDrzewo_Click(object sender, EventArgs e)
        {
            tvDrzewoDecyzyjne.Nodes.Clear();
            SystemDecyzyjny system = new SystemDecyzyjny(ofdSystemDecyzyjny.FileName);
            Gałąź korzeń = new Gałąź();
            Gałąź gałąź = ZbudujDrzewo(system,korzeń);
            TreeNode node = new TreeNode();
            node = WygenerujDrzewo(gałąź, node);
            tvDrzewoDecyzyjne.Nodes.Add(node);
        }

        private List<TreeNode> WygenerujDzieci(List<Węzeł> wezły)
        {
            List<TreeNode> dzieci = new List<TreeNode>();
            foreach (var wezel in wezły)
            {
                string napis = wezel.cecha.ToString();
                TreeNode dziecko = new TreeNode();
                dziecko = WygenerujDziecko(wezel.gałąźPod, dziecko, napis);
                dzieci.Add(dziecko);
            }
            return dzieci;
        }

        private TreeNode WygenerujDrzewo(Gałąź gałąź, TreeNode node)
        {
            List<TreeNode> dzieci = WygenerujDzieci(gałąź.węzły);
            TreeNode rodzic = new TreeNode("a" + (gałąź.wartoscGałęzi + 1).ToString(), dzieci.ToArray());
            return rodzic;
        }

        private TreeNode WygenerujDziecko(Gałąź gałąźPod, TreeNode dziecko, string napis)
        {
            if (gałąźPod.węzły.Count == 1)
            {
                //decyzja
                napis += " =>(d=" + gałąźPod.wartoscGałęzi + ")\n";
                dziecko = new TreeNode(napis);
                return dziecko;
            }
            TreeNode rodzic = new TreeNode();
            rodzic = WygenerujDrzewoRodzic(gałąźPod, rodzic, napis);
            return rodzic;
        }

        private TreeNode WygenerujDrzewoRodzic(Gałąź gałąźPod, TreeNode rodzic, string napis)
        {
            List<TreeNode> dzieci = WygenerujDzieci(gałąźPod.węzły);
            rodzic = new TreeNode(napis + " =>(a" + (gałąźPod.wartoscGałęzi + 1).ToString()+")", dzieci.ToArray());
            return rodzic; 
        }

        private string WygenerujLog(Gałąź gałąź, TreeNode node)
        {
            List<TreeNode> dzieci = new List<TreeNode>();
            string napis = "";
            {//zwykła gałąź
                if (gałąź.węzły.Count == 1)
                {
                    //decyzja

                    return napis += "=>(d=" + gałąź.wartoscGałęzi + ")\n";
                }
               
                foreach (var wezel in gałąź.węzły)
                {
                    TreeNode dziecko = new TreeNode(wezel.cecha.ToString());
                    napis += "a" + (gałąź.wartoscGałęzi + 1) + "=";
                    napis += wezel.cecha + " ";
                  //  tvDrzewoDecyzyjne.Nodes.Add(wezelNapis);
                    napis += WygenerujLog(wezel.gałąźPod,dziecko);
                    
                }
            }
            TreeNode rodzic = new TreeNode("a" + (gałąź.wartoscGałęzi + 1).ToString(),dzieci.ToArray());
            return napis;
        }

        private Gałąź ZbudujDrzewo(SystemDecyzyjny system,Gałąź gPrzed)
        {
            int atrybut = system.maksymalnyZyskAtrybut;//atrybut Wilgotność
             //Wilgotnosc jest oddzielną gałęzią
            gPrzed.wartoscGałęzi = atrybut;
            foreach (var wartoscAtrybutu in system.entropieAtrybutów[atrybut])//lista wezłów
            {
                //ustalenie wskaźników węzłów każdy węzeł będzie miał wskaźnik do Wilgotnosci
               
                if (wartoscAtrybutu.Value == 0 || system.iloscAtrybutow.Count == 1)
                {
                    gPrzed.węzły.Add(new Węzeł(wartoscAtrybutu.Key, gPrzed));
                    int decyzjaliscia = 0;
                    foreach (var d in system.czestoscAtrybutow[atrybut])
                    {
                        if (d.Value.Keys.Contains(wartoscAtrybutu.Key))
                        {
                            decyzjaliscia = d.Key;
                            break;
                        }

                    }
                    gPrzed.węzły = DodajLisciaDoWezla(system, gPrzed, decyzjaliscia,wartoscAtrybutu.Key);//gałąź POD! będąca liściem
                }
                else {//gdyby nie wszystkie decyzje na podstawie atrybutu były jasne musiałbym kolejny raz budować podsystem z mojego zbioru obiektow.
                    PodSystemDecyzyjny podSystem = new PodSystemDecyzyjny(system.zbiórObiektów, wartoscAtrybutu.Key, wartoscAtrybutu.Value, atrybut, system.iloscAtrybutow);
                    Gałąź nowaGałąź = new Gałąź();
                    //Połączenie! FUZJA!
                    gPrzed.węzły.Add(new Węzeł(wartoscAtrybutu.Key, nowaGałąź));
                    gPrzed.węzły = DodajGałąźPod(wartoscAtrybutu.Key,nowaGałąź,gPrzed);
                    ZbudujDrzewo(podSystem,nowaGałąź);//kolejne wywołanie funkcji
                }
            }
            return gPrzed;
        }

        private List<Węzeł> DodajGałąźPod(int key, Gałąź nowaGałąź, Gałąź gPrzed)
        {
           foreach(var wezel in gPrzed.węzły)
            {
                if (key == wezel.cecha) wezel.gałąźPod = nowaGałąź;
            }
            return gPrzed.węzły;
        }

        private List<Węzeł> DodajLisciaDoWezla(SystemDecyzyjny system, Gałąź gałąź, int decyzjaliscia, int key)
        {
            foreach(var wezel in gałąź.węzły)
            {
                if (wezel.cecha == key) {
                    Gałąź galazDecyzja = new Gałąź(decyzjaliscia);
                    galazDecyzja.węzły.Add(new Węzeł(-1, gałąź));
                    wezel.gałąźPod = galazDecyzja;
                }//dodaj lisc
            }
            return gałąź.węzły;
        }
    }
}
