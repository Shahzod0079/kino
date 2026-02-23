using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using kino.Classes;

namespace kino.Pages.Afisha
{
    public partial class Add : Page
    {
        List<KinoteatrContext> AllKinoteatrs = KinoteatrContext.Select();
        AfishaContext afisha;

        public Add()
        {
            InitializeComponent();
            kinoteatrs.ItemsSource = AllKinoteatrs;
            kinoteatrs.DisplayMemberPath = "Name";
            kinoteatrs.SelectedValuePath = "Id";
            btnAdd.Content = "Добавить";
        }

        // В конструкторе Add(AfishaContext afisha)
        public Add(AfishaContext afisha)
        {
            InitializeComponent();
            kinoteatrs.ItemsSource = AllKinoteatrs;
            kinoteatrs.DisplayMemberPath = "Name";
            kinoteatrs.SelectedValuePath = "Id";

            this.afisha = afisha;

            // Заполняем поля
            name.Text = afisha.Name;
            date.SelectedDate = afisha.Time;
            time.Text = afisha.Time.ToString("HH:mm");
            price.Text = afisha.Price.ToString();

            // ВЫБИРАЕМ КИНОТЕАТР В КОМБОБОКСЕ (ИСПРАВЛЕНО)
            var selectedKinoteatr = AllKinoteatrs.FirstOrDefault(x => x.Id == afisha.IdKinoteatr);
            if (selectedKinoteatr != null)
            {
                kinoteatrs.SelectedItem = selectedKinoteatr;  // .SelectedItem, а не .Text!
            }

            btnAdd.Content = "Изменить";
        }


        private void AddRecord(object sender, RoutedEventArgs e)
        {
            TimeSpan timeAfisha;
            int Price;

            // Проверки
            if (string.IsNullOrWhiteSpace(name.Text))
            {
                MessageBox.Show("Необходимо указать наименование");
                return;
            }

            if (kinoteatrs.SelectedItem == null)
            {
                MessageBox.Show("Выберите кинотеатр");
                return;
            }

            if (!date.SelectedDate.HasValue)
            {
                MessageBox.Show("Необходимо указать дату");
                return;
            }

            if (!TimeSpan.TryParse(time.Text, out timeAfisha))
            {
                MessageBox.Show("Необходимо указать время в формате ЧЧ:мм");
                return;
            }

            if (!int.TryParse(price.Text, out Price))
            {
                MessageBox.Show("Необходимо указать стоимость");
                return;
            }

            DateTime fullDateTime = date.SelectedDate.Value.Date + timeAfisha;
            var selectedKinoteatr = kinoteatrs.SelectedItem as KinoteatrContext;

            if (afisha == null)
            {

                AfishaContext newAfisha = new AfishaContext(
                    0,
                    selectedKinoteatr.Id,
                    name.Text,
                    fullDateTime,
                    Price);
                newAfisha.Add();
                MessageBox.Show("Запись успешно добавлена");
            }
            else
            {
                afisha = new AfishaContext(
                    afisha.Id,
                    selectedKinoteatr.Id,
                    name.Text,
                    fullDateTime,
                    Price);
                afisha.Update();
                MessageBox.Show("Запись успешно обновлена");
            }

            MainWindow.init.OpenPage(new Pages.Afisha.Main());


            if (MainWindow.init.frame.Content is Main mainPage)
            {
                mainPage.RefreshData();
                MainWindow.init.frame.NavigationService.GoBack();
            }
        }
    }
}