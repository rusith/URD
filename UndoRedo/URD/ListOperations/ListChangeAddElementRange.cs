using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URD.ListOperations
{
    public class ListChangeAddElementRange : ListChange, IDisposable, IUndoAble
    {

        public ListChangeAddElementRange(object list, object addedElements, int startIndex, string description)
        {
            if (URD.NowChangingObject == list) return;
            dispose = true;
            Description = string.Copy(description);
            Object = list;
            AddedElements = addedElements as System.Collections.IList;
            StartIndex = startIndex;

        }

        public void Dispose()
        {
            if (dispose && ((System.Collections.IList)Object).Contains(AddedElements[0]) && ((System.Collections.IList)Object).Contains(AddedElements[AddedElements.Count - 1]))
            {
                URD.AddChange(this);

            }

            dispose = false;
        }

        public void Undo()
        {
            URD.NowChangingObject = Object;
            foreach (object element in AddedElements)
                ((System.Collections.IList)Object).Remove(element);

            URD.RedoStack.Push(this);
            URD.NowChangingObject = null;

            return;
        }

        public void Redo()
        {
            URD.NowChangingObject = Object;
            int i = StartIndex;

            var list = Object as System.Collections.IList;
            foreach (object element in AddedElements)
            {
                list.Insert(i, element);
                i++;
            }

            URD.NowChangingObject = null;
            URD.UndoStack.Push(this);
        }

        public System.Collections.IList AddedElements { get; set; }
        public int StartIndex { get; set; }
        private bool dispose = false;


    }
}
