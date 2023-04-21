namespace EmberBanner.Editor.GameManagement.Tabs.Cards.Elements.Actions.Events
{
    public class ActionModifyingEventInspector : ActionEventInspector
    {
        protected override string UxmlKey { get; } = "ActionModifyingEventInspector";
        
        public ActionModifyingEventInspector(string eventName, int eventIndex, bool active) : base(eventName, eventIndex, active)
        {
        }
    }
}