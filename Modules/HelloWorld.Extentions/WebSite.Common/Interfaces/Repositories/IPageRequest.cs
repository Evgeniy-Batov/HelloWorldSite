using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Common.Interfaces.Repositories
{
    public interface IPageRequest
    {
        bool All {get;}
        ushort PageNumber { get; }
        ushort PageSize { get; }
        String SortColumng { get; }
        String SortOrder { get; }
    }
}
