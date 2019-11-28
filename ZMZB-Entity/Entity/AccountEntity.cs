using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Data.ORM;

namespace ZMZB_Entity.Entity
{
    [EntityMapper_TableName("t_account")]//表名
    public class AccountEntity
    {
        [PrimaryKey(PrimaryType.Increment)]
        public int Id { get; set; }
        [EntityMapper_ColumnName("LoginName")] //数据库的列名 
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string RealName { get; set; }
        public int RoleId { get; set; }
        public int Status { get; set; }
        public bool IsDebugOrAdmin { get; set; }
        public DateTime LastEditTime { get; set; }
        public DateTime CreateTime { get; set; }

        [EntityMapper_Ignore]//数据库操作时会忽略该字段 
        public string aa { get; set; } 
    }
}
