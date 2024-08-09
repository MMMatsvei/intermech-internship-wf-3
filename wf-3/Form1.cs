using System;
using System.Windows.Forms;

namespace wf_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonPrev_Click(object sender, EventArgs e)
        {
            userControl1.PreviousDays();
            dtpDate.Value = userControl1.getCenterDay();
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            userControl1.NextDays();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            userControl1.GoToDate(dtpDate.Value);
        }

        private void ButtonAddEvent_Click(object sender, EventArgs e)
        {
            userControl1.AddEvent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            userControl1.SaveEvents();
        }
    }
}
