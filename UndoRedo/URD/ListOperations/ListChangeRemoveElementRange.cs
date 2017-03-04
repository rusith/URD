using System;

namespace URD.ListOperations
{
    public class ListChangeRemoveElementRange : ListChange, IDisposable, IUndoAble
    {
        private bool _dispose;
        public System.Collections.IList RemovedElements { get; set; }
        public int StartIndex { get; set; }
        public int Count { get; set; }
        public ListChangeRemoveElementRange(object list, System.Collections.IList removedelements, int startindex, int count, string description)
        {
            if (list == null || URD.NowChangingObject == list) return;
            Object = list;
            RemovedElements = removedelements;
            StartIndex = startindex;
            Count = count;
            _dispose = true;
            Description = description;

        }
        public void Dispose()
        {
            if (!_dispose) return;
            URD.AddChange(this);
            _dispose = false;
        }

        public void Undo()
        {
            using (new ObjectChanging(Object, "", true, false, this))
            {
                var i = StartIndex;
                foreach (var element in RemovedElements)
                    ((System.Collections.IList)Object).Insert(StartIndex, element);  
            }
        }

        public void Redo()
        {
            using (new ObjectChanging(Object, "", true, true, this))
            {
                var list = Object as System.Collections.IList;
                foreach (var item in RemovedElements)
                {
                    list?.Remove(item);
                }
            }
        }


    }
}
