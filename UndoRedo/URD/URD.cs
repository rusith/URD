using System;
using System.ComponentModel;


namespace URD
{
    /// <summary>
    /// main class of the undo redo pattern
    /// </summary>
    public static class URD
    {
        #region Fields

        private static ChangeCollection _TempChangeCollection = null;
        private static DOStack<Change> _UndoStack = new DOStack<Change>(128);
        private static DOStack<Change> _RedoStack = new DOStack<Change>(128);
        private static string _NextUndoDescription = "";
        private static string _NextRedoDescription = "";
        private static bool ARCING = false;
        private static event NewChangeRecived _ChangeResevered;

        #endregion

        #region Properties

        public static Change NextUndo { get { return UndoStack.Peek(); } }
        public static Change NextRedo { get { return RedoStack.Peek(); } }
        public static bool CollectChanges { get; set; }
        public static ChangeCollection TempChangeCollection{get{return _TempChangeCollection;} set{_TempChangeCollection=value}}
        public static object NowChangingObject { get; set; }
        public static string NowChangingPropertyName { get; set; }
        public static event NewChangeRecived ChangeResevered{get{return _ChangeResevered; }}
        public static string NextUndoDescription
        {
            get { return "Undo " + _NextUndoDescription; }
            private set
            {
                _NextUndoDescription = value;
                NotifyStaticPropertyChange("NextUndoDescription");
            }
        }
        public static string NextRedoDescription
        {
            get
            {
                return "Redo " + _NextRedoDescription;
            }
            private set
            {
                _NextRedoDescription = value;
                NotifyStaticPropertyChange("NextRedoDescription");
            }
        }
        public static bool CanUndo
        {
            get { return UndoStack.Count > 0 ? true : false; }
        }
        public static bool CanRedo
        {
            get { return RedoStack.Count > 0 ? true : false; }
        }

        public static DOStack<Change> RedoStack { get { return _RedoStack; } }

        public static DOStack<Change> UndoStack { get { return _UndoStack; } }


        #endregion

        #region Methods
        public static void AddChange(Change change)
        {
            if (change == null || (change is IUndoAble) == false) return;
            if (CollectChanges)
            {
                ChangeResevered(change);
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
            if (change is Change == false && change is IUndoAble == false) return;
            NextUndoDescription = CanUndo ? UndoStack.Peek().Description : "";
            (change as IUndoAble).Undo();



        }

        public static void RedoLastUndo()
        {
            if (!CanRedo) return;
            var change = RedoStack.Pop();
            if (change is Change == false && change is IUndoAble == false) return;
            _NextRedoDescription = CanRedo ? RedoStack.Peek().Description : "";
            (change as IUndoAble).Redo();
        }

        public static void StartChangeCollecting(string Description)
        {
            if (CollectChanges)
            {
                ARCING = true;
                return;
            }
            CollectChanges = true;
            TempChangeCollection = new ChangeCollection();
            TempChangeCollection.Description = Description;

        }
        public static void EndChangeCollecting()
        {
            if (ARCING)
            {
                ARCING = false;
                return;
            }

            CollectChanges = false;

            if (TempChangeCollection.Changes.Count > 0)
                AddChange(TempChangeCollection);
            
            TempChangeCollection = null;
        }

        public static bool C(object Obj, string PropertyName = null)
        {

            return Obj == NowChangingObject ? PropertyName != null ?
                    PropertyName == NowChangingPropertyName ? false : true : false : true;
        }

        #endregion

        #region PropertyChangeNotification
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        public static void NotifyStaticPropertyChange(string Propertyname_)
        {
            if (StaticPropertyChanged != null)
                StaticPropertyChanged(null, new PropertyChangedEventArgs(Propertyname_));

        }
        #endregion
    }

    
    public delegate void NewChangeRecived(Change change);
}
