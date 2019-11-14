using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("race_type")]
    public class RaceType
    {
        [Key(false)]
        [Column("name")]
        public string Name { get; set; }

        [Column("run_count")]
        public int RunCount { get; set; }
    }
}