using System;

namespace URD.ListOperations
{
    public class ListChangeAddElementRange : ListChange, IDisposable, IUndoAble
    {
        private bool _dispose;
        public System.Collections.IList AddedElements { get; set; }
        public int StartIndex { get; set; }
        public ListChangeAddElementRange(object list, object addedElements, int startIndex, string description)
        {
            if (URD.NowChangingObject == list) return;
            _dispose = true;
            Description = string.Copy(description);
            Object = list;
            AddedElements = addedElements as System.Collections.IList;
            StartIndex = startIndex;
        }

        public void Dispose()
        {
            if (_dispose && ((System.Collections.IList)Object).Contains(AddedElements[0]) && ((System.Collections.IList)Object).Contains(AddedElements[AddedElements.Count - 1]))
                URD.AddChange(this);
            _dispose = false;
        }

        public void Undo()
        {
            using (new ObjectChanging(Object, "", true, false, this))
            {
                foreach (var element in AddedElements)
                    ((System.Collections.IList)Object).Remove(element);
            }
        }

        public void Redo()
        {
            using (new ObjectChanging(Object, "", true, true, this))
            {
                var i = StartIndex;
                var list = Object as System.Collections.IList;
                foreach (var element in AddedElements)
                {
                    list?.Insert(i, element);
                    i++;
                }
            }
        }
    }
}
