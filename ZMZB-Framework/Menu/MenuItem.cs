using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZB_Framework.Menu
{
    public class MenuItem
    {
        public double Id { get; set; }
        public string IconClass { get; set; }
        public string Describe { get; set; }

        public int GroupId { get; set; }
        public bool IsExtend { get; set; }
        public string Url { get; set; }
        public IEnumerable<MenuItem> SubMenuItem { get; set; }
    }
}
