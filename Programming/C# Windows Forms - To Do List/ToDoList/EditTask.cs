using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class EditTask : Form
    {
        public EditTask()
        {
            InitializeComponent();
            textBox_TaskTitle.MaxLength = 16;
            textBox_TaskDescription.MaxLength = 256;
        }

        private void _button_AddTask_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_ClearTitle_Click(object sender, EventArgs e)
        {
            textBox_TaskTitle.Text = "";
        }

        private void button_ClearDescription_Click(object sender, EventArgs e)
        {
            textBox_TaskDescription.Text = "";
        }

        private void button_ClearForm_Click(object sender, EventArgs e)
        {
            textBox_TaskTitle.Text = "";
            textBox_TaskDescription.Text = "";
        }
    }
}
