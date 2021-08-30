using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O10.Nomy.Rarible.Models
{
    public class AllItemsDto
    {
        public int Total { get; set; }
        public string Continuation { get; set; }
        public List<RaribleItemDto> Items { get; set; }
    }
}
