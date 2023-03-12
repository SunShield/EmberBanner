using EmberBanner.Core.Service.Classes.Collections;
using UnityEditor;

namespace EmberBanner.Editor.Service.Drawers
{
    [CustomPropertyDrawer(typeof(StringToCardModelDictionary))]
    [CustomPropertyDrawer(typeof(StringToActionParamModelDictionary))]
    [CustomPropertyDrawer(typeof(StringToUnitModelDictionary))]
    [CustomPropertyDrawer(typeof(StringToCrystalModelDictionary))]
    [CustomPropertyDrawer(typeof(StringToBattleModelDictionary))]
    [CustomPropertyDrawer(typeof(ActionTypeToArrowSpritesDictionary))]
    [CustomPropertyDrawer(typeof(StringToSpriteDictionary))]
    public class AddSerializableDictionariesDrawer : SerializableDictionaryPropertyDrawer
    {
        
    }
}