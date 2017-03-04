using System;
using System.Collections.Generic;
using System.Linq;

namespace URD
{
    public class ChangeCollection : Change, IDisposable, IUndoAble
    {
        readonly Stack<Change> _changes;
        readonly string _description = "";
        private bool _dispose;
        private readonly bool _alreadyChangeCollecting;
        public ChangeCollection(string description, params object[] args)
        {
            _changes = new Stack<Change>();
            _description = string.Format(description, args);
            URD.ChangeResevered += URD_ChangeResevered;
            _alreadyChangeCollecting = URD.CollectChanges;
            URD.CollectChanges = true;
            _dispose = true;
        }

        private void URD_ChangeResevered(Change change)
        {
            _changes?.Push(change);
        }

        public void Dispose()
        {
            if (!_dispose) return;
            URD.ChangeResevered -= URD_ChangeResevered;
            URD.CollectChanges = _alreadyChangeCollecting;
            URD.AddChange(new ChangeCollection { Description = _description, Changes = _changes });
            _dispose = false;
        }

        public void Undo()
        {
            if (Changes.Count < 1) return;

            foreach (var undoAble in Changes.OfType<IUndoAble>())
            {
                undoAble.Undo();
            }
        }

        public void Redo()
        {
            if (Changes.Count < 1) return;

            foreach (var undoAble in Changes.OfType<IUndoAble>())
            {
                undoAble.Redo();
            }
        }

        public ChangeCollection() { }
        public Stack<Change> Changes { get; set; }

    }
}
