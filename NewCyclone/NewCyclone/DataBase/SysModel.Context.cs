﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewCyclone.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SysModelContainer : DbContext
    {
        public SysModelContainer()
            : base("name=SysModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Db_SysUser> Db_SysUserSet { get; set; }
        public virtual DbSet<Db_SysMsg> Db_SysMsgSet { get; set; }
        public virtual DbSet<Db_SysTree> Db_SysTreeSet { get; set; }
        public virtual DbSet<Db_SysFileSet> Db_SysFileSet { get; set; }
        public virtual DbSet<Db_SysDoc> Db_SysDocSet { get; set; }
        public virtual DbSet<Db_SysDocCat> Db_SysDocCatSet { get; set; }
        public virtual DbSet<Db_SysDocFile> Db_SysDocFileSet { get; set; }
    }
}
