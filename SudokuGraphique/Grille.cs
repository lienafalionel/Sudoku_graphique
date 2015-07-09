using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGraphique
{
    public class Grille
    {
        private string nom;
        private string date;
        private string symboles;
        private int longueur;
        private Case[][] cases;
        private bool estResolu = false;

        public Grille()
        {
            
        }

        public Grille(string nom, string date, string symboles, int longueur)
        {
            Nom = nom;
            Date = date;
            Symboles = symboles;
            Longueur = longueur;
            cases = new Case[longueur][];
        }

        public bool EstValide()
        {
            //return (VerifSymboles() && VerifLignes() && VerifColonnes() && VerifSymboles());
            return VerifSymboles();
        }

        private bool VerifColonnes()
        {
            List<Case[]> colonnes = new List<Case[]>();

            for (int i = 0; i < 9; i++)
            {
                Case[] array = new Case[9];
                for (int j = 0; j < 9; j++)
                {
                    array[j] = cases[j][i];
                }
                colonnes.Add(array);
            }

            foreach(Case[] colonne in colonnes)
            {
                char[] symbolesArray = Symboles.ToCharArray();
                Array.Sort(symbolesArray);
                char[] ligneValeur = new char[9];
                for (int i = 0; i < colonne.Length; i++)
                {
                    ligneValeur[i] = colonne[i].Valeur;
                }
                Array.Sort(ligneValeur);

                if (!Enumerable.SequenceEqual(symbolesArray, ligneValeur))
                {
                    return false;
                }

                if (colonne.Length != 9)
                {
                    return false;
                }
            }

            return true;
        }

        private bool VerifSymboles()
        {
            foreach (Case[] t in Cases)
            {
                foreach (Case c in t)
                {
                    if ((!Symboles.Contains(c.Valeur)) && (c.Valeur != '.'))
                    {
                        return false;
                    }
                    if(c.Valeur == '.')
                    {
                        c.Hypotheses = Symboles.ToCharArray();
                    }
                    else
                    {
                        c.Hypotheses = null;
                    }
                }
            }
            return true;
        }

        private bool VerifLignes()
        {
            char[] symbolesArray = Symboles.ToCharArray();
            Array.Sort(symbolesArray);
            foreach (Case[] ligne in Cases)
            {
                char[] ligneValeur = new char[9];
                for (int i = 0; i < ligne.Length; i++)
                {
                    ligneValeur[i] = ligne[i].Valeur;
                }
                Array.Sort(ligneValeur);

                if (!Enumerable.SequenceEqual(symbolesArray, ligneValeur))
                {
                    return false;
                }
    
                if (ligne.Length != 9)
                {
                    return false;
                }
            }
            return true;
        }

         private bool verifierUneRegionOK(char[,] tab, int iDepart, int iFin, int jDepart, int jFin)
        {
            bool reponse = true;
            List<char> tabControl = new List<char>();
            for (int i = iDepart; i <= iFin; i++)
            {
                for (int j = jDepart; j <= jFin; j++)
                {
                    tabControl.Add(tab[i,j]);
                }
            }
            char[] tabControlChar = tabControl.ToArray();
            Array.Sort(tabControlChar);
            char[] symbolesArray = Symboles.ToCharArray();
            Array.Sort(symbolesArray);
            if (!Enumerable.SequenceEqual(symbolesArray, tabControlChar))
                reponse=false;
            return reponse;
         }

        private bool VerifRegions()
         {
             char[,] tab = GetConvertCasesToChars();
             bool OkVerif = true;
            int racineCarreLongueur = (int)(Math.Sqrt(longueur));
             int iDepart = 0;
            int iFin = racineCarreLongueur-1;
             int jDepart = 0;
            int jFin = racineCarreLongueur - 1;
 
             while (iFin < tab.GetLength(0) && OkVerif == true)
             {
                 OkVerif = verifierUneRegionOK(tab, iDepart, iFin, jDepart, jFin);
                 if (OkVerif)
                 {
                     if (jFin == tab.GetLength(0) - 1)
                     {
                         jDepart = 0;
                        jFin = racineCarreLongueur - 1;
                        iDepart += racineCarreLongueur;
                        iFin += racineCarreLongueur;
                     }
                     else
                     {
                        jDepart += racineCarreLongueur;
                        jFin += racineCarreLongueur;
                     }
                 }
             }
             return OkVerif;
         }

        public void resoudre()
        {
            for (int row = 0; row < Longueur; row++)
            {
                for (int column = 0; column < Longueur; column++)
                {
                    Case c = Cases[row][column];
                    if (c.Valeur == '.')
                    {
                        if(trouveHypotheses(ref c, column, row))
                        {
                            row = -1;
                            column = -1; //Redémarre au début de la grille
                            break;
                        }
                    }
                }
            }
            estResolu = true;
        }

        private bool trouveHypotheses(ref Case c, int column, int row)
        {
            Case[] ligneArray = Cases[row];

            List<Case> colonneList = new List<Case>();
            for (int i = 0; i < Longueur; i++)
            {
                colonneList.Add(Cases[i][column]);
            }
         
            List<Case> regionList = new List<Case>();
            int longueurRegion = Convert.ToInt16(Math.Sqrt(Longueur));

            int moduloColumn = column % longueurRegion;
            int columnStartRegion = column - moduloColumn;
            int columnEndRegion = columnStartRegion + longueurRegion - 1;

            int moduloRow = row % longueurRegion;
            int rowStartRegion = row - moduloRow;
            int rowEndRegion = rowStartRegion + longueurRegion - 1;

            for (int i = 0; i < Longueur; i++)
            {
                for (int j = 0; j < Longueur; j++)
                {
                    Case casee = Cases[j][i];
                    if(i >= columnStartRegion && i <= columnEndRegion && j >= rowStartRegion && j <= rowEndRegion)
                    {
                        regionList.Add(Cases[j][i]);
                    }
                }
            }

            //UnSeulCandidat(ref c, ligneArray, colonneList, regionList);

            return UnSeulCandidat(ref c, ligneArray, colonneList, regionList);
        }

        private bool UnSeulCandidat(ref Case c, Case[] ligneArray, List<Case> colonneList, List<Case> regionList)
        {
            // Recherche un seul candidat sur une ligne
            string ligne = string.Join<Case>("", ligneArray);
            IEnumerable<char> s = c.Hypotheses.Intersect(ligne);
            foreach (char ch in s)
            {
                int index = Array.IndexOf(c.Hypotheses, ch);
                c.Hypotheses = c.Hypotheses.Where(val => val != ch).ToArray();
            }
            if(c.NbreHypothese == 1)
            {
                c.Valeur = c.Hypotheses[0];
                return true;
            }

            // Recherche un seul candidat sur une colonne
            string colonne = string.Join<Case>("", colonneList);
            IEnumerable<char> interColonne = c.Hypotheses.Intersect(colonne);
            foreach (char ch in interColonne)
            {
                int index = Array.IndexOf(c.Hypotheses, ch);
                c.Hypotheses = c.Hypotheses.Where(val => val != ch).ToArray();
            }
            if(c.NbreHypothese == 1)
            {
                c.Valeur = c.Hypotheses[0];
                return true;
            }

            // Recherche un seul candidat sur une région
            string region = string.Join<Case>("", regionList);
            IEnumerable<char> interRegion = c.Hypotheses.Intersect(region);
            foreach (char ch in interRegion)
            {
                int index = Array.IndexOf(c.Hypotheses, ch);
                c.Hypotheses = c.Hypotheses.Where(val => val != ch).ToArray();
            }
            if (c.NbreHypothese == 1)
            {
                c.Valeur = c.Hypotheses[0];
                return true;
            }

            return false;
        }

         private char[,] GetConvertCasesToChars()
        {
            char[,] casesEnChar=new char[longueur,longueur];
            for(int i=0;i<longueur;i++)
            {
                for(int j=0;j<longueur;j++)
                {
                    casesEnChar[i,j] = cases[i][j].Valeur;
                }
            }
            return casesEnChar;
        }

        public override string ToString()
        {
            return Nom + "\t\t" + Date + "\t" + Longueur + "x" + Longueur;
        }

        public string print()
        {
            string grille = "";
            foreach (Case[] t in Cases)
            {
                foreach (Case c in t)
                {
                    grille += c.Valeur;
                }
                grille += "\n";
            }
            return grille;
        }
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Symboles
        {
            get { return symboles; }
            set { symboles = value; }
        }

        public int Longueur
        {
            get { return longueur; }
            set { longueur = value; }
        }

        public Case[][] Cases
        {
            get { return cases; }
            set { cases = value; }
        }

        public bool EstResolu
        {
            get { return estResolu; }
            set { estResolu = value; }
        }
    }
}
