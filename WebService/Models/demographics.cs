using System;
using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class demographics
    {
        [Key]
        public long? id { get; set; }

        public string street_type { get; set; }
        public string address { get; set; }
        public string number { get; set; }
        public string department { get; set; }
        public string suburb { get; set; }
        public string nationality { get; set; }
        public long? region_id { get; set; }
        public long? commune_id { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public string telephone { get; set; }
        public string city { get; set; }
        public string telephone2 { get; set; }
        public string email { get; set; }
        public long? patient_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
    }
}
