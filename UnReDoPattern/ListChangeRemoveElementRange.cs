using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSSubtitle.Controllers.UnReDoPattern
{
    class ListChangeRemoveElementRange : ListChange, IDisposable
    {
        public ListChangeRemoveElementRange(object list,System.Collections.IList removedelements,int startindex,int count,string description)
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


        public System.Collections.IList RemovedElements { get; set; }
        public int StartIndex { get; set; }
        public int Count { get; set; }

        private bool dispose;
    }
}
