using Wjw1.Infrastructure.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wjw1.Module.Task.Models;

namespace  Wjw1.Module.Task {
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public  class ApplicationDbContext: IdentityDbContext<SysUser>
    {
        #region 本功能模块表
        DbSet<TaskCenter> TaskCenters;
        #endregion
    }
}
