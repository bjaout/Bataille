using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{
    public class Carte:IComparable
    {
        public int Valeur { get; set; }
        public string Couleur { get; set; }
        public Dictionary<int,string> Type { get; set; }

        public Carte(int valeur, string couleur, Dictionary<int,string> type)
        {
            this.Valeur = valeur;
            this.Couleur = couleur;
            this.Type = type;
        }

        public override string ToString()
        {
            string strPuissance = "";
            Type.TryGetValue(this.Valeur, out strPuissance);
            return strPuissance + " de " + this.Couleur;
        }

        public int CompareTo(object obj)
        {
            int resultat = 0;
            try
            {
                Carte carte = obj as Carte;
                resultat = this.Valeur.CompareTo(carte.Valeur);
            }
            catch (Exception)
            {

            }
            return resultat;
        }
    }
}
