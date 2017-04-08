using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// 留言的状态
    /// </summary>
    public abstract class CommentState
    {
        /// <summary>
        /// 常规留言
        /// </summary>
        public static int Normal { get; } = 0;
        /// <summary>
        /// 项目负责人留言
        /// </summary>
        public static int ProjectLeader { get; } = 1;
        /// <summary>
        /// 领导留言
        /// </summary>
        public static int Manager { get; } = 2;
    }

    public abstract class ProjectPriority
    {
        /// <summary>
        /// 一般项目
        /// </summary>
        public static int Normal { get; } = 0;
        /// <summary>
        /// 重大项目
        /// </summary>
        public static int Significant { get; } = 1;
    }
}
