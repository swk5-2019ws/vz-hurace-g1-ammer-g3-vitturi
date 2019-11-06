using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    public abstract class DataObject
    {
        [Key]
        [Column("id")]
        public int Id
        {
            get => default;
            set { }
        }
    }
}