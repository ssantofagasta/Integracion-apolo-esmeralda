using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class demographics
    {
		[Key]
        public Nullable<long> id { get; set; }
		public string street_type { get; set; }
		public string address { get; set; }
		public string number { get; set; }
		public string department { get; set; }
		public string suburb { get; set; }
		public string nationality { get; set; }
		public Nullable<long> region_id { get; set; }
		public Nullable<long> commune_id { get; set; }
		public Nullable<double> latitude { get; set; }
		public Nullable<double> longitude  { get; set; }
		public string telephone { get; set; }
		public string  city { get; set; }
		public string telephone2 { get; set; }
		public string email { get; set; }
		public Nullable<long> patient_id { get; set; }
		public Nullable<DateTime> created_at { get; set; }
		public Nullable<DateTime> updated_at { get; set; }
		public Nullable<DateTime> deleted_at { get; set; }
	}
}
