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
using kino.Modell;

namespace kino.Pages.Afisha.Items
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        List<KinoteatrContext> AllKinoteatrs = KinoteatrContext.Select();
        AfishaContext item;
        Main main;

        public object Afisha { get; private set; }

        public Item(AfishaContext item, Main main)
        {
            InitializeComponent();

            var foundKinoteatr = AllKinoteatrs.FirstOrDefault(x => x.Id == item.IdKinoteatr);
            if (foundKinoteatr != null)
            {
                kinoteatrs.Text = foundKinoteatr.Name;
            }
            else
            {
                kinoteatrs.Text = "Кинотеатр не найден";
            }

            name.Text = item.Name;
            date.Text = item.Time.ToString("yyyy-MM-dd");
            time.Text = item.Time.ToString("HH:mm");
            price.Text = item.Price.ToString();

            this.item = item;
            this.main = main;
        }

        private void EditRecord(object sender, System.Windows.RoutedEventArgs e) =>
            MainWindow.init.OpenPage(new Pages.Afisha.Add(this.item));

        private void DeleteRecord(object sender, System.Windows.RoutedEventArgs e)
        {
            item.Delete();
            main.parent.Children.Remove(this);
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {

        }

        private void BuyTicket(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Afisha.BuyTicket(this.item));
        }
    }
}