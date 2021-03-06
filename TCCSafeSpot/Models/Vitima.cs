﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCCSafeSpot.Models
{
    [Table("Vitima")]
    public class Vitima
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [Required(ErrorMessage = "O campo 'E-mail' é obrigatório.")]
        public string Email { get; set; }

        [MaxLength(2)]
        public string Sexo { get; set; }

        public virtual IList<CrimeCadastrado> CrimesCadastrados { get; set; }

    }
}