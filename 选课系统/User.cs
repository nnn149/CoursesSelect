using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace 选课系统
{
    public class User
    {
        private string userName;//用户名

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string passWord;//密码

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }
        private string type;//类型

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private string name;//姓名

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string school;

        public string School//学校
        {
            get { return school; }
            set { school = value; }
        }
        private string department;

        public string Department//专业
        {
            get { return department; }
            set { department = value; }
        }

        public override string ToString()
        {
            return "userName:" + userName + "\n"+"password:" + passWord + "\n"+"type:" + type + "\n"+"name:" + name + "\n"+"school:" + school + "\n"+"department:" + department + "\n";
        }
    }
}
