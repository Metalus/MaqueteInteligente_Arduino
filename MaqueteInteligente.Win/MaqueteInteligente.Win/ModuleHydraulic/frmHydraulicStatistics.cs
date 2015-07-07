using DevExpress.XtraCharts;
using MI.Modules.SqlAccess;
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
    public partial class frmHydraulicStatistics : BaseXtraForm
    {
        public frmHydraulicStatistics()
        {
            InitializeComponent();
        }

        private void frmHydraulicStatistics_Load(object sender, EventArgs e)
        {
            using (SqlAccessHandle access = new SqlAccessHandle())
            {
                Dictionary<DateTime, float> Consumo = access.ConsumoAgua(DateTime.Today.AddDays(-2));
                SeriesCollection series = chartControl1.Series;
                series[0].Points.Clear();
                foreach (DateTime s in Consumo.Keys)
                    series[0].Points.Add(new SeriesPoint(s.ToString("d"), new object[] { Consumo[s] }));
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            using (SqlAccessHandle access = new SqlAccessHandle())
            {
                Dictionary<DateTime, float> Consumo = access.ConsumoAgua(dateTimePicker1.Value, dateTimePicker2.Value);
                SeriesCollection series = chartControl1.Series;
                series[0].Points.Clear();
                foreach (DateTime s in Consumo.Keys)
                    series[0].Points.Add(new SeriesPoint(s.ToString("d"), new object[] { Consumo[s] }));
            }
        }
    }
}
