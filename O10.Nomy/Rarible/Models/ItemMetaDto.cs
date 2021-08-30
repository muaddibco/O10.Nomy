using System.Collections.Generic;

namespace O10.Nomy.Rarible.Models
{
    public class ItemMetaDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ItemAttributeDto> Attributes { get; set; }

        public ImageDto Image { get; set; }
    }
}
