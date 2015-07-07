using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MI.Modules.Serial;
using System.Windows.Forms;

namespace MI.Modules.SocketTcp
{
    public class SocketHandle
    {
        NetworkStream networkStream;
        Socket socket;

        #region Eventos
        public event SocketConnectedHandle SocketConnected;
        public event SocketDisconnectedHandle SocketDisconnected;
        public event SocketDataReceivedHandle SocketDataReceived;
        public event SocketDataSentHandle SocketDataSent;
        #endregion


        public SocketHandle(string IP, ushort Porta)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(IPAddress.Parse(IP), Porta);
            networkStream = new NetworkStream(socket);
            new Thread(() => ReceivData(true));
        }

        private List<ArgsType> DeserializeArgs(int commands)
        {
            List<ArgsType> value = new List<ArgsType>();
            for (int i = 0; i < 16; i++)
                if ((commands & (0x01 << i)) == 0x01)
                    value.Add((ArgsType)(0x01 << i));

            return value;
        }

        private void ReceivData(bool ContinueReceive)
        {
            while (ContinueReceive)
            {
                byte[] EnumData = new byte[4];
                if (networkStream.Read(EnumData, 0, 4) == 0) break;

                int Commands = BitConverter.ToInt32(EnumData, 0);
                byte[] Values;
                List<ArgsType> Args = DeserializeArgs(Commands);
                for (int i = 0; i < Args.Count; i++)
                    switch (Args[i])
                    {
                        case ArgsType.Fluxo:
                        case ArgsType.VolumeR1:
                        case ArgsType.VolumeR2:
                        case ArgsType.VolumeTanque:
                        case ArgsType.ValorIluminacao:
                            Values = new byte[sizeof(float)];
                            if (networkStream.Read(Values, 0, sizeof(float)) == 0) goto BreakWhile;
                            OnSocketDataReceived(Args[i], BitConverter.ToSingle(Values, 0));
                            break;

                        default:
                            break;
                    }

                continue;
            BreakWhile:
                break;
            }
        }

        public void Disconnect()
        {
            networkStream.Close();
            networkStream.Dispose();
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket.Dispose();
        }

        protected virtual void OnSocketDataReceived(ArgsType arg, float value)
        {
            if (SocketDataReceived == null)
                return;
            Control control = SocketDataReceived.Target as Control;
            if (control != null && control.InvokeRequired)
                control.Invoke(SocketDataReceived, this, arg, value);
            else
                SocketDataReceived(this, arg, value);
        }

        protected virtual void OnSocketDataSent(params ArgsType[] Args)
        {
            if (SocketDataSent == null)
                return;
            Control control = SocketDataSent.Target as Control;
            if (control != null && control.InvokeRequired)
                control.Invoke(SocketDataSent, this, Args);
            else
                SocketDataSent(this, Args);
        }

        protected virtual void OnSocketConnected(string Msg)
        {
            if (SocketConnected == null)
                return;
            Control control = SocketConnected.Target as Control;
            if (control != null && control.InvokeRequired)
                control.Invoke(SocketConnected, this, Msg);
            else
                SocketConnected(this, Msg);
        }

        protected virtual void OnSocketDisconnected(string Msg)
        {
            if (SocketDisconnected == null)
                return;
            Control control = SocketDisconnected.Target as Control;
            if (control != null && control.InvokeRequired)
                control.Invoke(SocketDisconnected, this, Msg);
            else
                SocketDisconnected(this, Msg);
        }
    }
}
