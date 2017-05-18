using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Wjw1.Infrastructure;

namespace Wjw1.Module.Localization.Models
{
    public class Culture : DbSetBase
    {
        public string Name { get; set; }

        public IList<Resource> Resources { get; set; }
    }
}
