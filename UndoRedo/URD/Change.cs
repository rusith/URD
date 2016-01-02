namespace URD
{
    /// <summary>
    /// this is the base class of all type of changes 
    /// if you creating a new type of change class you must extend this class
    /// </summary>
    public class Change
    {

        public object Object { get; set; } = null;
        public string Description { get; set; } = "";
    }
}
