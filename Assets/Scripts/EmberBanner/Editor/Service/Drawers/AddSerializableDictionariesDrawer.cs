using EmberBanner.Core.Service.Classes.Collections;
using Serializables.Editor;
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
    [CustomPropertyDrawer(typeof(ActionTypeToColorDictionary))]
    [CustomPropertyDrawer(typeof(IntHashset))]
    public class AddSerializableDictionariesDrawer : SerializableDictionaryPropertyDrawer
    {
        
    }
}