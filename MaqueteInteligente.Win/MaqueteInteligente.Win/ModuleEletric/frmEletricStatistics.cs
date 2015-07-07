using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MI.Modules.Consumo;
using DevExpress.XtraCharts;
using MI.Modules.SqlAccess;

namespace MaqueteInteligente.Win.ModuleEletric
{
    public partial class frmEletricStatistics : BaseXtraForm
    {
        public frmEletricStatistics()
        {
            InitializeComponent();
            using (SqlAccessHandle access = new SqlAccessHandle())
            {
                Dictionary<DateTime, float> Consumo = access.ConsumoEnergia(DateTime.Today.AddDays(-2));
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
                Dictionary<DateTime, float> Consumo = access.ConsumoEnergia(dateTimePicker1.Value, dateTimePicker2.Value);
                SeriesCollection series = chartControl1.Series;
                series[0].Points.Clear();
                foreach (DateTime s in Consumo.Keys)
                    series[0].Points.Add(new SeriesPoint(s.ToString("d"), new object[] { Consumo[s] }));
            }
        }
    }
}
