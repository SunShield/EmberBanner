using UnityEngine.UIElements;

namespace UILibrary.UxmlElement.Editor
{
    public abstract class VisualElementWithUxml : VisualElement
    {
        protected abstract string UxmlKey { get; }
        
        protected VisualElement Root { get; private set; }

        protected VisualElementWithUxml()
        {
            BuildGeometry();
        }
        
        private void BuildGeometry()
        {
            var visualTree = UxmlDatabase.Instance.Uxmls[UxmlKey];
            Root = UQueryExtensions.Q<VisualElement>(visualTree.CloneTree(), "Root");
            Add(Root);
        }
    }
}