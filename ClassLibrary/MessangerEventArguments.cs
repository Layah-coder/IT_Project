
//Leah Oswald 7/20/2021

namespace IT_Project.ClassLibrary
{
    public class MessangerEventArguments
    {
        #region Constructors
        public MessangerEventArguments() : base()
        {
        }

        public MessangerEventArguments(string description, object package) : base()
        {
            MessageName = description;
            MessagePackage = package;
        }
        #endregion

        #region getters and setters
        public string MessageName { get; set; }
        public object MessagePackage { get; set; }
        #endregion
    }
}
