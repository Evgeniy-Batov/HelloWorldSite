using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Models.ViewModels;

namespace WebSite.Common.Interfaces.Repositories
{
    public interface IMainPageItemsRepository
    {
        IEnumerable<MainPageItemVM> LoadUiData();
        IPagedResult<MainPageItemVM> LoadPaged(IPageRequest pageRequest);
        MainPageItemVM CreateItem(MainPageItemVM newItem);
        MainPageItemVM UpdateItem(MainPageItemVM updatedItem);
        void DeleteGroup(int itemId);
    }
}
