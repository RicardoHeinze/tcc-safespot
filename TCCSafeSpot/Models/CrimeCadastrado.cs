using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCCSafeSpot.Models
{
    [Table("CrimeCadastrado")]
    public class CrimeCadastrado
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira uma descrição referente ao Crime.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo 'Data' é obrigatório.")]
        public string Data { get; set; }        

        public int VitimaId { get; set; }
        
        public virtual Vitima Vitima { get; set; }

        public int TipoCrimeId { get; set; }

        [Required(ErrorMessage = "Escolha um Tipo de Crime.")]
        public virtual TipoCrime TipoCrime { get; set; }

        public int EnderecoId { get; set; }
        
        public virtual Endereco Endereco { get; set; }

    }
}