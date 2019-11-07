using Hurace.Core.Interface;
using Hurace.Domain;

namespace Hurace.Core.Daos
{
    public class SkierDao : DataObjectDao<Skier>, ISkierDao
    {
        public SkierDao(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}