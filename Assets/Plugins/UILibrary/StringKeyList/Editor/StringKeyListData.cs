using System;
using UILibrary.ManagedList.Editor;

namespace UILibrary.StringKeyList.Editor
{
    public abstract class StringKeyListData<TInspectedValue> : ManagedListData<TInspectedValue>
    {
        public Predicate<TInspectedValue> PossibleElementCheckPredicate;
    }
}