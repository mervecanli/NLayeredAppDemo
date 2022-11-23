using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccesss.Abstract;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccesss.Concrete.EntityFramework
{
    public class EfCategoryDal: EfEntityRepositoryBase<Category, NorthwindContext>, ICategoryDal
    {
    }
}
