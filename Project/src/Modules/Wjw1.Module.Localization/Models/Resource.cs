using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Wjw1.Infrastructure;

namespace Wjw1.Module.Localization.Models
{
    /// <summary>
    /// 语言包资源
    /// </summary>
    public class Resource : DbSetId
    {
        /// <summary>
        /// Key
        /// </summary>
        [Display(Name ="Resource_Key"),Required(ErrorMessage = "Resource_Key_Required"),MaxLength(450)]
        public string Key { get; set; }
        /// <summary>
        /// 翻译
        /// </summary>

        [Display(Name = "Resource_Value"), MaxLength(450)]
        public string Value { get; set; }

        [ForeignKey("Culture"),Required(ErrorMessage = "Resource_Culture_Required"),Display(Name = "Resource_Culture")]
        public string CultureId { get; set; }

        [ScaffoldColumn(false)]
        public Culture Culture { get; set; }
    }
}
