using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared
{
    public class ProductQueryParam
    {
        public int? brandId { get; set; }

        public int? typeId { get; set; }

        public string? search { get; set; }

        public ProductSortingOptions? sort { get; set; }


        // make it as full prop for validate the entry data and give it default value

        private int _pageIndex = 1;

        public int PageIndex
        {
            get { return _pageIndex ; }

            set 
            {
               _pageIndex = (value <= 0) ? 1 : value;
            }
        }

        private const int _defucltPageSize = 5;
        private int _pageSize = _defucltPageSize;
        private const int _maxPageSize = 10;


        public int PageSize
        {
            get { return _pageSize ; }

            set 
            {
                if (_pageSize <= 0)
                    _pageSize = _defucltPageSize;
                else if (_pageSize >= 10)
                    _pageSize = _maxPageSize;
                else
                    _pageSize = value;
            }
        }


    }
}
