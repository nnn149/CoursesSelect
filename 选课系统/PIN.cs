using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
namespace 选课系统
{
    class PIN:Image
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string pin;

        public string Pin
        {
            get { return pin; }
            set { pin = value; }
        } 
    }
}
