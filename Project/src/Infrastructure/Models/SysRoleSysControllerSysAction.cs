using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wjw1.Infrastructure.Models
{
    public class SysRoleSysControllerSysAction :DbSetId
    {

        [ForeignKey("SysRole")]
        [Required]
        public string RoleId { get; set; }

        public SysRole SysRole { get; set; }

     
        [ForeignKey("SysControllerSysAction")]
        [Required]
        public string SysControllerSysActionId { get; set; }

        public SysControllerSysAction SysControllerSysAction { get; set; }
    }
}