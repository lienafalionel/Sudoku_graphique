using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SudokuGraphique
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SudokusViewModel ViewModelSudokus { get; set; }

        static App()
        {
            ViewModelSudokus = new SudokusViewModel();
        }
    }
}
