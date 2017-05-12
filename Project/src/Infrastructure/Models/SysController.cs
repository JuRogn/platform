using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wjw1.Infrastructure.Models
{
    public class SysController : DbSetId, IUserDictionary
    {
        public SysController()
        {
            SystemId = "000";
            TargetBlank = false;
            ControllerName = "Index";
            ActionName = "Index";
            Display = true;
            Enable = true;
            Ico = "fa-list-ul";
        }

        [Required]
        [ForeignKey("SysArea")]
        [Display(Name = "Area")]
        public string SysAreaId { get; set; }

        [ScaffoldColumn(false)]
        public SysArea SysArea { get; set; } 

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        public string ControllerName { get; set; }

        [MaxLength(50)]
        public string ActionName { get; set; }

        [MaxLength(50)]
        public string Parameter { get; set; }

        [MaxLength(50)]
        [Required]
        public string SystemId { get; set; }

        public bool Display { get; set; }

        public bool TargetBlank { get; set; }

        [DataType("Ico")]
        public string Ico { get; set; }

        [Display(Name = "Action")]
        public IEnumerable<SysControllerSysAction> SysControllerSysActions { get; set; }

        [ScaffoldColumn(false)]
        [NotMapped]
        public bool Selected { get; set; }

        public bool Enable { get; set; }
    }
}