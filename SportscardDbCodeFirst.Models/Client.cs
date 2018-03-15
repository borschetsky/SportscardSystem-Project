﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportscardDbCodeFirst.Models
{
    public class Client
    {
        public Client()
        {
            this.Sportscards = new HashSet<Sportscard>();
            this.Visits = new HashSet<Visit>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid first name format!")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid last name format!")]
        public string LastName { get; set; }

        [Range(18, 100, ErrorMessage ="Invalid age format!")]
        public int? Age { get; set; }

        public Guid CompanyId { get; set; }

        [Required]
        public virtual Company Company { get; set; }

        public virtual ICollection<Sportscard> Sportscards { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}