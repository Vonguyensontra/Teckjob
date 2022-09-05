using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobHunt.Models
{
    public class ApplyJobRequestModel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string JobID { get; set; }
        [Required]
        public string PhoneUser { get; set; }
        public string CVOld { get; set; }
        [Required]
        public string Base64Content { get; set; }
        [Required]
        public string FileName { get; set; }
    }
}