using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Model.Entities
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Animals = new HashSet<Animal>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;
        [Column(TypeName = "text")]
        public string? Description { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Animal>? Animals { get; set; }

  
	}
}
