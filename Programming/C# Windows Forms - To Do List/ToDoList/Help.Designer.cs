namespace ToDoList
{
    partial class Help
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Help));
            this.textBox_Help = new System.Windows.Forms.TextBox();
            this.button_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_Help
            // 
            this.textBox_Help.Location = new System.Drawing.Point(12, 12);
            this.textBox_Help.Multiline = true;
            this.textBox_Help.Name = "textBox_Help";
            this.textBox_Help.Size = new System.Drawing.Size(455, 248);
            this.textBox_Help.TabIndex = 0;
            this.textBox_Help.Text = resources.GetString("textBox_Help.Text");
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(353, 267);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(112, 40);
            this.button_Close.TabIndex = 1;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 319);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.textBox_Help);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Help";
            this.Text = "Help";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Help;
        private System.Windows.Forms.Button button_Close;
    }
}