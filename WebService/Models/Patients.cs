using System;
using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class Patients
    {
        [Key]
        public long id { get; set; }

        public int? run { get; set; }
        public string dv { get; set; }
        public string other_identification { get; set; }
        public string name { get; set; }
        public string fathers_family { get; set; }
        public string mothers_family { get; set; }
        public string gender { get; set; }
        public DateTime? birthday { get; set; }
        public string status { get; set; }
        public DateTime? deceased_at { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
    }
}
