

namespace CoreDemoSolution.Data.Classes
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Name{ get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Description { get; set; }

        public float Price { get; set; }
    }
}
