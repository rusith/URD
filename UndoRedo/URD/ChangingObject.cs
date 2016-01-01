using System;
using URD;

namespace URD.BasicOperations
{
   public class ChangingAObject
    {
        public void ChangingAObject(object changingObject,string changingpropertyname="")
        {
            URD.NowChangingObject=changingObject;
            if(changingpropertyname!="") URD.NowChangingPropertyName=changingpropertyname;
        }
        
        public void Dispose()
        {
            URD.NowChangingPropertyName="";
            URD.NowChangingObject=null;
        }
    }
}
