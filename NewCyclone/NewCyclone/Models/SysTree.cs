using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewCyclone.DataBase;

namespace NewCyclone.Models
{
    /// <summary>
    /// 基树-抽象
    /// </summary>
    /// <typeparam name="T">子类树的类型</typeparam>
    public abstract class SysTree<T>
    {

        /// <summary>
        /// 最大递归深度，如果为0则表示不限制
        /// </summary>
        protected static int maxDep = 0;
        
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// 当前递归的深度 默认从1 开始
        /// 仅在树形中有效
        /// </summary>
        protected int nodeDep = 1;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createOn { get; internal set; }

        private List<string> _childrenIdList = new List<string>();

        /// <summary>
        /// 所有子节点的ID集合
        /// </summary>
        public List<string> childrenIdList
        {
            get { return _childrenIdList; }
            set { _childrenIdList = value; }
        }

        private List<T> _children = new List<T>();

        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<T> children {
            get { return _children; }
            set { _children = value; }
        }

        /// <summary>
        /// 父节点
        /// </summary>
        public T parent { get; internal set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public string _parentId { get; set; }

        /// <summary>
        /// 当前节点的路径 格式 /根/子/当前
        /// </summary>
        public string path { get; internal set; }

        /// <summary>
        /// 根节点的ID
        /// </summary>
        public string rootId { get; internal set; }

        private void setInfo() {
            using (var db = new SysModelContainer())
            {
                var d = db.Db_SysTreeSet.Single(p => p.Id == Id);
                this.Id = d.Id;
                this._parentId = d.parentId;
                this.createOn = d.createdOn;
            }
            if (!string.IsNullOrEmpty(this._parentId))
            {
                this.parent = getParent(this);
            }
            //设置当前节点的路径表达形式
            this.path = "/" + this.Id + this.path;
            this.rootId = this.Id;
            getNodePath(this._parentId);
        }

        /// <summary>
        /// 使用ID构造树并限制递归的深度
        /// </summary>
        /// <param name="Id">节点ID</param>
        /// <param name="max">最大深度</param>
        /// <param name="now">当前深度</param>
        public SysTree(string Id, int max, int now) {
            this.Id = Id;
            maxDep = max;
            nodeDep = now;
            setInfo();
            //获取子节点的ID集合
            getChildIdList(this.Id, now);
            //获取子节点
            children = getChild(this);
        }

        /// <summary>
        /// 使用ID构造树
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="getchlid"></param>
        public SysTree(string Id,bool getchlid) {
            this.Id = Id;
            setInfo();
            if (getchlid) {
                //获取子节点
                children = getChild(this);
                getChildIdList(this.Id, 1);
            }
        }
        /// <summary>
        /// 获取当前的路径
        /// </summary>
        private void getNodePath(string pId)
        {
            if (!string.IsNullOrEmpty(pId)) {
                this.rootId = pId;
                using (var db = new SysModelContainer()) {
                    var d = db.Db_SysTreeSet.Single(p => p.Id == pId);
                    this.path = "/" + d.Id + this.path;
                    getNodePath(d.parentId);
                }
            }
        }

        /// <summary>
        /// 递归获取子节点ID的集合
        /// </summary>
        /// <param name="pId">父ID</param>
        /// <param name="dep">当前递归深度</param>
        private void getChildIdList(string pId,int dep)
        {
            if (dep < maxDep || maxDep == 0)
            {
                using (var db = new SysModelContainer())
                {
                    var crows = (from c in db.Db_SysTreeSet
                                 where c.parentId == pId
                                 select c.Id
                                 ).ToList();
                    if (crows.Count > 0)
                    {
                        int d = dep + 1;
                        this.childrenIdList.AddRange(crows);
                        foreach (string row in crows)
                        {
                            getChildIdList(row, d);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 实现递归获取子节点
        /// </summary>
        /// <param name="tree">父节点</param>
        /// <returns></returns>
        protected abstract List<T> getChild(SysTree<T> tree);

        /// <summary>
        /// 实现获取父节点
        /// </summary>
        /// <param name="tree"></param>
        protected abstract T getParent(SysTree<T> tree);
    }

    public enum SysCatTreeType {
        /// <summary>
        /// 大类
        /// </summary>
        文档
    }

    /// <summary>
    /// 分类树
    /// </summary>
    public class SysCatTree :SysTree<SysCatTree> {
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public SysCatTreeType cat { get; set; }



        private void setInfo() {
            using (var db = new SysModelContainer())
            {
                var d = db.Db_SysTreeSet.OfType<Db_CatTree>().Single(p => p.Id == Id);
                this.name = d.name;
                this.cat = (SysCatTreeType)d.cat;
            }
        }

        /// <summary>
        /// 使用ID构造树并限制递归的深度
        /// </summary>
        /// <param name="Id">节点的ID</param>
        /// <param name="max">最大深度控制，如果设置为1则只取当前节点的信息</param>
        /// <param name="now">当前递归的深度 默认为1</param>
        public SysCatTree(string Id, int max, int now = 1) : base(Id, max, now)
        {
            setInfo();
        }


        /// <summary>
        /// 使用ID构造树型对象 可设置是否获取子节点
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="getChlid"></param>
        public SysCatTree(string Id, bool getChlid = false) : base(Id, getChlid)
        {
            setInfo();
        }

        /// <summary>
        /// 递归获取子节点
        /// </summary>
        /// <param name="tree">父节点</param>
        /// <returns></returns>
        protected override List<SysCatTree> getChild(SysTree<SysCatTree> tree)
        {
            if (nodeDep < maxDep || maxDep == 0)
            {
                int dep = nodeDep + 1;
                using (var db = new SysModelContainer())
                {
                    List<SysCatTree> child = (from c in db.Db_SysTreeSet.OfType<Db_CatTree>().AsEnumerable()
                                              where c.parentId == tree.Id
                                              select new SysCatTree(c.Id, maxDep, dep)).ToList();
                    return child;
                }
            }
            else {
                return new List<SysCatTree>();
            }
        }

        /// <summary>
        /// 获取父节点
        /// </summary>
        /// <param name="tree"></param>
        protected override SysCatTree getParent(SysTree<SysCatTree> tree)
        {
            return new SysCatTree(tree._parentId);
        }


        public static SysCatTree create(string name, SysCatTreeType cat, string parentId = null) {
            using (var db = new SysModelContainer()) {
                Db_CatTree d = new Db_CatTree() {
                    cat = (int)cat,
                    createdOn = DateTime.Now,
                    Id = SysHelp.getNewId(),
                    name = name,
                    parentId = parentId
                };
                Db_SysTree newrow = db.Db_SysTreeSet.Add(d);
                db.SaveChanges();
                SysCatTree newtree = new SysCatTree(newrow.Id);
                return newtree;
            }
        }

        public static List<SysCatTree> getlist()
        {
            List<SysCatTree> result = new List<SysCatTree>();

            using (var db = new SysModelContainer()) {
                var res = (from c in db.Db_SysTreeSet.OfType<Db_CatTree>().AsEnumerable()
                          where (c.parentId == null || c.parentId=="")
                          select new SysCatTree(c.Id, false)
                          ).ToList();
                
                foreach (var t in res) {
                    result.Add(t);
                    var cl = (from c in db.Db_SysTreeSet.OfType<Db_CatTree>().AsEnumerable()
                             where t.childrenIdList.Contains(c.Id)
                             select new SysCatTree(c.Id, false)
                          ).ToList();
                    if (cl.Count > 0)
                    {
                        result.AddRange(cl);
                    }
                }
            }
            return result;
        }
    }
}