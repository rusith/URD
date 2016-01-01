using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URD.ListOperations
{
    public class ListChangeRemoveElementRange : ListChange, IDisposable, IUndoAble
    {
        public ListChangeRemoveElementRange(object list, System.Collections.IList removedelements, int startindex, int count, string description)
        {
            if (list == null || URD.NowChangingObject == list) return;
            Object = list;
            RemovedElements = removedelements;
            StartIndex = startindex;
            Count = count;
            dispose = true;
            Description = description;

        }
        public void Dispose()
        {
            if (dispose)
            {
                URD.AddChange(this);
                dispose = false;
            }
        }

        public void Undo()
        {
            URD.NowChangingObject = Object;
            int i = StartIndex;
            foreach (var element in RemovedElements)
            {
                ((System.Collections.IList)Object).Insert(StartIndex, element);
            }
            URD.NowChangingObject = null;
            URD.RedoStack.Push(this);
            return;
        }

        public void Redo()
        {

            URD.NowChangingObject = Object;
            var list = Object as System.Collections.IList;
            foreach (var item in RemovedElements) list.Remove(item);
            URD.NowChangingObject = null;
            URD.UndoStack.Push(this);
            return;
        }

        public System.Collections.IList RemovedElements { get; set; }
        public int StartIndex { get; set; }
        public int Count { get; set; }

        private bool dispose;
    }
}
