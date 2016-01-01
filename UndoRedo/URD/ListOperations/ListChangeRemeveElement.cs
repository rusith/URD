using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URD.ListOperations
{
    public class ListChangeRemoveElement : ListChange, IDisposable, IUndoAble
    {
        public ListChangeRemoveElement(object list, object removedelement, int removedat, string description)
        {
            if (list == null || removedelement == null || list == URD.NowChangingObject) return;
            Object = list;
            RemovedElement = removedelement;
            RemovedAt = removedat;
            Description = description;
            dispose = true;
        }
        public void Dispose()
        {
            if (dispose)
            {
                if (((System.Collections.IList)Object).Contains(RemovedElement) == false)
                {
                    URD.AddChange(this);
                }
                dispose = false;
            }
        }

        public void Undo()
        {

            URD.NowChangingObject = Object;
            if (((System.Collections.IList)Object).Count - 1 >= RemovedAt)
                ((System.Collections.IList)Object).Add(RemovedElement);
            else
                ((System.Collections.IList)Object).Insert(RemovedAt, RemovedElement);

            URD.NowChangingObject = null;
            URD.RedoStack.Push(this);
            return;
        }

        public void Redo()
        {

            URD.NowChangingObject = Object;
            ((System.Collections.IList)Object).Remove(RemovedElement);
            URD.NowChangingObject = null;
            URD.UndoStack.Push(this);
            return;
        }

        public object RemovedElement { get; set; }
        public int RemovedAt { get; set; }

        private bool dispose = false;
    }
}
