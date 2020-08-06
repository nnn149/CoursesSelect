using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 选课系统
{
    public class Course
    {
        private string id;//课程编号

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;//课程名称

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string type;//课程性质

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private string allTime;//总学时

        public string AllTime
        {
            get { return allTime; }
            set { allTime = value; }
        }
        private string teachTime;//授课学时

        public string TeachTime
        {
            get { return teachTime; }
            set { teachTime = value; }
        }
        private string studyTime;//实验或上机学时

        public string StudyTime
        {
            get { return studyTime; }
            set { studyTime = value; }
        }
        private double credit;//学分

        public double Credit
        {
            get { return credit; }
            set { credit = value; }
        }
        private string semester;//开学学期

        public string Semester
        {
            get { return semester; }
            set { semester = value; }
        }
        private string mark;//备注

        public string Mark
        {
            get { return mark; }
            set { mark = value; }
        }

        public override string ToString()
        {
            return "name:" + name + "\n" + "id:" + id + "\n" + "type:" + type + "\n" + "alltime:" + allTime + "\n" + "teachtime:" + teachTime + "\n" + "studytime:" + studyTime + "\n" + "credit:" + credit + "\n" + "semester:" + semester + "\n" + "mark:" + mark + "\n";
        }
    }
}
