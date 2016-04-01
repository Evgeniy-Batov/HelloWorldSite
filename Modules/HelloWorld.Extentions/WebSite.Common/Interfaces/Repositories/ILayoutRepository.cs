using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;

namespace WebSite.Common.Interfaces.Repositories
{
    public interface ILayoutRepository
    {
        IEnumerable<PageLayoutVM> LoadAllLayouts();
        PageLayoutVM LoadLayout(String pageName);
        PageLayoutVM UpdateLayout(PageLayoutVM updatedLayout);
    }
}
