using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Oppgave3.Model
{
    public class FAQ : IComparable<FAQ>

    {
        [Key]
        public int id { get; set; }

        [Required]
        public string spørsmål { get; set; }

        [Required]
        public string svar { get; set; }

        public int like { get; set; } = 0;

        public int dislike { get; set; } = 0;

        public int CompareTo(FAQ other)
        {
            if (this.like == other.like)
            {
                return this.id.CompareTo(other.id);
            }

            return other.like.CompareTo(this.like);
        }
    }
}