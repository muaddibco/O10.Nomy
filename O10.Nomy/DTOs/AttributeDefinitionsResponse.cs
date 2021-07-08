﻿using O10.Client.Common.Entities;
using O10.Client.Common.Integration;

namespace O10.Nomy.DTOs
{
    public class AttributeDefinitionsResponse
    {
        public AttributeDefinition[] AttributeDefinitions { get; set; }
        public ActionStatus IntegrationActionStatus { get; set; }
    }
}
