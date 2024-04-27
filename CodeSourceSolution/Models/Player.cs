using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace CodeSourceSolution.Models
{
    public class Player
    {
        public Player()
        {
            SeriesEntries = new HashSet<SeriesEntry>();
        }
        public int PlayerId { get; set; }
        [Required(ErrorMessage = "Please provide the picture."), StringLength(100), Display(Name = "Player Name")]
        
        public string? PlayerName { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; } 
        [StringLength(100), Display(Name = "Phone Number")]
        public string? Phone { get; set; }
        [Required, StringLength(100), Display(Name = "Picture")]
        public string? Picture { get; set; }
        public bool? MaritalStatus { get; set; }
        public virtual ICollection<SeriesEntry> SeriesEntries { get; set; }
    }
}
