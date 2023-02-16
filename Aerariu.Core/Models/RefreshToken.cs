﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerariu.Core.Models
{
    [Table("RefreshTokens")]
    public class RefreshToken : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime ValidTo { get; set; }
        [Required]
        public int TotalRefresh { get; set; }
        [Required]
        public bool IsRevoked { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
