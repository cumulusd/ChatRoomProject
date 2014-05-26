using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;

namespace ChatRoomSharp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single,ConcurrencyMode=ConcurrencyMode.Multiple,AutomaticSessionShutdown=true)]
    public class ChatRoom : IChatRoom
    {

        ReaderWriterLockSlim _RWLS;
        List<OperationContext> currentClients;

        public ChatRoom()
        {
            _RWLS = new ReaderWriterLockSlim();
            currentClients = new List<OperationContext>();
        }

        public void JoinRoom()
        {
            _RWLS.EnterWriteLock();
            currentClients.Add(OperationContext.Current);
            _RWLS.ExitWriteLock();
        }

        public void LeaveRoom()
        {
            _RWLS.EnterWriteLock();
            currentClients.Remove(OperationContext.Current);
            _RWLS.ExitWriteLock();
        }

        public void SendMessage(string fromClientName, string message)
        {
            String[] args = new String[2];
            args[0] = fromClientName;
            args[1] = message;


            ThreadPool.QueueUserWorkItem(new WaitCallback(doSendMessage),args);
        }

        private void doSendMessage(Object state)
        {
            String[] args = (String[])state;
            IChatRoomCallback theCallback;

            _RWLS.EnterReadLock();

            foreach (OperationContext _context in currentClients)
            {
                theCallback = _context.GetCallbackChannel<IChatRoomCallback>();
                theCallback.didReceiveMessage(args[0], args[1]);
            }

            _RWLS.ExitReadLock();
        }
    }

    
}
