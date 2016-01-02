using System;

namespace URD.ListOperations
{
    public class ListChangeAddElement : ListChange, IDisposable, IUndoAble
    {

        private bool dispose = false;
        public object AddedElement { get; set; }
        public int Index { get; set; }


        public ListChangeAddElement(object list, object addedelement, string description, int Index = -25)
        {
            if (list == null || URD.NowChangingObject == list) return;
            dispose = true;
            Object = list;
            Description = description;
            AddedElement = addedelement;

        }

        public void Dispose()
        {
            if (dispose)
            {
                if (((System.Collections.IList)Object).Contains(AddedElement))
                {
                    Index = ((System.Collections.IList)Object).IndexOf(AddedElement);
                    URD.AddChange(this);
                }

                dispose = false;
            }
        }

        public void Undo()
        {

            using (new ObjectChanging(Object, "", true, false, this))
            {
                ((System.Collections.IList)Object).Remove(AddedElement);
            }
        }

        public void Redo()
        {
            using (new ObjectChanging(Object, "", true, true, this))
            {
                ((System.Collections.IList)Object).Insert(Index, AddedElement);
            }
        }



       
    }
}
