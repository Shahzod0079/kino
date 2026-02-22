using System;
using System.Collections.Generic;
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

        public Add(AfishaContext afisha)
        {
            InitializeComponent();
            kinoteatrs.ItemsSource = AllKinoteatrs;
            kinoteatrs.DisplayMemberPath = "Name";
            kinoteatrs.SelectedValuePath = "Id";

            this.afisha = afisha;

            // Заполняем поля данными
            name.Text = afisha.Name;
            date.SelectedDate = afisha.Time;
            time.Text = afisha.Time.ToString("HH:mm");
            price.Text = afisha.Price.ToString();

            // Выбираем нужный кинотеатр
            foreach (var k in AllKinoteatrs)
            {
                if (k.Id == afisha.IdKinoteatr)
                {
                    kinoteatrs.SelectedItem = k;
                    break;
                }
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
                // Добавление
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
                // Обновление
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
        }
    }
}