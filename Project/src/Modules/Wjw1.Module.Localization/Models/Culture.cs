using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Wjw1.Infrastructure;

namespace Wjw1.Module.Localization.Models
{
    /// <summary>
    /// 文化语言包
    /// </summary>
    public class Culture : DbSetId //一个系统只需存储一份
    {
        [Display(Name ="Culture_Name"),Required(ErrorMessage = "Culture_Name_Required"),MaxLength(10,ErrorMessage = "Culture_Name_MaxLength")] //ErrorMessageResourceName = "Culture_Name_Required", ErrorMessageResourceType =typeof(Resource)
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public IList<Resource> Resources { get; set; }
    }
}
