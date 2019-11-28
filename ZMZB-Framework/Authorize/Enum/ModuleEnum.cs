using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZB_Framework.Authorize
{
    /// <summary>
    /// 菜单枚举
    /// </summary>
    public enum ModuleEnum
    {
        Empty = 0,


        [Description("基础功能")]
        Home = 1,

        [Description("角色信息")]
        Role = 2,


    }
}
