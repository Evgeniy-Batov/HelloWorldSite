using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Interfaces.Repositories;
using WebSite.Common.Models.ViewModels;
using WebSite.DAL.Db.Models;
using WebSite.DAL.Vistadb.Repositories;

namespace WebSite.DAL.Db.Repositories
{
    public class DbMainPageItemsRepository : IMainPageItemsRepository
    {
        private Context _dbContext;

        public IEnumerable<MainPageItemVM> LoadUiData()
        {
            List<MainPageItemVM> result = new List<MainPageItemVM>();

            foreach (MainPageItemDbM item in _dbContext.MainPageItems.Where(i => i.Order >= 0).OrderBy(i => i.Order))
            {
                result.Add(DbModel2ViewModel(item));
            }

            return result;
        }

        public DbMainPageItemsRepository(String connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentOutOfRangeException("connectionString");
            }
            _dbContext = new Context(connectionString);
        }

        private IEnumerable<MainPageItemVM> DbModel2ViewMode(IEnumerable<MainPageItemDbM> items)
        {
            List<MainPageItemVM> result = new List<MainPageItemVM>();
            if (items != null)
            {
                foreach (MainPageItemDbM c in items)
                {
                    result.Add(DbModel2ViewModel(c));
                }
            }
            return result;
        }

        private MainPageItemVM DbModel2ViewModel(MainPageItemDbM dbModel)
        {
            MainPageItemVM result = new MainPageItemVM();
            result.DestinationAction = dbModel.DestinationAction;
            result.DestinationActionParams = dbModel.DestinationActionParams;
            result.DestinationController = dbModel.DestinationController;
            result.Id = dbModel.Id;
            result.ImgUrl = dbModel.ImgUrl;
            result.ItemCss = dbModel.ItemCss;
            result.ItemText = dbModel.ItemText;
            result.ItemTitle = dbModel.ItemTitle;
            result.Order = dbModel.Order;

            return result;
        }

        public IPagedResult<MainPageItemVM> LoadPaged(IPageRequest pageRequest)
        {
            ulong totalMessages = (uint)_dbContext.MainPageItems.Count();
            ushort pageCount = (ushort)(totalMessages / pageRequest.PageSize + 1);
            ushort requestedPage = pageRequest.PageNumber;
            if (requestedPage < pageCount)
                requestedPage = pageCount;

            IEnumerable<MainPageItemDbM> items = null;

            if (pageRequest.All)
            {
                items = _dbContext.MainPageItems.ToList();
            }
            else if (pageRequest.SortOrder == "desc")
            {
                items = _dbContext.MainPageItems.OrderByDescending(m => m.Id).Skip(pageRequest.PageSize * (pageRequest.PageNumber - 1)).Take(pageRequest.PageSize).ToList();
            }
            else
            {
                items = _dbContext.MainPageItems.OrderBy(m => m.Id).Skip(pageRequest.PageSize * (pageRequest.PageNumber - 1)).Take(pageRequest.PageSize).ToList();
            }

            return new PagedResult<MainPageItemVM>(DbModel2ViewMode(items), pageRequest.PageSize, pageRequest.PageNumber, pageCount, totalMessages);
        }

        public MainPageItemVM CreateItem(MainPageItemVM newItem)
        {
            MainPageItemDbM newModel = new MainPageItemDbM(newItem);

            _dbContext.MainPageItems.Add(newModel);

            _dbContext.SaveChanges();

            return DbModel2ViewModel(newModel);
        }

        public MainPageItemVM UpdateItem(MainPageItemVM updatedItem)
        {
            MainPageItemDbM existingItem = _dbContext.MainPageItems.Where(i => i.Id == updatedItem.Id).FirstOrDefault();
            if (existingItem != null)
            {
                existingItem.ApplyChanges(updatedItem);
                _dbContext.SaveChanges();
            }
            return DbModel2ViewModel(existingItem);
        }

        public void DeleteGroup(int itemId)
        {
            MainPageItemDbM existingItem = _dbContext.MainPageItems.Where(i => i.Id == itemId).FirstOrDefault();
            if (existingItem != null)
            {
                _dbContext.MainPageItems.Remove(existingItem);
                _dbContext.SaveChanges();
            }
        }
    }
}
