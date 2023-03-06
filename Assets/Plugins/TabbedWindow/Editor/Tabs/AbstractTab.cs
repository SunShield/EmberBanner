using TabbedWindow.Windows;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TabbedWindow.Tabs
{
    public abstract class AbstractTab<TTargetObject> : VisualElement, IAbstractTab
        where TTargetObject : ScriptableObject
    {
        protected string AssetLocation { get; private set; }
        
        public AbstractWindow Window { get; private set; }
        public VisualElement ContentContainer { get; private set; }
        
        public string Name { get; private set; }
        public TTargetObject TargetObject { get; private set; }

        protected AbstractTab(AbstractWindow window, string name, string assetLocation)
        {
            Window = window;
            Name = name;
            AssetLocation = assetLocation;
            TargetObject = GetTargetObject();
            SetBaseVisualConfiguration();
            BuildGeometry();
        }

        private TTargetObject GetTargetObject()
        {
            // TODO: handle situation where no folder hierarchy exists
            
            var asset = AssetDatabase.LoadAssetAtPath<TTargetObject>(AssetLocation);
            if (asset != null) return asset;

            var so = ScriptableObject.CreateInstance<TTargetObject>();
            AssetDatabase.CreateAsset(so, AssetLocation);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return so;
        }

        private void SetBaseVisualConfiguration()
        {
            style.flexGrow = 1;
            style.flexDirection = FlexDirection.Row;
        }

        private void BuildGeometry()
        {
            BuildContentContainer();
        }
        
        private void BuildContentContainer()
        {
            ContentContainer = new VisualElement();
            ContentContainer.style.flexGrow = 1;
            BuildContent();
            Add(ContentContainer);
        }
        
        protected virtual void BuildContent() { }
    }
}