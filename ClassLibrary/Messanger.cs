
//Leah Oswald 7/20/2021

namespace IT_Project.ClassLibrary
{
    // Class that is used to send and receive messages.
    public class Messanger
    {

        private static Messanger _Instance;

        public static Messanger Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new Messanger();
                }

                return _Instance;
            }
            set { _Instance = value; }
        }


        public delegate void MessageRecievedHandler(object carrier, MessangerEventArguments e);

        public event MessageRecievedHandler MessageRecieved;

        public void SendMessage(string messageName)
        {
            SendMessage(messageName, null);
        }

        public void SendMessage(string messageName, object messagePackage)
        {
            MessangerEventArguments arg;
            arg = new MessangerEventArguments(messageName, messagePackage);
            RaiseMessageRecieved(arg);
        }

        protected void RaiseMessageRecieved(MessangerEventArguments e)
        {
            if (MessageRecieved != null)
            {
                MessageRecieved(this, e);
            }
        }
    }
}
