using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Windows.Threading;
using System.IO;

namespace 选课系统
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> userList = new List<User>();//用户列表
        public List<Course> courseList = new List<Course>();//课程列表
        List<Course> userCourseList = new List<Course>();//user课程列表
        User userMain = new User();
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            
            InitializeComponent();
            App.islogin = false;

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            Login();
            
        }
        void timer_Tick(object sender, EventArgs e)
        {
            labTime.Content =DateTime.Now.ToLongDateString()+ DateTime.Now.ToLongTimeString();
        }

        public void initialize()//登陆成功,初始化
        {
            
            userXmlInit();
            rbtnList.IsChecked = true;
            userMain = findUser(App.username, App.usertype);

            labUser.Content = App.username;
            labType.Content = App.usertype;
            labName.Content = userMain.Name;
            labSchool.Content = userMain.School;
            labDepartment.Content = userMain.Department;
            if (App.islogin == true)
            {
                labIsLogin.Content = "已登入";
            }
            if (App.usertype == "teacher")
            { teacherInit(); }
            if (App.usertype == "student")
            { studentInit(); }
        }

        private void teacherInit()
        {
            rbtnList.Visibility = Visibility.Visible;
            rbtnList.IsChecked = true;
            btnDelete.Visibility = Visibility.Visible;

        }
        private void studentInit()
        {
            rbtnList.Visibility = Visibility.Visible;
            rbtnSelect.Visibility = Visibility.Visible;
            rbtnList.IsChecked = true;
            btnRef.Visibility = Visibility.Visible;
        }
        private void userXmlInit()
        {
            //user.xml 初始化

            XmlDocument userXml = new XmlDocument();
            userXml.Load("./user.xml");
            XmlNode rootNode = userXml.SelectSingleNode("user");
            //Console.WriteLine(rootNode.ChildNodes[0].Name);
            foreach (XmlNode childNode in rootNode)
            {
                User us = new User();
                us.UserName = childNode.ChildNodes[0].InnerText;
                us.PassWord = childNode.ChildNodes[1].InnerText;
                us.Type = childNode.ChildNodes[2].InnerText;
                us.Name = childNode.ChildNodes[3].InnerText;
                us.School = childNode.ChildNodes[4].InnerText;
                us.Department = childNode.ChildNodes[5].InnerText;
                userList.Add(us);
            }
        }

        private void courseXmlInit()
        {
            //course.xml 初始化
            listCourse.ItemsSource = null;
            courseList.Clear();
            XmlDocument courseXml = new XmlDocument();
            courseXml.Load("./course.xml");
            //MessageBox.Show("");
            XmlNode rootNode = courseXml.SelectSingleNode("courses");
            //Console.WriteLine(rootNode.ChildNodes[0].Name);
            foreach (XmlNode childNode in rootNode)
            {
                Course course = new Course();
                course.Name = childNode.ChildNodes[0].InnerText;
                course.Id = childNode.ChildNodes[1].InnerText;
                course.Type = childNode.ChildNodes[2].InnerText;
                course.AllTime = childNode.ChildNodes[3].InnerText;
                course.TeachTime = childNode.ChildNodes[4].InnerText;
                course.StudyTime = childNode.ChildNodes[5].InnerText;
                course.Credit =double.Parse(childNode.ChildNodes[6].InnerText);
                course.Semester = childNode.ChildNodes[7].InnerText;
                course.Mark = childNode.ChildNodes[8].InnerText;
                courseList.Add(course);
            }
            listCourse.ItemsSource = courseList;
        }
        private User findUser(string username, string type)
        {
            return userList.Find(delegate(User u) { return u.UserName == username && u.Type == type; });
 
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void 测试_Click(object sender, RoutedEventArgs e)
        {
            Console.Write(userMain.ToString());
            Console.WriteLine(courseList[0].ToString());
            Console.WriteLine(courseList[1].ToString());
        }
        private void Login()
        {
            if (App.islogin == true)
            {
                MessageBox.Show("你已经登录!", "提示:", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            WindowLogin windowLogin = new WindowLogin();
            windowLogin.Owner = this;
            windowLogin.ShowDialog();
        }

        private void btnChangePsw_Click(object sender, RoutedEventArgs e)
        {
            WindowChangePsw windowChangePsw = new WindowChangePsw();
            windowChangePsw.ShowDialog();
        }

        private void mitemChangePsw_Click(object sender, RoutedEventArgs e)
        {
            WindowChangePsw windowChangePsw = new WindowChangePsw();
            windowChangePsw.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            courseXmlInit();
        }



        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (App.islogin == false)
            {
                MessageBox.Show("请先登录!", "提示:", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (App.usertype == "student")
            {
                MessageBox.Show("老师才能添加课程!", "提示:", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            WindowAddCourse windowAddCourse = new WindowAddCourse();
            windowAddCourse.Owner = this;
            windowAddCourse.ShowDialog();
        }

        private void btnAllSelect_Click(object sender, RoutedEventArgs e)
        {
            listCourse.SelectAll();
        }

        private void btnRest_Click(object sender, RoutedEventArgs e)
        {
            //if (App.islogin == false)
            //{
            //    MessageBox.Show("请先登录！", "提示：");
            //    return;
            //}
            if (App.islogin == false)
            {
                MessageBox.Show("请先登录!", "提示:", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (rbtnSelect.IsChecked == true)
            {
                listCourse.ItemsSource = null;
                userCourseXmlInit(); 
            }
            if (rbtnList.IsChecked == true)
            {
                listCourse.ItemsSource = null;
                courseXmlInit();
            }
            
            

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void mitemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void listCourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            labCount.Content ="已选课程："+ listCourse.SelectedItems.Count.ToString();
        }

        private void btnRef_Click(object sender, RoutedEventArgs e)
        {
            if (listCourse.SelectedItems.Count == 0)
            { return; }
            int n = listCourse.SelectedItems.Count;
            Course[] selectCourses = new Course[n];
            selectCourses.Initialize();
            for (int i = 0; i < n; i++)
            {
                selectCourses[i] = (Course)listCourse.SelectedItems[i];
            }
            Console.WriteLine(selectCourses.Length);
            refCoures(App.username, selectCourses);
            MessageBox.Show("选课成功!", "提示:", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void refCoures(string username,Course[] savecourse)
        {
            XmlDocument courseXml = new XmlDocument();
            XmlElement rootElement = courseXml.CreateElement("courses");
            
            courseXml.AppendChild(rootElement);
            //子节点
            XmlElement childElement;
            foreach (Course c in savecourse)
            {
                XmlNode xmlNode = courseXml.CreateElement("course");

                childElement = courseXml.CreateElement("name");
                childElement.InnerText = c.Name;
                xmlNode.AppendChild(childElement);

                childElement = courseXml.CreateElement("id");
                childElement.InnerText = c.Id;
                xmlNode.AppendChild(childElement);

                childElement = courseXml.CreateElement("type");
                childElement.InnerText = c.Type;
                xmlNode.AppendChild(childElement);

                childElement = courseXml.CreateElement("alltime");
                childElement.InnerText = c.AllTime;
                xmlNode.AppendChild(childElement);

                childElement = courseXml.CreateElement("teachtime");
                childElement.InnerText = c.TeachTime;
                xmlNode.AppendChild(childElement);

                childElement = courseXml.CreateElement("studytime");
                childElement.InnerText = c.StudyTime;
                xmlNode.AppendChild(childElement);

                childElement = courseXml.CreateElement("credit");
                childElement.InnerText = c.Credit.ToString();
                xmlNode.AppendChild(childElement);

                childElement = courseXml.CreateElement("semester");
                childElement.InnerText = c.Semester;
                xmlNode.AppendChild(childElement);

                childElement = courseXml.CreateElement("mark");
                childElement.InnerText = c.Mark;
                xmlNode.AppendChild(childElement);

                rootElement.AppendChild(xmlNode);
            }

            courseXml.Save("./course/" + username+ ".xml");

        }

        private void btnSeach_Click(object sender, RoutedEventArgs e)
        {
            if (App.islogin == false)
            {
                MessageBox.Show("请先登录!", "提示:", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            rbtnList.IsChecked = true;
            WindowSeach windowSeach = new WindowSeach(courseList);
            windowSeach.Owner = this;
            windowSeach.ShowDialog();
        }

        public void selectList(int index)
        {
            listCourse.Focus();
            listCourse.SelectedIndex = index;
            

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            

            int n= listCourse.SelectedItems.Count;
            int[] index=new int[n];
            int i = 0;
            
            foreach(Course c in listCourse.SelectedItems)
            {
                index[i] = listCourse.SelectedIndex;
                index[i]= listCourse.Items.IndexOf(c);
                i++;
            }
            for (int x = 0; x < index.Length-1; x++)
            {
                for (int y = 0; y < index.Length - x - 1; y++)
                {
                    if (index[y] < index[y + 1])
                    {
                        int temp = index[y];
                        index[y] = index[y+1];
                        index[y+1] = temp;
                    }
                }
            }

            deleteCourse(index);
            courseXmlInit();
        }
        private void deleteCourse(int[] index)
        {

            XmlDocument xml = new XmlDocument();
            xml.Load("./course.xml");
            XmlNode rootNode = xml.SelectSingleNode("courses");
            XmlNodeList childNods = rootNode.ChildNodes;
            Console.WriteLine(rootNode.ChildNodes.Count);
            for (int i = 0; i < index.Length; i++)
            {
                //rootNode.ChildNodes[index[i]].RemoveAll();
                Console.WriteLine(index[i]);
                rootNode.RemoveChild(rootNode.ChildNodes[index[i]]);
            }
            xml.Save("./course.xml");

            //foreach (XmlNode childNod in childNods)
            //{ 
            //}

 
        }

        private void rbtnSelect_Checked(object sender, RoutedEventArgs e)
        {
            
            if (rbtnSelect.IsChecked == true)
            {
                userCourseXmlInit();
 
            }
        }
        private void rbtnList_Checked(object sender, RoutedEventArgs e)
        {
            
            if (rbtnList.IsChecked == true)
            {
                courseXmlInit();

            }
        }

        private void userCourseXmlInit()
        {
            //user course.xml 初始化
            if (File.Exists("./course/" + App.username + ".xml") == false)
            {
                Console.Write("./course/" + App.username + ".xml");
                MessageBox.Show("你还没有选取课程!", "提示:", MessageBoxButton.OK, MessageBoxImage.Information);
                rbtnList.IsChecked = true;
                return;
            }
            
            listCourse.ItemsSource = null;
            userCourseList.Clear();
            XmlDocument courseXml = new XmlDocument();
            courseXml.Load("./course/"+App.username+".xml");
            //MessageBox.Show("");
            XmlNode rootNode = courseXml.SelectSingleNode("courses");
            //Console.WriteLine(rootNode.ChildNodes[0].Name);
            foreach (XmlNode childNode in rootNode)
            {
                Course course = new Course();
                course.Name = childNode.ChildNodes[0].InnerText;
                course.Id = childNode.ChildNodes[1].InnerText;
                course.Type = childNode.ChildNodes[2].InnerText;
                course.AllTime = childNode.ChildNodes[3].InnerText;
                course.TeachTime = childNode.ChildNodes[4].InnerText;
                course.StudyTime = childNode.ChildNodes[5].InnerText;
                course.Credit = double.Parse(childNode.ChildNodes[6].InnerText);
                course.Semester = childNode.ChildNodes[7].InnerText;
                course.Mark = childNode.ChildNodes[8].InnerText;
                userCourseList.Add(course);
            }
            listCourse.ItemsSource = userCourseList;
        }





    }
}
