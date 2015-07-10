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
            RésoudreBtn.IsEnabled = false;
        }

        private void Resoudre_Click(object sender, RoutedEventArgs e)
        {
            App.ViewModelSudokus.resoudre();
        }

        private void Init_Click(object sender, RoutedEventArgs e)
        {
            App.ViewModelSudokus = new SudokusViewModel();
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
                

                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(1, GridUnitType.Star);
                

                if(i % App.ViewModelSudokus.GrilleSelect.Longueur == 0)
                {
                    Border b = new Border();
                    b.BorderBrush = Brushes.Red;
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, i);

                    Grid.SetColumnSpan(b, Convert.ToInt16(Math.Sqrt(App.ViewModelSudokus.GrilleSelect.Longueur)));
                    b.BorderThickness = new Thickness(1);
                }

                GridSudoku.RowDefinitions.Add(row);
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



                    if(c.Hypotheses != null)
                    {
                        TextBlock txtBlockHypotheses = new TextBlock();
                        txtBlockHypotheses.Text = c.Hypotheses;
                        txtBlockHypotheses.FontSize = 8;
                        txtBlockHypotheses.VerticalAlignment = VerticalAlignment.Top;
                        txtBlockHypotheses.HorizontalAlignment = HorizontalAlignment.Center;
                        
                        Binding bindingHypothese = new Binding("Hypotheses");
                        bindingHypothese.Source = App.ViewModelSudokus.GrilleSelect.Cases[row][column];
                        bindingHypothese.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                        txtBlockHypotheses.SetBinding(TextBlock.TextProperty, bindingHypothese);
                        Grid.SetRow(txtBlockHypotheses, row);
                        Grid.SetColumn(txtBlockHypotheses, column);
                        GridSudoku.Children.Add(txtBlockHypotheses);
                    }



                    GridSudoku.Children.Add(txtBlock);
                }
            }
        }

        private void InitGridSudoku()
        {
            GridSudoku.Children.Clear();
            GridSudoku.RowDefinitions.Clear();
            GridSudoku.ColumnDefinitions.Clear();

            if (grilleListBox.SelectedIndex >= 0)
            {
                RésoudreBtn.IsEnabled = true;
            }
            else
            {
                RésoudreBtn.IsEnabled = false;
            }
        }
    }
}
