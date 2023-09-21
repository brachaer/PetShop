namespace PetShop.Model.Utilities
{
	public class AnimalFilterRequest : PagedDataRequest
	{
		public int? CategoryId { get; set; }
		public AnimalFilterRequest(PagedDataRequest request)
		{
			this.PageNumber = request.PageNumber;
			this.PageSize = request.PageSize;
			this.CategoryId = null;
		}
		public AnimalFilterRequest()
		{

		}
		public override string ToString()
		{
			return $"page:{PageNumber};size:{PageSize};category:{CategoryId}";
		}
	}
}
