using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliothequeVieuxMontreal.Models
{
    public partial class Retard
    {
        public int Id { get; set; }
        [Required]
        public int IdMembre { get; set; }
        [Required]
        public int IdLivre { get; set; }
        [Required]
        public int Nbjour { get; set; }
        [Required]
        public DateTime? DatePret { get; set; }

    }
}
