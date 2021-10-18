using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm_AppdDev.Models
{
    public class TraineesToCourses
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Trainee")]
        public int TraineeId { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Trainee Trainee { get; set; }

        public Course Course { get; set; }
    }
}