using Asm_AppdDev.Models;
using System.Collections.Generic;

namespace Asm_AppdDev.ViewModels.TraineesCourses
{
    public class TraineesToCoursesViewModels
    {
        public int CourseId { get; set; }
        public int TraineeId { get; set; }
        public List<Course> Courses { get; set; }

        public List<Trainee> Trainees { get; set; }
    }
}