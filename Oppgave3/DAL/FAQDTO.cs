using System.ComponentModel.DataAnnotations;

namespace Oppgave3.DAL
{
    public class FAQDTO
    {
        [Required]
        public int id { get; set; }

        [Required]
        public int likes { get; set; }

        [Required]
        public int dislikes { get; set; }
    }
}