using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Entities;
using WebApiDemo.Model;

namespace WebApiDemo.DataAccess
{
    public class EfProductDal:EfEntityRepositoryBase<NorthwindContext,Product>,IProductDal
    {
        public List<ProductModel> GetProductWithDetails()
        {
            using (NorthwindContext context=new NorthwindContext())
            {
               
                
                var details = from p in context.Products
                    join c in context.Categories on p.CategoryId equals c.CategoryId
                    select new ProductModel()
                    {
                        ProductId = p.ProductId,
                        CategoryName = c.CategoryName,
                        ProductName = p.ProductName,
                        
                    };
               
                
                return details.ToList();

            }
          
        }
    }
}
