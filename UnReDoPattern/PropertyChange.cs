using GSSubtitle.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSSubtitle.Controllers.UnReDoPattern
{
    public class PropertyChange : Change, IDisposable
    {
        public PropertyChange(object obJect, string propertyname, string description)
        {
            //if (URD.NowChangingObject == obJect)
            //{
            //    if (URD.NowChangingPropertyName.Equals(propertyname)) return;
            //}

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


        public object OldValue { get; set; } = null;
        public object NewValue { get; set; } = null;
        public string PropertyName { get; set; }

        private bool dispose;
        private bool _IsValueType = false;
        private bool _IsString = false;
    }
}
