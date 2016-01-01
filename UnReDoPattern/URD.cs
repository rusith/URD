using GSSubtitle.Windows.Editor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace GSSubtitle.Controllers.UnReDoPattern
{
    /// <summary>
    /// main class of the undo redo pattern
    /// </summary>
    public static class URD
    {
        #region Fields

        public static ChangeCollection TempChangeCollection = null;
        private static DOStack<Change> UndoStack = new DOStack<Change>(Settings.MaxUndoAndRedos);
        private static DOStack<Change> RedoStack = new DOStack<Change>(Settings.MaxUndoAndRedos);
        private static string _NextUndoDescription = "";
        private static string _NextRedoDescription = "";
        private static bool ARCING = false;

        public static event NewChangeRecived ChangeResevered;

        #endregion

        #region Properties

        public static Change NextUndo { get { return UndoStack.Peek(); } }
        public static Change NextRedo { get { return RedoStack.Peek(); } }
        public static bool CollectChanges { get; set; }
        public static object NowChangingObject { get; set; }
        public static string NowChangingPropertyName { get; set; }
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

        #endregion

        #region Methods
        public static void AddChange(Change change)
        {
            if (change == null) return;
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
            NextUndoDescription = CanUndo ? UndoStack.Peek().Description : "";
            if (change is ChangeCollection)
            {
                var changecollection = change as ChangeCollection;
                UndoChangeCollection(ref changecollection);
                RedoStack.Push(changecollection);
            }
            else if (change is PropertyChange)
            {
                var propertychange = change as PropertyChange;
                NowChangingObject = propertychange.Object;
                NowChangingPropertyName = propertychange.PropertyName;
                propertychange.Object.GetType().GetProperty(propertychange.PropertyName).SetValue(propertychange.Object, propertychange.OldValue, null);
                NowChangingObject = null;
                NowChangingPropertyName = null;
                RedoStack.Push(change);
                return;

            }
            else if (change is ListChange)
            {
                var listchange = change as ListChange;

                if (listchange is ListChangeAddElement)
                {
                    var listchangeaddelement = listchange as ListChangeAddElement;
                    NowChangingObject = listchangeaddelement.Object;
                    ((System.Collections.IList)listchangeaddelement.Object).Remove(listchangeaddelement.AddedElement);

                    NowChangingObject = null;
                    RedoStack.Push(change);

                    return;

                }
                else if (listchange is ListChangeAddElementRange)
                {
                    var listchangeaddelementrange = listchange as ListChangeAddElementRange;
                    NowChangingObject = listchangeaddelementrange.Object;
                    foreach (object element in listchangeaddelementrange.AddedElements)
                    {
                        ((System.Collections.IList)listchangeaddelementrange.Object).Remove(element);
                    }
                    RedoStack.Push(change);
                    NowChangingObject = null;

                    return;
                }

                else if (listchange is ListChangeRemoveElement)
                {
                    var lcre = listchange as ListChangeRemoveElement;
                    NowChangingObject = lcre.Object;
                    if (((System.Collections.IList)lcre.Object).Count - 1 >= lcre.RemovedAt)
                    {
                        ((System.Collections.IList)lcre.Object).Add(lcre.RemovedElement);
                    }
                    else
                    {
                        ((System.Collections.IList)lcre.Object).Insert(lcre.RemovedAt, lcre.RemovedElement);
                    }
                    NowChangingObject = null;
                    RedoStack.Push(change);
                    return;
                }
                else if (listchange is ListChangeRemoveElementRange)
                {
                    var lcrer = listchange as ListChangeRemoveElementRange;
                    NowChangingObject = lcrer.Object;
                    int i = lcrer.StartIndex;
                    foreach (var element in lcrer.RemovedElements)
                    {
                        ((System.Collections.IList)lcrer).Insert(lcrer.StartIndex, element);
                    }
                    NowChangingObject = null;
                    RedoStack.Push(change);
                    return;
                }
            }

        }
        private static void UndoChangeCollection(ref ChangeCollection collection)
        {
            if (collection == null || collection.Changes.Count < 1) return;

            foreach (Change change in collection.Changes)
            {
                if (change is ChangeCollection)
                {
                    var changecollection = change as ChangeCollection;
                    UndoChangeCollection(ref changecollection);
                }
                if (change is PropertyChange)
                {
                    var propertychange = change as PropertyChange;
                    NowChangingObject = propertychange.Object;
                    NowChangingPropertyName = propertychange.PropertyName;
                    propertychange.Object.GetType().GetProperty(propertychange.PropertyName).SetValue(propertychange.Object, propertychange.OldValue, null);
                    NowChangingObject = null;
                    NowChangingPropertyName = null;

                }
                else if (change is ListChange)
                {
                    var listchange = change as ListChange;

                    if (listchange is ListChangeAddElement)
                    {
                        var listchangeaddelement = listchange as ListChangeAddElement;
                        NowChangingObject = listchangeaddelement.Object;
                        ((System.Collections.IList)listchangeaddelement.Object).Remove(listchangeaddelement.AddedElement);
                        NowChangingObject = null;
                    }
                    else if (listchange is ListChangeAddElementRange)
                    {
                        var listchangeaddelementrange = listchange as ListChangeAddElementRange;
                        NowChangingObject = listchangeaddelementrange.Object;
                        foreach (object element in listchangeaddelementrange.AddedElements)
                        {
                            ((System.Collections.IList)listchangeaddelementrange.Object).Remove(element);
                        }
                    }

                    else if (listchange is ListChangeRemoveElement)
                    {
                        var lcre = listchange as ListChangeRemoveElement;
                        NowChangingObject = lcre.Object;
                        if (((System.Collections.IList)lcre.Object).Count - 1 >= lcre.RemovedAt)
                        {
                            ((System.Collections.IList)lcre.Object).Insert(lcre.RemovedAt, lcre.RemovedElement);
                        }
                        else
                        {
                            ((System.Collections.IList)lcre.Object).Insert(lcre.RemovedAt, lcre.RemovedElement);
                        }
                        NowChangingObject = null;
                    }
                    else if (listchange is ListChangeRemoveElementRange)
                    {
                        var lcrer = listchange as ListChangeRemoveElementRange;
                        NowChangingObject = lcrer.Object;
                        int i = lcrer.StartIndex;
                        foreach (var element in lcrer.RemovedElements)
                        {
                            ((System.Collections.IList)lcrer).Insert(lcrer.StartIndex, element);
                        }
                        NowChangingObject = null;
                    }
                }
            }
        }


        public static void RedoLastUndo()
        {
            if (!CanRedo) return;
            var change = RedoStack.Pop();
            _NextRedoDescription = CanRedo ? RedoStack.Peek().Description : "";

            if (change is ChangeCollection)
            {
                var changecollection = change as ChangeCollection;
                RedoChangeCollection(ref changecollection);
                UndoStack.Push(changecollection);
            }
            else if (change is PropertyChange)
            {
                var pc = change as PropertyChange;

                NowChangingObject = pc.Object;
                NowChangingPropertyName = pc.PropertyName;
                pc.Object.GetType().GetProperty(pc.PropertyName).SetValue(pc.Object, pc.OldValue, null);
                NowChangingPropertyName = "";
                NowChangingObject = null;
            }
            else if (change is ListChange)
            {
                var lc = change as ListChange;

                if (lc is ListChangeAddElement)
                {
                    var lcae = lc as ListChangeAddElement;

                    NowChangingObject = lcae.Object;
                    ((System.Collections.IList)lcae).Insert(lcae.Index, lcae.AddedElement);
                    NowChangingObject = null;
                    UndoStack.Push(change);
                    return;
                }
                else if (lc is ListChangeAddElementRange)
                {
                    var lcaer = lc as ListChangeAddElementRange;
                    NowChangingObject = lcaer.Object;
                    int i = lcaer.StartIndex;

                    var list = lcaer.Object as System.Collections.IList;
                    foreach (object element in lcaer.AddedElements)
                    {
                        list.Insert(i, element);
                        i++;
                    }

                    NowChangingObject = null;
                    UndoStack.Push(change);
                    return;

                }
                else if (lc is ListChangeRemoveElement)
                {
                    var lcre = lc as ListChangeRemoveElement;

                    NowChangingObject = lcre.Object;
                    ((System.Collections.IList)lcre.Object).Remove(lcre.RemovedElement);
                    NowChangingObject = null;
                    UndoStack.Push(change);
                    return;
                }
                else if (lc is ListChangeRemoveElementRange)
                {
                    var lcrer = lc as ListChangeRemoveElementRange;

                    NowChangingObject = lcrer.Object;
                    var list = lcrer.Object as System.Collections.IList;
                    foreach (var item in lcrer.RemovedElements) list.Remove(item);
                    NowChangingObject = null;
                    UndoStack.Push(change);
                    return;
                }
            }

        }

        private static void RedoChangeCollection(ref ChangeCollection collection)
        {
            if (collection == null || collection.Changes.Count < 1) return;

            foreach (Change change in collection.Changes)
            {

                if (change is ChangeCollection)
                {
                    var cc = change as ChangeCollection;
                    RedoChangeCollection(ref cc);
                }

                else if (change is PropertyChange)
                {
                    var pc = change as PropertyChange;

                    NowChangingObject = pc.Object;
                    NowChangingPropertyName = pc.PropertyName;
                    pc.Object.GetType().GetProperty(pc.PropertyName).SetValue(pc.Object, pc.OldValue, null);
                    NowChangingPropertyName = "";
                    NowChangingObject = null;
                }

                else if (change is ListChange)
                {
                    var lc = change as ListChange;

                    if (lc is ListChangeAddElement)
                    {
                        var lcae = lc as ListChangeAddElement;

                        NowChangingObject = lcae.Object;
                        ((System.Collections.IList)lcae).Insert(lcae.Index, lcae.AddedElement);
                    }
                    else if (lc is ListChangeAddElementRange)
                    {
                        var lcaer = lc as ListChangeAddElementRange;
                        NowChangingObject = lcaer.Object;
                        int i = lcaer.StartIndex;

                        var list = lcaer.Object as System.Collections.IList;
                        foreach (object element in lcaer.AddedElements)
                        {
                            list.Insert(i, element);
                            i++;
                        }

                    }
                    else if (lc is ListChangeRemoveElement)
                    {
                        var lcre = lc as ListChangeRemoveElement;

                        NowChangingObject = lcre.Object;
                        ((System.Collections.IList)lcre.Object).Remove(lcre.RemovedElement);
                    }
                    else if (lc is ListChangeRemoveElementRange)
                    {
                        var lcrer = lc as ListChangeRemoveElementRange;

                        NowChangingObject = lcrer.Object;
                        var list = lcrer.Object as System.Collections.IList;
                        foreach (var item in lcrer.RemovedElements) list.Remove(item);
                        NowChangingObject = null;
                    }
                }
            }

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
            {
                AddChange(TempChangeCollection);
            }
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
