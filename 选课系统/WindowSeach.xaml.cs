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

namespace 选课系统
{
    /// <summary>
    /// WindowSeach.xaml 的交互逻辑
    /// </summary>
    public partial class WindowSeach : Window
    {
        List<Course> courselist;
        public WindowSeach(List<Course> courselist)
        {
            InitializeComponent();
            this.courselist = courselist;
        }

        private void btnSeach_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m ;
            m = (MainWindow)this.Owner;
            int n;
            if (cmbSeach.SelectedIndex == 1)
            {
                n = courselist.FindIndex(delegate(Course c) { return c.Name == txtSeach.Text; });
                
            }
            else
            {
                n = courselist.FindIndex(delegate(Course c) { return c.Id == txtSeach.Text; });
 
            }
            Console.Write(n);
            if (n == -1)
            {
                MessageBox.Show("找不到你查询的课程", "提示:", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            m.selectList(n);
            this.Close();

        }
    }
}
