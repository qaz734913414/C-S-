﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using CommonLibrary;
using System.Threading;
using HslCommunication;
using HslCommunication.Enthernet;
using HslCommunication.BasicFramework;


/***************************************************************************************
 * 
 *    模版日期    2017-06-16
 *    创建人      胡少林
 *    版权所有    胡少林
 *    授权说明    模版仅授权个人使用，如需商用，请联系hsl200909@163.com洽谈
 *    说明一      JSON组件引用自james newton-king，遵循MIT授权协议
 *    说明二      文件的图标来源于http://fileicons.chromefans.org/,感谢作者的无私分享
 * 
 ****************************************************************************************/

/***************************************************************************************
 * 
 *    版本说明    最新版以github为准，由于提交更改比较频繁，需要经常查看官网地址:https://github.com/dathlin/C-S-
 *    注意        本代码的相关操作未作密码验证，如有需要，请自行完成
 *    示例        密码验证具体示例参照Form1_FormClosing(object sender, FormClosingEventArgs e)方法
 *    如果        遇到启动调试就退出了，请注释掉Program.cs文件中的指允许启动一个实例的代码
 * 
 ****************************************************************************************/





namespace 软件系统客户端模版
{
    public partial class FormMainWindow : Form
    {
        public FormMainWindow()
        {
            InitializeComponent();
        }

        #region 窗口的属性和方法
        /// <summary>
        /// 指示窗口是否显示的标志
        /// </summary>
        private bool IsWindowShow { get; set; } = false;

        private void FormMainWindow_Load(object sender, EventArgs e)
        {
            //udp测试
            //SendServerUdpData(0, "载入了窗体");

            //窗口载入
            label_userName.Text = UserClient.UserAccount.UserName;
            label_grade.Text = AccountGrade.GetDescription(UserClient.UserAccount.Grade);
            label_factory.Text = UserClient.UserAccount.Factory;
            label_register.Text = UserClient.UserAccount.RegisterTime.ToString();
            label_last.Text = UserClient.UserAccount.LastLoginTime.ToString();
            label_times.Text = UserClient.UserAccount.LoginFrequency.ToString();
            label_address.Text = UserClient.UserAccount.LastLoginIpAddress;

            //状态栏设置
            toolStripStatusLabel_time.Alignment = ToolStripItemAlignment.Right;
            statusStrip1.LayoutStyle = ToolStripLayoutStyle.StackWithOverflow;
            toolStripStatusLabel1.Text = $"本软件著作权归{Resource.StringResouce.SoftCopyRight}所有";

            //绑定事件，仅执行一次，不能放到show方法里
            net_socket_client.MessageAlerts += Net_socket_client_MessageAlerts;
            net_socket_client.LoginFailed += Net_socket_client_LoginFailed;
            net_socket_client.LoginSuccess += Net_socket_client_LoginSuccess;
            net_socket_client.AcceptByte += Net_socket_client_AcceptByte;
            net_socket_client.AcceptString += Net_socket_client_AcceptString;
            //启动网络服务
            Net_Socket_Client_Initialization();

            label_Announcement.Text = UserClient.Announcement;

            toolStripStatusLabel_Version.Text = UserClient.CurrentVersion.ToString();

            //初始化窗口
            MainRenderInitialization();
        }
        private void FormMainWindow_Shown(object sender, EventArgs e)
        {
            //窗口显示
            IsWindowShow = true;

            //udp测试
            //SendServerUdpData(0, "显示了窗体");

            //是否显示更新日志，显示前进行判断该版本是否已经显示过了
            if (UserClient.JsonSettings.IsNewVersionRunning)
            {
                UserClient.JsonSettings.IsNewVersionRunning = false;
                UserClient.JsonSettings.SaveToFile();
                更新日志ToolStripMenuItem_Click(null, new EventArgs());
            }

            //根据权限使能菜单
            if (UserClient.UserAccount.Grade < AccountGrade.SuperAdministrator)
            {
                日志查看ToolStripMenuItem.Enabled = false;
                账户管理ToolStripMenuItem.Enabled = false;
                远程更新ToolStripMenuItem.Enabled = false;
                注册账号ToolStripMenuItem.Enabled = false;
                消息发送ToolStripMenuItem.Enabled = false;
            }
            //启动定时器
            TimeTickInitilization();
        }
        private void FormMainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //窗口关闭
            IsWindowShow = false;
            //通知服务器退出网络服务
            net_socket_client.ClientClose();

            //等待一秒退出
            using (FormWaitInfomation fwm = new FormWaitInfomation("正在退出程序...", 1000))
            {
                fwm.ShowDialog();
            }
        }


        #endregion

        #region 菜单逻辑块

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //实例化一个密码修改的窗口，并指定了实现修改的具体方法，指定了密码长度
            using (FormPasswordModify fpm = new FormPasswordModify(UserClient.UserAccount.Password,
                p =>
                {
                    JObject json = new JObject
                    {
                        { UserAccount.UserNameText, UserClient.UserAccount.UserName },
                        { UserAccount.PasswordText, p }
                    };
                    return UserClient.Net_simplify_client.ReadFromServer(CommonHeadCode.SimplifyHeadCode.密码修改, json.ToString()).IsSuccess;
                }, 6, 8))
            {
                fpm.ShowDialog();
            }
        }

        private void 关于本软件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormAbout fa = new FormAbout(Resource.StringResouce.SoftName,
                UserClient.CurrentVersion, 2017, Resource.StringResouce.SoftCopyRight))
            {
                fa.ShowDialog();
            }
        }

        private void 更新日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //更新情况复位
            if (UserClient.JsonSettings.IsNewVersionRunning)
            {
                UserClient.JsonSettings.IsNewVersionRunning = false;
                UserClient.JsonSettings.SaveToFile();
            }
            using (FormUpdateLog ful = new FormUpdateLog(UserClient.HistoryVersions))
            {
                ful.ShowDialog();
            }
        }

        private void 版本号说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormAboutVersion fav = new FormAboutVersion(UserClient.CurrentVersion))
            {
                fav.ShowDialog();
            }
        }

        private void 更改公告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormInputAndAction fiaa = new FormInputAndAction(str => UserClient.Net_simplify_client.ReadFromServer(
                 CommonHeadCode.SimplifyHeadCode.更新公告, str).IsSuccess, UserClient.Announcement, "请输入公告内容"))
            {
                fiaa.ShowDialog();
            }
        }

        private void 日志查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormLog flg = new FormLog())
            {
                flg.ShowDialog();
            }
        }

        private void 注册账号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormRegisterAccount fra = new FormRegisterAccount())
            {
                fra.ShowDialog();
            }
        }

        private void 账户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAccountManage fam = new FormAccountManage(() =>
            {
                OperateResultString result = UserClient.Net_simplify_client.ReadFromServer(CommonHeadCode.SimplifyHeadCode.获取账户);
                if (result.IsSuccess) return result.Content;
                else return result.ToMessageShowString();
            }, m => UserClient.Net_simplify_client.ReadFromServer(CommonHeadCode.SimplifyHeadCode.更细账户, m).IsSuccess);
            fam.ShowDialog();
            fam.Dispose();
        }

        private void 远程更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UserClient.UserAccount.UserName == "admin")
            {
                using (FormUpdateRemote fur = new FormUpdateRemote())
                {
                    fur.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("权限不足！");
            }
        }

        private void linkLabel_logout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.QuitCode = 1;
            Close();
        }

        private void 留言板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetShowRenderControl(UIControls_Chat);
            UIControls_Chat?.InputFocus();
            UIControls_Chat?.ScrollToDown();
        }

        private void 意见反馈ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormInputAndAction fiaa = new FormInputAndAction(str => UserClient.Net_simplify_client.ReadFromServer(
                 CommonHeadCode.SimplifyHeadCode.意见反馈, UserClient.UserAccount.UserName + ":" + str).IsSuccess, "", "请输入意见反馈："))
            {
                fiaa.ShowDialog();
            }
        }

        private void 消息发送ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormInputAndAction fiaa = new FormInputAndAction(str => UserClient.Net_simplify_client.ReadFromServer(
                 CommonHeadCode.SimplifyHeadCode.群发消息, UserClient.UserAccount.UserName + ":" + str).IsSuccess, "", "请输入群发的消息："))
            {
                fiaa.ShowDialog();
            }
        }

        #endregion

        #region 异步网络块

        private Net_Socket_Client net_socket_client = new Net_Socket_Client();

        private void Net_Socket_Client_Initialization()
        {
            try
            {
                net_socket_client.KeyToken = CommonHeadCode.KeyToken;//新增的身份令牌
                net_socket_client.EndPointServer = new System.Net.IPEndPoint(
                    System.Net.IPAddress.Parse(UserClient.ServerIp),
                    CommonLibrary.CommonLibrary.Port_Main_Net);
                net_socket_client.ClientAlias = $"{UserClient.UserAccount.UserName} ({UserClient.UserAccount.Factory})";//标记客户端在线的名称
                net_socket_client.ClientStart();
            }
            catch (Exception ex)
            {
                SoftBasic.ShowExceptionMessage(ex);
            }
        }
        /// <summary>
        /// 接收到服务器的字节数据的回调方法
        /// </summary>
        /// <param name="state">网络连接对象</param>
        /// <param name="customer">用户自定义的指令头，用来区分数据用途</param>
        /// <param name="data">数据</param>
        private void Net_socket_client_AcceptString(AsyncStateOne state, int customer, string data)
        {
            if (customer == CommonHeadCode.MultiNetHeadCode.弹窗新消息)
            {
                if (IsHandleCreated) Invoke(new Action(() =>
                {
                    FormPopup fpp = new FormPopup(data, Color.DodgerBlue, 10000);
                    fpp.Show();
                }));
            }
            else if (customer == CommonHeadCode.MultiNetHeadCode.总在线信息)
            {
                if (IsHandleCreated) Invoke(new Action(() =>
                {
                    listBox1.DataSource = data.Split('#');
                }));
            }
            else if (customer == CommonHeadCode.MultiNetHeadCode.关闭客户端)
            {
                if (IsHandleCreated) Invoke(new Action(() =>
                {
                    Close();
                }));
            }
            else if (customer == CommonHeadCode.SimplifyHeadCode.更新公告)
            {
                //此处应用到了同步类的指令头
                if (IsHandleCreated) Invoke(new Action(() =>
                {
                    UserClient.Announcement = data;
                    label_Announcement.Text = data;
                    FormPopup fpp = new FormPopup(data, Color.DodgerBlue, 10000);
                    fpp.Show();
                }));
            }
            else if (customer == CommonHeadCode.MultiNetHeadCode.初始化数据)
            {
                //收到服务器的数据
                JObject json = JObject.Parse(data);
                UserClient.DateTimeServer = json["Time"].ToObject<DateTime>();
                List<string> chats = JArray.Parse(json["chats"].ToString()).ToObject<List<string>>();
                StringBuilder sb = new StringBuilder();
                chats.ForEach(m => { sb.Append(m + Environment.NewLine); });
                if (IsHandleCreated) Invoke(new Action(() =>
                {
                    toolStripStatusLabel_time.Text = UserClient.DateTimeServer.ToString("yyyy-MM-dd HH:mm");
                    label_file_count.Text = json["FileCount"].ToObject<int>().ToString();
                    UIControls_Chat.AddChatsHistory(sb.ToString());
                }));
            }
            else if (customer == CommonHeadCode.MultiNetHeadCode.文件总数量)
            {
                if (IsHandleCreated) Invoke(new Action(() =>
                {
                    label_file_count.Text = data;
                }));
            }
            else if (customer == CommonHeadCode.MultiNetHeadCode.留言版消息)
            {
                if (IsHandleCreated) Invoke(new Action(() =>
                {
                    UIControls_Chat?.DealwithReceive(data);
                }));
            }
        }

        private void Net_socket_client_AcceptByte(AsyncStateOne object1, int customer, byte[] object2)
        {
            //接收到服务器发来的字节数据
            if (IsHandleCreated) Invoke(new Action(() =>
            {
                MessageBox.Show(customer.ToString());
            }));
        }

        private void Net_socket_client_LoginSuccess()
        {
            //登录成功，或重新登录成功的事件，有些数据的初始化可以放在此处
            if (IsHandleCreated) Invoke(new Action(() =>
            {
                toolStripStatusLabel_status.Text = "客户端启动成功";
            }));
        }

        private void Net_socket_client_LoginFailed(int object1)
        {
            //登录失败的情况，如果连续三次连接失败，请考虑退出系统
            if (object1 > 3)
            {
                if (IsHandleCreated) Invoke(new Action(() =>
                {
                    Close();
                }));
            }
        }

        private void Net_socket_client_MessageAlerts(string object1)
        {
            //信息提示
            if (IsHandleCreated) Invoke(new Action(() =>
            {
                toolStripStatusLabel_status.Text = object1;
            }));
        }


        #endregion

        #region 主界面管理块

        /// <summary>
        /// 文件显示的控件
        /// </summary>
        private UIControls.ShareFilesRender UIControls_Files { get; set; }

        /// <summary>
        /// 用于聊天的控件
        /// </summary>
        private UIControls.OnlineChatRender UIControls_Chat { get; set; }





        /// <summary>
        /// 所有在主界面显示的控件集
        /// </summary>
        private List<UserControl> all_main_render = new List<UserControl>();
        /// <summary>
        /// 正在显示的子界面
        /// </summary>
        private UserControl CurrentRender { get; set; } = null;
        /// <summary>
        /// 主界面的初始化
        /// </summary>
        private void MainRenderInitialization()
        {
            //将所有的子集控件添加进去

            /*******************************************************************************
             * 
             *    例如此处展示了文件控件是如何添加进去的 
             *    1.先进行实例化，赋值初始参数
             *    2.添加进项目
             *    3.显示
             *
             *******************************************************************************/

            UIControls_Files = new UIControls.ShareFilesRender()
            {
                Visible = false,
                Parent = panel_main,//决定了放在哪个界面显示，此处示例
                Dock = DockStyle.Fill,
            };
            all_main_render.Add(UIControls_Files);

            UIControls_Chat = new UIControls.OnlineChatRender((m) =>
            {
                net_socket_client.Send(CommonHeadCode.MultiNetHeadCode.留言版消息, m);
            })
            {
                Visible = false,
                Parent = panel_main,//决定了放在哪个界面显示，此处示例
                Dock = DockStyle.Fill,
            };
            all_main_render.Add(UIControls_Chat);

        }

        private void SetShowRenderControl(UserControl control)
        {
            if (!ReferenceEquals(CurrentRender, control))
            {
                CurrentRender = control;
                all_main_render.ForEach(c => c.Visible = false);
                control.Visible = true;
            }
        }
        private void SetShowRenderControl(Type typeControl)
        {
            UserControl control = null;
            foreach (var c in all_main_render)
            {
                if (c.GetType() == typeControl)
                {
                    control = c;
                    break;
                }
            }
            if (control != null) SetShowRenderControl(control);
        }

        private void label_file_count_Click(object sender, EventArgs e)
        {
            //点击查看了共享文件
            SetShowRenderControl(UIControls_Files);
            UIControls_Files.UpdateFiles();
        }





        #endregion

        #region Udp发送示例
        /// <summary>
        /// 调用该方法并指定参数即可，最长字符串不得
        /// </summary>
        /// <param name="data"></param>
        private void SendServerUdpData(int customer, string data)
        {
            //测试发送udp消息
            UserClient.Net_Udp_Client.SendMessage(customer, data);
        }

        #endregion

        #region 后台计数线程

        /*********************************************************************************************
         * 
         *    说明       一个后台线程，用来执行一些周期执行的东西
         *    注意       它不仅执行每秒触发的代码，也支持每分钟，每天，每月，每年等等
         * 
         ********************************************************************************************/


        /// <summary>
        /// 初始化后台的计数线程
        /// </summary>
        public void TimeTickInitilization()
        {

            Thread thread = new Thread(new ThreadStart(ThreadTimeTick));
            thread.IsBackground = true;
            thread.Start();
        }

        public void ThreadTimeTick()
        {
            Thread.Sleep(300);//加一个微小的延时
            int second = DateTime.Now.Second - 1;
            int minute = -1;
            int hour = -1;
            int day = -1;
            Action DTimeShow = delegate
            {
                //显示服务器的时间和当前网络的延时时间，通常为毫秒
                toolStripStatusLabel_time.Text = net_socket_client.ServerTime.ToString("yyyy-MM-dd HH:mm:ss") + $"({net_socket_client.DelayTime}ms)";
            };

            while (IsWindowShow)
            {
                while (DateTime.Now.Second == second)
                {
                    Thread.Sleep(20);
                }
                second = DateTime.Now.Second;
                if (IsWindowShow && IsHandleCreated) Invoke(DTimeShow);
                //每秒钟执行的代码
                UserClient.DateTimeServer = net_socket_client.ServerTime;

                if (second == 0)
                {
                    //每个0秒执行的代码
                }
                if (minute != DateTime.Now.Minute)
                {
                    minute = DateTime.Now.Minute;
                    //每分钟执行的代码
                }
                if (hour != DateTime.Now.Hour)
                {
                    hour = DateTime.Now.Hour;
                    //每小时执行的代码
                }
                if (day != DateTime.Now.Day)
                {
                    day = DateTime.Now.Day;
                    //每天执行的代码
                }
            }
        }


        #endregion

        
    }
}
