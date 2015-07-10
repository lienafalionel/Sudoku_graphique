using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SudokuGraphique
{
    public class SudokusViewModel
    {
        public string NomApplication { get; set; }
        public string pathFile { get; set; }
        public ObservableCollection<Grille> grilles { get; set; }
        public Grille GrilleSelect { get; set; }

        public SudokusViewModel()
        {
            pathFile = @"Sudokus à Résoudre.sud";
            NomApplication = "Sudoku";
            grilles = new ObservableCollection<Grille>();
            parseGrilles();
            GrilleSelect = grilles[0];
        }

        internal void resoudre()
        {
            GrilleSelect.resoudre();
        }

        private void parseGrilles()
        {
            string line;
            StreamReader file = new StreamReader(pathFile);
            while ((line = file.ReadLine()) != null)
            {
                string nom = file.ReadLine();
                string date = file.ReadLine();
                string symboles = file.ReadLine();
                int longueur = symboles.Length;
                Grille grille = new Grille(nom, date, symboles, longueur);
                for (int i = 0; i < longueur; i++)
                {
                    grille.Cases[i] = new Case[longueur];
                    for (int j = 0; j < longueur; j++)
                    {
                        grille.Cases[i][j] = new Case();
                        grille.Cases[i][j].Valeur = (char)file.Read();
                        grille.Cases[i][j].Row = i;
                        grille.Cases[i][j].Column = j;
                    }
                    char tabulation = (char)file.Read();
                    char sautDeLigne = (char)file.Read();
                }
                if ((grille.Longueur == 9 || grille.Longueur == 16 || grille.Longueur == 25) && grille.EstValide())
                {
                    grilles.Add(grille);
                }
            }
            file.Close();
        }
    }
}
