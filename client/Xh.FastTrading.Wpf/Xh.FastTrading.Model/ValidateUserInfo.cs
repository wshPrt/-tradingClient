using System;
using System.ComponentModel.DataAnnotations;

namespace Xh.FastTrading.Wpf.Model
{
    // [MetadataType(typeof(ValidateExceptionVM))]
    public class ValidateUserInfo: ValidateModelBase
    {

        /// <summary>
        /// ID
        /// </summary>
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Token
        /// </summary>
        private string token;
        public string Token
        {
            get { return token; }
            set { token = value; RaisePropertyChanged(() => Token); }
        }

        /// <summary>
        /// 登录名
        /// </summary>
        [Required]
        private String userName;
        public String UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                RaisePropertyChanged(() => UserName);
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new AccessViolationException("用户名不能为空!");
                //}
            }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged(() => Name);
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new AccessViolationException("姓名不能为空！");
                //}
            }
        }

        /// <summary>
        /// 手机
        /// </summary>
        [Required]
        private string phone;
        [RegularExpression(@"^[-]?[1-9]{8,11}\d*$|^[0]{1}$", ErrorMessage = "用户电话必须为8-11位的数值.")]
        public string Phone
        {
            get { return phone; }
            set { phone = value; RaisePropertyChanged(() => Phone);}
        }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码!")]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).*$")]
        [StringLength(maximumLength: 15, ErrorMessage = "Minimum 8 and maximum 15 characters.", MinimumLength = 8)]
        private String password;
        public String Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(() => Password); }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [RegularExpression("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$", ErrorMessage = "请填写正确的邮箱地址.")]
        private String email;
        public String Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged(() => Email);}
        }

    }
}
