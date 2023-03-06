using EmberBanner.Core.Service.Classes.Collections;
using UnityEditor;

namespace EmberBanner.Editor.Service.Drawers
{
    [CustomPropertyDrawer(typeof(StringToCardModelDictionary))]
    [CustomPropertyDrawer(typeof(StringToActionParamModelDictionary))]
    public class AddSerializableDictionariesDrawer : SerializableDictionaryPropertyDrawer
    {
        
    }
}