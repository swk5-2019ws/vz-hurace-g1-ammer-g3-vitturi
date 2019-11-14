using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("start_list")]
    public class StartList : DataObject
    {
        [ForeignKey("race_id")]
        public Race Race { get; set; }

        [ForeignKey("skier_id")]
        public Skier Skier { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("run_number")]
        public int RunNumber { get; set; }
    }
}