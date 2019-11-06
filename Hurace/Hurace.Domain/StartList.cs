using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("start_lists")]
    public class StartList : DataObject
    {
        public Race Race
        {
            get => default;
            set { }
        }

        public Skier Skier
        {
            get => default;
            set { }
        }

        [Column("number")]
        public int Number
        {
            get => default;
            set { }
        }

        public Run Run
        {
            get => default;
            set { }
        }
    }
}