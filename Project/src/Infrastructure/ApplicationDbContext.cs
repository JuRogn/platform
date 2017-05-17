using Wjw1.Infrastructure.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace  Wjw1.Infrastructure
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public partial class ApplicationDbContext: IdentityDbContext<SysUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Conventions.Remove<DecimalPropertyConvention>();
            //builder.Conventions.Add(new DecimalPropertyConvention(38, 4));

            //为表生成 基本的存储过程 Insert Update Delete
            //builder.Types().Configure(a => a.MapToStoredProcedures());

            // 变成内存表
            //builder.Entity<SysUserLog>().ForSqlServerIsMemoryOptimized();//没啥用

            // 创建索引
            //builder.Entity<SysUserLog>(a => a.HasIndex(b => new { b.Deleted, b.CreatedDate }));// 不管用 不知道为啥 再研究

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public Task<int> CommitAsync()
        {
            return SaveChangesAsync();
        }
        
        #region 系统表

        /// <summary>
        /// 缓存
        /// </summary>
        public DbSet<SysCache> SysCaches { get; set; }

        /// <summary>
        /// 企业
        /// </summary>
        public DbSet<SysEnterprise> SysEnterprises { get; set; }

        public DbSet<SysEnterpriseSysUser> SysEnterpriseSysUsers { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<SysRole> SysRoles { get; set; }

        //public DbSet<IdentityUserRole> UserRoles { get; set; }

        public DbSet<SysArea> SysAreas { get; set; }

        public DbSet<SysController> SysControllers { get; set; }

        public DbSet<SysControllerSysAction> SysControllerSysActions { get; set; }

        public DbSet<SysAction> SysActions { get; set; }

        public DbSet<SysRoleSysControllerSysAction> SysRoleSysControllerSysActions { get; set; }

        /// <summary>
        /// 系统帮助信息
        /// </summary>
        public DbSet<SysHelp> SysHelps { get; set; }

        /// <summary>
        /// 系统帮助信息分类
        /// </summary>
        public DbSet<SysHelpClass> SysHelpClasses { get; set; }

        /// <summary>
        /// 用户操作日志
        /// </summary>
        public DbSet<SysUserLog> SysUserLogs { get; set; }

        /// <summary>
        /// 用户搜索记录
        /// </summary>
        public DbSet<SysKeyword> SysKeywords { get; set; }

        /// <summary>
        /// 系统消息
        /// </summary>
        public DbSet<SysBroadcast> SysBroadcasts { get; set; }

        /// <summary>
        /// 系统消息读取记录
        /// </summary>
        public DbSet<SysBroadcastReceived> SysBroadcastReceiveds { get; set; }

        /// <summary>
        /// 语言包
        /// </summary>
        public DbSet<SysLanguagePack> SysLanguagePacks { get; set; }

        public DbSet<SysSignalR> SysSignalRs { get; set; }

        /// <summary>
        /// SignalR 在线人员信息
        /// </summary>
        public DbSet<SysSignalROnline> SysSignalROnlines { get; set; }

        /// <summary>
        /// 验证码存储
        /// </summary>
        public DbSet<VerifyCode> VerifyCodes { get; set; }

        /// <summary>
        /// 组织架构
        /// </summary>
        public DbSet<SysDepartment> SysDepartments { get; set; }

        /// <summary>
        /// 用户关联部门
        /// </summary>
        public DbSet<SysDepartmentSysUser> SysDepartmentSysUser { get; set; }


        #endregion
        
    }
}
