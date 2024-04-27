using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeSourceSolution.Models
{
    public class Format
    {
        public Format()
        {
            this.SeriesEntries = new HashSet<SeriesEntry>();
        }
        [Required]
        public int FormatId { get; set; }
        [Required, StringLength(100), Display(Name = "Format Name")]
        public string? FormatName { get; set; }

        public virtual ICollection<SeriesEntry> SeriesEntries { get; set; }
    }
}
