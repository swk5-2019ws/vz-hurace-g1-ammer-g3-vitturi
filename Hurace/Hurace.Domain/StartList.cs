using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("start_list")]
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

        [Column("run_number")]
        public int RunNumber
        {
            get => default;
            set { }
        }
    }
}