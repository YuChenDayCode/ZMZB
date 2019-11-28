using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZB_Framework.Authorize
{
    //枚举值 不要设置为10的倍数 
    /// <summary>
    /// 页面具体权限
    /// </summary>
    public enum AuthorizeType
    {
        [Description("列表")]
        List = 1,

        [Description("详细")]
        Look = 2,

        [Description("新增")]
        Add = 3,

        [Description("修改")]
        Editor = 5,

        [Description("删除")]
        Delete = 7,

        [Description("导入")]
        Import = 8,

        [Description("导出")]
        Export = 8,

        [Description("自定义1")]
        Custom1 = 11,

        [Description("自定义2")]
        Custom2 = 12,

        [Description("自定义3")]
        Custom3 = 13,

        [Description("自定义4")]
        Custom4 = 14
    }
}
