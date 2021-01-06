using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xh.FastTrading.Wpf.ViewModel;
using System.Windows;

namespace Xh.FastTrading.Wpf.Common.CommonHelper
{
  public static  class CommonHelper
    {
        public static T GetViewModel<T>()
        {
            var locator = Application.Current.Resources["Locator"] as ViewModelLocator;
            if (locator != null) return locator.GetViewModel<T>();
            return default(T);
        }
    }
}
