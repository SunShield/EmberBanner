namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.TypedElements
{
    public class SupportActionListElement : ActionListElement
    {
        protected override void PostInitialize()
        {
            base.PostInitialize();
            MagnitudeTypeDropdown.parent.Remove(MagnitudeTypeDropdown);
        }
    }
}