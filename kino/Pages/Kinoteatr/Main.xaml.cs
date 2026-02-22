using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using kino.Classes;

namespace kino.Pages.Kinoteatr
{
    public partial class Main : Page
    {
        List<KinoteatrContext> AllKinoteatrs;

        public Main()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            AllKinoteatrs = KinoteatrContext.Select();
            DisplayKinoteatrs(AllKinoteatrs);
        }

        private void DisplayKinoteatrs(List<KinoteatrContext> kinoteatrs)
        {
            parent.Children.Clear();
            foreach (KinoteatrContext item in kinoteatrs)
            {

                parent.Children.Add(new Items.Item(item, this));
            }
        }

        private void ApplyFilter(object sender, RoutedEventArgs e)
        {
            List<KinoteatrContext> filteredList = new List<KinoteatrContext>();

            foreach (KinoteatrContext k in AllKinoteatrs)
            {
                bool matches = true;

                if (!string.IsNullOrWhiteSpace(txtName.Text))
                    if (!k.Name.ToLower().Contains(txtName.Text.ToLower()))
                        matches = false;

                if (matches && int.TryParse(txtMinZal.Text, out int minZal))
                    if (k.CountZal < minZal)
                        matches = false;

                if (matches && int.TryParse(txtMaxZal.Text, out int maxZal))
                    if (k.CountZal > maxZal)
                        matches = false;

                if (matches)
                    filteredList.Add(k);
            }

            DisplayKinoteatrs(filteredList);
        }

        private void ClearFilter(object sender, RoutedEventArgs e)
        {
            txtName.Text = "";
            txtMinZal.Text = "";
            txtMaxZal.Text = "";
            DisplayKinoteatrs(AllKinoteatrs);
        }

        private void AddRecord(object sender, RoutedEventArgs e) =>
            MainWindow.init.OpenPage(new Add());
    }
}