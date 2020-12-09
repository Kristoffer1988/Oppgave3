using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oppgave3.Model
{
    public class Skjema
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string navn { get; set; }

        [Required]
        public string mail { get; set; }

        [Required]
        public string spørsmål { get; set; }
    }
}