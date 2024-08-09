using System.Windows.Forms;

namespace wf_3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Button buttonPrev;
        private Button buttonNext;
        private Button buttonAddEvent;
        private DateTimePicker dtpDate;
        private UserControl1 userControl1;
        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonAddEvent = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.userControl1 = new UserControl1();
            this.SuspendLayout();

            // 
            // buttonPrev
            // 
            this.buttonPrev.Location = new System.Drawing.Point(25, 25);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(75, 23);
            this.buttonPrev.TabIndex = 0;
            this.buttonPrev.Text = "<";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Click += new System.EventHandler(this.ButtonPrev_Click);

            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(927, 25);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 1;
            this.buttonNext.Text = ">";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.ButtonNext_Click);

            // 
            // buttonAddEvent
            // 
            this.buttonAddEvent.Location = new System.Drawing.Point(356, 25);
            this.buttonAddEvent.Name = "buttonAddEvent";
            this.buttonAddEvent.Size = new System.Drawing.Size(150, 23);
            this.buttonAddEvent.TabIndex = 3;
            this.buttonAddEvent.Text = "Добавить событие";
            this.buttonAddEvent.UseVisualStyleBackColor = true;
            this.buttonAddEvent.Click += new System.EventHandler(this.ButtonAddEvent_Click);

            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.CustomFormat = "dd.MM.yyyy";
            this.dtpDate.Size = new System.Drawing.Size(150, 23);
            this.dtpDate.Location = new System.Drawing.Point(521, 25);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.TabIndex = 2;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);

            // 
            // userControl1
            // 
            this.userControl1.Location = new System.Drawing.Point(25, 60);
            this.userControl1.Name = "userControl1";
            this.userControl1.Size = new System.Drawing.Size(977, 407);
            this.userControl1.TabIndex = 2;

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1027, 492);
            this.Controls.Add(this.userControl1);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPrev);
            this.Controls.Add(this.buttonAddEvent);
            this.Controls.Add(this.dtpDate);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            this.Name = "Form1";
            this.Text = "Calendar";
            this.ResumeLayout(false);
        }

        #endregion
    }
}

