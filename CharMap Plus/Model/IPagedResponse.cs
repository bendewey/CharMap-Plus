using System.Collections.Generic;

namespace CharMap_Plus.Model
{
    public interface IPagedResponse<K>
    {
        IEnumerable<K> Items { get; set; }
        int VirtualCount { get; set; }
    }
}