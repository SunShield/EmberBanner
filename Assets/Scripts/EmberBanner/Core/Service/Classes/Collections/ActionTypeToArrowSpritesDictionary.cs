﻿using System;
using EmberBanner.Core.Enums.Actions;
using EmberBanner.Unity.Battle.Systems.Visuals.Arrows;
using Serializables;

namespace EmberBanner.Core.Service.Classes.Collections
{
    [Serializable]
    public class ActionTypeToArrowSpritesDictionary : SerializableDictionary<ActionType, ActionArrowSprites>
    {
        
    }
}