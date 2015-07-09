using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGraphique
{
    public class Case : INotifyPropertyChanged
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
            set {
                valeur = value;
                OnPropertyChanged("valeur");
            }
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
