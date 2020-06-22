using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class Patients
    {
		[Key]
        public int id { get; set; }
		public int run { get; set; }
		public string dv { get; set; }
		public string other_identification { get; set; }
		public string name { get; set; }
		public string fathers_family { get; set; }
		public string mothers_family { get; set; }
		public string gender { get; set; }
		public Nullable<DateTime> birthday { get; set; }
		public string status { get; set; }
		public Nullable<DateTime> deceased_at { get; set; }
		public Nullable<DateTime> created_at { get; set; }
		public Nullable<DateTime> updated_at { get; set; }
		public Nullable<DateTime> deleted_at { get; set; }

	}
}
