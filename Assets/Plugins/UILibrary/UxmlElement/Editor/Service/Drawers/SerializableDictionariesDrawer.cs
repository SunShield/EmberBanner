using Serializables.Editor;
using UILibrary.UxmlElement.Unity.Service;
using UnityEditor;

namespace UILibrary.UxmlElement.Editor.Service.Drawers
{
    [CustomPropertyDrawer(typeof(StringToVisualTreeAssetDictionary))]
    public class SerializableDictionariesDrawer : SerializableDictionaryPropertyDrawer
    {
    }
}