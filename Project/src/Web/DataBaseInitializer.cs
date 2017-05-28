using Wjw1.Infrastructure;
using Wjw1.Infrastructure.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Web
{
    //数据初始化程序
    public class DataBaseInitializer: IDataBaseInitializer
    {

        public  void SeedData(IApplicationBuilder app)
        {


            using (var serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;

                using (var db = serviceProvider.GetService<ApplicationDbContext>())
                {
                    //if (!db.Database.EnsureCreated())
                    db.Database.Migrate();



                    //初始化 数据

                    // 企业信息
                    var sysEnterprises = new[]
                    {
                        new SysEnterprise
                        {
                            Id = "100",
                            EnterpriseName = "精一科技"//系统初始化为平台运营方
                        },
                        new SysEnterprise
                        {
                            Id = "100001",
                            EnterpriseName = "测试企业"//系统测试用，系统交付客户公司名称
                        }
                    };
                    
                    foreach (var sysEnterprise in sysEnterprises)
                        if (
                            !db.SysEnterprises.AnyAsync(
                                    a => a.EnterpriseName == sysEnterprise.EnterpriseName && a.Id == sysEnterprise.Id)
                                .Result)
                            db.SysEnterprises.AddAsync(sysEnterprise);

                    // 定义好的区域
                    var sysAreas = new[]
                    {
                        new SysArea
                        {
                            Id = "Dev",
                            AreaName = "Dev",
                            Name = "开发平台",//系统开发和系统部署
                            SystemId = "999"
                        },
                        new SysArea
                        {
                            Id = "Platform",
                            AreaName = "Platform",
                            Name = "管理平台",
                            SystemId = "002"
                        }
                    };

                    foreach (var sysArea in sysAreas)
                        if (
                            !db.SysAreas.AnyAsync(
                                a => a.Id == sysArea.Id &&
                                     a.AreaName == sysArea.AreaName && a.Name == sysArea.Name &&
                                     a.SystemId == sysArea.SystemId).Result)
                            db.SysAreas.AddAsync(sysArea).Wait();
                    
                    // 操作类型
                    var sysActions = new[]
                    {
                        new SysAction
                        {
                            Name = "列表",
                            ActionName = "Index",
                            SystemId = "001",
                            System = true
                        },
                        new SysAction
                        {
                            Name = "详细",
                            ActionName = "Details",
                            SystemId = "003",
                            System = true
                        },
                        new SysAction
                        {
                            Name = "新建",
                            ActionName = "Create",
                            SystemId = "004",
                            System = true
                        },
                        new SysAction
                        {
                            Name = "编辑",
                            ActionName = "Edit",
                            SystemId = "005",
                            System = true
                        },
                        new SysAction
                        {
                            Name = "删除",
                            ActionName = "Delete",
                            SystemId = "006",
                            System = true
                        },

                        new SysAction
                        {
                            Name = "导出",
                            ActionName = "Report",
                            SystemId = "007",
                            System = false
                        },

                        new SysAction
                        {
                            Name = "导入",
                            ActionName = "Import",
                            SystemId = "008",
                            System = false
                        }
                        //可手动添加其他需要的Action
                    };
                    foreach (var sysAction in sysActions)
                        if (!db.SysActions.AnyAsync(a => a.ActionName == sysAction.ActionName).Result)
                            db.SysActions.AddAsync(sysAction).Wait();

                    #region SysController 控制器

                    var sysControllers = new[]
                    {
                        #region 基础内容 100
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "管理平台",
                            ControllerName = "Home",
                            SystemId = "100",
                            Display = false
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "使用帮助",
                            ControllerName = "Help",
                            SystemId = "100200",
                            Display = false
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "消息中心",
                            ControllerName = "MyMessageCenter",
                            SystemId = "100300",
                            Display = false
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "修改密码",
                            ControllerName = "ChangePassword",
                            SystemId = "100400",
                            Display = false
                        },

                        #endregion other                



                        #region 任务管理 800
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "任务管理",
                            ControllerName = "Index",
                            SystemId = "800",
                            Ico = "fa-line-chart",
                            Display = true
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "任务中心",
                            ControllerName = "TaskCenter",
                            SystemId = "800200",
                            Ico = "fa-tasks"
                        },

                        #endregion

                        #region 用户管理 900
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "用户管理",
                            ControllerName = "Index",
                            SystemId = "900",
                            Ico = "fa-users",
                            Display = true
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "组织架构",
                            ControllerName = "SysDepartment",
                            SystemId = "900200",
                            Ico = "fa-dot-circle-o"
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "角色管理",
                            ControllerName = "SysRole",
                            SystemId = "900300",
                            Ico = "fa-dot-circle-o"
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "用户管理",
                            ControllerName = "SysUser",
                            SystemId = "900400",
                            Ico = "fa-dot-circle-o"
                        },

                        #endregion
                
                        #region 系统开发配置 950
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "系统设置",
                            ControllerName = "Index",
                            SystemId = "950",
                            Ico = "fa-cog"
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "操作类型",
                            ControllerName = "SysAction",
                            SystemId = "950300",
                            Ico = "fa-th-large"
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "系统模块",
                            ControllerName = "SysController",
                            SystemId = "950400",
                            Ico = "fa-puzzle-piece"
                        },

                            new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "语言",
                            ControllerName = "Culture",
                            SystemId = "950600",
                            Ico = "fa-language"
                        },

                            new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "语言包",
                            ControllerName = "Resource",
                            SystemId = "950601",
                            Ico = "fa-language"
                        },

                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "帮助信息",
                            ControllerName = "SysHelp",
                            SystemId = "950900",
                            Ico = "fa-info-circle"
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "帮助信息分类",
                            ControllerName = "SysHelpClass",
                            SystemId = "950950",
                            Ico = "fa-info-circle"
                        },
                        new SysController
                        {
                            SysAreaId = "Platform",
                            Name = "用户日志",
                            ControllerName = "SysUserLog",
                            SystemId = "950990",
                            Ico = "fa-calendar"
                        },

                        #endregion
                    };

                    foreach (var sysController in sysControllers)
                        if (
                            !db.SysControllers.AnyAsync(
                                a =>
                                    a.SysAreaId == sysController.SysAreaId &&
                                    a.ControllerName == sysController.ControllerName).Result)
                            db.SysControllers.AddAsync(sysController).Wait();

                    #endregion

                    db.SaveChangesAsync().Wait();
                    //关联系统默认Action到控制器
                    var sysControllerSysActions = (from sysAction in db.SysActions.Where(a => a.System)
                                                   from sysController in db.SysControllers
                                                   select
                                                   new SysControllerSysAction
                                                   {
                                                       SysActionId = sysAction.Id,
                                                       SysControllerId = sysController.Id
                                                   }).ToArray();

                    foreach (var sysControllerSysAction in sysControllerSysActions)
                        if (!db.SysControllerSysActions.AnyAsync(
                            a =>
                                a.SysActionId == sysControllerSysAction.SysActionId &&
                                a.SysControllerId == sysControllerSysAction.SysControllerId).Result)
                            db.SysControllerSysActions.AddAsync(sysControllerSysAction).Wait();

                    db.SaveChangesAsync().Wait();

                    // 角色及权限数据
                    #region 默认角色
                    var defualtRole = new SysRole
                    {
                        Id = "DefaultRole",
                        Name = "DefaultRole",
                        RoleName = "默认角色",
                        System = true,
                        SystemId = "998",
                        EnterpriseId = "100"
                    };

                    if (!db.SysRoles.AnyAsync(a => a.Id == defualtRole.Id).Result)
                        db.SysRoles.AddAsync(defualtRole).Wait();

                    #endregion
                    #region  超级管理员
                    var sysRole = new SysRole
                        {
                            Id = "SuperAdmin",
                            Name = "SuperAdmin",
                            RoleName = "超级管理员",
                            System = true,
                            SystemId = "999",
                            EnterpriseId = "100"
                        };

                        if (!db.SysRoles.AnyAsync(a => a.Id == sysRole.Id).Result)
                            db.SysRoles.AddAsync(sysRole).Wait();

                    //超级管理员自动获得所有权限 Todo:舍弃，超级管理员应该只有系统设置权限 区域/控制器/Action/企业或租户
                    var sysRoleSysControllerSysActions = (from aa in db.SysControllerSysActions
                                                              select
                                                              new SysRoleSysControllerSysAction
                                                              {
                                                                  SysControllerSysActionId = aa.Id,
                                                                  RoleId = sysRole.Id
                                                              }).ToArray();

                        foreach (var sysRoleSysControllerSysAction in sysRoleSysControllerSysActions)
                            if (
                                !db.SysRoleSysControllerSysActions.AnyAsync(
                                    a =>
                                        a.RoleId == sysRoleSysControllerSysAction.RoleId &&
                                        a.SysControllerSysActionId ==
                                        sysRoleSysControllerSysAction.SysControllerSysActionId).Result)
                                db.SysRoleSysControllerSysActions.AddAsync(sysRoleSysControllerSysAction).Wait();
                    
                    db.SaveChangesAsync().Wait();
                    #endregion
                }
            }
        }
    }
}
