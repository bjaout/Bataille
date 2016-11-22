using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{
    public class PaquetCartes : List<Carte>
    {
        public PaquetCartes():base()
        {
            for(int i = 0; i < 13; i++)
            {
                this.Add(new Carte(i, "Coeur", PuissanceCarte.Standard));
                this.Add(new Carte(i, "Trèfle", PuissanceCarte.Standard));
                this.Add(new Carte(i, "Carreau", PuissanceCarte.Standard));
                this.Add(new Carte(i, "Pique", PuissanceCarte.Standard));
            }
        }

        public void Melanger()
        {
            List<Carte> listeMelangee = new List<Carte>();
            Random geneRandom = new Random();
            while (this.Count > 0)
            {
                int nbRandom = geneRandom.Next(this.Count);
                listeMelangee.Add(this.ElementAt(nbRandom));
                this.RemoveAt(nbRandom);
            }
            this.AddRange(listeMelangee);
        }

        public void DistribuerCartes(List<Joueur> joueurs)
        {
            while (this.Count > 0)
            {
                foreach (Joueur joueur in joueurs)
                {
                    joueur.Main.Enqueue(this.ElementAt(0));
                    this.RemoveAt(0);
                }
            }
        }
    }
}
