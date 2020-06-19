using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Services;

namespace WebService.Models
{
    public class Sospecha
    {
        private EsmeraldaContext context;
        public int ID { get; set; }
        public int age { get; set; }
        public enum gender {
            
            male,
            female,
            others,
            unknown
        }
        //public DateTime sampleAt { get; set; }
        //public int epidemiological_week { get; set; }
        //public string origin { get; set; }
        //public string status { get; set; }
        //public string runrun_medic { get; set; }
        //public string symptoms { get; set; }
        //public DateTime reception { get; set; }
        //public int receptor_id { get; set; }
        //public DateTime result_ifd_at { get; set; }
        //public string result_ifd { get; set; }
        //public string subtype { get; set; }
        //public DateTime pscr_sars_cov_2_at { get; set; }
        //public string pscr_sars_cov_2 { get; set; }
        //public string sample_type { get; set; }
        //public int validator_id { get; set; }
        //public DateTime sent_isp_at { get; set; }
        //public string external_laboratory { get; set; }
        //public int paho_flu { get; set; }
        //public int epivigila { get; set; }
        //public Boolean gestation { get; set; }
        //public int gestation_week { get; set; }
        //public Boolean close_contact { get; set; }
        //public Boolean functionary { get; set; }
        //public DateTime notification_at { get; set; }
        //public string notification_mechanism { get; set; }
        //public DateTime discharged_at { get; set; }
        //public string observation  { get; set; }
        //public Boolean discharge_test { get; set; }
        //public int minsal_ws_id { get; set; }
        //public int patient_id { get; set; }
        //public int laboratory_id { get; set; }
        //public int establishment_id { get; set; }
        //public int user_id { get; set; }
    }
}
