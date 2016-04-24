using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewCyclone.Models
{
    public class SysLog
    {
        [Display(Name ="主键")]
        [Required]
        [StringLength(50)]
        public string id { get; set; }

        [Display(Name = "消息主体")]
        [Required]
        [StringLength(50)]
        public string message { get; set; }
    }
}