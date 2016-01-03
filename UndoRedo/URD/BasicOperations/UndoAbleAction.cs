using System;


namespace URD.BasicOperations
{
    public class UndoAbleAction:Change,IUndoAble
    {
        private Action _action;
        private Action _reverceAction;
        public Action action { get { return _action; } set { _action = value; } }
        public Action reverceAction { get { return _reverceAction; } set { _reverceAction = value; } }

        public void Redo()
        {
            _action.Invoke();
            URD.UndoStack.Push(this);
        }

        public void Undo()
        {
            _reverceAction.Invoke();
            URD.RedoStack.Push(this);
        }
    }
}
