using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.ProductModule
{
    public class Product : BaseClass<int>
    {

        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }




        #region RelationShips

        #region Product - ProductBrand (1:M)
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; } = default!;
        #endregion


        #region Product - ProductType (1:M)
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; } = default!;
        #endregion


        #endregion

    }
}
