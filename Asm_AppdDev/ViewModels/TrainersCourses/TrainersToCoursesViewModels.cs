using Asm_AppdDev.Models;
using System.Collections.Generic;

namespace Asm_AppdDev.ViewModels.TrainersCourses
{
    public class TrainersToCoursesViewModels
    {
        public int CourseId { get; set; }
        public int TrainerId { get; set; }

        public List<Course> Courses { get; set; }
        public List<Trainer> Trainers { get; set; }
    }
}