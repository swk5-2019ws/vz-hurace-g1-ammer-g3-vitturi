using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("start_lists")]
    public class StartList : DataObject
    {
        [ForeignKey("race_id")]
        public Race Race
        {
            get => default;
            set { }
        }

        [ForeignKey("skier_id")]
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

        [ForeignKey("run_id")]
        public Run Run
        {
            get => default;
            set { }
        }
    }
}