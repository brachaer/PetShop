using System.ComponentModel.DataAnnotations;

namespace PetShop.Model.Utilities
{
    public class PagedDataRequest
    {
        [Display(Name = "Page Number")]
        [Range(1, 1000)]
        public int PageNumber { get; set; } = 1;
        [Display(Name = "Page Size")]
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;
        public int Skip
        {
            get
            {
                return (PageNumber - 1) * PageSize;
            }
        }

        public override string ToString()
        {
            return $"page:{PageNumber};size:{PageSize}";
        }
    }
}
