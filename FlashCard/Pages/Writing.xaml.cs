﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace FlashCard.Pages
{
    /// <summary>
    /// Interaction logic for Writing.xaml
    /// </summary>
    public partial class Writing : UserControl
    {
        SpeechSynthesizer syn;
        public Writing()
        {
            InitializeComponent();
            Helpers.mainWindow.Height = 800;
            Helpers.mainWindow.Width = 1100;
            Helpers.mainWindow.Top = 10;
            syn = new SpeechSynthesizer();
            syn.Rate = -8;
            syn.SelectVoiceByHints(VoiceGender.Female);
            T = new DispatcherTimer();
            T.Interval = TimeSpan.FromMilliseconds(10);
            T.Tick += (a, c) =>
            {
                if (sp != null)
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        Clock.Text = sp.Elapsed.ToString();
                    }));
                }
            };
        }
        Stopwatch sp = null;
        DispatcherTimer T = null;
        string text;
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            T.Stop();
            text = Original.Text;
            syn.SpeakAsyncCancelAll();
            var spp = text.Split('.', ',');
            sp = new Stopwatch();
            sp.Start();
            T.Start();
            Keyboard.Focus(Type);
            Type.Focus();
            await Task.Delay(2000);
            foreach (var item in spp)
            {
                syn.SpeakAsync(item);
                if (item.Length < 20)
                {

                }
                else if (item.Length >= 20 && item.Length <= 50)
                    syn.SpeakAsync(item);
                else
                {
                    syn.SpeakAsync(item);
                    syn.SpeakAsync(item);
                }
                await Task.Delay(3000);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            syn.SpeakAsyncCancelAll();
            syn.Dispose();
            T.Stop();
        }
        bool pr = false;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (pr)
            {
                syn.Resume();
                sp.Start();
            }
            else
            {
                syn.Pause();
                sp.Stop();
            }
            pr = !pr;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            T.Stop();
            sp.Stop();
            syn.SpeakAsyncCancelAll();
            var t1 = text.Split(' ');
            var t2 = Type.Text.Split(' ');
            var prob = new StringBuilder();
            if (t1.Length == t2.Length)
            {
                for (int i = 0; i < t1.Length; i++)
                {
                    if (!t1[i].Equals(t2[i], StringComparison.InvariantCultureIgnoreCase))
                        prob.AppendLine($"{t2[i]} -> {t1[i]}");
                }
                MessageBox.Show(prob.ToString());
            }
            else MessageBox.Show("missmatch");
        }
        bool p2 = false;
        private void Type_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift)
            {
                if (!p2)
                    syn.Pause();
                else syn.Resume();
                p2 = !p2;
            }
        }
    }
}
