using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoList
{
    public class Task
    {
        private string _title, _description;
        private DateTime _deadline;
        private bool _complete, _visible;

        public Task() //Sets the private variables to values that are going to be used a task defaults.
        {
            _title = "(Title)";
            _description = "(Description)";
            _deadline = DateTime.Now.Date;
            _complete = false;
            _visible = false;
        }

        public string Title //Allows the title string to be retrived and used to set the task's title in the "ToDoList", to the default text.
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public string Description //Allows the description string to be retrived and used to set the task's description in the "ToDoList", to the default text.
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public DateTime Deadline //Allows the deadline string to be retrived and used to set the task's deadline in the "ToDoList", to the default date.
        {
            get
            {
                return _deadline;
            }
            set
            {
                _deadline = value;
            }
        }

        public bool Complete //Allows the complete bool to be retrived and used to set the task's complete bool in the "ToDoList", to the default value.
        {
            get
            {
                return _complete;
            }
            set
            {
                _complete = value;
            }
        }

        public bool Visible //Allows the visible bool to be retrived and used to set the task's visible bool in the "ToDoList", to the default value.
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }
    }
}
