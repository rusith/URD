using System;


namespace URD.BasicOperations
{
    public class UndoAbleAction:Change,IUndoAble
    {
        public Action Action { get; set; }

        public Action ReverceAction { get; set; }

        public void Redo()
        {
            Action.Invoke();
            URD.UndoStack.Push(this);
        }

        public void Undo()
        {
            ReverceAction.Invoke();
            URD.RedoStack.Push(this);
        }
    }
}
