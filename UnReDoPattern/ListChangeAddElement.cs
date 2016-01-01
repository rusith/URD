using GSSubtitle.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSSubtitle.Controllers.UnReDoPattern
{
    class ListChangeAddElement:ListChange,IDisposable
    {
        
        /// <summary>
        /// define in s using block to add change to URD
        /// </summary>
        /// <param name="list">Containing List</param>
        /// <param name="addedelement">added element</param>
        /// <param name="index">added item index (pass -50 to auto define)</param>
        /// <param name="description">Description of the change</param>
        public ListChangeAddElement(object list, object addedelement,  string description,int Index=-25)
        {
            if (list==null || URD.NowChangingObject == list) return;
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


        public object AddedElement { get; set; }
        public int Index { get; set; }

        private bool dispose = false;
    }
}
