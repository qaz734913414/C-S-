using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// SQL DATEBASE 兼容的账户类
    /// 由于该账户表已在数据表中存在，故而使用本账户类
    /// </summary>
    public class Account : BasicFramework.ISqlDataType
    {
        public string 姓名 { get; set; } = string.Empty;
        public string 密码 { get; set; } = string.Empty;
        public string 科名 { get; set; } = string.Empty;
        public string 职位 { get; set; } = string.Empty;
        public DateTime 注册日期 { get; set; } = DateTime.Now;
        public string 移动短号 { get; set; } = string.Empty;
        public string 移动长号 { get; set; } = string.Empty;

        public void LoadBySqlDataReader(SqlDataReader sdr)
        {
            姓名 = sdr[nameof(姓名)].ToString();
            密码 = sdr[nameof(密码)].ToString();
            科名 = sdr[nameof(科名)].ToString();
            职位 = sdr[nameof(职位)].ToString();
            注册日期 = Convert.ToDateTime(sdr[nameof(注册日期)]);
            移动短号 = sdr[nameof(移动短号)].ToString();
            移动长号 = sdr[nameof(移动长号)].ToString();
        }

        public override string ToString()
        {
            return 姓名;
        }

        /// <summary>
        /// SQL语句，获取所有的账户信息
        /// </summary>
        public static string SqlSelected { get; } = "SELECT * FROM DBO.装备中心人员信息表1";
    }
}
