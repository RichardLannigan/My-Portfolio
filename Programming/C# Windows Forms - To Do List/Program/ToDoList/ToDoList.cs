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
    public partial class ToDoList : Form
    {
        int AddNewTask;
        string AddTask = "Add Task";
        string EditTask = "Edit Task";

        DateTime Today;
        DateTime Deadline;
        DateTime TimeLimit;

        public ToDoList()
        {
            InitializeComponent();
            this.Size = new Size(682, 594);
            groupBox_Task1.Visible = false;
            groupBox_Task2.Visible = false;
            groupBox_Task3.Visible = false;
            groupBox_Task4.Visible = false;
            groupBox_Task5.Visible = false;

            label_TaskTitle1.Text = "Title";
            label_TaskTitle2.Text = "Title";
            label_TaskTitle3.Text = "Title";
            label_TaskTitle4.Text = "Title";
            label_TaskTitle5.Text = "Title";

            textBox_TaskDescription1.Text = "Description";
            textBox_TaskDescription2.Text = "Description";
            textBox_TaskDescription3.Text = "Description";
            textBox_TaskDescription4.Text = "Description";
            textBox_TaskDescription5.Text = "Description";
        }

        private void newTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTask ET = new EditTask();
            ET.ShowDialog();

            AddNewTask++;

            if (AddNewTask == 1)
            {
                Task T1 = new Task();
                label_TaskTitle1.Text = T1.Title;
                textBox_TaskDescription1.Text = T1.Description;
                dateTimePicker1.Text = T1.Deadline;

                groupBox_Task1.Visible = true;
                label_TaskTitle1.Text = ET.textBox_TaskTitle.Text;
                textBox_TaskDescription1.Text = ET.textBox_TaskDescription.Text;
                dateTimePicker1.Text = ET.dateTimePicker.Text;
            }

            if (AddNewTask == 2)
            {
                Task T2 = new Task();
                label_TaskTitle2.Text = T2.Title;
                textBox_TaskDescription2.Text = T2.Description;
                dateTimePicker2.Text = T2.Deadline;

                groupBox_Task2.Visible = true;
                label_TaskTitle2.Text = ET.textBox_TaskTitle.Text;
                textBox_TaskDescription2.Text = ET.textBox_TaskDescription.Text;
                dateTimePicker2.Text = ET.dateTimePicker.Text;
            }
            if (AddNewTask == 3)
            {
                Task T3 = new Task();
                label_TaskTitle3.Text = T3.Title;
                textBox_TaskDescription3.Text = T3.Description;
                dateTimePicker3.Text = T3.Deadline;

                groupBox_Task3.Visible = true;
                label_TaskTitle3.Text = ET.textBox_TaskTitle.Text;
                textBox_TaskDescription3.Text = ET.textBox_TaskDescription.Text;
                dateTimePicker3.Text = ET.dateTimePicker.Text;

            }
            if (AddNewTask == 4)
            {
                Task T4 = new Task();
                label_TaskTitle4.Text = T4.Title;
                textBox_TaskDescription4.Text = T4.Description;
                dateTimePicker4.Text = T4.Deadline;

                groupBox_Task4.Visible = true;
                label_TaskTitle4.Text = ET.textBox_TaskTitle.Text;
                textBox_TaskDescription4.Text = ET.textBox_TaskDescription.Text;
                dateTimePicker4.Text = ET.dateTimePicker.Text;
            }
            if (AddNewTask == 5)
            {
                Task T5 = new Task();
                label_TaskTitle5.Text = T5.Title;
                textBox_TaskDescription5.Text = T5.Description;
                dateTimePicker5.Text = T5.Deadline;

                groupBox_Task5.Visible = true;
                label_TaskTitle5.Text = ET.textBox_TaskTitle.Text;
                textBox_TaskDescription5.Text = ET.textBox_TaskDescription.Text;
                dateTimePicker5.Text = ET.dateTimePicker.Text;
            }
            if (AddNewTask == 6)
            {
                AddNewTask--;
            }
        }

        private void button_Task1Edit_Click(object sender, EventArgs e)
        {
            EditTask EF = new EditTask();
            EF.ShowDialog();
            label_TaskTitle1.Text = EF.textBox_TaskTitle.Text;
            textBox_TaskDescription1.Text = EF.textBox_TaskDescription.Text;
            dateTimePicker1.Text = EF.dateTimePicker.Text;
        }
        private void button_Task2Edit_Click(object sender, EventArgs e)
        {
            EditTask EF = new EditTask();
            EF.ShowDialog();
            label_TaskTitle2.Text = EF.textBox_TaskTitle.Text;
            textBox_TaskDescription2.Text = EF.textBox_TaskDescription.Text;
            dateTimePicker2.Text = EF.dateTimePicker.Text;
        }
        private void button_Task3Edit_Click(object sender, EventArgs e)
        {
            EditTask EF = new EditTask();
            EF.ShowDialog();
            label_TaskTitle3.Text = EF.textBox_TaskTitle.Text;
            textBox_TaskDescription3.Text = EF.textBox_TaskDescription.Text;
            dateTimePicker3.Text = EF.dateTimePicker.Text;
        }
        private void button_Task4Edit_Click(object sender, EventArgs e)
        {
            EditTask EF = new EditTask();
            EF.ShowDialog();
            label_TaskTitle4.Text = EF.textBox_TaskTitle.Text;
            textBox_TaskDescription4.Text = EF.textBox_TaskDescription.Text;
            dateTimePicker4.Text = EF.dateTimePicker.Text;
        }
        private void button_Task5Edit_Click(object sender, EventArgs e)
        {
            EditTask EF = new EditTask();
            EF.ShowDialog();
            label_TaskTitle5.Text = EF.textBox_TaskTitle.Text;
            textBox_TaskDescription5.Text = EF.textBox_TaskDescription.Text;
            dateTimePicker5.Text = EF.dateTimePicker.Text;
        }

        private void button_Task1Delete_Click(object sender, EventArgs e)
        {
            label_TaskTitle1.Text = label_TaskTitle2.Text;
            textBox_TaskDescription1.Text = textBox_TaskDescription2.Text;
            label_TaskTitle2.Text = label_TaskTitle3.Text;
            textBox_TaskDescription2.Text = textBox_TaskDescription3.Text;
            label_TaskTitle3.Text = label_TaskTitle4.Text;
            textBox_TaskDescription3.Text = textBox_TaskDescription4.Text;
            label_TaskTitle4.Text = label_TaskTitle5.Text;
            textBox_TaskDescription4.Text = textBox_TaskDescription5.Text;
            if (AddNewTask == 1)
            {
                groupBox_Task1.Visible = false;
            }
            if (AddNewTask == 2)
            {
                groupBox_Task2.Visible = false;
            }
            if (AddNewTask == 3)
            {
                groupBox_Task3.Visible = false;
            }
            if (AddNewTask == 4)
            {
                groupBox_Task4.Visible = false;
            }
            if (AddNewTask == 5)
            {
                groupBox_Task5.Visible = false;
            }
            AddNewTask--;
        }
        private void button_Task2Delete_Click(object sender, EventArgs e)
        {
            label_TaskTitle2.Text = label_TaskTitle3.Text;
            textBox_TaskDescription2.Text = textBox_TaskDescription3.Text;
            label_TaskTitle3.Text = label_TaskTitle4.Text;
            textBox_TaskDescription3.Text = textBox_TaskDescription4.Text;
            label_TaskTitle4.Text = label_TaskTitle5.Text;
            textBox_TaskDescription4.Text = textBox_TaskDescription5.Text;
            if (AddNewTask == 1)
            {
                groupBox_Task1.Visible = false;
            }
            if (AddNewTask == 2)
            {
                groupBox_Task2.Visible = false;
            }
            if (AddNewTask == 3)
            {
                groupBox_Task3.Visible = false;
            }
            if (AddNewTask == 4)
            {
                groupBox_Task4.Visible = false;
            }
            if (AddNewTask == 5)
            {
                groupBox_Task5.Visible = false;
            }
            AddNewTask--;
        }
        private void button_Task3Delete_Click(object sender, EventArgs e)
        {
            label_TaskTitle3.Text = label_TaskTitle4.Text;
            textBox_TaskDescription3.Text = textBox_TaskDescription4.Text;
            label_TaskTitle4.Text = label_TaskTitle5.Text;
            textBox_TaskDescription4.Text = textBox_TaskDescription5.Text;
            if (AddNewTask == 1)
            {
                groupBox_Task1.Visible = false;
            }
            if (AddNewTask == 2)
            {
                groupBox_Task2.Visible = false;
            }
            if (AddNewTask == 3)
            {
                groupBox_Task3.Visible = false;
            }
            if (AddNewTask == 4)
            {
                groupBox_Task4.Visible = false;
            }
            if (AddNewTask == 5)
            {
                groupBox_Task5.Visible = false;
            }
            AddNewTask--;
        }
        private void button_Task4Delete_Click(object sender, EventArgs e)
        {
            label_TaskTitle4.Text = label_TaskTitle5.Text;
            textBox_TaskDescription4.Text = textBox_TaskDescription5.Text;
            if (AddNewTask == 1)
            {
                groupBox_Task1.Visible = false;
            }
            if (AddNewTask == 2)
            {
                groupBox_Task2.Visible = false;
            }
            if (AddNewTask == 3)
            {
                groupBox_Task3.Visible = false;
            }
            if (AddNewTask == 4)
            {
                groupBox_Task4.Visible = false;
            }
            if (AddNewTask == 5)
            {
                groupBox_Task5.Visible = false;
            }
            AddNewTask--;
        }
        private void button_Task5Delete_Click(object sender, EventArgs e)
        {
            if (AddNewTask == 1)
            {
                groupBox_Task1.Visible = false;
            }
            if (AddNewTask == 2)
            {
                groupBox_Task2.Visible = false;
            }
            if (AddNewTask == 3)
            {
                groupBox_Task3.Visible = false;
            }
            if (AddNewTask == 4)
            {
                groupBox_Task4.Visible = false;
            }
            if (AddNewTask == 5)
            {
                groupBox_Task5.Visible = false;
            }
            AddNewTask--;
        }
    }
}
