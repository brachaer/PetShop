using PetShop.Model.Utilities;

namespace PetShop.Data.Repositories
{
	public interface IRepository<T> where T : class
	{
		Task<T> Get(int id);
		Task<PagedDataResponse<T>> Get(PagedDataRequest request);
		Task<IEnumerable<T>> GetAll();
		Task<int> Add(T item);
		Task<T> Update(T item);
		Task<T> Delete(int id);
	}
}
