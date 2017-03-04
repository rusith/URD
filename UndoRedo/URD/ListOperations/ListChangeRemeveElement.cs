using System;

namespace URD.ListOperations
{
    public class ListChangeRemoveElement : ListChange, IDisposable, IUndoAble
    {


        private bool _dispose;
        public object RemovedElement { get; set; }
        public int RemovedAt { get; set; }

        public ListChangeRemoveElement(object list, object removedelement, int removedat, string description)
        {
            if (list == null || removedelement == null || list == URD.NowChangingObject) return;
            Object = list;
            RemovedElement = removedelement;
            RemovedAt = removedat;
            Description = description;
            _dispose = true;
        }
        public void Dispose()
        {
            if (!_dispose) return;
            if (((System.Collections.IList)Object).Contains(RemovedElement) == false)
            {
                URD.AddChange(this);
            }
            _dispose = false;
        }

        public void Undo()
        {
            using (new ObjectChanging(Object, "", true, false, this))
            {
                if (((System.Collections.IList)Object).Count - 1 >= RemovedAt)
                    ((System.Collections.IList)Object).Add(RemovedElement);
                else
                    ((System.Collections.IList)Object).Insert(RemovedAt, RemovedElement);
            }
        }

        public void Redo()
        {
            using (new ObjectChanging(Object, "", true, true, this))
            {
                ((System.Collections.IList)Object).Remove(RemovedElement);
            }
        }


    }
}
