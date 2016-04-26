using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewCyclone.Models
{
    /// <summary>
    /// 树
    /// </summary>
    public class SysTree
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public string _parentId { get; set; }
    }

    
}