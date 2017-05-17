using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wjw1.Module.Task.ViewModels
{
    /// <summary>
    /// 任务内容列表模型
    /// </summary>
    public class TaskCenterListModel
    {
        public string UserName { get; internal set; }

        public string TaskType { get; internal set; }

        public string TaskExecutor { get; internal set; }

        public string CreatedBy { get; internal set; }

        [DataType(DataType.Html)]
        public string Content { get; internal set; }

        [DataType("Files")]
        public string Files { get; internal set; }
        public string TaskExecutorId { get; internal set; }
        public decimal Duration { get; internal set; }
        public string ScheduleEndTime { get; internal set; }
        public string Id { get; internal set; }
        public string ActualEndTime { get; internal set; }
        public string Title { get; internal set; }
        public string CreatedDate { get; internal set; }
    }
}
