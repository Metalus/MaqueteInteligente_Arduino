using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Modules.Serial
{
#if BETA
    public delegate void SerialDataReceivedHandle(object sender, TypeData typeData, RoomData roomData, object Arg);

    public delegate void SerialDataSentHandle(object sender, TypeData typeData, RoomData roomData, object Arg);
#else
    public delegate void SerialDataReceivedHandle(object sender, ArgsType Args, object Value);

    public delegate void SerialDataSentHandle(object sender,params ArgsType[] Args);
    
    public delegate void SerialAllDataReceivedHandle(object sender, EventArgs e);
#endif

    public delegate void SerialConnectedHandle(object sender, string Msg);

    public delegate void SerialDisconnectedHandle(object sender, string Msg);

}
