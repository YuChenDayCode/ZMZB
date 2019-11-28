using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZB_Framework.Authorize
{
    public interface IModuleAttribute
    {

        int Id { get; }
        string Name { get; }

        string GroupName { get; }
        string Describe { get; }
        string Area { get; }
        /// <summary>
        /// 默认action
        /// </summary>
        string Action { get; }
        bool IsMenu { get; }

        /// <summary>
        /// 上级菜单模块
        /// </summary>
        int ParentId { get; }
        /// <summary>
        /// 菜单图标样式
        /// </summary>
        string IconClass { get; }
        /// <summary>
        /// 扩展标记
        /// </summary>
        bool IsExtend { get; set; }
    }
}
