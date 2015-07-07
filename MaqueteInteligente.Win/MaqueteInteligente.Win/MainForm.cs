using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using MI.Modules.Serial;
using System.IO.Ports;
using MaqueteInteligente.Win.ModuleEletric;
using MI.Modules.SocketTcp;
using MI.Modules.SqlAccess;
using MaqueteInteligente.Win.ModuleHydraulic;
using System.Threading;
using System.Diagnostics;


namespace MaqueteInteligente.Win
{
    public partial class MainForm : RibbonForm
    {
        #region Forms
        frmEletricStatistics eletricStatistics;
        frmHydraulicStatistics hydraulicStatistics;
        frmDimmer dimmer;
        #endregion

        List<string> Serials = new List<string>();

        public static SerialBridge serialBridge = null;

        public MainForm()
        {
            InitializeComponent();
        }

        void socket_SocketDataReceived(object sender, ArgsType Args, object Value)
        {
            switch (Args)
            {
                case ArgsType.VolumeR1:
                    pbR1.Position = (int)Value;
                    break;

                case ArgsType.VolumeR2:
                    pbR2.Position = (int)Value;
                    break;

                case ArgsType.VolumeTanque:
                    pbTanque.Position = (int)Value;
                    break;

                case ArgsType.ValorIluminacao:
                    int[] Luminosidades = (int[])Value;
                    ScaleQ1.Value = (Luminosidades[0] * 100) / 255;
                    ScaleQ2.Value = (Luminosidades[1] * 100) / 255;
                    ScaleSala.Value = (Luminosidades[2] * 100) / 255;
                    ScaleCozinha.Value = (Luminosidades[3] * 100) / 255;
                    ScaleBanheiro.Value = (Luminosidades[4] * 100) / 255;
                    break;
            }
        }

        private void OpenMDI<T>(ref T form) where T : XtraForm
        {
            if (form == null || form.IsDisposed)
            {
                //panel1.SendToBack();
                panel1.Visible = false;
                form = Activator.CreateInstance<T>();
                form.WindowState = FormWindowState.Maximized;
                form.MdiParent = this;
                form.Show();
                form.BringToFront();
                form.FormClosed += (s, e) => panel1.Visible = true;
            }
            else
            {
                panel1.Visible = false;
                form.BringToFront();
            }
        }

        void serialBridge_SerialDataReceived(object sender, ArgsType Args, object Value)
        {
            switch (Args)
            {
                case ArgsType.VolumeR1:
                    pbR1.Position = Convert.ToInt32(Value);
                    lbR1.Text = pbR1.Position.ToString();
                    break;

                case ArgsType.VolumeR2:
                    pbR2.Position = Convert.ToInt32(Value);
                    lbR2.Text = pbR2.Position.ToString();
                    break;

                case ArgsType.VolumeTanque:
                    pbTanque.Position = Convert.ToInt32(Value);
                    if (pbTanque.Position < Convert.ToInt32(lbTQ.Text))
                    {
                        int Consumo = Convert.ToInt32(lbTQ.Text) - Convert.ToInt32(pbTanque.Position);
                        AplicarConsumoAgua(Consumo);   
                    }
                    lbTQ.Text = pbTanque.Position.ToString();
                    break;

                case ArgsType.ValorIluminacao:
                    int[] Luminosidades = (int[])Value;
                    ScaleQ1.Value = (Luminosidades[0] * 100) / 255;
                    ScaleQ2.Value = (Luminosidades[1] * 100) / 255;
                    ScaleSala.Value = (Luminosidades[2] * 100) / 255;
                    ScaleCozinha.Value = (Luminosidades[3] * 100) / 255;
                    ScaleBanheiro.Value = (Luminosidades[4] * 100) / 255;
                    AplicarConsumoEletrico(Luminosidades);
                    break;
                default:
                    break;
            }

        }

        private void AplicarConsumoAgua(int Consumo)
        {
            /*if (hydraulicReal != null && !hydraulicReal.IsDisposed)
                hydraulicReal.ApplyValues(Consumo);*/
            using (SqlAccessHandle access = new SqlAccessHandle())
                access.AtualizarConsumoAgua(DateTime.Today, (float)Consumo);             
        }

        private void AplicarConsumoEletrico(int[] Luminosidades)
        {
            if (dimmer != null && !dimmer.IsDisposed)
                dimmer.ApplyValues(Luminosidades);
            float Consumo = 0;
            for (int i = 0; i < 5; i++)
                Consumo += (float)(((Luminosidades[i] + 1) * 4) * 0.001953);

            using (SqlAccessHandle access = new SqlAccessHandle())
                access.AtualizarConsumoEnergia(DateTime.Today, Consumo);
        }

        private void iExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (XtraMessageBox.Show(
                "Deseja fechar o programa?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            { e.Cancel = true; }

            base.OnClosing(e);
            Process.GetCurrentProcess().Kill();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenMDI<frmEletricStatistics>(ref eletricStatistics);
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenMDI<frmDimmer>(ref dimmer);
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenMDI<frmHydraulicStatistics>(ref hydraulicStatistics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] PortsName = SerialPort.GetPortNames();
            bool UpdateCOM = PortsName.Length != repositoryItemComboBox2.Items.Count;

            foreach (string s in PortsName)
                if (!repositoryItemComboBox2.Items.Contains(s))
                {
                    UpdateCOM = true;
                    break;
                }

            if (UpdateCOM)
            {
                repositoryItemComboBox2.Items.Clear();
                repositoryItemComboBox2.Items.AddRange(PortsName);
            }
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (barButtonItem4.Caption == "Conectar")
                {
                    barButtonItem4.Visibility = BarItemVisibility.Never;
                    barEditItem2.Visibility = BarItemVisibility.Always;
                    SerialPort serial = new SerialPort((string)barEditItem1.EditValue, 115200);
                    serialBridge = new SerialBridge(serial);
                    
                    serialBridge.SerialDataReceived += serialBridge_SerialDataReceived;
                    Thread.Sleep(500);
                    serialBridge.SendData(ArgsType.VolumeR1, ArgsType.VolumeR2, ArgsType.VolumeTanque);
                    serialBridge.SerialAllDataReceived += serialBridge_SerialAllDataReceived;
                    barButtonItem4.Caption = "Desconectar";
                    barButtonItem4.Visibility = BarItemVisibility.Always;
                    barEditItem2.Visibility = BarItemVisibility.Never;
                }
                else
                {
                    if (serialBridge.IsOpen)
                        serialBridge.CloseSerial();
                    barButtonItem4.Caption = "Conectar";
                }
            }
            catch
            {
                XtraMessageBox.Show("Não foi possível se comunicar com módulo da maquete.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void serialBridge_SerialAllDataReceived(object sender, EventArgs e)
        {
            serialBridge.SendData(chkLuzBanheiro.Checked ? 0 : ArgsType.OffLuz_Banheiro,
                                     chkLuzCozinha.Checked ? 0 : ArgsType.OffLuz_Cozinha,
                                     chkLuzQ1.Checked ? 0 : ArgsType.OffLuz_Q1,
                                     chkLuzQ2.Checked ? 0 : ArgsType.OffLuz_Q2,
                                     chkLuzSala.Checked ? 0 : ArgsType.OffLuz_Sala,
                                     ArgsType.ValorIluminacao,
                                     ArgsType.VolumeR1,
                                     ArgsType.VolumeR2,
                                     ArgsType.VolumeTanque);
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            new AboutForm().ShowDialog();
        }
    }
}