using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Data.ORM;

namespace ZMZB_Entity.Entity
{
    [EntityMapper_TableName("t_role")]
    public class RoleEntity
    {
        [PrimaryKey(PrimaryType.Increment)]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public bool IsEnable { get; set; }
        public DateTime? CreateTime { get; set; }

    }
}
