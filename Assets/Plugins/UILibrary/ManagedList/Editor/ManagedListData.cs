using System;
using System.Collections.Generic;

namespace UILibrary.ManagedList.Editor
{
    public abstract class ManagedListData<TInspectedValue>
    {
        public Predicate<TInspectedValue> ElementInListPredicate;
        public Action<string> OnAddElementClickedCallback;
        public Action<string> OnRemoveElementClickedCallback;
        public Func<List<TInspectedValue>> ValuesPoolGetter;
        public Func<string, TInspectedValue> ElementByKeyGetter;
    }
}