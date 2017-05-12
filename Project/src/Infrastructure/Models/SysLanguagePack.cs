using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Wjw1.Infrastructure.Models
{
    public class SysLanguagePack : DbSetId
    {

        public SysLanguagePack()
        {
          
        }

        [MaxLength(200)]
        [Required]
        public string Language { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        [MaxLength(200)]
        [Required]

        public string Value { get; set; }
    }
}