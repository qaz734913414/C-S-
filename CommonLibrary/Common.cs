using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    public class CommonLibrary
    {
        #region 公用端口设计块

        //======================================================================================
        //    此处的所有的网络端口应该重新指定，防止其他人的项目连接到你的程序上
        //    假设你们的多个项目服务器假设在一台电脑的情况，就绝对要替换下面的端口号

        /// <summary>
        /// 主网络端口，此处随机定义了一个数据
        /// </summary>
        public static int Port_Main_Net { get; } = 17652;
        /// <summary>
        /// 同步网络访问的端口，此处随机定义了一个数据
        /// </summary>
        public static int Port_Second_Net { get; } = 14568;
        /// <summary>
        /// 用于软件系统更新的端口，此处随机定义了一个数据
        /// </summary>
        public static int Port_Update_Net { get; } = 17538;
        /// <summary>
        /// 用于软件远程更新的端口，此处随机定义了一个数据
        /// </summary>
        public static int Port_Update_Remote { get; } = 26435;
        /// <summary>
        /// 共享文件的端口号
        /// </summary>
        public static int Port_Share_File { get; } = 34261;

        #endregion

        
    }

    /// <summary>
    /// 一个扩展的用户账户示例，代替服务器和客户端的账户类即可
    /// </summary>
    public class UserAccountEx : BasicFramework.UserAccount
    {
        /// <summary>
        /// 用户的年龄
        /// </summary>
        public int Age { get; set; } = 0;
        /// <summary>
        /// 用户的家庭住址
        /// </summary>
        public string HomeAddress { get; set; } = "";
        /// <summary>
        /// 所在科室
        /// </summary>
        public string Belong { get; set; } = string.Empty;
        /// <summary>
        /// 职位
        /// </summary>
        public string Job { get; set; } = string.Empty;
        /// <summary>
        /// 手机短号
        /// </summary>
        public string PhoneShort { get; set; } = string.Empty;
        /// <summary>
        /// 手机长号
        /// </summary>
        public string Phone { get; set; } = string.Empty;
    }
}
