
using System;
using URD.Tools;

namespace URD.BasicOperations
{
    //property change
    public class PropertyChange : Change, IDisposable, IUndoAble
    {
        
        private bool _dispose;
        private readonly bool _isValueType;
        private readonly bool _isString;
        
        public object OldValue { get; set; }
        public object NewValue { get; set; } 
        public string PropertyName { get; set; }
        
        
        public PropertyChange(object obJect, string propertyname, string description)
        {

            _dispose = true;
            Description = description;
            Object = obJect;
            var property = obJect.GetType().GetProperty(propertyname).GetValue(obJect, null);
            _isValueType = property.GetType().IsValueType;
            _isString = obJect is string;
            OldValue = _isValueType ? property :
            _isString ? string.Copy((string)property) : SerializationTools.GetCopyOfAObject(property);
            PropertyName = propertyname;

        }
        public void Dispose()
        {
            if (!_dispose) return;
            var value = Object.GetType().GetProperty(PropertyName).GetValue(Object, null);
            NewValue = _isValueType ? value : _isString ? string.Copy((string)value) : SerializationTools.GetCopyOfAObject(value);
            URD.AddChange(this);
            _dispose = false;
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
    }
}
