namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.Events
{
    public class ActionActiveEventInspector : ActionEventInspector
    {
        protected override string UxmlKey { get; } = "ActionActiveEventInspector";
        
        public ActionActiveEventInspector(string eventName, int eventIndex, bool active) : base(eventName, eventIndex, active)
        {
        }
    }
}