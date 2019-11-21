using Hurace.Core.Interface;
using Hurace.Domain;

namespace Hurace.Core.Daos
{
    public class RunDao : DataObjectDao<Run>, IRunDao
    {
        public RunDao(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}