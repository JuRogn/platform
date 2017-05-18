using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Wjw1.Infrastructure;

namespace Wjw1.Module.Localization.Models
{
    public class Resource : DbSetBase
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public long CultureId { get; set; }

        public Culture Culture { get; set; }
    }
}
