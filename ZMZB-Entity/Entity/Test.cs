using Framework.Data.ORM;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCloud.Entity.Entity
{
    [EntityMapper_TableName("test1")]
    public class Test
    {
        [PrimaryKey(PrimaryType.Increment)]
        public int Id { get; set; }
        public string test { get; set; }
    }
}
