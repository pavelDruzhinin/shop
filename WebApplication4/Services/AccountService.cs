using System.Linq;
using WebApplication4.DataAccess;

namespace WebApplication4.Services
{
    public class AccountService
    {
        public bool Login(string login, string password)
        {
            using (var db = new ShopContext())
            {
                var customer = db.Customers.FirstOrDefault(x => x.Login == login && x.Password == password);

                return customer != null;
            }
        }
    }
}