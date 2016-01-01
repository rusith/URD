using GSSubtitle.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSSubtitle.Controllers.UnReDoPattern
{
    class ListChangeAddElementRange:ListChange,IDisposable
    {
        
        public ListChangeAddElementRange(object list,object addedElements, int startIndex, string description)
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

        public System.Collections.IList AddedElements { get; set; } 
        public int StartIndex { get; set; }
        private bool dispose = false;


    }
}
