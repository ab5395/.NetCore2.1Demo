

namespace CoreDemoSolution.Repository.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ProductModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter product name")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter product price")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter product price")]
        public float Price { get; set; }
    }
}
