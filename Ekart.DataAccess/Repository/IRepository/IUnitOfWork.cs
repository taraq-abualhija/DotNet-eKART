using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekart.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IProductRepository product { get; }
        ICategoryRepository category { get; }
        ICompanyRepository company { get; }
        IShoppingCartRepository shoppingCart { get; }
        IApplicationUserRepository applicationUser { get; }

        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }


        void Save();
    }
}
