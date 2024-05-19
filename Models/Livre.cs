using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BibliothequeVieuxMontreal.Models
{
    public partial class Livre
    {
        public Livre()
        {
            Prets = new HashSet<Pret>();
        }

        public int Id { get; set; }
        [DisplayName("Titre du livre")]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s]{2,100}$", ErrorMessage = "Vous devez taper un titre comportant des lettres et/ou des chiffres de taille entre 2 et 100 caractères")]
        public string Titre { get; set; } = null!;
        [Required]
        public string Auteur { get; set; } = null!;
        [Required]
        [RegularExpression(@"^[0-9]{2,4}$", ErrorMessage = "Vous devez taper un nombre entre 2 et 4 chiffres")]
        public int Pages { get; set; }
        [Required]
        [DisplayName("Ctégorie")]
        public string Cetegorie { get; set; } = null!;

        public virtual ICollection<Pret> Prets { get; set; }
    }
}
