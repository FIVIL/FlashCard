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
    public partial class Synonym : UserControl, IDisposable
    {
        private List<Word> Words;
        private Word Current => Words[index];
        private int index;
        private Dictionary dic;
        private SpeechSynthesizer syn;
        public Synonym()
        {
            InitializeComponent();
            index = -1;
            dic = new Dictionary();
            Words = dic.GetAll().ToList();
            var rnd = new Random();
            Words = Words.OrderBy(x => rnd.Next(Words.Count)).ToList();
            syn = new SpeechSynthesizer();
            syn.Volume = 100;
            syn.Rate = -2;
            next();
            Helpers.mainWindow.Height = 350;
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
            }
            else MessageBox.Show("ended");
        }

        public void Dispose()
        {
            ((IDisposable)dic).Dispose();
            syn.Dispose();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Definition.Text = Current.Definitions;
            Persian.Text = Current.Persian;
            Pron.Text = Current.Pron;
        }
    }
}
