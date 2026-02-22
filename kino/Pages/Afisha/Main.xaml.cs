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

namespace kino.Pages.Afisha
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        List<AfishaContext> AllAfishas = AfishaContext.Select();

        public Main()
        {
            InitializeComponent();

            foreach (AfishaContext item in AllAfishas)
            {
                parent.Children.Add(new Items.Item(item, this));
            }
        }

        private void AddRecord(object sender, System.Windows.RoutedEventArgs e) =>
            MainWindow.init.OpenPage(new Pages.Afisha.Add());
    }
}
