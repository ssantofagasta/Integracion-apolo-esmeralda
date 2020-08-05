using System;
using System.ComponentModel.DataAnnotations;

namespace WebService.Request
{
    public class Sospecha
    {
        public long? id { get; set; }
        public int? age { get; set; }
        public string gender { get; set; }
        public DateTime? sample_at { get; set; }
        public int? epidemiological_week { get; set; }
        public string origin { get; set; }
        public string status { get; set; }
        public string run_medic { get; set; }
        public string symptoms { get; set; }
        public DateTime? symptoms_at { get; set; }
        public DateTime? reception_at { get; set; }
        public long? receptor_id { get; set; }
        public DateTime? result_ifd_at { get; set; }
        public string result_ifd { get; set; }
        public string subtype { get; set; }
        public DateTime? pscr_sars_cov_2_at { get; set; }
        public string pscr_sars_cov_2 { get; set; }
        public string sample_type { get; set; }
        public long? validator_id { get; set; }
        public DateTime? sent_isp_at { get; set; }
        public string external_laboratory { get; set; }
        public int? paho_flu { get; set; }
        public int? epivigila { get; set; }
        public bool? gestation { get; set; }
        public int? gestation_week { get; set; }
        public bool? close_contact { get; set; }
        public bool? functionary { get; set; }
        public DateTime? notification_at { get; set; }
        public string notification_mechanism { get; set; }
        public DateTime? discharged_at { get; set; }
        public string observation { get; set; }
        public bool? discharge_test { get; set; }
        public long? minsal_ws_id { get; set; }
        public long? patient_id { get; set; }
        public long? laboratory_id { get; set; }
        public long? establishment_id { get; set; }
        public long? user_id { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? deleted_at { get; set; }
    }
}
