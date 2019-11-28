using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Data.ORM
{
    /// <summary>
    /// 主键标识
    /// </summary>.
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {
        readonly PrimaryType type;

        /// <summary>
        /// 主键类型
        /// </summary>
        public PrimaryType PrimaryType => type;
        /// <summary>
        /// 标识主键和类型
        /// </summary>
        /// <param name="type"></param>
        public PrimaryKey(PrimaryType type)
        {
            this.type = type;
        }

    }
}
