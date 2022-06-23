﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Common
{
    public class News : BaseEntity<int>
    {
        [Required]
        [MaxLength(150)]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}
