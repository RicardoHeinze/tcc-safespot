﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCCSafeSpot.Models
{
    [Table("CrimeSSP")]
    public class CrimeSSP
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo 'Data' é obrigatório.")]
        public string Data { get; set; }

        public string Periodo { get; set; }

        public int TipoCrimeId { get; set; }
        
        public virtual TipoCrime TipoCrime { get; set; }

        public int EnderecoId { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public virtual Endereco Endereco { get; set; }
    }
}