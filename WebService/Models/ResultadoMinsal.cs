using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class ResultadoMinsal
    {
        public long? id_muestra { get; set; }

        public string resultado { get; set; }
    }
}

