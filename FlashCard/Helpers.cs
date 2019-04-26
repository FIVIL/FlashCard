using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FlashCard
{
    public static class Helpers
    {
        public static Grid mainGrid { get; set; }
        public static MainWindow mainWindow { get; set; }
        public static void Navigate(UserControl userControl)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(userControl);
        }
    }
}
