using FlashCard.Pages;
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

namespace FlashCard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Helpers.mainGrid = this.Content;
            Helpers.mainWindow = this;
            Helpers.Navigate(new MainPage());
        }

        private void Synonym_Click(object sender, RoutedEventArgs e)
        {
            Helpers.Navigate(new Synonym(0));
        }

        private void Spelling_Click(object sender, RoutedEventArgs e)
        {
            Helpers.Navigate(new Synonym(1));
        }

        private void SingleWord_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GroupWord_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FileWord_Click(object sender, RoutedEventArgs e)
        {
            Helpers.Navigate(new ReadFromFile());
        }
    }
}
