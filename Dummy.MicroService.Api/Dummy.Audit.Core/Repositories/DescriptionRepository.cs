using Dummy.Audit.Core.Model;
using Dummy.Audit.Core.Model.ClassesDomain;
using Dummy.Audit.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Dummy.Audit.Core.Repositories
{
    public class DescriptionRepository : IDescriptionRepository
    {
        DomainContext _domainContext;

        public DescriptionRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }


        public async Task<(string, string)> GetDescription(string tableName, string descriptionColumn, string id)
        {
            var query = $"SELECT {descriptionColumn} FROM {tableName} WHERE Id = @id";
            var parameter = new MySqlParameter("@id", id);

            var type = GetTypeDicc(tableName);
            if (type == null)
            {
                throw new ArgumentException("Invalid table name", nameof(tableName));
            }


            var algo = _domainContext.Set<OrderType>();
            var result = _domainContext.Database.SqlQuery<string>($"SELECT [name] FROM [order_types]").ToListAsync();

            return (descriptionColumn, "hola");
        }

        public Type GetTypeDicc(string tableName)
        {
            switch (tableName)
            {
                case "order_types":
                    return typeof(OrderType);
                case "address_types":
                    return typeof(AddressType);
                case "contact_types":
                    return typeof(ContactType);
                case "employee_types":
                    return typeof(EmployeeType);
                case "orders":
                    return typeof(Order);
                case "payment_methods":
                    return typeof(PaymentMethod);
                default:
                    return null;
            }
        }
    }
}
