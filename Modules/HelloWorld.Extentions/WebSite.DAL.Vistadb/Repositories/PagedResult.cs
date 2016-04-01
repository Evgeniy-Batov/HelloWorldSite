using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSite.Common.Interfaces.Repositories;

namespace WebSite.DAL.Vistadb.Repositories
{
    public class PagedResult<T> : IPagedResult<T>
    {
        private IEnumerable<T> _data;
        private ushort         _itemsPerPage;
        private ushort         _pageNumber;
        private ushort         _totalAmount;
        private ulong          _totalRecords;

        public PagedResult(IEnumerable<T> data,ushort itemsPerPage,ushort pageNumber,ushort totalAmount,ulong totalRecords)
        {
            _data = data;
            _itemsPerPage = itemsPerPage;
            _pageNumber = pageNumber;
            _totalAmount = totalAmount;
            _totalRecords = totalRecords;
        }

        public IEnumerable<T> rows
        {
            get { return _data; }
        }

        public uint total
        {
            get { return _totalAmount; }
        }

        public uint page
        {
            get { return _pageNumber; }
        }

        public ulong records
        {
            get { return _totalRecords; }
        }


        public IEnumerable<T> Data
        {
            get { return _data; }
        }

        public ushort ItemsPerPage
        {
            get { return _itemsPerPage; }
        }

        public ushort PageNumber
        {
            get { return _pageNumber; }
        }

        public ulong TotalAmount
        {
            get { return _totalAmount; }
        }
    }
}
