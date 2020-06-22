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
        public int Id { get; set; }
		public string street_type { get; set; }
		public string address { get; set; }
		public string number { get; set; }
		public string department { get; set; }
		public string nationality { get; set; }
		public int region_id { get; set; }
		public int commune_id { get; set; }
		public string commune { get; set; }
		public string town { get; set; }
		public double latitude { get; set; }
		public double longitude  { get; set; }
		public string telephone { get; set; }
		public string telephone2 { get; set; }
		public string email { get; set; }
		public int patient_id { get; set; }
		public Nullable<DateTime> created_at { get; set; }
		public Nullable<DateTime> updated_at { get; set; }
		public Nullable<DateTime> deleted_at { get; set; }
	}
}
