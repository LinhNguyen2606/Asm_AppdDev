using Asm_AppdDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asm_AppdDev.ViewModels
{
    public class CourseCategoriesViewModel
    {
        public Course Course { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}