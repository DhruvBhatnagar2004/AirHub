using Airhub.pages;
using MongoDB.Driver;
using System;
using System.Windows;

namespace Airhub
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            home h = new home();
            h.Show();
            this.Close();
        }

       
    }
}
