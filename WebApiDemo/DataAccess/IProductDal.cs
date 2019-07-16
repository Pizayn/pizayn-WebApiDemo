using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Entities;
using WebApiDemo.Model;

namespace WebApiDemo.DataAccess
{
    public interface IProductDal:IEfEntityRepositoryBase<Product>
    {
        List<ProductModel> GetProductWithDetails();
    }
}
