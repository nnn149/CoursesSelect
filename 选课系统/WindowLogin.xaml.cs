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
using System.Windows.Shapes;
using System.Xml;
using System.IO;

namespace 选课系统
{
    
    /// <summary>
    /// WindowLogin.xaml 的交互逻辑
    /// </summary>
    public partial class WindowLogin : Window
    {
        
        
        List<PIN> pinList=new List<PIN>();//验证码列表
        List<User> userList = new List<User>();//用户列表
        public WindowLogin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            initialize();
        }

        private void initialize()
        {
            //验证码初始化
            string imgPate = "./images/PIN";
            DirectoryInfo di = new DirectoryInfo(imgPate);
            FileInfo[] pinFiles = di.GetFiles("*");
            int n=0;
            foreach (var item in pinFiles)
            {
                
                PIN p = new PIN();
                p.Source = new BitmapImage(new Uri("./images/PIN/" + item.Name,UriKind.RelativeOrAbsolute));
                string t=item.Name;
                string name="";
                for (int i = 0; i < 4; i++)
                {
                    name += t[i];
                }
                p.Pin = name;
                p.Id=n;
                p.Name = "imgPIN";
                p.MouseDown += new MouseButtonEventHandler(imgPin_MouseDown);
                p.Height = 27;
                p.Width = 72;
                p.HorizontalAlignment = HorizontalAlignment.Left;
                p.VerticalAlignment = VerticalAlignment.Top;
                p.Margin = new Thickness(5);
                pinList.Add(p);
                n++;

            }
            
            skpPIN.Children.Add(pinList[0]);//添加验证码

            Label labPIN = new Label();
            labPIN.VerticalAlignment = VerticalAlignment.Top;
            labPIN.FontSize = 12;
            labPIN.Margin = new Thickness(1);
            labPIN.Name = "labPin";
            labPIN.Foreground = Brushes.Blue;
            labPIN.Content = "看不清\n换一张";
            labPIN.MouseDown += new MouseButtonEventHandler(labPin_MouseDown);
            skpPIN.Children.Add(labPIN);//添加标签
            

            
            
            
            
            
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
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
                userList.Add(us);
            }


            //PIN p = (PIN)gridMain.FindName("imgPIN");
            // Console.WriteLine(skpPIN.Children.Count);
            //PIN p = (PIN)skpPIN.Children[0];
            //Console.WriteLine(p.Id);
            //foreach (PIN p in skpPIN.Children)
            //{
            //    if (p is PIN)
            //    {
            //        PIN imgPIN = (PIN)p;
            //        Console.WriteLine(imgPIN.Id);
            //    }
            //}
            

            if (rbtnStudent.IsChecked == true)
            {
                login(txtUser.Text, txtPsw.Password, "student");
            }
            else
            {
                login(txtUser.Text, txtPsw.Password, "teacher");
            }
            
            
            
        }
        private bool login(string username, string password, string type)//登录
        {
            PIN p = (PIN)skpPIN.Children[0];
            if (txtPIN.Text != p.Pin.ToString())
            {

                MessageBox.Show("验证码错误!", "提示:", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            User us = userList.Find(delegate(User u) { return u.UserName == username && u.Type == type; });
            if (us == null)
            {
                MessageBox.Show("用户不存在!", "提示:", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtPsw.Password != us.PassWord)
            {
                MessageBox.Show("密码错误!", "提示:", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;

            }
            App.username = username;
            App.usertype = type;
            App.islogin = true;
            MainWindow mainWindow;
            mainWindow=(MainWindow)this.Owner;
            //mainWindow.Visibility = Visibility.Visible; 
            mainWindow.initialize();
            this.Close();
            return true;
            
        }
        private void imgPin_MouseDown(object sender, MouseButtonEventArgs e)//验证码图片点击
        {
            changePIN();
            
        }
        private void changePIN()//改变验证码
        {
            
            PIN p = (PIN)skpPIN.Children[0];
            Console.WriteLine(p.Id);
            skpPIN.Children.Remove(skpPIN.Children[0]);
            
            if (p.Id < pinList.Count-1)
            {
                skpPIN.Children.Insert(0, pinList[p.Id + 1]);
                
            }
            else
            {
                skpPIN.Children.Insert(0, pinList[0]);
            }
        }
        private void labPin_MouseDown(object sender, MouseButtonEventArgs e)//标签点击
        {
            changePIN();
            
        }

        private void btnChangePsw_Click(object sender, RoutedEventArgs e)
        {
            WindowChangePsw windowChangePsw = new WindowChangePsw();
            windowChangePsw.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Application.Current.Shutdown();
        }

  

    }
}
