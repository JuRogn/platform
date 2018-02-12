using System;
using System.Collections.Generic;
using System.Text;

namespace Wjw1.Module.Localization.ViewModels
{
    public class ResourceItemVm
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string CultureId { get; set; }

        public bool IsTranslated { get; set; }
    }
}
