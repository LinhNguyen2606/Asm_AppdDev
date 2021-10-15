using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm_AppdDev.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        //khoa ngoai vs bang user
        public string StaffId { get; set; }

        public ApplicationUser User { get; set; }

        //for validation
        [Required(ErrorMessage = "Please enter Training Staff Name")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Staff Name Should Between 4 to 30 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter Training Staff Age")]
        [Range(20, 80, ErrorMessage = "Training Staff Age Should Between 20 to 80")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please enter Training Staff Address")]
        public string Address { get; set; }
    }
}