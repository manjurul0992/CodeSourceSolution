namespace CodeSourceSolution.Models
{
    public class SeriesEntry
    {
        public int SeriesEntryId { get; set; }
        public int? PlayerId { get; set; }
        public int FormatId { get; set; }

        public virtual Format? Format { get; set; }
        public virtual Player? Player { get; set; }
    }
}
