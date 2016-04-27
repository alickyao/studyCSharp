using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewCyclone.DataBase;

namespace NewCyclone.Models
{
    /// <summary>
    /// 基础文档
    /// </summary>
    public class SysDoc
    {
        public SysDoc() {
            using (var db = new SysModelContainer()) {
                Db_SysDoc d = db.Db_SysDocSet.Single(p => p.Id == "56b6b444-d387-4e7d-9d44-fbd572e45994");
                db.Db_SysDocSet.Remove(d);
                db.SaveChanges();
            }
        }
    }

    /// <summary>
    /// 带图集的文档
    /// </summary>
    public class SysDocImgRote : SysDoc {
        /// <summary>
        /// 构造方法
        /// </summary>
        public SysDocImgRote() {
            using (var db = new SysModelContainer()) {
                Db_DocImgRote r = new Db_DocImgRote()
                {
                    caption = "cesh",
                    createdOn = DateTime.Now,
                    Id = Guid.NewGuid().ToString(),
                    isDeleted = false,
                    width = "",
                    second = "",
                    modifiedOn = DateTime.Now,
                    height = ""
                };
                db.Db_SysDocSet.Add(r);
                db.SaveChanges();
            }
        }
    }
}