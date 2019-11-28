using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZB_Framework.Authorize
{
    public class MyModuleEntity
    {
        public string ModuleName { get; set; }
        /// <summary>
        /// 模型编号(不同系统可能相同，非唯一）
        /// </summary>
        public double Id { get; set; }
        public string Describe { get; set; }
        /// <summary>
        /// 是否是扩展权限
        /// </summary>
        public bool IsExtend { get; set; }


        /// <summary>
        /// 权限列表
        /// </summary>
        public IList<MyAuthorizeEntity> Authorizes { get; set; }

        public string Area { get; set; }

        public bool IsMenu { get; set; }
        /// <summary>
        /// 默认方法名
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        ///  分组信息
        /// </summary>
        public double ParentId { get; set; }
        /// <summary>
        /// 菜单图标样式
        /// </summary>
        public string IconClass { get; set; }
    }
}
