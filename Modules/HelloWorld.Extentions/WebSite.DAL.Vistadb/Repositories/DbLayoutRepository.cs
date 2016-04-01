using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Interfaces.Repositories;
using WebSite.Common.Models.ViewModels;
using WebSite.DAL.Db.Models;

namespace WebSite.DAL.Db.Repositories
{
    public class DbLayoutRepository : ILayoutRepository
    {
        private Context _dbContext;

        public DbLayoutRepository(String connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentOutOfRangeException("connectionString");
            }
            _dbContext = new Context(connectionString);
        }

        private PageLayoutVM DbModel2ViewModel(PageLayoutDbM db)
        {
            if (db == null)
                return null;

            PageLayoutVM result = new PageLayoutVM();
            result.Description  = db.Description;
            result.Id           = db.Id;
            result.KeyWords     = db.KeyWords;
            result.Markup       = db.Markup;
            result.PageName     = db.PageName;
            result.PageTitle    = db.PageTitle;
            result.Robots       = db.Robots;
            result.TitleH1      = db.TitleH1;
            
            return result;
        }

        public IEnumerable<PageLayoutVM> LoadAllLayouts()
        {
            List<PageLayoutVM> res = new List<PageLayoutVM>();
            foreach (PageLayoutDbM m in _dbContext.PageLayouts)
            {
                res.Add(DbModel2ViewModel(m));
            }
            return res;
        }

        public PageLayoutVM LoadLayout(string pageName)
        {
            return DbModel2ViewModel(_dbContext.PageLayouts.FirstOrDefault(l=>l.PageName.Equals(pageName)));
        }

        public PageLayoutVM UpdateLayout(PageLayoutVM updatedLayout)
        {
            PageLayoutDbM layout = _dbContext.PageLayouts.FirstOrDefault(l => l.Id == updatedLayout.Id);
            if (layout == null)
            {
                layout = new PageLayoutDbM(updatedLayout);
                _dbContext.PageLayouts.Add(layout);
            }
            else
            {
                layout.ApplyChanges(updatedLayout);
            }
            _dbContext.SaveChanges();
            return DbModel2ViewModel(layout);
        }
    }
}
