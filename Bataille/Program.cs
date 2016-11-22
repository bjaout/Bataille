using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{
    class Program
    {
        static void Main(string[] args)
        {
            Bataille bataille = new Bataille();
            bataille.PlayedEvent += new PlayedEventHandler(PliJouee);
            bataille.PliGainedEvent += new PliGainedEventHandler(PliGagnee);
            bataille.BatailleEvent += new BatailleEventHandler(BatailleEv);
            Console.WriteLine("Gagnant : " + bataille.Run());
            Console.ReadLine();
        }

        private static void PliJouee(object sender, PlayedEventArgs e)
        {
            Console.Write("Joueur 1 joue : " + e.CarteJoueurs.Pop());
            Console.Write("\t");
            Console.WriteLine("Joueur 2 joue : " + e.CarteJoueurs.Pop());
        }

        private static void PliGagnee(object sender, PliGainedEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("Gagnant du pli : " + e.Gagnant.Nom + " avait " + e.Gagnant.Main.Count + " cartes.");
            Console.WriteLine("Il gagne sur le dernier pli :");
            foreach(Carte carte in e.Pli)
            {
                Console.WriteLine("\t- " + carte);
            }
            if(e.Bataille.Count != 0)
            {
                Console.WriteLine("Il gagne grace aux batailles :");
                foreach(Carte carte in e.Bataille)
                {
                    Console.WriteLine("\t- " + carte);
                }
            }
            Console.WriteLine("Le perdant du pli : " + e.Perdant.Nom + " a encore " + e.Perdant.Main.Count);
        }

        private static void BatailleEv(object sender, EventArgs e)
        {
            Console.WriteLine("Bataille");
        }


    }
}
