using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("country")]
    public class Country : DataObject
    {
        [Column("code")]
        public string Code { get; set; }
    }
}