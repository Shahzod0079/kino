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
using kino.Classes;

namespace kino.Pages.Kinoteatr.Items
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        KinoteatrContext Kinoteatr;
        Main main;

        public Item(KinoteatrContext kinoteatr, Main main = null)
        {
            InitializeComponent();

            name.Text = kinoteatr.Name;
            countZal.Text = kinoteatr.CountZal.ToString();
            Count.Text = kinoteatr.Count.ToString();

            this.Kinoteatr = kinoteatr;
            this.main = main;
        }

        private void EditRecord(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Kinoteatr.Add(this.Kinoteatr));
        }

        private void DeleteRecord(object sender, System.Windows.RoutedEventArgs e)
        {
            Kinoteatr.Delete();

            if (main != null)
            {
                main.parent.Children.Remove(this);
            }
            else
            {
                MainWindow.init.OpenPage(new Pages.Kinoteatr.Main());
            }
        }
    }
}