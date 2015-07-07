using DevExpress.XtraCharts;
using MI.Modules.Serial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MaqueteInteligente.Win.ModuleHydraulic
{
    public partial class frmHydraulicReal : BaseXtraForm
    {
        public frmHydraulicReal()
        {
            InitializeComponent();
            chartControl1.Series[0].Points.Clear();
        }


        public void ApplyValues(int Consumo)
        {
            lock (this)
            {
                DateTime now = DateTime.Now;
                    Series s = chartControl1.Series[0];
                    s.Points.Add(new SeriesPoint(new System.DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, 0), Consumo));
                    if (s.Points.Count >= 80)
                        s.Points.RemoveRange(0, 1);
            }
        }
    }
}
