ChatRoomProject
===============

When Windows Communication Foundation (WCF) was releasted back in .NET 3.5 I saw an amazing opportunity: easily allow applications to communicate! This project is a simple example of that.

You would setup the "ChatRoomServer" and host it using either IIS or WAS and then change the configuration files of the "ChatRoomClient" to point at the URL you chose for the server.

The service and client are simple in this example: whenever a message is sent from one of the connected clients it is broadcasted to all of the other clients. There is a lot more to do with error handling and connection management, but the code works in the ideal testing case.

If anything, it shows how powerful WCF is in that most of the work is actually in getting the configuration files and deployment right, rather than actually writing code.
