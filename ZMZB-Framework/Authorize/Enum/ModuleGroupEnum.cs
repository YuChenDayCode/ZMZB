using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZB_Framework.Authorize
{
    /// <summary>
    /// 模块组枚举
    /// </summary>
    public enum ModuleGroupEnum
    {
        [Description("根目录")]
        Root = 0,

        [Description("系统管理")]
        BaseInfo = 1,
        [Description("综合查询")]
        Business1 = 2,
        [Description("费用管理")]
        FeeMange = 3,
        [Description("物品管理")]
        GoodsManage = 2,
    }
}
