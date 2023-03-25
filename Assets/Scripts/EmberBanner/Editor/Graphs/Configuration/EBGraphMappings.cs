using System;
using System.Collections.Generic;
using EmberBanner.Core.Graphs.Card;
using EmberBanner.Core.Graphs.Card.Action;
using OerGraph.Editor.Configuration.Mappings.Graphs;
using UnityEngine;

namespace EmberBanner.Editor.Graphs.Configuration
{
    [CreateAssetMenu(menuName = "EB/Graphs/Mappings/GraphMappings", fileName = "Graph Mappings")]
    public class EBGraphMappings : OerGraphMappings
    {
        public override Dictionary<string, Type> GetGraphTypes()
        {
            return new()
            {
                { "Card",   typeof(CardGraph) },
                { "Action", typeof(ActionGraph) }
            };
        }
    }
}