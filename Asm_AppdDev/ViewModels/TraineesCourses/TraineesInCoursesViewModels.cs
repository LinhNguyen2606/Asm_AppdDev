using Asm_AppdDev.Models;
using System.Collections.Generic;

namespace Asm_AppdDev.ViewModels.TraineesCourses
{
    public class TraineesInCoursesViewModels
    {
        public Course Course { get; set; }

        public List<Trainee> Trainees { get; set; }
    }
}