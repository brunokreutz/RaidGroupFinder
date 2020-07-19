using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RaidGroupFinder.Data
{
    public class TrainerCode
    {
        [Required]
        [StringLength(14, ErrorMessage = "Trainer Code is too long.")]
        [RegularExpression("[0-9]{4}[ ][0-9]{4}[ ][0-9]{4}|[0-9]{12}", ErrorMessage ="Invalid Trainer Code")]
        public string Value { get; set; }

        public TrainerCode()
        {
        }
    }
}
