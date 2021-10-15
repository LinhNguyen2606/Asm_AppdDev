using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asm_AppdDev.Models
{
    public class Category
    {
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		[DisplayName("Category Name")]
		public string Name { get; set; }

		[Required]
		[StringLength(255)]
		[DisplayName("Descriptions")]
		public string Description { get; set; }
	}
}