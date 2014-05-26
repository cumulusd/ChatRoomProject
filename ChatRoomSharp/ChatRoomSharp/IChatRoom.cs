using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ChatRoomSharp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(CallbackContract=typeof(IChatRoomCallback))]
    public interface IChatRoom
    {

        [OperationContract]
        void JoinRoom();

        [OperationContract]
        void LeaveRoom();

        [OperationContract]
        void SendMessage(String fromClientName, String message);


        
        // TODO: Add your service operations here
    }


    public interface IChatRoomCallback
    {
        [OperationContract]
        void didReceiveMessage(String fromClientName, String message);
    }


}
