using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Model.Entities
{
    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        public int Id { get; set; }

        public int AnimalId { get; set; }
        [StringLength(100)]
        [DisplayName("User Name")]
        public string? UserName { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime? CommentDate { get; set; }
        [Column(TypeName = "text")]
        public string? Content { get; set; }

        [ForeignKey("AnimalId")]
        [InverseProperty("Comments")]
        public virtual Animal Animal { get; set; } = null!;
    }
}
