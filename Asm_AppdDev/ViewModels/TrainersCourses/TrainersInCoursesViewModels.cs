using Asm_AppdDev.Models;
using System.Collections.Generic;

namespace Asm_AppdDev.ViewModels.TrainersCourses
{
    public class TrainersInCoursesViewModels
    {
        public Course Course { get; set; }

        public List<Trainer> Trainers { get; set; }
    }
}