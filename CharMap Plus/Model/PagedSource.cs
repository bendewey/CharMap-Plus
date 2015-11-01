using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharMap_Plus.Model
{
    public class PagedSource<T> : IPagedSource<T>
    {
        private IEnumerable<T> _source;
        private int _count;

        public PagedSource(IEnumerable<T> source)
        {
            _source = source;
            _count = source.Count();
        }

        public Task<IPagedResponse<T>> GetPage(int page, int pageSize)
        {
            return Task.FromResult((IPagedResponse<T>)new PagedResponse<T>
            {
                Items = _source.Skip(page * pageSize).Take(pageSize),
                VirtualCount = _count
            });
        }
    }
}
