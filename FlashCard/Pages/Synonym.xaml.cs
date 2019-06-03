using FlashCard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
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
    /// Interaction logic for Synonym.xaml
    /// </summary>
    public partial class Synonym : UserControl
    {
        private List<Word> Words;
        private Word Current => Words[index];
        private int index;
        private Dictionary dic;
        private SpeechSynthesizer syn;
        private Random rnd;
        private int mod;
        public Synonym(int mod)
        {
            InitializeComponent();
            this.mod = mod;
            Helpers.mainWindow.Height = 350;
            syn = new SpeechSynthesizer();
            dic = new Dictionary();
            rnd = new Random();
            syn.Volume = 100;
            syn.Rate = -2;
            Load();
            Unloaded += (e, s) =>
            {
                dic.Dispose();
                syn.Dispose();
            };
            if (mod == 0) Mode.Text = "Synonym";
            else if (mod == 2) Mode.Text = "Reverse Synonym";
            else if (mod == 1) Mode.Text = "Spelling";
            else if (mod == 3) Mode.Text = "Pronunciation";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mod == 0) Current.Meaning++;
            else if (mod == 1) Current.Spelling++;
            else if (mod == 2) Current.Meaning++;
            else if (mod == 3) Current.PronScore++;
            dic.Update(Current);
            next();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (mod == 0) Current.Meaning--;
            else if (mod == 1) Current.Spelling--;
            else if (mod == 2) Current.Meaning--;
            else if (mod == 3) Current.PronScore--;
            dic.Update(Current);
            next();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            syn.SpeakAsync(Current.TheWord);
        }

        private void next()
        {
            if (index + 1 < Words.Count)
            {
                index++;
                if (mod == 0)
                {
                    word.Text = Current.TheWord;
                    Definition.Text = string.Empty;
                }
                else if (mod == 1)
                {
                    word.Text = string.Empty;
                    syn.SpeakAsync(Current.TheWord);
                    Definition.Text = string.Empty;
                }
                else if (mod == 2)
                {
                    word.Text = string.Empty;
                    Definition.Text = Current.Definitions;
                }
                else if (mod == 3)
                {
                    word.Text = Current.TheWord;
                    Definition.Text = string.Empty;
                }
                Persian.Text = string.Empty;
                Pron.Text = string.Empty;
                MeaningScore.Text = Current.Meaning.ToString();
                SpellScore.Text = Current.Spelling.ToString();
                Counter.Text = $"{index} from {Words.Count}";
            }
            else MessageBox.Show("ended");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Definition.Text = Current.Definitions;
            Persian.Text = Current.Persian;
            Pron.Text = Current.Pron;
            word.Text = Current.TheWord;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var ci = CountImportance.Text;
            if (string.IsNullOrWhiteSpace(ci))
            {
                Load();
            }
            else if (!ci.Contains(':'))
            {
                Load(int.Parse(ci));
            }
            else
            {
                var p = ci.Split(':');
                if (p.Length == 2)
                {
                    if (string.IsNullOrWhiteSpace(p[0])) Load(0, int.Parse(p[1]));
                    else Load(int.Parse(p[0]), int.Parse(p[1]));
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(p[0]) && string.IsNullOrWhiteSpace(p[1])) Load(0, 1, int.Parse(p[2]));
                    else if (string.IsNullOrWhiteSpace(p[1])) Load(int.Parse(p[0]), 1, int.Parse(p[2]));
                    else if (string.IsNullOrWhiteSpace(p[0])) Load(0, int.Parse(p[1]), int.Parse(p[2]));
                }
            }

        }
        private void LoadBasic()
        {
            index = -1;
            if (mod == 0) Words = dic.GetWord(x => x.IsMeaning).ToList();
            else if (mod == 1) Words = dic.GetWord(x => x.IsSpelling).ToList();
            else if (mod == 2) Words = dic.GetWord(x => x.IsMeaning).ToList();
            else if (mod == 3) Words = dic.GetWord(x => x.IsPron).ToList();
        }
        private void LoadBasic(int diff)
        {
            index = -1;
            if (mod == 0) Words = dic.GetWord(x => x.Meaning < diff && x.IsMeaning).ToList();
            else if (mod == 1) Words = dic.GetWord(x => x.IsSpelling && x.Spelling < diff).ToList();
            else if (mod == 2) Words = dic.GetWord(x => x.Meaning < diff && x.IsMeaning).ToList();
            else if (mod == 3) Words = dic.GetWord(x => x.PronScore < diff && x.IsPron).ToList();
        }
        private void LoadBasic(int diffMin, int diffMax)
        {
            index = -1;
            if (mod == 0) Words = dic.GetWord(x => x.Meaning < diffMin && x.IsMeaning && x.Meaning > diffMax).ToList();
            else if (mod == 1) Words = dic.GetWord(x => x.IsSpelling && x.Spelling < diffMin && x.Spelling > diffMax).ToList();
            else if (mod == 2) Words = dic.GetWord(x => x.Meaning < diffMin & x.Meaning > diffMax && x.IsMeaning).ToList();
            else if (mod == 3) Words = dic.GetWord(x => x.PronScore < diffMin && x.PronScore > diffMax && x.IsPron).ToList();
        }
        private void Load()
        {

            LoadBasic();
            Words = Words.OrderBy(x => rnd.Next(Words.Count)).ToList();
            next();
        }
        private void Load(int max)
        {
            LoadBasic();
            Words = Words.OrderBy(x => rnd.Next(Words.Count)).Take(max).ToList();
            next();
        }
        private void Load(int max, int diff)
        {
            LoadBasic(diff);
            if (max == 0) Words = Words.OrderBy(x => rnd.Next(Words.Count)).ToList();
            else Words = Words.OrderBy(x => rnd.Next(Words.Count)).Take(max).ToList();
            next();
        }
        private void Load(int max, int diffMin, int diffMax)
        {
            LoadBasic(diffMin, diffMax);
            if (max == 0) Words = Words.OrderBy(x => rnd.Next(Words.Count)).ToList();
            else Words = Words.OrderBy(x => rnd.Next(Words.Count)).Take(max).ToList();
            next();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (index > 0)
            {
                index--;
                if (mod == 0)
                {
                    word.Text = Current.TheWord;
                    Definition.Text = string.Empty;
                }
                else if (mod == 1)
                {
                    word.Text = string.Empty;
                    syn.SpeakAsync(Current.TheWord);
                    Definition.Text = string.Empty;
                }
                else if (mod == 2)
                {
                    word.Text = string.Empty;
                    Definition.Text = Current.Definitions;
                }
                else if (mod == 3)
                {
                    word.Text = Current.TheWord;
                    Definition.Text = string.Empty;
                }
                Persian.Text = string.Empty;
                Pron.Text = string.Empty;
                MeaningScore.Text = Current.Meaning.ToString();
                SpellScore.Text = Current.Spelling.ToString();
                Counter.Text = $"{index} from {Words.Count}";
            }
            else MessageBox.Show("ended");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            next();
        }
    }
}
