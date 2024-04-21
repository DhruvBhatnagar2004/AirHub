using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Airhub.pages
{
    /// <summary>
    /// Interaction logic for home.xaml
    /// </summary>
    public partial class home : Window
    {
        public home()
        {
            InitializeComponent();
        }
        private void Login_click(object sender, RoutedEventArgs e)
        {
            loginView.Visibility = Visibility.Visible;
            

        }
       

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            signupView.Visibility = Visibility.Visible;
        }
    }
}
