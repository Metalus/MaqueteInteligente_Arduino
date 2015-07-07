using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaqueteInteligente.Win
{
    public class BaseXtraForm : XtraForm
    {
        public delegate void LoadCompletedEventHandler();
        public event LoadCompletedEventHandler LoadCompleted;

        public BaseXtraForm()
        {
            this.Shown += new EventHandler(BaseForm_Shown);
        }

        void BaseForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (LoadCompleted != null)
                LoadCompleted();
        }
    }
}
