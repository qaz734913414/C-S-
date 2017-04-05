using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary;
using IndustryEthernet;

namespace 软件系统客户端模版
{
    public class ModelProject
    {
        public static List<Project> Projects { get; set; } = null;
        public static List<Project> RecentlyFinishProjects { get; set; } = null;

        public static OperateResultString UpdateProjects()
        {
            OperateResultString result = new OperateResultString();
            try
            {
                Projects = Project.GetFromDatabase();
                RecentlyFinishProjects = Project.GetDaysFinishFromDatabase(UserClient.DateTimeServer.AddDays(-7));
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
