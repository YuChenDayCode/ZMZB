using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Data.ORM;

namespace ZMZB_Entity.Entity
{
    [EntityMapper_TableName("t_authorize")]
    public class AuthorizeEntity
    {
        [PrimaryKey(PrimaryType.Increment)]
        public Int32 Id { get; set; }
        public Double MenuId { get; set; }
        public Int32 RoleId { get; set; }

    }
}
