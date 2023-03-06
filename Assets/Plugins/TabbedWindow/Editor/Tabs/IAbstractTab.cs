using TabbedWindow.Windows;

namespace TabbedWindow.Tabs
{
    public interface IAbstractTab
    {
        string Name { get; }
        AbstractWindow Window { get; }
    }
}