using FlashCard.Model;
using Microsoft.Win32;
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
        private int mod;
        public ReadFromFile(int mod)
        {
            InitializeComponent();
            this.mod = mod;
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
                Spelling = new CheckBox();
                Spelling.Margin = new Thickness(0, 0, 3, 0);
                Meaning = new CheckBox();
                Meaning.Margin = new Thickness(0, 0, 3, 0);
                Meaning.IsChecked = true;
                IsPron = new CheckBox();
                IsPron.Margin = new Thickness(0, 0, 3, 0);
            }
            public Item(Word w)
            {
                id = w.Id;
                ReadFromFile.index++;
                this.word = new TextBox();
                this.word.Margin = new Thickness(1);
                this.word.Text = w.TheWord;
                this.def = new TextBox();
                this.def.Margin = new Thickness(1);
                this.def.TextWrapping = TextWrapping.Wrap;
                this.def.AcceptsReturn = true;
                this.def.Text = w.Definitions;
                this.per = new TextBox();
                this.per.Margin = new Thickness(1);
                this.per.Text = w.Persian;
                this.pron = new TextBox();
                this.pron.Margin = new Thickness(1);
                this.pron.Text = w.Pron;
                Spelling = new CheckBox();
                Spelling.IsChecked = w.IsSpelling;
                Spelling.Margin = new Thickness(0, 0, 3, 0);
                Meaning = new CheckBox();
                Meaning.Margin = new Thickness(0, 0, 3, 0);
                Meaning.IsChecked = w.IsMeaning;
                IsPron = new CheckBox();
                IsPron.Margin = new Thickness(0, 0, 3, 0);
                IsPron.IsChecked = w.IsPron;

                this.defScore = new TextBox();
                this.defScore.Margin = new Thickness(0, 0, 3, 0);
                this.defScore.Text = w.Meaning.ToString();
                this.defScore.Width = 30;
                this.spelScore = new TextBox();
                this.spelScore.Margin = new Thickness(0, 0, 3, 0);
                this.spelScore.Text = w.Spelling.ToString();
                this.spelScore.Width = 30;
                this.PronScore = new TextBox();
                this.PronScore.Margin = new Thickness(0, 0, 3, 0);
                this.PronScore.Text = w.PronScore.ToString();
                this.PronScore.Width = 30;
            }
            private readonly TextBox defScore;
            private readonly TextBox spelScore;
            private readonly TextBox PronScore;
            private readonly TextBox word;
            private readonly TextBox def;
            private readonly TextBox per;
            private readonly TextBox pron;
            private readonly CheckBox Meaning;
            private readonly CheckBox Spelling;
            private readonly CheckBox IsPron;
            private Guid id;
            private bool del = false;

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
                var spp = new StackPanel();
                spp.Margin = new Thickness(1);
                spp.Orientation = Orientation.Horizontal;
                spp.Children.Add(new TextBlock() { Text = "Meaning:", FontSize = 10, Margin = new Thickness(0, 0, 3, 0) });
                spp.Children.Add(Meaning);
                spp.Children.Add(new TextBlock() { Text = "Pron:", FontSize = 10, Margin = new Thickness(0, 0, 3, 0) });
                spp.Children.Add(IsPron);
                spp.Children.Add(new TextBlock() { Text = "Spelling:", FontSize = 10, Margin = new Thickness(0, 0, 3, 0) });
                spp.Children.Add(Spelling);
                sp.Children.Add(spp);
                b.Child = sp;
                return b;
            }
            public Border UpdateVisual()
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
                var sppp = new StackPanel();
                sppp.Margin = new Thickness(1);
                sppp.Orientation = Orientation.Horizontal;
                sppp.Children.Add(new TextBlock() { Text = $"Meaning: ", FontSize = 10, Margin = new Thickness(0, 0, 3, 0) });
                sppp.Children.Add(this.defScore);
                sppp.Children.Add(new TextBlock() { Text = $"Pron: ", FontSize = 10, Margin = new Thickness(0, 0, 3, 0) });
                sppp.Children.Add(this.PronScore);
                sppp.Children.Add(new TextBlock() { Text = $"Spelling: ", FontSize = 10, Margin = new Thickness(0, 0, 3, 0) });
                sppp.Children.Add(this.spelScore);
                sp.Children.Add(sppp);
                var spp = new StackPanel();
                spp.Margin = new Thickness(1);
                spp.Orientation = Orientation.Horizontal;
                spp.Children.Add(new TextBlock() { Text = "Meaning:", FontSize = 10, Margin = new Thickness(0, 0, 3, 0) });
                spp.Children.Add(Meaning);
                spp.Children.Add(new TextBlock() { Text = "Pron:", FontSize = 10, Margin = new Thickness(0, 0, 3, 0) });
                spp.Children.Add(IsPron);
                spp.Children.Add(new TextBlock() { Text = "Spelling:", FontSize = 10, Margin = new Thickness(0, 0, 3, 0) });
                spp.Children.Add(Spelling);
                var bt = new Button() { Content = "delete" };
                bt.Click += (e, s) =>
                 {
                     del = true;
                     b.Background = Brushes.Red;
                 };
                spp.Children.Add(bt);
                sp.Children.Add(spp);
                b.Child = sp;
                return b;
            }
            public (string word, string def, string per, string pron, bool mean, bool sp, bool ispron) Get
                => (this.word.Text, this.def.Text, this.per.Text, this.pron.Text,
                (bool)Meaning.IsChecked, (bool)Spelling.IsChecked, (bool)IsPron.IsChecked);
            public (string word, string def, string per, string pron, bool mean, bool sp, bool ispron, int defscore, int spescore, int pronscore, bool del, Guid id) Update
               => (this.word.Text, this.def.Text, this.per.Text, this.pron.Text,
                (bool)Meaning.IsChecked, (bool)Spelling.IsChecked, (bool)IsPron.IsChecked,
                int.Parse(defScore.Text), int.Parse(spelScore.Text), int.Parse(PronScore.Text), del, id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mod == 0) read();
            else if (mod == 1) readFromDB();
        }
        private void read()
        {
            Words.Children.Clear();
            items.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var sb = new StringBuilder();
                var lines = System.IO.File.ReadAllLines(openFileDialog.FileName);
                foreach (var item in lines)
                {
                    if (item.Contains("**"))
                    {
                        sb.AppendLine(item);
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(item)) continue;
                    var p = breakUp(item);
                    var it = new Item(p.word, p.def, p.per, p.pron);
                    items.Add(it);
                    Words.Children.Add(it.Visual());
                    sb.AppendLine($"{item} **");
                }
                System.IO.File.WriteAllText(openFileDialog.FileName, sb.ToString());
            }
        }
        private void readFromDB()
        {
            Words.Children.Clear();
            items.Clear();
            using (var db = new Dictionary())
            {
                var words = db.GetAll()
                    .OrderBy(x => x.IsMeaning)
                    .ThenBy(x => x.IsPron)
                    .ThenBy(x => x.IsSpelling)
                    .ThenBy(x => x.Meaning)
                    .ThenBy(x => x.PronScore)
                    .ThenBy(x => x.Spelling)
                    .ThenBy(x => x.TheWord).ToList();
                foreach (var item in words)
                {
                    var it = new Item(item);
                    items.Add(it);
                    Words.Children.Add(it.UpdateVisual());
                }
            }
        }

        private (string word, string def, string per, string pron) breakUp(string inp)
        {
            var res = inp.Split('>');
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = res[i].Replace('-', ' ').Trim();
            }
            switch (res.Length)
            {
                case 1:
                    return (res[0], "", "", "");
                case 2:
                    return (res[0], res[1], "", "");
                case 3:
                    return (res[0], res[1], res[2], "");
                case 4:
                    return (res[0], res[1], res[2], res[3]);
                default:
                    return ("", "", "", "");
            }
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
            index = 0;
            if (mod == 0) Insert();
            else if (mod == 1) Update();
        }
        private void Insert()
        {
            var tek = new List<string>();
            int c = 0;
            int c2 = 0;
            using (var db = new Dictionary())
            {
                var words = db.GetAll().ToList();
                c = words.Count;
                foreach (var item in items)
                {
                    if (string.IsNullOrWhiteSpace(item.Get.word)) continue;
                    var p = words.FirstOrDefault(x => x.TheWord.Contains(item.Get.word));
                    if (p is null) p = words.FirstOrDefault(x => item.Get.word.Contains(x.TheWord));
                    if (p is null)
                    {
                        var w = new Word()
                        {
                            TheWord = item.Get.word,
                            Definitions = item.Get.def,
                            Persian = item.Get.per,
                            Pron = item.Get.pron,
                            Meaning = -int.Parse(Cat.Text),
                            Spelling = -int.Parse(Spel.Text),
                            PronScore = -int.Parse(Cat3.Text),
                            IsMeaning = item.Get.mean,
                            IsSpelling = item.Get.sp || (AllSpell.IsChecked ?? false),
                            IsPron = item.Get.ispron
                        };
                        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(w));
                        db.Insert(w);
                    }
                    else
                    {
                        tek.Add(p.TheWord);
                        if (string.IsNullOrWhiteSpace(p.Definitions)) p.Persian = item.Get.def;
                        if (string.IsNullOrWhiteSpace(p.Persian)) p.Persian = item.Get.per;
                        if (string.IsNullOrWhiteSpace(p.Pron)) p.Persian = item.Get.pron;
                        p.Meaning -= (int.Parse(Cat.Text) + 5);
                        db.Update(p);
                    }
                }
                var words2 = db.GetAll().ToList();
                c2 = words2.Count;
            }
            items.Clear();
            Words.Children.Clear();
            MessageBox.Show($"Words from {c} to {c2},{Newtonsoft.Json.JsonConvert.SerializeObject(tek, Newtonsoft.Json.Formatting.Indented)}");
        }
        private void Update()
        {
            using (var db = new Dictionary())
            {
                foreach (var item in items)
                {
                    if (item.Update.del) db.Remove(item.Update.id);
                    else
                    {
                        db.Update(new Word(item.Update.id)
                        {
                            TheWord = item.Update.word,
                            Definitions = item.Update.def,
                            Persian = item.Update.per,
                            Pron = item.Update.pron,
                            IsMeaning = item.Update.mean,
                            IsSpelling = item.Update.sp,
                            Meaning = item.Update.defscore,
                            Spelling = item.Update.spescore,
                            IsPron = item.Update.ispron,
                            PronScore = item.Update.pronscore
                        });
                    }
                }
            }
            items.Clear();
            Words.Children.Clear();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(Words is null))
            {
                Words.Children.Clear();
                items.Clear();
                using (var db = new Dictionary())
                {
                    var words = db.GetAll()
                        .Where(x => x.TheWord.Contains(Search.Text))
                        .OrderBy(x => x.IsMeaning)
                        .ThenBy(x => x.IsPron)
                        .ThenBy(x => x.IsSpelling)
                        .ThenBy(x => x.Meaning)
                        .ThenBy(x => x.PronScore)
                        .ThenBy(x => x.Spelling)
                        .ThenBy(x => x.TheWord).ToList();
                    foreach (var item in words)
                    {
                        var it = new Item(item);
                        items.Add(it);
                        Words.Children.Add(it.UpdateVisual());
                    }
                }
            }
        }
    }
}
