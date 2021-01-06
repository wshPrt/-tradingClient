using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Xh.FastTrading.Wpf.Common.Converters
{
   public class ValueConverter: IValueConverter
    {
        [Flags]
        public enum TypeEnum
        {
            PC端 = 1,
            APP端 = 0,
            系统管理 = 700,
            交易 = 100,
            高级交易 = 200,
            指定交易 = 300,
            风控 = 600,
            单元 = 500,
            查看 = 400
        }

        public class authority_modules
        {
            public TypeEnum State { get; set; }
        }

        public TypeEnum SetValue { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return false;

            var getData = (TypeEnum)value;
            SetValue = getData;
            switch (parameter.ToString())
            {
                case "PC端":
                    if ((getData & TypeEnum.PC端) == TypeEnum.PC端)
                        return true;
                    break;
                case "APP端":
                    if ((getData & TypeEnum.APP端) == TypeEnum.APP端)
                        return true;
                    break;
                case "系统管理":
                    if ((getData & TypeEnum.系统管理) == TypeEnum.系统管理)
                        return true;
                    break;
                case "交易":
                    if ((getData & TypeEnum.系统管理) == TypeEnum.交易)
                        return true;
                    break;
                case "高级交易":
                    if ((getData & TypeEnum.高级交易) == TypeEnum.高级交易)
                        return true;
                    break;
                case "指定交易":
                    if ((getData & TypeEnum.指定交易) == TypeEnum.指定交易)
                        return true;
                    break;
                case "风控":
                    if ((getData & TypeEnum.风控) == TypeEnum.风控)
                        return true;
                    break;
                case "单元":
                    if ((getData & TypeEnum.单元) == TypeEnum.单元)
                        return true;
                    break;
                case "查看":
                    if ((getData & TypeEnum.查看) == TypeEnum.查看)
                        return true;
                    break;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //SetValue = TypeEnum.None;
            if ((bool)value)
            {
                switch (parameter.ToString())
                {
                    case "PC端":
                        SetValue = SetValue | TypeEnum.PC端;
                        break;
                    case "APP端":
                        SetValue = SetValue | TypeEnum.APP端;
                        break;
                    case "系统管理":
                        SetValue = SetValue | TypeEnum.系统管理;
                        break;
                    case "交易":
                        SetValue = SetValue | TypeEnum.交易;
                        break;
                    case "高级交易":
                        SetValue = SetValue | TypeEnum.高级交易;
                        break;
                    case "指定交易":
                        SetValue = SetValue | TypeEnum.指定交易;
                        break;
                    case "风控":
                        SetValue = SetValue | TypeEnum.风控;
                        break;
                    case "单元":
                        SetValue = SetValue | TypeEnum.单元;
                        break;
                    case "查看":
                        SetValue = SetValue | TypeEnum.查看;
                        break;
                }
            }
            switch (parameter.ToString())
            {
                case "PC端":
                    if ((SetValue & TypeEnum.PC端) == TypeEnum.PC端)  //如果存在则移除
                        SetValue = SetValue ^ TypeEnum.PC端;
                    break;
                case "APP端":
                    if ((SetValue & TypeEnum.APP端) == TypeEnum.APP端)  //如果存在则移除
                        SetValue = SetValue ^ TypeEnum.APP端;
                    break;
                case "系统管理":
                    if ((SetValue & TypeEnum.系统管理) == TypeEnum.系统管理)  //如果存在则移除
                        SetValue = SetValue ^ TypeEnum.系统管理;
                    break;
                case "交易":
                    if ((SetValue & TypeEnum.交易) == TypeEnum.交易)  //如果存在则移除
                        SetValue = SetValue ^ TypeEnum.交易;
                    break;
                case "高级交易":
                    if ((SetValue & TypeEnum.高级交易) == TypeEnum.高级交易)  //如果存在则移除
                        SetValue = SetValue ^ TypeEnum.高级交易;
                    break;
                case "指定交易":
                    if ((SetValue & TypeEnum.指定交易) == TypeEnum.指定交易)  //如果存在则移除
                        SetValue = SetValue ^ TypeEnum.指定交易;
                    break;
                case "风控":
                    if ((SetValue & TypeEnum.风控) == TypeEnum.风控)  //如果存在则移除
                        SetValue = SetValue ^ TypeEnum.风控;
                    break;
                case "单元":
                    if ((SetValue & TypeEnum.单元) == TypeEnum.单元)  //如果存在则移除
                        SetValue = SetValue ^ TypeEnum.单元;
                    break;
                case "查看":
                    if ((SetValue & TypeEnum.查看) == TypeEnum.查看)  //如果存在则移除
                        SetValue = SetValue ^ TypeEnum.查看;
                    break;
            }
            return SetValue;
        }
           
    }
}
