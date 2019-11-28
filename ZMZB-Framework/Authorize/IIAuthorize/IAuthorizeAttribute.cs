using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMZB_Framework.Authorize
{
    public interface IAuthorizeAttribute
    {
        AuthorizeType Type { get; }
        string Name { get; }
    }
}
