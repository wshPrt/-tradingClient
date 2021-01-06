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

namespace Xh.FastTrading.Wpf.Views
{
    /// <summary>
    /// PurchaseView.xaml 的交互逻辑
    /// </summary>
    public partial class BuyPurchaseView :UserControl 
    {
        public BuyPurchaseView()
        {
            InitializeComponent();
        }

        private void tb3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue != 0)
            {
                double a = Math.Round(e.NewValue); 
                if (a != 0 && a >= 1)
                {
                    txtPercentage.Text = (a/100).ToString("P2");
                }
                 var Percentage = Convert.ToDouble(txtPercentage.Text.Replace("%", "")) / 100; 
                 txtTage.Text =(Double.Parse(lblMaxBuyNumber.Content.ToString()) * Percentage).ToString();
            }
        }
    }
 }
