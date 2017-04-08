﻿using System;
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
    /// 选项类，包含了所有的标识和文本的对应关系
    /// </summary>
    public class BasicOptions
    {
        /// <summary>
        /// 测试，用于生成数据状态的信息存储
        /// </summary>
        public static readonly List<BasicOptions> test = new List<BasicOptions>()
        {
            new BasicOptions(0,"测试一"),
            new BasicOptions(1,"测试二"),
            new BasicOptions(2,"测试三"),
        };


        public static readonly List<BasicOptions> Belongs = new List<BasicOptions>()
        {
            new BasicOptions(0,"技术一科"),
            new BasicOptions(1,"技术二科"),
            new BasicOptions(2,"技术三科"),
            new BasicOptions(3,"综合科"),
            new BasicOptions(4,"设备科"),
            new BasicOptions(5,"计量科"),
        };

        public static readonly List<BasicOptions> ProjectGrade = new List<BasicOptions>()
        {
            new BasicOptions(0,"常规项目"),
            new BasicOptions(1,"重点项目")
        };

        public static readonly List<BasicOptions> ProjectCategroy = new List<BasicOptions>()
        {
            new BasicOptions(0,"一般"),
        };


        /// <summary>
        /// 实例化一个对象
        /// </summary>
        public BasicOptions()
        {

        }
        /// <summary>
        /// 根据信息实例化一个选项对象
        /// </summary>
        /// <param name="code"></param>
        /// <param name="des"></param>
        public BasicOptions(int code, string des)
        {
            IntegerCode = code;
            Description = des;
        }
        /// <summary>
        /// 整数的代号
        /// </summary>
        public int IntegerCode { get; set; } = 0;
        /// <summary>
        /// 代号描述的文本
        /// </summary>
        public string Description { get; set; } = string.Empty;
        public override string ToString()
        {
            return Description;
        }
    }
    


    public class UserAccountEx:BasicFramework.UserAccount
    {
        /// <summary>
        /// 用户的年纪
        /// </summary>
        public int Age { get; set; } = 0;
        /// <summary>
        /// 用户的家庭住址
        /// </summary>
        public string HomeAddress { get; set; } = string.Empty;

        /// <summary>
        /// 所在的科室
        /// </summary>
        public string Belong { get; set; } = string.Empty;
        /// <summary>
        /// 职位
        /// </summary>
        public string Job { get; set; } = string.Empty;
        /// <summary>
        /// 手机的短号
        /// </summary>
        public string PhoneShort { get; set; } = string.Empty;
        /// <summary>
        /// 手机的长号
        /// </summary>
        public string Phone { get; set; } = string.Empty;
    }
}
