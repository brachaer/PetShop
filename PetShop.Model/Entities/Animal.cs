using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Model.Entities
{
    [Table("Animal")]
    public partial class Animal
    {
        public Animal()
        {
            Comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        [DisplayName("Animal Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;
        [Column(TypeName = "date")]
        [DisplayName("Animal Birth Date")]
        public DateTime? BirthDate { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please write short description for animal")]
        public string? Description { get; set; }
        [StringLength(255)]
        [DisplayName("Image")]
        public string? ImageUrl { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Animals")]
        public virtual Category? Category { get; set; } = null!;
        [InverseProperty("Animal")]
        public virtual ICollection<Comment> Comments { get; set; }= new List<Comment>();
  
    }
}
