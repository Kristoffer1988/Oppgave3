using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oppgave3.DAL
{
    public class SkjemaDTO
    {
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string navn { get; set; }

        [RegularExpression(@"^([a-zæøåA-ZÆØÅ0-9_\-\.]+)@([a-zæøåA-ZÆØÅ0-9_\-\.]+)\.([a-zæøåA-ZÆØÅ]{2,5})$")]
        public string mail { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string spørsmål { get; set; }
    }
}