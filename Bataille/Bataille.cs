using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{

    public delegate void PlayedEventHandler(object sender, PlayedEventArgs e);
    public delegate void BatailleEventHandler(object sender, EventArgs e);
    public delegate void PliGainedEventHandler(object sender, PliGainedEventArgs e);


    class Bataille
    {
        private Joueur joueur1 = new Joueur("Toto");
        private Joueur joueur2 = new Joueur("Titi");
        private PaquetCartes jeuDeCarte = new PaquetCartes();
        public event PlayedEventHandler PlayedEvent;
        public event BatailleEventHandler BatailleEvent;
        public event PliGainedEventHandler PliGainedEvent;


        public int Run()
        {
            List<Joueur> joueurs = new List<Joueur>() { joueur1, joueur2 };
            jeuDeCarte.Melanger();
            jeuDeCarte.DistribuerCartes(joueurs);
            return JouerPartie(joueurs);
        }

        private int JouerPartie(List<Joueur> joueurs)
        {
            bool jouer = true;
            int gagnant = 0;
            while (jouer)
            {
                if ((joueurs[0].Main.Count() == 0))
                {
                    jouer = false;
                    gagnant = 1;
                }
                else if (joueurs[1].Main.Count() == 0)
                {
                    jouer = false;
                    gagnant = 0;
                }
                if (jouer)
                {
                    Stack<Carte> pliActuelle = new Stack<Carte>();
                    Stack<Carte> batailleActuelle = new Stack<Carte>();
                    PliGainedEventArgs args = new PliGainedEventArgs();
                    int perdantPli;
                    int gagnantPli = JouerPli(joueurs, ref pliActuelle, ref batailleActuelle);

                    
                    args.Gagnant = joueurs[gagnantPli];
                    if (gagnantPli == 0) { perdantPli = 1; }
                    else { perdantPli = 0; }
                    args.Perdant = joueurs[perdantPli];
                    args.Pli = new Stack<Carte>(pliActuelle);
                    args.Bataille = new Stack<Carte>(batailleActuelle);
                    OnGained(args);
                    foreach (Carte carte in pliActuelle)
                    {
                        joueurs[gagnantPli].Main.Enqueue(carte);
                    }
                    foreach (Carte carte in batailleActuelle)
                    {
                        joueurs[gagnantPli].Main.Enqueue(carte);
                    }
                }
            }
            return gagnant;
        }

        private int JouerPli(List<Joueur> joueurs, ref Stack<Carte> pliActuelle, ref Stack<Carte> batailleActuelle)
        {
            int indexGagnant = 0;
            bool jouer = true;
            if ((joueurs[0].Main.Count() == 0))
            {
                jouer = false;
                indexGagnant = 1;
            }
            else if (joueurs[1].Main.Count() == 0)
            {
                jouer = false;
                indexGagnant = 0;
            }
            if (jouer)
            {
                foreach (Joueur joueur in joueurs)
                {
                    pliActuelle.Push(joueur.Main.Dequeue());
                }
                PlayedEventArgs args = new PlayedEventArgs();
                args.CarteJoueurs = new Stack<Carte>(pliActuelle.Reverse());
                OnPlayed(args);
                Carte meilleureCarte = null;
                int index = 0;
                foreach (Carte carte in pliActuelle)
                {
                    if (meilleureCarte != null)
                    {
                        if (meilleureCarte.CompareTo(carte) > 0)
                        {

                        }
                        else if (meilleureCarte.CompareTo(carte) == 0)
                        {
                            OnBataille(EventArgs.Empty);
                            foreach (Carte carte2 in pliActuelle)
                            {
                                batailleActuelle.Push(carte2);
                            }
                            pliActuelle.Clear();
                            foreach (Joueur joueur in joueurs)
                            {
                                batailleActuelle.Push(joueur.Main.Dequeue());
                            }
                            indexGagnant = JouerPli(joueurs, ref pliActuelle, ref batailleActuelle);// Bataille
                            break;
                        }
                        else
                        {
                            meilleureCarte = carte;
                            indexGagnant = index;
                        }
                    }
                    else
                    {
                        meilleureCarte = carte;
                    }
                    index++;
                }
            }
            return indexGagnant;

        }

        protected void OnPlayed(PlayedEventArgs e)
        {
            if (PlayedEvent != null)
            {
                PlayedEvent(this, e);
            }
        }

        protected void OnBataille(EventArgs e)
        {
            if(BatailleEvent!=null)
            {
                BatailleEvent(this, EventArgs.Empty);
            }
        }

        protected void OnGained(PliGainedEventArgs e)
        {
            if(PliGainedEvent!=null)
            {
                PliGainedEvent(this, e);
            }
        }
    }

    public class PlayedEventArgs : EventArgs
    {
        public Stack<Carte> CarteJoueurs { get; set; }
    }

    public class PliGainedEventArgs : EventArgs
    {
        public Stack<Carte> Pli { get; set; }
        public Stack<Carte> Bataille { get; set; }
        public Joueur Gagnant { get; set; }
        public Joueur Perdant { get; set; }
    }
}
