using EmberBanner.Core.Models.Battles;
using NFate.Editor.EditorElements;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Battles.Elements
{
    public class BattleNavigatorElement : NavigatorElementWithUxml<BattleModel>
    {
        protected override string UxmlKey { get; } = "BattleNavigatorElement";
        
        protected Label NameLabel { get; private set; }
        protected VisualElement Sprite { get; private set; }
        
        public BattleNavigatorElement() : base()
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