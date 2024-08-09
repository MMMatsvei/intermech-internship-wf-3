using System;
using System.Windows.Forms;

namespace wf_3
{
    public partial class Form2 : Form
    {
        public CalendarEvent calendarEvent { get; private set; }
        public DateTime startDateTime { get; private set; }


        public Form2()
        {
            InitializeComponent();
            dtpDate.Value = DateTime.Today;
            this.buttonDelete.Enabled = false;
        }

        public Form2(DateTime centerDay)
        {
            InitializeComponent();
            calendarEvent = new CalendarEvent();
            DateTime dt = centerDay;
            dtpDate.Value = new DateTime(dt.Year, dt.Month, dt.Day);
            dtpTime.Value = new DateTime(dt.Year, dt.Month, dt.Day, 8, 0, 0);
            this.buttonDelete.Enabled = false;
        }

        public Form2(DateTime centerDay, int columnIndex, int rowIndex)
        {
            InitializeComponent();
            calendarEvent = new CalendarEvent();
            DateTime dt = centerDay.AddDays(columnIndex - 4);
            dtpDate.Value = new DateTime(dt.Year, dt.Month, dt.Day);
            dtpTime.Value = new DateTime(dt.Year, dt.Month, dt.Day, rowIndex + 8, 0, 0);
            this.buttonDelete.Enabled = false;
        }

        public Form2(DateTime centerDay, DataGridViewCell cell)
        {
            InitializeComponent();
            calendarEvent = new CalendarEvent();
            DateTime dt = centerDay.AddDays(cell.ColumnIndex - 4);
            dtpDate.Value = new DateTime(dt.Year, dt.Month, dt.Day);
            dtpTime.Value = new DateTime(dt.Year, dt.Month, dt.Day, cell.RowIndex + 8, 0, 0);
            startDateTime = new DateTime(dt.Year, dt.Month, dt.Day, cell.RowIndex + 8, 0, 0);
            txtName.Text = cell.Value.ToString();
            cboCategory.Text = CalendarEvent.ConvertColor(cell.Style.BackColor);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if(dtpTime.Value.Hour >= 8 && dtpTime.Value.Hour != 0)
            {
                DateTime dt = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, dtpTime.Value.Hour, 0, 0);
                calendarEvent = new CalendarEvent(txtName.Text, dt, cboCategory.SelectedItem.ToString()); ;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                dtpTime.Value = new DateTime(dtpTime.Value.Year, dtpTime.Value.Month, dtpTime.Value.Day, 8, 0, 0);
                MessageBox.Show("В приложении графический интерфейс пока не рассчитан на события раньше 8:00");
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            calendarEvent = null;
            DialogResult = DialogResult.Abort;
            Close();
        }
    }
}
