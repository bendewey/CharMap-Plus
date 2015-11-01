using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharMap_Plus.Model
{
    public class PagedResponse<T> : IPagedResponse<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int VirtualCount { get; set; }
    }
}
