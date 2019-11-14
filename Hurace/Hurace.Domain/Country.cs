using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("country")]
    public class Country
    {
        [Key(false)]
        [Column("code")]
        public int Code
        {
            get => default;
            set { }
        }
    }
}