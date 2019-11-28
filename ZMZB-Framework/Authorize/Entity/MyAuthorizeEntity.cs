using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZB_Framework.Authorize
{
    public class MyAuthorizeEntity
    {
        /// <summary>
        /// 唯一,为浮点型数据,用以划分反射出来的controller及action
        /// </summary>
        public double Id { get; set; }
        /// <summary>
        /// 名称,显示在菜单上面的名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路由名,用于显示简单路由名如HomeController,显示Home
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 所属模块编号
        /// </summary>
        public double ParentId { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public Dictionary<string, object> Parameter { get; set; }
        /// <summary>
        /// 是否显示为菜单
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 菜单图标样式
        /// </summary>
        public string IconClass { get; set; }

    }
}
