using System;
using System.Collections.Generic;
using System.Text;

namespace ogloszeniahubert.Models
{
    public class OgloszeniaUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        public string Wojewodztwo { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string ImagePath { get; set; }
        public string FullLogoPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return string.Empty;
                }
                return String.Format("https://ogloszeniahubert1.azurewebsites.net/{0}",ImagePath.Substring(1));
            }
        }
        public int Date { get; set; }
        public object ImageArray { get; set; }


    }
}
