using System;
using EmberBanner.Core.Models.Actions.Params;
using Serializables;

namespace EmberBanner.Core.Service.Classes.Collections
{
    [Serializable]
    public class StringToActionParamModelDictionary : SerializableDictionary<string, ActionParamModel>
    {
    }
}