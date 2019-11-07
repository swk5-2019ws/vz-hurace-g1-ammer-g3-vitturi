using Hurace.Core.Interface;
using Hurace.Domain;

namespace Hurace.Core.Daos
{
    public class StartListDao : DataObjectDao<StartList>, IStartListDao
    {
        public StartListDao(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}