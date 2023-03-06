using EmberBanner.Core.Models.Cards;
using NFate.Editor.EditorElements;
using UnityEngine.UIElements;

namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements
{
    public class CardNavigatorElement : NavigatorElementWithUxml<CardModel>
    {
        protected override string UxmlKey { get; } = "CardNavigatorElement";

        protected Label NameLabel { get; private set; }
        protected VisualElement Sprite { get; private set; }
        
        public CardNavigatorElement() : base()
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