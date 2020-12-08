using System.Collections.Generic;
using System.Threading.Tasks;
using EcoGrpc.Customer;

namespace EcoSpider.Repositories
{
    public class CustomerRepository
    {
        List<CustomerModel> _customers = new List<CustomerModel>()
        {
            new CustomerModel()
            {
                Id = 1, Name = "Yuri Difusor", Address = "Matelândia 674", EmailAddress = "YurinhoDifusor_2009gmail.com"
            },
            new CustomerModel()
            {
                Id = 2,
                Name = "Carlos Startupero", Address = "Visconde do Rio Branco 12345",
                EmailAddress = "carlosnaopagosalgado@gmail.com"
            },
            new CustomerModel()
            {
                Id = 3,
                Name = "Lucas da Stradinha Rebaxada", Address = "Visconde do Rio Branco 447",
                EmailAddress = "lucas_stradinha_2010@gmail.com"
            },
        };

        public Task<List<CustomerModel>> GetCustomers()
        {
            return Task.FromResult(_customers);
        }
    }
}