using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oppgave3.Model
{
    public class Kategori
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string kategori { get; set; }

        [Required]
        public List<FAQ> faqs { get; set; }
    }
}