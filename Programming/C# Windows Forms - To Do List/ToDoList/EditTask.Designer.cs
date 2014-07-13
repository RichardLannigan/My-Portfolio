namespace ToDoList
{
    partial class EditTask
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_TaskTitle = new System.Windows.Forms.TextBox();
            this.label_TaskTitle = new System.Windows.Forms.Label();
            this.button_AddTask = new System.Windows.Forms.Button();
            this.label_TaskDescription = new System.Windows.Forms.Label();
            this.textBox_TaskDescription = new System.Windows.Forms.TextBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.button_ClearDescription = new System.Windows.Forms.Button();
            this.button_ClearTitle = new System.Windows.Forms.Button();
            this.groupBox_Edit = new System.Windows.Forms.GroupBox();
            this.button_ClearForm = new System.Windows.Forms.Button();
            this.groupBox_Edit.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_TaskTitle
            // 
            this.textBox_TaskTitle.Location = new System.Drawing.Point(11, 54);
            this.textBox_TaskTitle.MaxLength = 16;
            this.textBox_TaskTitle.Name = "textBox_TaskTitle";
            this.textBox_TaskTitle.Size = new System.Drawing.Size(152, 20);
            this.textBox_TaskTitle.TabIndex = 28;
            // 
            // label_TaskTitle
            // 
            this.label_TaskTitle.AutoSize = true;
            this.label_TaskTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TaskTitle.Location = new System.Drawing.Point(7, 19);
            this.label_TaskTitle.Name = "label_TaskTitle";
            this.label_TaskTitle.Size = new System.Drawing.Size(177, 20);
            this.label_TaskTitle.TabIndex = 27;
            this.label_TaskTitle.Text = "Title (Max Character 16)";
            // 
            // button_AddTask
            // 
            this.button_AddTask.Location = new System.Drawing.Point(143, 227);
            this.button_AddTask.Name = "button_AddTask";
            this.button_AddTask.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_AddTask.Size = new System.Drawing.Size(98, 44);
            this.button_AddTask.TabIndex = 12;
            this.button_AddTask.Text = "Done";
            this.button_AddTask.UseVisualStyleBackColor = true;
            this.button_AddTask.Click += new System.EventHandler(this._button_AddTask_Click);
            // 
            // label_TaskDescription
            // 
            this.label_TaskDescription.AutoSize = true;
            this.label_TaskDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TaskDescription.Location = new System.Drawing.Point(7, 83);
            this.label_TaskDescription.Name = "label_TaskDescription";
            this.label_TaskDescription.Size = new System.Drawing.Size(234, 20);
            this.label_TaskDescription.TabIndex = 29;
            this.label_TaskDescription.Text = "Description (Max character 256)";
            // 
            // textBox_TaskDescription
            // 
            this.textBox_TaskDescription.Location = new System.Drawing.Point(11, 110);
            this.textBox_TaskDescription.MaxLength = 256;
            this.textBox_TaskDescription.Multiline = true;
            this.textBox_TaskDescription.Name = "textBox_TaskDescription";
            this.textBox_TaskDescription.Size = new System.Drawing.Size(230, 111);
            this.textBox_TaskDescription.TabIndex = 30;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(11, 255);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(126, 20);
            this.dateTimePicker.TabIndex = 37;
            // 
            // button_ClearDescription
            // 
            this.button_ClearDescription.Location = new System.Drawing.Point(14, 227);
            this.button_ClearDescription.Name = "button_ClearDescription";
            this.button_ClearDescription.Size = new System.Drawing.Size(122, 23);
            this.button_ClearDescription.TabIndex = 38;
            this.button_ClearDescription.Text = "Clear Description";
            this.button_ClearDescription.UseVisualStyleBackColor = true;
            this.button_ClearDescription.Click += new System.EventHandler(this.button_ClearDescription_Click);
            // 
            // button_ClearTitle
            // 
            this.button_ClearTitle.Location = new System.Drawing.Point(174, 52);
            this.button_ClearTitle.Name = "button_ClearTitle";
            this.button_ClearTitle.Size = new System.Drawing.Size(76, 23);
            this.button_ClearTitle.TabIndex = 39;
            this.button_ClearTitle.Text = "Clear Title";
            this.button_ClearTitle.UseVisualStyleBackColor = true;
            this.button_ClearTitle.Click += new System.EventHandler(this.button_ClearTitle_Click);
            // 
            // groupBox_Edit
            // 
            this.groupBox_Edit.Controls.Add(this.button_ClearForm);
            this.groupBox_Edit.Controls.Add(this.button_ClearTitle);
            this.groupBox_Edit.Controls.Add(this.button_ClearDescription);
            this.groupBox_Edit.Controls.Add(this.dateTimePicker);
            this.groupBox_Edit.Controls.Add(this.textBox_TaskDescription);
            this.groupBox_Edit.Controls.Add(this.label_TaskDescription);
            this.groupBox_Edit.Controls.Add(this.button_AddTask);
            this.groupBox_Edit.Controls.Add(this.label_TaskTitle);
            this.groupBox_Edit.Controls.Add(this.textBox_TaskTitle);
            this.groupBox_Edit.Location = new System.Drawing.Point(12, 12);
            this.groupBox_Edit.Name = "groupBox_Edit";
            this.groupBox_Edit.Size = new System.Drawing.Size(256, 282);
            this.groupBox_Edit.TabIndex = 36;
            this.groupBox_Edit.TabStop = false;
            this.groupBox_Edit.Text = "Edit Task";
            // 
            // button_ClearForm
            // 
            this.button_ClearForm.Location = new System.Drawing.Point(185, 16);
            this.button_ClearForm.Name = "button_ClearForm";
            this.button_ClearForm.Size = new System.Drawing.Size(65, 29);
            this.button_ClearForm.TabIndex = 40;
            this.button_ClearForm.Text = "Clear Form";
            this.button_ClearForm.UseVisualStyleBackColor = true;
            this.button_ClearForm.Click += new System.EventHandler(this.button_ClearForm_Click);
            // 
            // EditTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 303);
            this.Controls.Add(this.groupBox_Edit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EditTask";
            this.groupBox_Edit.ResumeLayout(false);
            this.groupBox_Edit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TextBox textBox_TaskTitle;
        public System.Windows.Forms.Label label_TaskTitle;
        public System.Windows.Forms.Button button_AddTask;
        public System.Windows.Forms.Label label_TaskDescription;
        public System.Windows.Forms.TextBox textBox_TaskDescription;
        public System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Button button_ClearDescription;
        private System.Windows.Forms.Button button_ClearTitle;
        public System.Windows.Forms.GroupBox groupBox_Edit;
        private System.Windows.Forms.Button button_ClearForm;

    }
}