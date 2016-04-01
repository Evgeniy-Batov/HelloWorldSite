using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Interfaces.Repositories;

namespace WebSite.Common.Utils
{
    public class PagedRequest:IPageRequest
    {
        private ushort _pageNumber;
        private ushort _pageSize;
        private string _sortColumn;
        private string _sortOrder;

        private bool _all;

        public bool All
        {
            get { return _all; }
        }

        public static PagedRequest Everything()
        {
            return new PagedRequest(1, ushort.MaxValue, String.Empty, String.Empty,true);
        }

        public PagedRequest(ushort pageNumber,ushort pageSize,String sortColumn,String sortOrder,bool all)
            :this(pageNumber,pageSize,sortColumn,sortOrder)
        {
            _all = all;
        }


        public PagedRequest(ushort pageNumber,ushort pageSize,String sortColumn,String sortOrder)
        {
            _pageSize   = pageSize;
            _pageNumber = pageNumber;
            _sortOrder  = sortOrder;
            _sortColumn = sortColumn;
        }

        public ushort PageNumber
        {
            get { return _pageNumber; }
        }

        public ushort PageSize
        {
            get { return _pageSize; }
        }

        public string SortColumng
        {
            get { return _sortColumn; }
        }

        public string SortOrder
        {
            get { return _sortOrder; }
        }
    }
}
