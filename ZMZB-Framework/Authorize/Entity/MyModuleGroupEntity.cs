using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZB_Framework.Authorize
{
    public class MyModuleGroupEntity
    {
        public double Id { get; set; }
        public string GroupName { get; set; }
        public string GroupDescribe { get; set; }
        public double ParentId { get; set; }

        public IList<MyModuleEntity> MyModules { get; set; }
    }
}
