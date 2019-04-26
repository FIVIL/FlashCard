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
        public Synonym()
        {
            InitializeComponent();
            Helpers.mainWindow.Height = 350;
            syn = new SpeechSynthesizer();
            dic = new Dictionary();
            rnd = new Random();
            syn.Volume = 100;
            syn.Rate = -2;
            Load()
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Current.Meaning++;
            dic.Update(Current);
            next();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Current.Meaning--;
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
                word.Text = Current.TheWord;
                Definition.Text = string.Empty;
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
                if (string.IsNullOrWhiteSpace(p[0])) Load(0, int.Parse(p[1]));
                else Load(int.Parse(p[0]), int.Parse(p[1]));
            }

        }
        private void Load()
        {
            index = -1;
            Words = dic.GetAll().ToList();
            Words = Words.OrderBy(x => rnd.Next(Words.Count)).ToList();
            next();
        }
        private void Load(int max)
        {
            index = -1;
            Words = dic.GetAll().ToList();
            Words = Words.OrderBy(x => rnd.Next(Words.Count)).Take(max).ToList();
            next();
        }
        private void Load(int max, int diff)
        {
            index = -1;
            Words = dic.GetWord(x => x.Meaning < diff).ToList();
            if (max == 0) Words = Words.OrderBy(x => rnd.Next(Words.Count)).ToList();
            else Words = Words.OrderBy(x => rnd.Next(Words.Count)).Take(max).ToList();
            next();
        }
    }
}
