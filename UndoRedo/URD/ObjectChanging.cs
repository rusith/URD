using System;

namespace URD
{
    /// <summary>
    /// manage object changes . help to reduce code 
    /// (use using block )
    /// </summary>
    public class ObjectChanging: IDisposable
    {
        private readonly bool _autoAdd;
        private readonly bool _undoOrRedo;
        private readonly Change _changeToAdd;
        
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="changingObject">changing object</param>
        /// <param name="changingPropertyName">changing property name pass "" if don't want </param>
        /// <param name="autoAdd">pass true if you want to automatically push a change to undo or redo stack when desposing this object</param>
        /// <param name="undoOrRedo">true = push change to undo stack when desposing  false = push change to redo stack when desposing</param>
        /// <param name="changeToAdd">change to auto push</param>
        public  ObjectChanging(object changingObject,string changingPropertyName,bool autoAdd=false,bool undoOrRedo=true,Change changeToAdd=null)
        {
            URD.NowChangingObject = changingObject;
            if (!string.IsNullOrEmpty(changingPropertyName))
                URD.NowChangingPropertyName = changingPropertyName;
            _autoAdd = autoAdd;
            _undoOrRedo = undoOrRedo;
            _changeToAdd = changeToAdd;
        }

        public void Dispose()
        {
            URD.NowChangingObject = null;
            URD.NowChangingPropertyName = "";
            if (!_autoAdd) return;
            if (_changeToAdd == null) return;
            if (_undoOrRedo)  URD.UndoStack.Push(_changeToAdd);
            else URD.RedoStack.Push(_changeToAdd);
        }
    }
}
