using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace CommonLibrary
{
    /// <summary>
    /// 用来管理的项目类
    /// </summary>
    public class Project : BasicFramework.ISqlDataType
    {
        public static string SqlSelect { get; } = @"SELECT[SequenceNumber]
          ,[ProjectState]
          ,[ProjectId]
          ,[ProjectCategory]
          ,[ProjectDepartment]
          ,[ProjectPriority]
          ,[PorjectName]
          ,[Leader]
          ,[DateCreate]
          ,[DateStart]
          ,[DatePlanFinish]
          ,[DateFinish]
          ,[DateUpdate]
          ,[CurrentNode]
          ,[Progress]
          ,[Members]
         FROM[dbo].[Project]";

        /// <summary>
        /// 获取所有执行中的项目
        /// </summary>
        /// <returns></returns>
        public static List<Project> GetFromDatabase()
        {
            return BasicFramework.SoftSqlOperate.ExecuteSelectEnumerable<Project>(SqlServerSupport.SqlConnectStr, SqlSelect + " WHERE ProjectState = 1");
        }

        /// <summary>
        /// 获取指定时间内完成的项目
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static List<Project> GetDaysFinishFromDatabase(DateTime time)
        {
            return BasicFramework.SoftSqlOperate.ExecuteSelectEnumerable<Project>(SqlServerSupport.SqlConnectStr, SqlSelect +
                " WHERE DateFinish > '" + time + "' AND ProjectState = 2");
        }


        /// <summary>
        /// 数据库标识的序号
        /// </summary>
        public int SequenceNumber { get; set; } = 0;
        /// <summary>
        /// 项目的状态，0:已经删除,1:执行中,2:已完成
        /// </summary>
        public int ProjectState { get; set; } = 0;
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// 项目类别，用来检索属于什么类别的项目，比如按照功能
        /// </summary>
        public int ProjectCategory { get; set; } = 0;
        /// <summary>
        /// 项目的归属部门，或是科室
        /// </summary>
        public string ProjectDepartment { get; set; } = string.Empty;
        /// <summary>
        /// 项目优先级，指示项目的紧急情况，也可以用来生成部门或公司的重点项目
        /// </summary>
        public int ProjectPriority { get; set; } = 0;
        /// <summary>
        /// 项目的名称
        /// </summary>
        public string PorjectName { get; set; } = string.Empty;
        /// <summary>
        /// 项目负责人
        /// </summary>
        public string Leader { get; set; } = string.Empty;
        /// <summary>
        /// 项目的创建时间，由系统指定，无法修改
        /// </summary>
        public DateTime DateCreate { get; set; } = DateTime.Now;
        /// <summary>
        /// 项目的起始时间
        /// </summary>
        public DateTime DateStart { get; set; } = DateTime.Now;
        /// <summary>
        /// 项目计划完成时间
        /// </summary>
        public DateTime DatePlanFinish { get; set; } = DateTime.Now;
        /// <summary>
        /// 项目完成时间
        /// </summary>
        public DateTime DateFinish { get; set; } = DateTime.Now;
        /// <summary>
        /// 项目的更新时间
        /// </summary>
        public DateTime DateUpdate { get; set; } = DateTime.Now;
        /// <summary>
        /// 项目的当前节点，应该有项目负责人更新
        /// </summary>
        public string CurrentNode { get; set; } = string.Empty;
        /// <summary>
        /// 项目的进度，0-100，用来管理
        /// </summary>
        public int Progress { get; set; } = 0;

        
        /// <summary>
        /// 项目组的所有成员
        /// </summary>
        public ProjectMember Members { get; set; } = new ProjectMember();


        public List<ProjectDetail> GetDetails()
        {
            //获取细节
            return ProjectDetail.GetListFromDatabase(SequenceNumber);
        }
        /// <summary>
        /// 用于
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public int UpdateCurrentNode(string node)
        {
            if (SequenceNumber < 1) throw new Exception("项目不存在，无法更新");
            //更新节点
            using (SqlConnection conn = new SqlConnection(SqlServerSupport.SqlConnectStr))
            {
                conn.Open();
                //项目负责人更新
                string cmdStr = $"UPDATE DBO.Project SET CurrentNode='{node.Replace('\'', '-')}',DateUpdate=GETDATE() " +
                "WHERE SequenceNumber=" + SequenceNumber;
                if (BasicFramework.SoftSqlOperate.ExecuteSql(conn, cmdStr) == 1)
                {
                    cmdStr = $"INSERT INTO DBO.ProjectDetail VALUES({SequenceNumber},{CommentState.ProjectLeader},'{Leader}',GETDATE(),'{node.Replace('\'', '-')}')";
                    BasicFramework.SoftSqlOperate.ExecuteSql(conn, cmdStr);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 将数据插入数据库中
        /// </summary>
        /// <returns></returns>
        public int InsertSqlDatabase()
        {
            if (SequenceNumber > 0) throw new Exception("该项目已存在，无法新建项目");

            string cmdStr = $@"INSERT INTO [dbo].[Project]
           ([ProjectState]
           ,[ProjectId]
           ,[ProjectCategory]
           ,[ProjectDepartment]
           ,[ProjectPriority]
           ,[PorjectName]
           ,[Leader]
           ,[DateCreate]
           ,[DateStart]
           ,[DatePlanFinish]
           ,[DateFinish]
           ,[DateUpdate]
           ,[CurrentNode]
           ,[Progress]
           ,[Members]) VALUES
           ({ProjectState}
           ,'{ProjectId.Replace('\'','-')}'
           ,{ProjectCategory}
           ,'{ProjectDepartment}'
           ,{ProjectPriority}
           ,'{PorjectName.Replace('\'', '-')}'
           ,'{Leader}'
           ,GETDATE()
           ,'{DateStart}'
           ,'{DatePlanFinish}'
           ,'{DateFinish}'
           ,'{DateUpdate}'
           ,'{CurrentNode.Replace('\'', '-')}'
           ,'{Progress}'
           ,'{JArray.FromObject(Members).ToString()}')";
            return BasicFramework.SoftSqlOperate.ExecuteSql(SqlServerSupport.SqlConnectStr, cmdStr);
        }

        

        public void LoadBySqlDataReader(SqlDataReader sdr)
        {
            SequenceNumber = Convert.ToInt32(sdr[nameof(SequenceNumber)]);
            ProjectState = Convert.ToInt32(sdr[nameof(ProjectState)]);
            ProjectId = sdr[nameof(ProjectId)].ToString();
            ProjectCategory = Convert.ToInt32(sdr[nameof(ProjectCategory)]);
            ProjectDepartment = sdr[nameof(ProjectDepartment)].ToString();
            ProjectPriority = Convert.ToInt32(sdr[nameof(ProjectPriority)]);
            PorjectName = sdr[nameof(PorjectName)].ToString();
            Leader = sdr[nameof(Leader)].ToString();
            DateCreate = Convert.ToDateTime(sdr[nameof(DateCreate)]);
            DateStart = Convert.ToDateTime(sdr[nameof(DateStart)]);
            DatePlanFinish = Convert.ToDateTime(sdr[nameof(DatePlanFinish)]);
            DateFinish = Convert.ToDateTime(sdr[nameof(DateFinish)]);
            DateUpdate = Convert.ToDateTime(sdr[nameof(DateUpdate)]);
            CurrentNode = sdr[nameof(CurrentNode)].ToString().Trim();
            Progress = Convert.ToInt32(sdr[nameof(Progress)]);
            Members = new ProjectMember(JArray.Parse(sdr[nameof(Members)].ToString()).ToObject<List<Member>>());
        }
    }



    public class ProjectDetail : BasicFramework.ISqlDataType
    {
        public static string GetSqlSelected(int projectNumber)
        {
            return @"SELECT[SequenceNumber]
      ,[ProjectNumber]
      ,[CommentState]
      ,[CommentName]
      ,[CommentTime]
      ,[CommentContent]
        FROM[dbo].[ProjectDetail] WHERE[ProjectNumber]=" + projectNumber;
        }

        /// <summary>
        /// 获取指定项目的所有更新细节
        /// </summary>
        /// <param name="projectNumber">唯一的项目编号</param>
        /// <returns></returns>
        public static List<ProjectDetail> GetListFromDatabase(int projectNumber)
        {
            return BasicFramework.SoftSqlOperate.ExecuteSelectEnumerable<ProjectDetail>(
                SqlServerSupport.SqlConnectStr, GetSqlSelected(projectNumber));
        }


        /// <summary>
        /// 数据库标识的序号
        /// </summary>
        public int SequenceNumber { get; set; } = 0;
        /// <summary>
        /// 项目的编号
        /// </summary>
        public int ProjectNumber { get; set; } = 0;
        /// <summary>
        /// 留言状态,0:常规 1:负责人留言(蓝色标记) 2:领导留言(红色标记)
        /// </summary>
        public int CommentState { get; set; } = 0;
        /// <summary>
        /// 项目留言人
        /// </summary>
        public string CommentName { get; set; } = string.Empty;
        /// <summary>
        /// 项目留言时间
        /// </summary>
        public DateTime CommentTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 项目留言的具体内容，有可能有好几行数据
        /// </summary>
        public string CommentContent { get; set; } = string.Empty;

        /// <summary>
        /// 从数据库中加载数据
        /// </summary>
        /// <param name="sdr"></param>
        public void LoadBySqlDataReader(SqlDataReader sdr)
        {
            SequenceNumber = Convert.ToInt32(sdr[nameof(SequenceNumber)]);
            ProjectNumber = Convert.ToInt32(sdr[nameof(ProjectNumber)]);
            CommentState = Convert.ToInt32(sdr[nameof(CommentState)]);
            CommentName = sdr[nameof(CommentName)].ToString();
            CommentTime = Convert.ToDateTime(nameof(CommentTime));
            CommentContent = sdr[nameof(CommentContent)].ToString();
        }
        /// <summary>
        /// 将记录插入到数据库中
        /// </summary>
        /// <returns></returns>
        public int InsertSql()
        {
            string cmdStr = @"INSERT INTO[dbo].[ProjectDetail]
           ([ProjectNumber]
           ,[CommentState]
           ,[CommentName]
           ,[CommentTime]
           ,[CommentContent])
     VALUES
           (" + ProjectNumber + "," + CommentState + ",'" + CommentName + "',GETDATE(),'" + CommentContent.Replace('\'', ',') + "')";
            return BasicFramework.SoftSqlOperate.ExecuteSql(SqlServerSupport.SqlConnectStr, cmdStr);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns></returns>
        public int DeleteSql()
        {
            if (SequenceNumber < 1) throw new Exception("找不到该数据");
            string cmdStr = "DELETE DBO.ProjectDetail WHERE SequenceNumber=" + SequenceNumber;
            return BasicFramework.SoftSqlOperate.ExecuteSql(SqlServerSupport.SqlConnectStr, cmdStr);
        }

    }
    

    /// <summary>
    /// 单个的项目组成员
    /// </summary>
    public class Member
    {
        /// <summary>
        /// 成员名字
        /// </summary>
        public string MemberName { get; set; } = string.Empty;
        /// <summary>
        /// 成员任务
        /// </summary>
        public string TaskDistribution { get; set; } = string.Empty;
    }

    public class ProjectMember : IEnumerable<Member>
    {
        private List<Member> AllMembers { get; set; } = new List<Member>();


        public ProjectMember()
        {

        }
        public ProjectMember(List<Member> members)
        {
            AllMembers = members;
        }
            
        public IEnumerator<Member> GetEnumerator()
        {
            return AllMembers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return AllMembers.GetEnumerator();
        }
        /// <summary>
        /// 新增一个成员对象
        /// </summary>
        /// <param name="item">成员对象</param>
        public void ItemAdd(Member item)
        {
            //线程安全的添加
            lock (lock_list_operate)
            {
                AllMembers.Add(item);
            }
        }
        /// <summary>
        /// 删除一个成员
        /// </summary>
        /// <param name="name"></param>
        public void ItemDelete(string name)
        {
            for (int i = AllMembers.Count - 1; i >= 0; i--)
            {
                if(AllMembers[i].MemberName == name)
                {
                    AllMembers.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// 获取本对象的JSON表示方式
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return JObject.FromObject(this).ToString();
        }

        /// <summary>
        /// 获取用于显示的字符串文本
        /// </summary>
        /// <returns></returns>
        public string ToShowCreate()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < AllMembers.Count; i++)
            {
                sb.Append(AllMembers[i].MemberName + ",");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 判断指定名称的人是否在项目成员中
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsSpecifiedMemberIn(string name)
        {
            for (int i = 0; i < AllMembers.Count; i++)
            {
                if (AllMembers[i].MemberName == name) return true;
            }
            return false;
        }

        private object lock_list_operate = new object();
    }
}
