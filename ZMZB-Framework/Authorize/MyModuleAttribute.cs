using Framework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZB_Framework.Authorize
{
    /// <summary>
    /// 模块特性类
    /// </summary>
    public class MyModuleAttribute : Attribute, IModuleAttribute
    {
        readonly int id;
        readonly string name;

        readonly string area;
        readonly string describe;
        readonly string action;
        readonly bool isMenu;
        readonly int parentId;
        readonly string iconClass;
        readonly string groupName;
        public int Id => id;

        public string Name => name;


        public string Area => area;

        public string Describe => describe;

        public string Action => action;

        public bool IsMenu => isMenu;

        public int ParentId => parentId;

        public string IconClass => iconClass;

        public string GroupName => groupName;

        public bool IsExtend { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module">模块类型</param>
        /// <param name="group">所属分组</param>
        /// <param name="area">所属区域</param>
        /// <param name="parentModule">所属上级模块</param>
        /// <param name="iconClass">ico图标</param>
        /// <param name="isMenu">是否显示为菜单</param>
        public MyModuleAttribute(ModuleEnum module, ModuleGroupEnum group = ModuleGroupEnum.Root, string area = null, string iconClass = ModuleIcon.Default, bool isMenu = true)
        {
            id = (int)module;
            this.describe = module.GetDescribeInfo();
            this.name = module.ToString();
            this.area = area;
            this.parentId = (int)group;
            this.groupName = group.GetDescribeInfo();
            this.iconClass = iconClass;
            this.isMenu = isMenu;
        }

        public MyModuleAttribute(ModuleEnum module, string action, ModuleGroupEnum group = ModuleGroupEnum.Root, string area = null) : this(module, group, area)
        {
            this.action = action;
        }
    }
}
