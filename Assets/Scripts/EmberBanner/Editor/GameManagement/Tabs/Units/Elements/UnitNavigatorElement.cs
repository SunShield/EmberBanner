using EmberBanner.Core.Models.Units;
using NFate.Editor.EditorElements;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Units.Elements
{
    public class UnitNavigatorElement : NavigatorElementWithUxml<UnitModel>
    {
        protected override string UxmlKey { get; } = "UnitNavigatorElement";
        
        protected Label NameLabel { get; private set; }
        protected VisualElement Sprite { get; private set; }
        
        public UnitNavigatorElement() : base()
        {
            NameLabel = Root.Q<Label>("NameLabel");
            Sprite = Root.Q<VisualElement>("Sprite");
        }
        
        protected override void DoBindElement()
        {
            NameLabel.text = WrappedElement.Name;
            Sprite.style.backgroundImage = new StyleBackground(WrappedElement.Sprite);
        }

        public override void DoUpdateElement()
        {
            Sprite.style.backgroundImage = new StyleBackground(WrappedElement.Sprite);
        }
    }
}