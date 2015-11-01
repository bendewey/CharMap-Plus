using System.Threading.Tasks;

namespace CharMap_Plus.Model
{
    public interface IPagedSource<K>
    {
        Task<IPagedResponse<K>> GetPage(int page, int pageSize);
    }
}