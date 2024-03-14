﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class CartItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public User Users { get; set; }

        [ForeignKey("ProductId")]
        public Product Products { get; set; }

        [Range(1, byte.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        [Required]
        public byte Quantity { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}