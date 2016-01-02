using System;

namespace URD
{
    /// <summary>
    /// manage object changes . help to reduce code 
    /// (use using block )
    /// </summary>
    public class ObjectChanging: IDisposable
    {
        private bool _AutoAdd = false;
        private bool _UndoOrRedo = false;
        private Change _ChangeToAdd = null;
        
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="ChangingObject">changing object</param>
        /// <param name="ChangingPropertyName">changing property name pass "" if don't want </param>
        /// <param name="AutoAdd">pass true if you want to automatically push a change to undo or redo stack when desposing this object</param>
        /// <param name="UndoOrRedo">true = push change to undo stack when desposing  false = push change to redo stack when desposing</param>
        /// <param name="ChangeToAdd">change to auto push</param>
        public  ObjectChanging(object ChangingObject,string ChangingPropertyName,bool AutoAdd=false,bool UndoOrRedo=true,Change ChangeToAdd=null)
        {
            URD.NowChangingObject = ChangingObject;
            if (ChangingPropertyName != "" && ChangingPropertyName!=null)
                URD.NowChangingPropertyName = ChangingPropertyName;
            _AutoAdd = AutoAdd;
            _UndoOrRedo = UndoOrRedo;
            _ChangeToAdd = ChangeToAdd;
        }

        public void Dispose()
        {
            URD.NowChangingObject = null;
            URD.NowChangingPropertyName = "";
            if (_AutoAdd)
            {
                if (_ChangeToAdd!=null)
                {
                    if (_UndoOrRedo)  URD.UndoStack.Push(_ChangeToAdd);
                    else URD.RedoStack.Push(_ChangeToAdd);
                }
            }
        }
    }
}
