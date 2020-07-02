using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCCSafeSpot.Models
{ 

    [Table("Endereco")]
    public class Endereco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Cep { get; set; }

        [MaxLength(3)]
        public string Estado { get; set; }

        [MaxLength(50)]
        public string CidadeBO { get; set; }

        public string Bairro { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public virtual IList<CrimeCadastrado> CrimesCadastrados { get; set; }

        public virtual IList<CrimeSSP> CrimesSSP { get; set; }
    }
}