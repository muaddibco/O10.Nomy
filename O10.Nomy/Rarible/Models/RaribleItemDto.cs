using System.Collections.Generic;

namespace O10.Nomy.Rarible.Models
{
    public class RaribleItemDto
    {
        public string Id { get; set; }
        public string Contract { get; set; }
        public string TokenId { get; set; }
        public List<RaribleAccountDto> Creators { get; set; }
        public string Supply { get; set; }
        public string LazySupply { get; set; }
        public List<string> Owners { get; set; }
        public bool Deleted { get; set; }
        public ItemMetaDto Meta { get; set; }
    }
}
