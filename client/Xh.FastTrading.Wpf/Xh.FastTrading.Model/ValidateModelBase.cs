using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Model
{

    public class ValidateModelBase: ObservableObject,IDataErrorInfo
    {
        public ValidateModelBase() 
        {

        }
        /// <summary>
        /// 表当验证错误集合
        /// </summary>
        public Dictionary<String, String> dataErrors = new Dictionary<String, String>();

        /// 是否验证错误集合
        /// <summary>
        /// </summary>
        public Boolean IsValidated
        {
            get
            {
                if (dataErrors != null && dataErrors.Count > 0)
                {
                    return false;
                }
                return true;
            }
        }


        public string this[string columnName]
        {
            get
            {
                ValidationContext vc = new ValidationContext(this, null, null);
                vc.MemberName = columnName;
                var res = new List<ValidationResult>();
                var result = Validator.TryValidateProperty(this.GetType().GetProperty(columnName).GetValue(this, null), vc, res);
                if (res.Count > 0)
                {
                    String errorInfo = string.Join(Environment.NewLine, res.Select(r => r.ErrorMessage).ToArray());
                    AddDic(dataErrors, columnName, errorInfo);
                    return errorInfo;
                }
                RemoveDic(dataErrors, vc.MemberName);
                return null;
            }
        }
        public string Errors
        {
            get { return null; }
        }

        public string Error => throw new NotImplementedException();


        /// <summary>
        /// 移除字典
        /// </summary>
        /// <param name="dics"></param>
        /// <param name="dicKey"></param>
        private void RemoveDic(Dictionary<String, String> dics, String dicKey)
        {
            dics.Remove(dicKey);
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="dics"></param>
        /// <param name="dicKey"></param>
        private void AddDic(Dictionary<String, String> dics, String dicKey, string dicValue)
        {
            if (!dics.ContainsKey(dicKey)) dics.Add(dicKey, dicValue);
        }
    }
}
