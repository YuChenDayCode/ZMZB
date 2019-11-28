using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZMZB_Framework.Authorize;

namespace ZMZB_Framework.Authorize
{
    public class AuthorizeModuleManage
    {
        private static AuthorizeModuleManage instance;
        protected AuthorizeModuleManage() { }
        readonly static IList<MyModuleEntity> modules;
        readonly static ConcurrentDictionary<int, MyModuleGroupEntity> hash; //权限缓存
        static AuthorizeModuleManage()
        {
            hash = new ConcurrentDictionary<int, MyModuleGroupEntity>();
            modules = new List<MyModuleEntity>();
        }
      
        public static AuthorizeModuleManage GetAuthorizeModuleManage()
        {
            return instance = instance ?? new AuthorizeModuleManage();
        }
        public IEnumerable<MyModuleGroupEntity> Groups => hash.Values;
        public IEnumerable<MyModuleEntity> Modules => modules;
        public MyModuleEntity this[string name] { get { return (from t in modules where t.ModuleName == name select t).FirstOrDefault(); } } //索引函数

        public static void Register<T, K>(Assembly assembly)
           where T : Attribute, IAuthorizeAttribute
           where K : Attribute, IModuleAttribute
        {

            var comparer = new MyAuthorizeComparer();
            var tt = from t in assembly.GetTypes() let interfaceType = t.GetInterface(typeof(IAuthorize).FullName) where interfaceType != null select t;
            foreach (var t in tt)
            {
                MyModuleEntity model = null;
                var moduleAttr = (IModuleAttribute)t.GetCustomAttribute(typeof(K), false);
                if (moduleAttr != null)
                {
                    var authorizeEntitys = GetAuthorizes<T>(comparer, t, moduleAttr);
                    model = new MyModuleEntity()
                    {
                        IsExtend = moduleAttr.IsExtend,
                        Authorizes = authorizeEntitys.ToList(),
                        Describe = moduleAttr.Describe,
                        ModuleName = moduleAttr.Name,
                        Id = moduleAttr.Id,
                        Area = moduleAttr.Area,
                        Action = moduleAttr.Action,
                        IsMenu = moduleAttr.IsMenu,
                        ParentId = moduleAttr.ParentId,
                        IconClass = moduleAttr.IconClass,

                    };
                    modules.Add(model);

                    hash.GetOrAdd(moduleAttr.ParentId, k => new MyModuleGroupEntity()
                    {
                        Id = moduleAttr.ParentId,
                        GroupName = moduleAttr.GroupName,
                        MyModules = new List<MyModuleEntity>(),
                        ParentId = 0,
                    }).MyModules.Add(model);

                }
            }
        }
        private static IEnumerable<MyAuthorizeEntity> GetAuthorizes<T>(MyAuthorizeComparer comparer, Type t, IModuleAttribute moduleAttr) where T : Attribute, IAuthorizeAttribute
        {
            var z = from t1 in t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    let attr = (IAuthorizeAttribute)t1.GetCustomAttribute(typeof(T), false)
                    where attr != null
                    select new MyAuthorizeEntity()
                    {
                        Id = Convert.ToDouble($"{moduleAttr.Id}.{(int)attr.Type}"),
                        MethodName = t1.Name,
                        Name = attr.Name,
                        ParentId = moduleAttr.ParentId,
                        IconClass = moduleAttr.IconClass
                    };
            var result = z.Distinct(comparer);

            return result;
        }
    }
    /// <summary>
    /// 去重相同项
    /// </summary>
    public class MyAuthorizeComparer : IEqualityComparer<MyAuthorizeEntity>
    {
        public bool Equals(MyAuthorizeEntity x, MyAuthorizeEntity y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(MyAuthorizeEntity obj)
        {
            return 0;
        }
    }

}
