using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BibliothequeVieuxMontreal.Models
{
    public partial class Membre
    {
        public Membre()
        {
            Prets = new HashSet<Pret>();
        }

        [DisplayName("Numéro du membre")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom est obligatoire")]
        [DisplayName("Nom du membre")]
        public string Nom { get; set; } = null!;
        [Required(ErrorMessage = "Le prénom est obligatoire")]
        [DisplayName("Prénom du membre")]
        public string Prenom { get; set; } = null!;
        [DisplayName("Âge du membre")]
        [Required(ErrorMessage = "L'âge est obligatoire")]
        [Range(18, 65, ErrorMessage = "L'âge doit être comprise entre 18 et 65 inclusivement.")]
        public int Age { get; set; }

        public virtual ICollection<Pret> Prets { get; set; }
    }
}
