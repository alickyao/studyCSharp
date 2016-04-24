using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NewCyclone.Models
{
    public class SysContext : DbContext
    {
        public SysContext() : base("name=AppContext") {

        }

        public DbSet<SysLog> sysLogs { get; set; }
    }
}