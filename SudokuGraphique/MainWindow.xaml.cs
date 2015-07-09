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
        }

        private void grilleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            InitGridSudoku();

            App.ViewModelSudokus.GrilleSelect = (Grille)grilleListBox.SelectedItem;

            

            /*if (App.ViewModelSudokus.GrilleSelect.Longueur != 9)
            {*/
            for (int i = 0; i < App.ViewModelSudokus.GrilleSelect.Longueur; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                GridSudoku.RowDefinitions.Add(row);

                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(1, GridUnitType.Star);
                GridSudoku.ColumnDefinitions.Add(column);
            }

            //}

            for (int row = 0; row < App.ViewModelSudokus.GrilleSelect.Longueur; row++)
            {
                for (int column = 0; column < App.ViewModelSudokus.GrilleSelect.Longueur; column++)
                {
                    Case c = App.ViewModelSudokus.GrilleSelect.Cases[row][column];

                    TextBlock txtBlock = new TextBlock();
                    txtBlock.Text = c.Valeur.ToString();
                    txtBlock.FontSize = 14;
                    txtBlock.FontWeight = FontWeights.Bold;
                    txtBlock.VerticalAlignment = VerticalAlignment.Center;
                    txtBlock.HorizontalAlignment = HorizontalAlignment.Center;

                    Binding binding = new Binding("Valeur");
                    binding.Source = App.ViewModelSudokus.GrilleSelect.Cases[row][column];
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    txtBlock.SetBinding(TextBlock.TextProperty, binding);
                    Grid.SetRow(txtBlock, row);
                    Grid.SetColumn(txtBlock, column);



                    /*if(c.Hypotheses != null)
                    {
                        TextBlock txtBlockHypotheses = new TextBlock();
                        txtBlockHypotheses.Text = new String(c.Hypotheses);
                        txtBlockHypotheses.FontSize = 8;
                        txtBlockHypotheses.VerticalAlignment = VerticalAlignment.Top;
                        txtBlockHypotheses.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(txtBlockHypotheses, row);
                        Grid.SetColumn(txtBlockHypotheses, column);
                        GridSudoku.Children.Add(txtBlockHypotheses);
                    }*/



                    GridSudoku.Children.Add(txtBlock);
                }
            }
        }

        private void InitGridSudoku()
        {
            GridSudoku.Children.Clear();
            GridSudoku.RowDefinitions.Clear();
            GridSudoku.ColumnDefinitions.Clear();
        }
    }
}
