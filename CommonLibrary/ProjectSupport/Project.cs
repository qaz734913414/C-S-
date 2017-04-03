using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// 用来管理的项目类
    /// </summary>
    public class Project : BasicFramework.ISqlDataType
    {
        public static string SqlSelect { get; } = @"USE [SQL106]
           SELECT[SequenceNumber]
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
         FROM[dbo].[Project]
         WHERE[ProjectState]=1";


        /// <summary>
        /// 数据库标识的序号
        /// </summary>
        public int SequenceNumber { get; set; } = 0;
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



        public List<ProjectDetail> ProjectDetails { get; set; } = new List<ProjectDetail>();

        /// <summary>
        /// 项目组的所有成员
        /// </summary>
        public ProjectMember Members { get; set; } = new ProjectMember();


        public void GetDetails()
        {
            //获取细节
        }
        public void UpdateCurrentNode(string node)
        {
            //更新节点

        }


        public void GetMembers()
        {
            
        }



        public void LoadBySqlDataReader(SqlDataReader sdr)
        {
            throw new NotImplementedException();
        }
    }



    public class ProjectDetail : BasicFramework.ISqlDataType
    {
        public static string GetSqlSelected(int projectNumber)
        {
            return @"USE [SQL106]
        SELECT[SequenceNumber]
      ,[ProjectNumber]
      ,[CommentState]
      ,[CommentName]
      ,[CommentTime]
      ,[CommentContent]
        FROM[dbo].[ProjectDetail] WHERE[ProjectNumber]=" + projectNumber;
        }
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

        public void LoadBySqlDataReader(SqlDataReader sdr)
        {
            throw new NotImplementedException();
        }

        public int InsertSql()
        {
            string cmdStr = @"USE [SQL106]
    INSERT INTO[dbo].[ProjectDetail]
           ([ProjectNumber]
           ,[CommentState]
           ,[CommentName]
           ,[CommentTime]
           ,[CommentContent])
     VALUES
           (" + ProjectNumber + "," + CommentState + ",'" + CommentName + "',GETDATE(),'" + CommentContent.Replace('\'', ',') + "')";
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

        private object lock_list_operate = new object();
    }
}
