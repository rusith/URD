using System;
using System.Collections.Generic;

namespace URD
{
    public class ChangeCollection : Change, IDisposable, IUndoAble
    {
        Stack<Change> _Changes;
        string _Description = "";
        private bool _dispose = false;
        private bool AlreadyChangeCollecting = false;
        public ChangeCollection(string Description, params string[] args)
        {
            _Changes = new Stack<Change>();
            _Description = string.Format(Description, args);
            URD.ChangeResevered += URD_ChangeResevered;
            AlreadyChangeCollecting = URD.CollectChanges;
            URD.CollectChanges = true;
            _dispose = true;
        }

        private void URD_ChangeResevered(Change change)
        {
            if (_Changes != null)
                _Changes.Push(change);
        }

        public void Dispose()
        {
            if (_dispose)
            {
                URD.ChangeResevered -= URD_ChangeResevered;
                URD.CollectChanges = AlreadyChangeCollecting;
                URD.AddChange(new ChangeCollection { Description = _Description, Changes = _Changes });
                _dispose = false;
            }
        }

        public void Undo()
        {
            if (Changes.Count < 1) return;

            foreach (Change change in Changes)
                (change as IUndoAble).Undo();

        }

        public void Redo()
        {
            if (Changes.Count < 1) return;

            foreach (Change change in Changes)
                (change as IUndoAble).Redo();

        }

        public ChangeCollection() { }
        public Stack<Change> Changes { get; set; }

    }
}
