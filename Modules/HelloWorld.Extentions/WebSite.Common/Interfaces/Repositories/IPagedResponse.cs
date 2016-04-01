using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Common.Interfaces.Repositories
{
    public interface IPagedResult<T>
    {
        IEnumerable<T> Data { get; }
        ushort ItemsPerPage { get; }
        ushort PageNumber   { get; }
        ulong  TotalAmount  { get; }
    }
}
