using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Farmer")]
        public string FarmerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        public DateTime ProductionDate { get; set; }

        public virtual ApplicationUser Farmer { get; set; }
    }
}