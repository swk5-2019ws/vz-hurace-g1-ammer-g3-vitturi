using Hurace.Core.Mapper.Attributes;

namespace Hurace.Domain
{
    [Table("start_list")]
    public class StartList : DataObject
    {
        [Column("number")]
        public int Number { get; set; }
        
        [Column("race_data_id")]
        public RaceData RaceData { get; set; }
    }
}