using FlashCard.Model;
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

        private void SynonymReverse_Click(object sender, RoutedEventArgs e)
        {
            Helpers.Navigate(new Synonym(2));
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
            Helpers.Navigate(new ReadFromFile(0));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            using (var dic = new Dictionary())
            {
                var p = dic.GetAll().ToArray();

                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(p, Newtonsoft.Json.Formatting.Indented));
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Helpers.Navigate(new ReadFromFile(1));
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Helpers.Navigate(new Synonym(3));
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Helpers.Navigate(new Writing());
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            //using (var dic = new Dictionary())
            //{
            //    var items = dic.GetAll().ToList();
            //    foreach (var item in items)
            //    {
            //        //if (item.CategoryMeaning == "normal" && item.Meaning == 0 && !item.TheWord.Contains('.'))
            //        //{
            //        //    item.CategoryMeaning = "Cna";
            //        //    dic.Update(item);
            //        //}
            //        //if (item.CategoryMeaning == "normal" && item.Meaning <= 10 && item.Meaning >= -10)
            //        //{
            //        //    item.CategoryMeaning = "Cna";
            //        //    dic.Update(item);
            //        //}
            //        if (item.CategoryMeaning == "Cna")
            //        {
            //            item.Meaning = 0;
            //            dic.Update(item);
            //        }
            //    }
            //}

        }
    }
}
