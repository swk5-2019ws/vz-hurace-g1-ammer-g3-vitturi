using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("countries")]
    public class Country
    {
        [Key]
        [Column("national_code")]
        public int NationalCode
        {
            get => default;
            set { }
        }
    }
}