
using System;
using URD.Tools;

namespace URD.BasicOperations
{
    //property change
    public class PropertyChange : Change, IDisposable, IUndoAble
    {
        public PropertyChange(object obJect, string propertyname, string description)
        {

            dispose = true;
            Description = description;
            Object = obJect;
            var property = obJect.GetType().GetProperty(propertyname).GetValue(obJect, null);
            _IsValueType = property.GetType().IsValueType;
            _IsString = obJect is string;
            OldValue = _IsValueType ? property :
            _IsString ? string.Copy(property as string) : SerializationTools.GetCopyOfAObject(property);
            PropertyName = propertyname;

        }
        public void Dispose()
        {
            if (dispose)
            {
                object value = Object.GetType().GetProperty(PropertyName).GetValue(Object, null);
                NewValue = _IsValueType ? value : _IsString ? string.Copy(value as string) : SerializationTools.GetCopyOfAObject(value);
                URD.AddChange(this);
                dispose = false;
            }

        }

        public void Undo()
        {
            using (new ObjectChanging(Object, PropertyName, true,false, this))
            {
                Object.GetType().GetProperty(PropertyName).SetValue(Object, OldValue, null);
            }
        }

        public void Redo()
        {
            using (new ObjectChanging(Object, PropertyName, true, true, this))
            {
                Object.GetType().GetProperty(PropertyName).SetValue(Object, NewValue, null);
            }
        }

        public object OldValue { get; set; } = null;
        public object NewValue { get; set; } = null;
        public string PropertyName { get; set; }

        private bool dispose;
        private bool _IsValueType = false;
        private bool _IsString = false;
    }
}
