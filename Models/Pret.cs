using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BibliothequeVieuxMontreal.Models
{
    public partial class Pret
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Livre")]
        public int IdLivre { get; set; }
        [Required]
        [DisplayName("Membre")]
        public int IdMembre { get; set; }
        [Required]
        [DisplayName("Date de début")]
        public DateTime DateDebut { get; set; }
        [DisplayName("Date de Fin")]
        public DateTime DateFin { get; set; }
        [DisplayName("Livre")]
        public virtual Livre IdLivreNavigation { get; set; } = null!;
        [DisplayName("Membre")]
        public virtual Membre IdMembreNavigation { get; set; } = null!;
    }
}
//[DataType(DataType.Date)]