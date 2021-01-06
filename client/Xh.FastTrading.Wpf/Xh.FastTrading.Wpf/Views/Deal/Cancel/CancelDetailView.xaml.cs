using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xh.FastTrading.Wpf.Model;
using Xh.FastTrading.Wpf.ViewModel.Deal;

namespace Xh.FastTrading.Wpf.Views
{
    /// <summary>
    /// CancelDetailView.xaml 的交互逻辑
    /// </summary>
    public partial class CancelDetailView : UserControl
    {
        public CancelDetailView()
        {
            InitializeComponent();
            
        }


        //获取并选中DependencyObject中的CheckBox
        DealCancelOrderModel obj = new DealCancelOrderModel();
        public void GetVisualChild(DependencyObject parent)
        {
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                DependencyObject v = (DependencyObject)VisualTreeHelper.GetChild(parent, i);
                CheckBox child = v as CheckBox;

                if (child == null)
                {
                    GetVisualChild(v);
                }
                else
                {
                    child.IsChecked = true;
                    
                    //obj.IsSelected = child.IsChecked;
                    return;
                }
            }
        }
        public void GetVisualClean(DependencyObject parent)
        {
           

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                DependencyObject v = (DependencyObject)VisualTreeHelper.GetChild(parent, i);
                CheckBox child = v as CheckBox;

                if (child == null)
                {
                    GetVisualClean(v);
                }
                else
                {
                    child.IsChecked = false;
                    return;
                }
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelected_Click(object sender, RoutedEventArgs e)
        {
            GetVisualChild(dataGridFert);
           // var a = dataGridFert.SelectedItem;
        }
        /// <summary>
        /// 全清
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllClean_Click(object sender, RoutedEventArgs e)
        {
            GetVisualClean(dataGridFert);
        }
 
    }
}
