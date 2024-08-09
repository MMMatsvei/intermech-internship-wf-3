using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace wf_3
{
    public partial class UserControl1 : UserControl
    {
        SortedDictionary<DateTime, CalendarEvent> calendarEvents;

        DateTime centerDay = DateTime.Now;
        DateTime[] days = new DateTime[9];

        string filePath = "data.bin";

        public UserControl1()
        {
            InitializeComponent();

            clickTimer = new Timer();
            clickTimer.Interval = 300;
            clickTimer.Tick += ClickTimer_Tick;

            LoadEvents();
            DrawCalendar();
        }

        private void DrawCalendar()
        {
            dataGridView1.ColumnCount = 9;
            dataGridView1.RowCount = 16;

            for (int i = 0; i < 9; i++)
            {
                days[i] = centerDay.AddDays(i - 4);
                dataGridView1.Columns[i].HeaderText = days[i].ToString("ddd, dd.MM", new CultureInfo("ru-RU"));
                dataGridView1.Columns[i].Width = 100;
            }

            for (int i = 8; i < 24; i++)
            {
                dataGridView1.Rows[i - 8].HeaderCell.Value = i.ToString() + ":00";
            }

            DrawEvents();
        }

        private void DrawEvents()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Value = null;
                    cell.Style.BackColor = Color.White;
                }
            }

            DateTime dt = new DateTime(days[0].Year, days[0].Month, days[0].Day, 8, 0, 0);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 8; j < 24; j++)
                {
                    if(calendarEvents.ContainsKey(dt)) 
                    {
                        dataGridView1.Rows[j - 8].Cells[i].Value = calendarEvents[dt].getName();
                        dataGridView1.Rows[j - 8].Cells[i].Style.BackColor = calendarEvents[dt].getColor();
                    }
                    dt = dt.AddHours(1);
                }
                dt = dt.AddHours(8);
            }
        }

        public DateTime getCenterDay()
        {
            return centerDay;
        }
        private DateTime getDateTime(int columnIndex, int rowIndex)
        {
            DateTime dt = centerDay.AddDays(columnIndex - 4);
            dt = new DateTime(dt.Year, dt.Month, dt.Day, rowIndex + 8, 0, 0);
            return dt;
        }

        public void PreviousDays()
        {
            centerDay = centerDay.AddDays(-3);
            this.DrawCalendar();
            
            if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.ColumnIndex <= 5)
            {
                dataGridView1.CurrentCell = dataGridView1[dataGridView1.CurrentCell.ColumnIndex + 3, dataGridView1.CurrentCell.RowIndex];
            }
            else
            {
                dataGridView1.CurrentCell = null;
            }
        }

        public void NextDays()
        {
            centerDay = centerDay.AddDays(3);
            this.DrawCalendar();

            if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.ColumnIndex >= 3)
            {
                dataGridView1.CurrentCell = dataGridView1[dataGridView1.CurrentCell.ColumnIndex - 3, dataGridView1.CurrentCell.RowIndex];
            }
            else
            {
                dataGridView1.CurrentCell = null;
            };
        }

        public void AddEvent()
        {
            using (var form2 = new Form2(centerDay))
            {
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    var calendarEvent = form2.calendarEvent;

                    if(calendarEvents.ContainsKey(calendarEvent.getDateTime())) {
                        calendarEvents[calendarEvent.getDateTime()] = calendarEvent;
                    }
                    else
                    {
                        calendarEvents.Add(calendarEvent.getDateTime(), calendarEvent);
                    }

                    DrawEvents();
                }
            }
        }

        public void GoToDate(DateTime dt)
        {
            centerDay = dt;
            this.DrawCalendar();

            if (dataGridView1.CurrentCell != null)
            {
                dataGridView1.CurrentCell = null;
            };
        }


        public void SaveEvents()
        {
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, calendarEvents);
            }
        }

        public void LoadEvents()
        {
            if (!File.Exists(filePath))
            {
                calendarEvents = new SortedDictionary<DateTime, CalendarEvent>();
                return;
            }

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var formatter = new BinaryFormatter();
                calendarEvents = (SortedDictionary<DateTime, CalendarEvent>)formatter.Deserialize(stream);
            }
        }



        private bool isDragging = false;
        private bool isMouseDown = false;
        private Point initialMousePosition;
        private Timer clickTimer;

        private void ClickTimer_Tick(object sender, EventArgs e)
        {
            clickTimer.Stop(); 
            isDragging = false;
            isMouseDown = false;
        }


        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isDragging) return;

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    using (var form2 = new Form2(centerDay, e.ColumnIndex, e.RowIndex))
                    {
                        if (form2.ShowDialog() == DialogResult.OK)
                        {
                            var calendarEvent = form2.calendarEvent;
                            calendarEvents.Add(calendarEvent.getDateTime(), calendarEvent);

                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = calendarEvent.getName();
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = calendarEvent.getColor();
                        }
                    }
                }
                else
                {
                    using (var form2 = new Form2(centerDay, dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex]))
                    {
                        var f2sd = form2.ShowDialog();
                        if (f2sd == DialogResult.OK)
                        {
                            calendarEvents.Remove(getDateTime(e.ColumnIndex, e.RowIndex));
                            var calendarEvent = form2.calendarEvent;
                            calendarEvents[calendarEvent.getDateTime()] = calendarEvent;

                            DrawEvents();
                        }
                        else if(f2sd == DialogResult.Abort)
                        {
                            var dt = form2.startDateTime;
                            calendarEvents.Remove(dt);
                            DrawEvents();
                        }
                    }
                }
            }
        }


        private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var hit = dataGridView1.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    int columnIndex = hit.ColumnIndex;
                    int rowIndex = hit.RowIndex;

                    dataGridView1.CurrentCell = dataGridView1[columnIndex, rowIndex];

                    initialMousePosition = e.Location;

                    if (isMouseDown)
                    {
                        clickTimer.Stop();
                        isMouseDown = false;
                        DataGridView1_CellDoubleClick(sender, new DataGridViewCellEventArgs(columnIndex, rowIndex));
                    }
                    else
                    {
                        isMouseDown = true;
                        clickTimer.Start();
                    }
                }
            }
            /*
            if (e.Button == MouseButtons.Left)
            {
                var hit = dataGridView1.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    int rowIndex = hit.RowIndex;
                    int columnIndex = hit.ColumnIndex;

                    var cell = dataGridView1.Rows[rowIndex].Cells[columnIndex];
                    if (cell.Value != null)
                    {
                        Console.WriteLine(cell.Value.ToString());
                        Console.WriteLine(getDateTime(columnIndex, rowIndex));
                        Console.WriteLine(cell.Style.BackColor);
                        var calendarEvent = new CalendarEvent(cell.Value.ToString(), getDateTime(columnIndex, rowIndex), cell.Style.BackColor);
                        
                        dataGridView1.DoDragDrop(calendarEvent, DragDropEffects.Move);
                    }
                }
            }
            */
        }

        private void DataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Math.Abs(e.X - initialMousePosition.X) > 5 || Math.Abs(e.Y - initialMousePosition.Y) > 5)
                {
                    isDragging = true;

                    var hit = dataGridView1.HitTest(e.X, e.Y);
                    if (hit.Type == DataGridViewHitTestType.Cell)
                    {
                        int rowIndex = hit.RowIndex;
                        int columnIndex = hit.ColumnIndex;
                        
                        var cell = dataGridView1.Rows[rowIndex].Cells[columnIndex];
                        if (cell.Value != null)
                        {
                            var calendarEvent = new CalendarEvent(cell.Value.ToString(), getDateTime(columnIndex, rowIndex), cell.Style.BackColor);

                            dataGridView1.DoDragDrop(calendarEvent, DragDropEffects.Move);
                        }
                    }
                }
            }
        }

        private void DataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(CalendarEvent)))
            {
                var calendarEvent = (CalendarEvent)e.Data.GetData(typeof(CalendarEvent));

                var point = dataGridView1.PointToClient(new Point(e.X, e.Y));
                var hit = dataGridView1.HitTest(point.X, point.Y);

                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    int rowIndex = hit.RowIndex;
                    int columnIndex = hit.ColumnIndex;

                    dataGridView1.Rows[rowIndex].Cells[columnIndex].Value = calendarEvent.getName();
                    dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.BackColor = calendarEvent.getColor();

                    calendarEvents.Remove(calendarEvent.getDateTime());
                    calendarEvent.setDateTime(getDateTime(columnIndex, rowIndex));

                    if(calendarEvents.ContainsKey(calendarEvent.getDateTime()))
                    {
                        calendarEvents[calendarEvent.getDateTime()] = calendarEvent;
                    }
                    else
                    {
                        calendarEvents.Add(calendarEvent.getDateTime(), calendarEvent);
                    }

                    DrawCalendar();
                }
            }
            isDragging = false;
        }

        private void DataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void DataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            isMouseDown = false;
        }
    }
}
