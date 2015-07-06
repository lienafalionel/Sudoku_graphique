using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGraphique
{
    public class Case
    {
        private char valeur;
        private int nbreHypothese;
        private char[] hypotheses = new char[9];

        public Case()
        {

        }

        public char Valeur
        {
            get { return valeur; }
            set { valeur = value; }
        }

        public int NbreHypothese
        {
            get { return hypotheses.Length; }
            set { nbreHypothese = value; }
        }

        public char[] Hypotheses
        {
            get { return hypotheses; }
            set { hypotheses = value; }
        }

        public override string ToString()
        {
            return Convert.ToString(valeur);
        }
    }
}
