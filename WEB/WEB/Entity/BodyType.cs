﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB.Entity
{
    [Table("BodyType")]
    public class BodyType
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(TypeName = "tinyint")]
        public byte body_id { get; set; }
        [Required]
        [StringLength(10), Column(TypeName = "varchar(10)")]
        public string body_name { get; set;}
        public ICollection<Car> Cars { get; set; }
    }
}
