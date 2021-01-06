using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Xh.FastTrading.Wpf.Converters
{
    public class TreeMarginConverter : IValueConverter
    {
        public double Length { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as TreeViewItem;
            if (item == null)
                return new Thickness(0);

            FrameworkElement elem = item;
            var parent = VisualTreeHelper.GetParent(item);
            var count = 0;
            while (parent != null && !(parent is TreeView))
            {
                var tvi = parent as TreeViewItem;
                if (parent is TreeViewItem)
                    count++;
                parent = VisualTreeHelper.GetParent(parent);
            }

            return new Thickness(Length * count, 0, 0, 0);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
             throw new System.NotImplementedException();
        }
    }
}