using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OgloszeniaHubertWebApi.Models
{
    public class OgloszeniaUser
    {
        public int Id { get; set; }
        [Required, StringLength(15)]
        public string UserName { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email in not valid")]
        public string Email { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        public string Wojewodztwo { get; set; }
        public string ImagePath { get; set; }
        public int Date { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        [NotMapped]
        public byte[] ImageArray { get; set; }
    }
}