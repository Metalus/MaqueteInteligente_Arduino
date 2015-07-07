using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Threading;

namespace MI.Modules.Serial
{
    public class SerialBridge //: SerialPort
    {

        SerialPort serialArduino;

        public bool IsOpen
        {
            get { return serialArduino.IsOpen; }
        }

        public static Dictionary<string, ArgsType> ConvertArgs =
            new Dictionary<string, ArgsType>
            {
                {"V1:",ArgsType.VolumeR1},
                {"V2:",ArgsType.VolumeR2},
                {"VT:",ArgsType.VolumeTanque},
                {"FL:",ArgsType.Fluxo},
                {"VI:",ArgsType.ValorIluminacao}
            };

        #region Eventos
        public event SerialConnectedHandle SerialConnected;
        public event SerialDisconnectedHandle SerialDisconnected;
        public event SerialDataReceivedHandle SerialDataReceived;
        public event SerialDataSentHandle SerialDataSent;
        public event SerialAllDataReceivedHandle SerialAllDataReceived;
        #endregion

        public SerialBridge(SerialPort serialArduino)
        {
            try
            {
                this.serialArduino = serialArduino;

                this.serialArduino.DataReceived += ReceivData;
                this.serialArduino.Open();
                if (IsOpen)
                    OnSerialConnected("Conexão realizada com sucesso");
            }
            catch (Exception ex)
            {
                OnSerialConnected(ex.Message);
            }
        }

        public void CloseSerial()
        {
            try
            {

                SendData(ArgsType.ShutdownClient);
                serialArduino.Close();
                serialArduino.Dispose();
                OnSerialDisconnected("Desconectado com sucesso");
            }
            catch (Exception ex)
            {
                OnSerialDisconnected(ex.Message);
            }
        }

        private void ReceivData(object s, SerialDataReceivedEventArgs e)
        {
            string[] Valores = serialArduino
                             .ReadTo(";")
                             .Replace("\n", string.Empty)
                             .Split('\r');
            if (String.IsNullOrEmpty(Valores[0])) return;
            for (int j = 0; j < Valores.Length -1; j++)
            {
                ArgsType arg = ConvertArgs[Valores[j].Substring(0, 3)];
                object Value;
                if (arg == ArgsType.ValorIluminacao)
                {
                    int[] Luminosidades = new int[5];
                    for (int i = 0; i < 5; i++)
                        Luminosidades[i] = Convert.ToInt32(Valores[j + i +1]);
                    Value = Luminosidades;
                    OnSerialDataReceived(arg, Value);
                    break;
                }
                else
                    Value = int.Parse(Valores[j].Substring(3));

                OnSerialDataReceived(arg, Value);
            }
            OnSerialAllDataReceived();
        }

        public bool SendData(params ArgsType[] Args)
        {
            lock (this)
            {
                try
                {
                    int ArgMapped = 0;
                    for (int i = 0; i < Args.Length; i++)
                        ArgMapped |= (int)Args[i];

                    serialArduino.Write(ArgMapped.ToString());
                    OnSerialDataSent(Args);
                    return true;
                }
                catch { return false; }
            }
        }

        protected virtual void OnSerialDataReceived(ArgsType arg, object value)
        {
            if (SerialDataReceived == null)
                return;
            Control control = SerialDataReceived.Target as Control;
            if (control != null && control.InvokeRequired)
                control.Invoke(SerialDataReceived, this, arg, value);
            else
                SerialDataReceived(this, arg, value);
        }

        protected virtual void OnSerialDataSent(params ArgsType[] Args)
        {
            if (SerialDataSent == null)
                return;
            Control control = SerialDataSent.Target as Control;
            if (control != null && control.InvokeRequired)
                control.Invoke(SerialDataSent, this, Args);
            else
                SerialDataSent(this, Args);
        }

        protected virtual void OnSerialAllDataReceived()
        {
            if (SerialAllDataReceived == null)
                return;
            Control control = SerialAllDataReceived.Target as Control;
            if (control != null && control.InvokeRequired)
                control.Invoke(SerialAllDataReceived, this, EventArgs.Empty);
            else
                SerialAllDataReceived(this, EventArgs.Empty);
        }

        protected virtual void OnSerialConnected(string Msg)
        {
            if (SerialConnected == null)
                return;
            Control control = SerialConnected.Target as Control;
            if (control != null && control.InvokeRequired)
                control.Invoke(SerialConnected, this, Msg);
            else
                SerialConnected(this, Msg);
        }

        protected virtual void OnSerialDisconnected(string Msg)
        {
            if (SerialDisconnected == null)
                return;
            Control control = SerialDisconnected.Target as Control;
            if (control != null && control.InvokeRequired)
                control.Invoke(SerialDisconnected, this, Msg);
            else
                SerialDisconnected(this, Msg);
        }
    }
}
