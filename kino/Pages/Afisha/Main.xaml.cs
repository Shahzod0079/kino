using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using kino.Classes;

namespace kino.Pages.Afisha
{
    public partial class Main : Page
    {
        List<AfishaContext> AllAfishas;
        List<KinoteatrContext> AllKinoteatrs;
        private List<AfishaContext> currentDisplayList;


        public Main()
        {
            InitializeComponent();

            LoadData();
            LoadKinoteatrs();
        }

        private void LoadData()
        {
            AllAfishas = AfishaContext.Select();
            DisplayAfishas(AllAfishas);
        }

        private void LoadKinoteatrs()
        {
            AllKinoteatrs = KinoteatrContext.Select();
            cmbKinoteatr.ItemsSource = AllKinoteatrs;
            cmbKinoteatr.DisplayMemberPath = "Name";
            cmbKinoteatr.SelectedValuePath = "Id";
            cmbKinoteatr.SelectedIndex = -1;

        }

        private void DisplayAfishas(List<AfishaContext> afishas)
        {
            parent.Children.Clear();
            foreach (AfishaContext item in afishas)
            {
                parent.Children.Add(new Items.Item(item, this));
            }
        }

        private void ApplyFilter(object sender, RoutedEventArgs e)
        {
            List<AfishaContext> filteredList = new List<AfishaContext>();

            foreach (AfishaContext a in AllAfishas)
            {
                bool matches = true;
                if (!string.IsNullOrWhiteSpace(txtName.Text))
                {
                    if (!a.Name.ToLower().Contains(txtName.Text.ToLower()))
                        matches = false;
                }
                if (matches && cmbKinoteatr.SelectedItem != null)
                {
                    var selectedKinoteatr = cmbKinoteatr.SelectedItem as KinoteatrContext;
                    if (a.IdKinoteatr != selectedKinoteatr.Id)
                        matches = false;
                }
                if (matches && dpDateFrom.SelectedDate.HasValue)
                {
                    if (a.Time.Date < dpDateFrom.SelectedDate.Value.Date)
                        matches = false;
                }
                if (matches && dpDateTo.SelectedDate.HasValue)
                {
                    if (a.Time.Date > dpDateTo.SelectedDate.Value.Date)
                        matches = false;
                }
                if (matches && int.TryParse(txtPriceFrom.Text, out int priceFrom))
                {
                    if (a.Price < priceFrom)
                        matches = false;
                }
                if (matches && int.TryParse(txtPriceTo.Text, out int priceTo))
                {
                    if (a.Price > priceTo)
                        matches = false;
                }

                if (matches)
                {
                    filteredList.Add(a);
                }
            }

            DisplayAfishas(filteredList);
        }
        private void ClearFilter(object sender, RoutedEventArgs e)
        {
            txtName.Text = "";
            cmbKinoteatr.SelectedIndex = -1;
            dpDateFrom.SelectedDate = null;
            dpDateTo.SelectedDate = null;
            txtPriceFrom.Text = "";
            txtPriceTo.Text = "";

            currentDisplayList = AllAfishas;
            DisplayAfishas(AllAfishas);
        }
        private void ApplySort(object sender, RoutedEventArgs e)
        {
            if (currentDisplayList == null) return;
            
            List<AfishaContext> sortedList = new List<AfishaContext>(currentDisplayList);

            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    sortedList = sortedList.OrderBy(a => a.Name).ToList();
                    break;
                case 1:
                    sortedList = sortedList.OrderBy(a => a.Time).ToList();
                    break;
                case 2: 
                    sortedList = sortedList.OrderBy(a => a.Price).ToList();
                    break;
                case 3: 
                    sortedList = sortedList.OrderByDescending(a => a.Price).ToList();
                    break;
            }

            DisplayAfishas(sortedList);
        }
        private void AddRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Add());
        }

        public void RefreshData()
        {
            LoadData();
        }
    }
}