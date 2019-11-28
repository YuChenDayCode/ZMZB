using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMZB_Entity.Entity;
using ZMZB_Framework.Authorize;

namespace ZMZB_Framework.Menu
{
    public class MenuManage
    {
        static ConcurrentDictionary<string, IEnumerable<MyModuleGroupEntity>> myGroupEntitys;
        static MenuManage()
        {
            myGroupEntitys = new ConcurrentDictionary<string, IEnumerable<MyModuleGroupEntity>>();

        }
        public static bool LoadModules()
        {
            var ModuleGroups = AuthorizeModuleManage.GetAuthorizeModuleManage().Groups;
            myGroupEntitys.AddOrUpdate("menu", k => ModuleGroups, (k, value) => ModuleGroups);
            return true;
        }

        /// <summary>
        /// 获取菜单权限
        /// </summary>
        /// <param name="isDebug">是否管理员或调试账号</param>
        /// <param name="authorizes">权限</param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static IEnumerable<MenuItem> GetMenuItems(bool isDebug, IEnumerable<AuthorizeEntity> authorizes, int parentId = 0)
        {
            var menuItems = new List<MenuItem>();

            var groups = myGroupEntitys["menu"];
            foreach (var g in groups)
            {
                var menuItem = new MenuItem();
                if (g.Id > 0)
                {
                    menuItem.Id = g.Id;
                    menuItem.Describe = g.GroupName;
                    menuItem.SubMenuItem = from t in g.MyModules
                                           where (isDebug || authorizes.Any(a => Math.Floor(a.MenuId) == t.Id)) && t.IsMenu
                                           select CraterMenuItems(t, isDebug, authorizes);
                    menuItems.Add(menuItem);
                }
                else
                {
                    menuItems.AddRange(from t in g.MyModules
                                       where (isDebug || authorizes.Any(a => Math.Floor(a.MenuId) == t.Id)) && t.IsMenu
                                       select CraterMenuItems(t, isDebug, authorizes));
                }
            }
            return menuItems;
        }

        private static MenuItem CraterMenuItems(MyModuleEntity m, bool isDebug, IEnumerable<AuthorizeEntity> authorizes)
        {
            return new MenuItem()
            {
                Id = m.Id,
                IconClass = m.IsExtend ? string.Empty : m.IconClass,
                Describe = m.Describe,
                IsExtend = m.IsExtend,
                Url = $"{m.Area}/{m.ModuleName}/{m.Action ?? string.Empty}",
                SubMenuItem = m.IsExtend ? from t in m.Authorizes
                                           where isDebug || authorizes.Any(a => Math.Floor(a.MenuId) == m.Id) 
                                           select new MenuItem()
                                           {
                                               Id = t.Id,
                                               Describe = t.Name,
                                               IconClass = m.IconClass,
                                               Url = $"{m.Area}/{m.ModuleName}/{m.Action ?? string.Empty}?{ string.Join("&", (from p in t.Parameter select $"{p.Key}={p.Value}")) }"
                                           } : null
            };
        }

    }
}
