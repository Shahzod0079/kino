using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using kino.Classes;

namespace kino.Pages.Kinoteatr
{
    public partial class Filter : Page
    {
        public delegate void FilterAppliedHandler(KinoteatrFilter filter);
        public event FilterAppliedHandler FilterApplied;

        public Filter()
        {
            InitializeComponent();
        }

        private void ApplyFilter(object sender, RoutedEventArgs e)
        {
            KinoteatrFilter filter = new KinoteatrFilter();

            // Название
            filter.Name = txtName.Text;

            // Залы
            if (int.TryParse(txtMinZal.Text, out int minZal))
                filter.MinCountZal = minZal;
            if (int.TryParse(txtMaxZal.Text, out int maxZal))
                filter.MaxCountZal = maxZal;

            // Места
            if (int.TryParse(txtMinCount.Text, out int minCount))
                filter.MinCount = minCount;
            if (int.TryParse(txtMaxCount.Text, out int maxCount))
                filter.MaxCount = maxCount;

            FilterApplied?.Invoke(filter);
        }

        private void ClearFilter(object sender, RoutedEventArgs e)
        {
            txtName.Text = "";
            txtMinZal.Text = "";
            txtMaxZal.Text = "";
            txtMinCount.Text = "";
            txtMaxCount.Text = "";
        }
    }
}