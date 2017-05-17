using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Wjw1.Module.Task.Models;

namespace Wjw1.Module.Task.ViewModels
{
    /// <summary>
    /// 任务中心编辑模型
    /// </summary>
    public class TaskCenterEditModel : TaskCenter
    {
        [ScaffoldColumn(false)]
        public decimal Duration { get; set; }

        [ScaffoldColumn(false)]
        public string ActualEndTime { get; set; }
    }
}
