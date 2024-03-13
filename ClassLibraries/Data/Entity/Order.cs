﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public User Users { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string OrderCode { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string Address { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
