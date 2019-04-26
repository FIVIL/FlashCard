using FlashCard.Model;
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

namespace FlashCard.Pages
{
    /// <summary>
    /// Interaction logic for ReadFromFile.xaml
    /// </summary>
    public partial class ReadFromFile : UserControl
    {
        private List<Item> items;
        public static int index;
        public ReadFromFile()
        {
            InitializeComponent();
            Helpers.mainWindow.Height = 980;
            Helpers.mainWindow.Top = 1;
            items = new List<Item>();
            Words.Children.Clear();
            index = 0;
        }
        class Item
        {
            public Item(string word, string def, string per, string pron)
            {
                ReadFromFile.index++;
                this.word = new TextBox();
                this.word.Margin = new Thickness(1);
                this.word.Text = word;
                this.def = new TextBox();
                this.def.Margin = new Thickness(1);
                this.def.TextWrapping = TextWrapping.Wrap;
                this.def.AcceptsReturn = true;
                this.def.Text = def;
                this.per = new TextBox();
                this.per.Margin = new Thickness(1);
                this.per.Text = per;
                this.pron = new TextBox();
                this.pron.Margin = new Thickness(1);
                this.pron.Text = pron;
            }
            private readonly TextBox word;
            private readonly TextBox def;
            private readonly TextBox per;
            private readonly TextBox pron;
            public Border Visual()
            {
                var b = new Border();
                b.BorderThickness = new Thickness(2);
                b.Margin = new Thickness(3, 10, 3, 10);
                b.BorderBrush = Brushes.Black;
                b.Padding = new Thickness(2);
                b.Background = Brushes.Wheat;
                b.CornerRadius = new CornerRadius(3);
                var sp = new StackPanel();
                sp.Children.Add(new TextBlock() { Text = $"Word: {ReadFromFile.index}", FontSize = 10 });
                sp.Children.Add(this.word);
                sp.Children.Add(new TextBlock() { Text = "Def:", FontSize = 10 });
                sp.Children.Add(this.def);
                sp.Children.Add(new TextBlock() { Text = "Per:", FontSize = 10 });
                sp.Children.Add(this.per);
                sp.Children.Add(new TextBlock() { Text = "Pron:", FontSize = 10 });
                sp.Children.Add(this.pron);
                b.Child = sp;
                return b;
            }
            public (string word, string def, string per, string pron) Get
                => (this.word.Text, this.def.Text, this.pron.Text, this.pron.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Words.Children.Clear();
            var lines = System.IO.File.ReadAllLines("Input.txt");
            foreach (var item in lines)
            {
                if (string.IsNullOrWhiteSpace(item)) continue;
                var p = breakUp(item);
                var it = new Item(p.word, p.def, "", "");
                items.Add(it);
                Words.Children.Add(it.Visual());
            }
        }
        private (string word, string def) breakUp(string inp)
        {
            var res = inp.Split('>');
            res[0] = res[0].Replace('-', ' ');
            res[0] = res[0].Trim();
            if (res.Length > 1)
            {
                res[1] = res[1].Trim();
                return (res[0], res[1]);
            }
            return (res[0], "");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var it = new Item("", "", "", "");
            items.Add(it);
            Words.Children.Add(it.Visual());
            SV.ScrollToEnd();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (var db = new Dictionary())
            {
                foreach (var item in items)
                {
                    var w = new Word()
                    {
                        TheWord = item.Get.word,
                        Definitions = item.Get.def,
                        Persian = item.Get.per,
                        Pron = item.Get.pron,
                        Meaning = 0,
                        Spelling = 0
                    };
                    db.Insert(w);
                }
            }
        }
    }
}
