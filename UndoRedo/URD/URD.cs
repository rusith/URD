using System;
using System.ComponentModel;
using URD.BasicOperations;

namespace URD
{
    /// <summary>
    /// main class of the undo redo pattern
    /// </summary>
    public static class URD
    {
        #region Fields

        private static string _nextUndoDescription = "";
        private static string _nextRedoDescription = "";
        private static bool _arcing;

        static URD()
        {
            TempChangeCollection = null;
        }

        public static event NewChangeRecived ChangeResevered;

        #endregion

        #region Properties

        public static Change NextUndo { get { return UndoStack.Peek(); } }
        public static Change NextRedo { get { return RedoStack.Peek(); } }
        public static bool CollectChanges { get; set; }
        public static ChangeCollection TempChangeCollection { get; set; }

        public static object NowChangingObject { get; set; }
        public static string NowChangingPropertyName { get; set; }
        public static string NextUndoDescription
        {
            get { return "Undo " + _nextUndoDescription; }
            private set
            {
                _nextUndoDescription = value;
                NotifyStaticPropertyChange("NextUndoDescription");
            }
        }
        public static string NextRedoDescription
        {
            get
            {
                return "Redo " + _nextRedoDescription;
            }
            //private set
            //{
            //    _nextRedoDescription = value;
            //    NotifyStaticPropertyChange("NextRedoDescription");
            //}
        }
        public static bool CanUndo
        {
            get { return UndoStack.Count > 0; }
        }
        public static bool CanRedo
        {
            get { return RedoStack.Count > 0; }
        }

        public static DOStack<Change> RedoStack { get; } = new DOStack<Change>(128);

        public static DOStack<Change> UndoStack { get; } = new DOStack<Change>(128);

        #endregion

        #region Methods
        public static void AddChange(Change change)
        {
            if (change == null || (change is IUndoAble) == false) return;
            if (CollectChanges)
            {
                ChangeResevered?.Invoke(change);
                return;
            }
            UndoStack.Push(change);
            NextUndoDescription = change.Description;
        }


        public static void UndoLastChnage()
        {

            //checking can undo
            if (!CanUndo) return;

            var change = UndoStack.Pop();
            if (change != null == false) return;
            NextUndoDescription = CanUndo ? UndoStack.Peek().Description : "";
            var undoAble = change as IUndoAble;
            undoAble?.Undo();
        }

        public static void RedoLastUndo()
        {
            if (!CanRedo) return;
            var change = RedoStack.Pop();
            if (change != null == false || change is IUndoAble == false) return;
            _nextRedoDescription = CanRedo ? RedoStack.Peek().Description : "";
            (change as IUndoAble).Redo();
        }

        public static void StartChangeCollecting(string description)
        {
            if (CollectChanges)
            {
                _arcing = true;
                return;
            }
            CollectChanges = true;
            TempChangeCollection = new ChangeCollection {Description = description};

        }
        public static void EndChangeCollecting()
        {
            if (_arcing)
            {
                _arcing = false;
                return;
            }

            CollectChanges = false;

            if (TempChangeCollection.Changes.Count > 0)
                AddChange(TempChangeCollection);
            
            TempChangeCollection = null;
        }

        public static void NewUndoAbleAction(Action actiOn, Action reverseAction, string description)
        {
            if (actiOn == null || reverseAction == null) return;
            AddChange(new UndoAbleAction { action = actiOn, reverceAction = reverseAction, Description = description });
        }

        public static bool C(object obj, string propertyName = null)
        {

            return obj != NowChangingObject || propertyName != null && propertyName != NowChangingPropertyName;
        }

        #endregion

        #region PropertyChangeNotification
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        public static void NotifyStaticPropertyChange(string propertyname)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyname));

        }
        #endregion
    }

    
    public delegate void NewChangeRecived(Change change);
}
