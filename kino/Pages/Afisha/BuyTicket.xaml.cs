using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using kino.Classes;

namespace kino.Pages.Afisha
{
    public partial class BuyTicket : Page
    {
        AfishaContext selectedFilm;

        public BuyTicket(AfishaContext film)
        {
            InitializeComponent();
            selectedFilm = film;

            txtFilm.Text = film.Name;
            txtKinoteatr.Text = GetKinoteatrName(film.IdKinoteatr);
            txtDateTime.Text = film.Time.ToString("dd.MM.yyyy HH:mm");
            txtPrice.Text = film.Price.ToString();

            for (int i = 1; i <= 30; i++)
            {
                cmbSeat.Items.Add(i);
            }
            cmbSeat.SelectedIndex = 0;
        }

        private string GetKinoteatrName(int id)
        {
            var kinoteatr = KinoteatrContext.Select().FirstOrDefault(k => k.Id == id);
            return kinoteatr != null ? kinoteatr.Name : "Неизвестный кинотеатр";
        }
        private void BuyTicket_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClientName.Text))
            {
                MessageBox.Show("Введите имя покупателя!");
                return;
            }

            if (cmbSeat.SelectedItem == null)
            {
                MessageBox.Show("Выберите место!");
                return;
            }

            TicketContext newTicket = new TicketContext(
                0,  
                selectedFilm.Id,
                txtClientName.Text,
                (int)cmbSeat.SelectedItem,
                DateTime.Now,
                selectedFilm.Price,
                true
            );

            newTicket.Add();

            MessageBox.Show("Билет успешно сохранен!");
            MainWindow.init.OpenPage(new Main());
        }
    }
}