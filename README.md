# URP
undo redo pattern for C# applications
this is a basic undo pattern for C# applications. developed for use in GS subtitle project (not published) 
in this pattern when you want a undo / redo able action you will need to warp that action with using statement.
as a example if you going to change a property 
you can do this

using (new PropertyChange(Object, "Property", "Property changed "))
{
  Object.Property="new value";
}

now the change you made to the "Property" property of the "Object" object is added to the drop out stack and it can undo or redo any time you want.

if you want a undoAble list change you can do it using same syntax

```C#
private void AddNewItem(<string>List list,string item,bool undoAble)
{
	using (undoAble?new ListChangeAddElement(list,item,"add new string to the string list"):null) //you can use a condition then you can minimize code
	{
		list.Add(item);
	}
}
```

Removing a Item 
```C#
private void RemoveItem(list,item,bool undoAble)
{
	using(undoAble?new ListChangeRemoveElement(list,item,list.IndexOf(item)," remove "+item+" from the string list"))
	{
		list.Remove(item);
	}
}
```
so on.. (check the code)

benefits of this pattern 

low CPU,Memory usage 
undo only what you want



