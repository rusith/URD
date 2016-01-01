using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSSubtitle.Controllers.UnReDoPattern
{
    class ListChangeRemoveElement : ListChange, IDisposable
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

        public object RemovedElement { get; set; }
        public int RemovedAt { get; set; }

        private bool dispose = false;
    }
}