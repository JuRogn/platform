using Wjw1.Infrastructure.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace  Wjw1.Infrastructure //Todo: 验证命名空间是否需要保持一致
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public partial class ApplicationDbContext: IdentityDbContext<SysUser>
    {
        #region 本功能模块表
        #endregion
    }
}
