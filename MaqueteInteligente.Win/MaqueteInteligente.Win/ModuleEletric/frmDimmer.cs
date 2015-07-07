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

namespace MaqueteInteligente.Win.ModuleEletric
{
    public partial class frmDimmer : BaseXtraForm
    {
        public frmDimmer()
        {
            InitializeComponent();
            chartControl1.Series[0].Points.Clear();
            chartControl1.Series[1].Points.Clear();
            chartControl1.Series[2].Points.Clear();
            chartControl1.Series[3].Points.Clear();
            chartControl1.Series[4].Points.Clear();
        }

        public void ApplyValues(int[] Luminosidade)
        {
            lock (this)
            {
                lbQ1.Text = ((Luminosidade[(int)Comodos.Quarto1] * 4 + 1) * 0.001953) + "W";
                lbQ2.Text = ((Luminosidade[(int)Comodos.Quarto2] * 4 + 1) * 0.001953) + "W";
                lbSala.Text = ((Luminosidade[(int)Comodos.Sala]  * 4 + 1) * 0.001953)+ "W";
                lbCozinha.Text = ((Luminosidade[(int)Comodos.Cozinha]  * 4 + 1) * 0.001953)+ "W";
                lbBanheiro.Text = ((Luminosidade[(int)Comodos.Banheiro]  * 4 + 1) * 0.001953)+ "W";
                DateTime now = DateTime.Now;
            
                for (int i = 0; i < chartControl1.Series.Count; i++)
                {
                    Series s = chartControl1.Series[i];
                    s.Points.Add(new SeriesPoint(new System.DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, 0), (float)(((Luminosidade[i] * 4) + 1) * 0.001953)));
                    if (s.Points.Count >= 80)
                        s.Points.RemoveRange(0, 1);
                }
            }
        }
    }
}
