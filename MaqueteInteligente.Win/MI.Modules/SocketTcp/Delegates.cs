using MI.Modules.Serial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Modules.SocketTcp
{

    public delegate void SocketDataReceivedHandle(object sender, ArgsType Args, float Value);

    public delegate void SocketDataSentHandle(object sender,params ArgsType[] Args);

    public delegate void SocketConnectedHandle(object sender, string Msg);

    public delegate void SocketDisconnectedHandle(object sender, string Msg);

}
