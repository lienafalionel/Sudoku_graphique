using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuGraphique
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.ViewModelSudokus;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.ViewModelSudokus.resoudre();
            MessageBox.Show(App.ViewModelSudokus.GrilleSelect.print());
        }

        private void grilleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.ViewModelSudokus.GrilleSelect = (Grille)grilleListBox.SelectedItem;
            //Grid.ColumnDefinitions.
        }
    }
}
