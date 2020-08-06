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
    /// WindowAddCourse.xaml 的交互逻辑
    /// </summary>
    public partial class WindowAddCourse : Window
    {
        public WindowAddCourse()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //course.xml 初始化
            XmlDocument courseXml = new XmlDocument();
            courseXml.Load("./course.xml");
            XmlElement rootElement = (XmlElement)courseXml.FirstChild;
            XmlNode xmlNode = courseXml.CreateElement("course");
            XmlElement xmlChildElement;
            for (int i = 0; i < 9; i++)
            {
                switch (i)
                {
                    case 0: 
                        {
                            xmlChildElement = courseXml.CreateElement("name");
                            xmlChildElement.InnerText = txtName.Text;
                            xmlNode.AppendChild(xmlChildElement);
                        } break;
                    case 1:
                        {
                            xmlChildElement = courseXml.CreateElement("id");
                            xmlChildElement.InnerText = txtId.Text;
                            xmlNode.AppendChild(xmlChildElement);
                        } break;
                    case 2:
                        {
                            xmlChildElement = courseXml.CreateElement("type");
                            xmlChildElement.InnerText = txtType.Text;
                            xmlNode.AppendChild(xmlChildElement);
                        } break;
                    case 3:
                        {
                            xmlChildElement = courseXml.CreateElement("alltime");
                            xmlChildElement.InnerText = txtAllTime.Text;
                            xmlNode.AppendChild(xmlChildElement);
                        } break;
                    case 4:
                        {
                            xmlChildElement = courseXml.CreateElement("teachtime");
                            xmlChildElement.InnerText = txtTeachTime.Text;
                            xmlNode.AppendChild(xmlChildElement);
                        } break;
                    case 5:
                        {
                            xmlChildElement = courseXml.CreateElement("studytime");
                            xmlChildElement.InnerText = txtStudyTime.Text;
                            xmlNode.AppendChild(xmlChildElement);
                        } break;
                    case 6:
                        {
                            xmlChildElement = courseXml.CreateElement("credit");
                            xmlChildElement.InnerText = txtCredit.Text;
                            xmlNode.AppendChild(xmlChildElement);
                        } break;
                    case 7:
                        {
                            xmlChildElement = courseXml.CreateElement("semester");
                            xmlChildElement.InnerText = txtSemester.Text;
                            xmlNode.AppendChild(xmlChildElement);
                        } break;
                    case 8:
                        {
                            xmlChildElement = courseXml.CreateElement("mark");
                            xmlChildElement.InnerText = txtMark.Text;
                            xmlNode.AppendChild(xmlChildElement);
                        } break;
                }
            }
            rootElement.AppendChild(xmlNode);
            courseXml.Save("./course.xml");
            MessageBox.Show("保存成功", "提示");
        }
        
        private void addCourse(object txt, string name)
        {
            
            
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
