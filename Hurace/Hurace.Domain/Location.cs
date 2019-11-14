using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("location")]
    public class Location : DataObject
    {
        [Column("name")]
        public string Name
        {
            get => default;
            set { }
        }

        [ForeignKey("country_code")]
        public Country Country
        {
            get => default;
            set { }
        }
    }
}