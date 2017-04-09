using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary;
using IndustryEthernet;

namespace 软件系统客户端模版
{
    public class ModalProject
    {
        /// <summary>
        /// 所有的执行中的项目
        /// </summary>
        public static List<Project> Projects { get; set; } = null;
        /// <summary>
        /// 近期完成的项目
        /// </summary>
        public static List<Project> RecentlyFinishProjects { get; set; } = null;

        /// <summary>
        /// 部门所有的账户信息
        /// </summary>
        public static List<Account> DepartmentAccounts { get; set; } = null;
        /// <summary>
        /// 更新所有的执行中项目和近一周执行完成的项目
        /// </summary>
        /// <returns></returns>
        public static OperateResultString UpdateProjects()
        {
            OperateResultString result = new OperateResultString();
            try
            {
                Projects = Project.GetFromDatabase();
                RecentlyFinishProjects = Project.GetDaysFinishFromDatabase(UserClient.DateTimeServer.AddDays(-7));
                DepartmentAccounts = BasicFramework.SoftSqlOperate.ExecuteSelectEnumerable<Account>(
                    SqlServerSupport.SqlConnectStr, Account.SqlSelected);
                result.IsSuccess = true;
                return result;
            }
            catch(Exception ex)
            {
                result.Message = "数据下载失败：" + ex.Message;
                return result;
            }
        }

    }
}
