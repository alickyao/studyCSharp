using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using NewCyclone.DataBase;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace NewCyclone.Models
{
    /// <summary>
    /// 角色
    /// </summary>
    public class SysRoles {
        /// <summary>
        /// 系统全部角色
        /// </summary>
        public static List<SysRoles> sysRoles { get; private set; }

        static SysRoles() {
            //sysRoles = new List<SysRoles>() {
            //    new SysRoles() {
            //        discribe = "系统管理员权限",
            //        name = "管理员",
            //        role = "admin"
            //    },
            //    new SysRoles() {
            //        discribe = "系统工作人员",
            //        name = "工作人员",
            //        role = "user"
            //    },
            //    new SysRoles() {
            //        discribe = "系统会员用户",
            //        name = "会员",
            //        role = "member"
            //    }
            //};

            string path = HttpContext.Current.Server.MapPath("/App_Set/SysRole.xml");
            //var writer = new XmlSerializer(typeof(List<SysRoles>));
            //var wfile = new StreamWriter(path);
            //writer.Serialize(wfile, sysRoles);
            //wfile.Close();

            //从文件反序列化
            XmlSerializer reader = new XmlSerializer(typeof(List<SysRoles>));
            StreamReader file = new StreamReader(path);
            sysRoles = (List<SysRoles>)reader.Deserialize(file);
            file.Close();
        }


        /// <summary>
        /// 角色
        /// </summary>
        public string role { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string discribe { get; set; }
    }

    /// <summary>
    /// 基类用户
    /// </summary>
    public abstract class SysUser
    {
        /// <summary>
        /// 登录名（ID）
        /// </summary>
        public string loginName { get; set; }

        /// <summary>
        /// 角色用户的角色
        /// </summary>
        public string role { get; set; }


        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime createdOn { get; set; }


        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public Nullable<DateTime> lastLoginTime { get; set; }

        /// <summary>
        /// 是否已经禁用
        /// </summary>
        public bool isDisabled { get; set; }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns>返回被删除的用户对象</returns>
        public void delete() {
            using (var db = new SysModelContainer())
            {
                var d = db.Db_SysUserSet.Single(p => p.loginName == loginName);
                d.isDeleted = true;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public abstract void getInfo();
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="loginname"></param>
        public abstract void getInfo(string loginname);

        /// <summary>
        /// 保存用户信息
        /// </summary>
        public abstract void saveInfo();
    }

    /// <summary>
    /// 系统管后台用户
    /// </summary>
    public class SysManagerUser : SysUser
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string fullName;
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobilePhone;
        /// <summary>
        /// 职位
        /// </summary>
        public string jobTitle;


        /// <summary>
        /// 构造方法
        /// </summary>
        public SysManagerUser() {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="loginname">登录名</param>
        public SysManagerUser(string loginname) {
            getInfo(loginname);
        }

        #region -- 用户菜单
        private List<SysMenu> userMenu = new List<SysMenu>();

        /// <summary>
        /// 获取用户的菜单(根据用户的角色)
        /// </summary>
        /// <returns></returns>
        public List<SysMenu> getUserMenu()
        {
            List<SysMenu> m = SysMenu.sysMenu;
            foreach (var s in m)
            {
                if (s.roles.Count == 0 ? true : s.roles.Contains(this.role))
                {
                    this.userMenu.Add(new SysMenu()
                    {
                        icon = s.icon,
                        roles = s.roles,
                        text = s.text,
                        url = s.url,
                        children = s.children == null ? new List<SysMenu>() : getUserCheldMenu(s.children)
                    });
                }
            }
            return this.userMenu;
        }


        /// <summary>
        /// 根据权限递归子菜单
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        private List<SysMenu> getUserCheldMenu(List<SysMenu> children)
        {
            List<SysMenu> child = new List<SysMenu>();
            foreach ( var s in children) {
                if (s.roles.Count == 0 ? true : s.roles.Contains(this.role))
                {
                    child.Add(new SysMenu()
                    {
                        icon = s.icon,
                        roles = s.roles,
                        text = s.text,
                        url = s.url,
                        children = s.children == null ? new List<SysMenu>() : getUserCheldMenu(s.children)
                    });
                }
            }
            return child;
        }
        #endregion


        #region -- 验证与新增

        /// <summary>
        /// 获取登录名在数据库中出现的次数
        /// </summary>
        /// <param name="lgname"></param>
        /// <returns></returns>
        private int getLoginNameCount(string lgname) {
            using (var db = new SysModelContainer()) {
                return (from c in db.Db_SysUserSet where c.loginName.Equals(lgname) select c.loginName).Count();
            }
        }

        /// <summary>
        /// 注册新的用户
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public SysManagerUser create(ViewModelUserRegisterRequest condtion) {
            var v = SysValidata.valiData(condtion);
            if (v.code == BaseResponseCode.异常) {
                throw new SysException(v.msg, condtion);
            }
            int c = getLoginNameCount(condtion.loginname);
            if (c > 0) {
                throw new SysException("登录名已存在", condtion);
            }
            using (var db = new SysModelContainer()) {
                Db_ManagerUser dbuser = new Db_ManagerUser() {
                    createdOn = DateTime.Now,
                    fullName = condtion.fullName,
                    isDeleted = false,
                    isDisabled = false,
                    jobTitle = condtion.jobTitle,
                    loginName = condtion.loginname,
                    mobilePhone = condtion.mobilePhone,
                    passWord = "abc",
                    role = condtion.role
                };
                db.Db_SysUserSet.Add(dbuser);
                db.SaveChanges();
            }
            getInfo(condtion.loginname);
            return this;
        }

        #endregion


        #region -- 获取

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public override void getInfo()
        {
            using (var db = new SysModelContainer())
            {
                var d = db.Db_SysUserSet.OfType<Db_ManagerUser>().Single(p => p.loginName == this.loginName);
                setUserInfo(d);
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="loginname"></param>
        public override void getInfo(string loginname)
        {
            using (var db = new SysModelContainer()) {
                var d = db.Db_SysUserSet.OfType<Db_ManagerUser>().Single(p => p.loginName == loginname);
                setUserInfo(d);
            }
        }

        /// <summary>
        /// 从数据库中赋值到对象
        /// </summary>
        /// <param name="d"></param>
        private void setUserInfo(Db_ManagerUser d) {
            this.loginName = d.loginName;
            this.fullName = d.fullName;
            this.jobTitle = d.jobTitle;
            this.mobilePhone = d.mobilePhone;
            this.lastLoginTime = d.lastLoginTime;
            this.role = d.role;
            this.isDisabled = d.isDisabled;
            this.createdOn = d.createdOn;
        }

        #endregion


        #region -- 登陆

        /// <summary>
        /// 验证登陆
        /// </summary>
        /// <param name="condtion"></param>
        /// <returns></returns>
        public SysManagerUser checkLogin(ViewModelLoginReqeust condtion) {
            var r = SysValidata.valiData(condtion);
            if (r.code == BaseResponseCode.异常) {
                throw new SysException(r.msg, condtion);
            }
            using (var db = new SysModelContainer()) {
                var d = db.Db_SysUserSet.OfType<Db_ManagerUser>().SingleOrDefault(p => p.loginName.Equals(condtion.loginName) && p.passWord.ToLower().Equals(condtion.pwd.ToLower()) && !p.isDeleted && !p.isDisabled);
                if (d == null) {
                    throw new SysException("用户名或密码不正确", condtion);
                }
                setUserInfo(d);
                return this;
            }
        }

        #endregion

        /// <summary>
        /// 保存用户信息
        /// </summary>
        public override void saveInfo()
        {
            
        }
    }

    /// <summary>
    /// 新增管理员请求参数
    /// </summary>
    public class ViewModelUserRegisterRequest {
        /// <summary>
        /// 登录名
        /// </summary>
        [Required(AllowEmptyStrings =false,ErrorMessage ="登录名必填")]
        [StringLength(50,MinimumLength =5,ErrorMessage ="登录名至少需要5个字符")]
        public string loginname { get; set; }

        /// <summary>
        /// 角色（可选admin,user）
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "角色信息必填")]
        public string role { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(AllowEmptyStrings = false,ErrorMessage ="请填写姓名")]
        [MaxLength(50)]
        public string fullName;
        /// <summary>
        /// 手机号
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [Phone(ErrorMessage = "电话号码格式不正确")]
        public string mobilePhone;
        /// <summary>
        /// 职位
        /// </summary>
        public string jobTitle;
    }

    /// <summary>
    /// 用户登录请求
    /// </summary>
    public class ViewModelLoginReqeust {
        /// <summary>
        /// 登录名
        /// </summary>
        [Required(ErrorMessage ="登录名必填")]
        public string loginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string pwd { get; set; }

        /// <summary>
        /// 登陆时的IP地址
        /// </summary>
        public string ip { get; set; }

        /// <summary>
        /// 登陆设备信息
        /// </summary>
        public string device { get; set; }
    }
}