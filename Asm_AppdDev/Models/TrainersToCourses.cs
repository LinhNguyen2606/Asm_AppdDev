using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm_AppdDev.Models
{
    public class TrainersToCourses
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Trainer")]
        public int TrainerId { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Trainer Trainer { get; set; }

        public Course Course { get; set; }
    }
}