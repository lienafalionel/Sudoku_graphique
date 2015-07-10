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
        private string hypotheses;
        private int row;
        private int column;

        public Case()
        {

        }

        public char Valeur
        {
            get { return valeur; }
            set {
                valeur = value;
                OnPropertyChanged("Valeur");
            }
        }

        public int NbreHypothese
        {
            get { return hypotheses.Length; }
            set { nbreHypothese = value; }
        }

        public string Hypotheses
        {
            get { return hypotheses; }
            set { hypotheses = value;
                OnPropertyChanged("Hypotheses");
            }
        }

        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        public int Column
        {
            get { return column; }
            set { column = value; }
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
