using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    public class SqlServerSupport
    {
        /// <summary>
        /// 数据库的连接字符串，此处可以选择加个密，防止被反编译破解密码
        /// </summary>
        public static string SqlConnectStr { get; } =
            "Data Source = 127.0.0.1;Initial Catalog = SQL106;User Id = hsl;Password = 123456789;";



    }
}
