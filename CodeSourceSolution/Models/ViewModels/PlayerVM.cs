using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace CodeSourceSolution.Models.ViewModels
{
    public class PlayerVM
    {
        public PlayerVM()
        {
            this.FormatList = new List<int>();
        }

        public int PlayerId { get; set; }
        [Required]
        public string? PlayerName { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public string? Picture { get; set; }
        [Required(ErrorMessage = "Please provide the picture.")]
        public IFormFile? PicturePath { get; set; }
        public bool MaritalStatus { get; set; }

        public List<int> FormatList { get; set; }
    }
}
