using Airhub.files;
using Airhub.pages;
using System.Windows;

namespace Airhub
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void StartBotClick(object sender, RoutedEventArgs e)
        {
            Bot b = new Bot();
            b.Show();
            this.Close();

        }
    }
}
