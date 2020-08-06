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

namespace 选课系统
{
    /// <summary>
    /// WindowChangePsw.xaml 的交互逻辑
    /// </summary>
    public partial class WindowChangePsw : Window
    {
        List<User> userList = new List<User>();//用户列表
        XmlDocument userXml = new XmlDocument();
        public WindowChangePsw()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            userXml.Load("./user.xml");

            //Console.WriteLine(rootNode.ChildNodes[0].Name);
            //foreach (XmlNode childNode in rootNode)
            //{
            //    User us = new User();
            //    us.UserName = childNode.ChildNodes[0].InnerText;
            //    us.PassWord = childNode.ChildNodes[1].InnerText;
            //    us.Type = childNode.ChildNodes[2].InnerText;
            //    userList.Add(us);
            //}
        }
        private void changePsw(string username,string odlpsw,string type,string newpsw)
        {
            XmlNode rootNode = userXml.SelectSingleNode("user");
            //XmlNodeList childNodeList = rootNode.ChildNodes;
            int i=0;
            foreach (XmlNode childNode in rootNode)
            {

                if(username==childNode.ChildNodes[0].InnerText)
                {
                    if(type==childNode.ChildNodes[2].InnerText){


                        if (odlpsw == childNode.ChildNodes[1].InnerText)
                        {
                            childNode.ChildNodes[1].InnerText = newpsw;
                            userXml.Save("./user.xml");
                            MessageBox.Show("修改成功，将重启程序！","提示:",MessageBoxButton.OK,MessageBoxImage.Information);
                            Application.Current.Shutdown();
                        }
                        else
                        {
                            MessageBox.Show("密码错误!", "提示:", MessageBoxButton.OK, MessageBoxImage.Error);
                            return ;
                        }

                    }
                }
                if(i==rootNode.ChildNodes.Count-1)
                {
                    MessageBox.Show("用户不存在!", "提示:", MessageBoxButton.OK, MessageBoxImage.Error);
                    return ;
                }
                i++;
                
                
            }

        }
        private void btnChangePsw_Click(object sender, RoutedEventArgs e)
        {
            //User us;
            if (rbtnStudent.IsChecked == true)
            {
                changePsw(txtUser.Text, txtOldPws.Password, "student", txtNewPsw.Password);
                //us = userList.Find(delegate(User u) { return u.UserName == txtUser.Text && u.Type == "student"; });
            }
            else
            {
                changePsw(txtUser.Text, txtOldPws.Password, "teacher", txtNewPsw.Password);
                //us = userList.Find(delegate(User u) { return u.UserName == txtUser.Text && u.Type == "teacher"; });
            }
            
            
            
            //if (us == null)
            //{
            //    MessageBox.Show("用户不存在!", "提示:", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            //if (txtOldPws.Text != us.PassWord)
            //{
            //    MessageBox.Show("密码错误!", "提示:", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;

            //}
        }
    }
}
