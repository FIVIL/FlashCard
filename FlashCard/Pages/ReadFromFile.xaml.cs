﻿using FlashCard.Model;
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
                Spelling = new CheckBox();
                Spelling.Margin = new Thickness(0, 0, 3, 0);
                Meaning = new CheckBox();
                Meaning.Margin = new Thickness(0, 0, 3, 0);
                Meaning.IsChecked = true;

            }
            private readonly TextBox word;
            private readonly TextBox def;
            private readonly TextBox per;
            private readonly TextBox pron;
            private readonly CheckBox Meaning;
            private readonly CheckBox Spelling;
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
                spp.Children.Add(new TextBlock() { Text = "Spelling:", FontSize = 10, Margin = new Thickness(0, 0, 3, 0) });
                spp.Children.Add(Spelling);
                sp.Children.Add(spp);
                b.Child = sp;
                return b;
            }
            public (string word, string def, string per, string pron, bool mean, bool sp) Get
                => (this.word.Text, this.def.Text, this.pron.Text, this.pron.Text, (bool)Meaning.IsChecked, (bool)Spelling.IsChecked);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            read();
        }
        private void read()
        {
            Words.Children.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var sb = new StringBuilder();
                var lines = System.IO.File.ReadAllLines(openFileDialog.FileName);
                foreach (var item in lines)
                {
                    if (item.Contains("**")) continue;
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
            using (var db = new Dictionary())
            {
                var words = db.GetAll().ToList();
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
                            Meaning = 0,
                            Spelling = 0,
                            IsMeaning = item.Get.mean,
                            IsSpelling = item.Get.sp
                        };
                        db.Insert(w);
                    }
                    else
                    {
                        p.Meaning += 5;
                        db.Update(p);
                    }
                }
            }
            items.Clear();
            Words.Children.Clear();
        }
    }
}
