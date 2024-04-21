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
using Airhub.files;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Airhub
{
    /// <summary>
    /// Interaction logic for testPage.xaml
    /// </summary>
    public partial class testPage : Window
    {
        String name;
        String pass;
        DbConnection conn = new DbConnection();

        IMongoCollection<BsonDocument> loginCollection;

        public testPage()
        {
            InitializeComponent();
        }
        
        private bool AuthenticateUser(string username, string password)
        {
            

            loginCollection = conn.DB
                .GetCollection<BsonDocument>("login");

            var filter = Builders<BsonDocument>
                .Filter.Eq("username", username) & Builders<BsonDocument>
                .Filter.Eq("password", password);

            var user = loginCollection.Find(filter).FirstOrDefault();
            return user != null;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {   

        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the text when the TextBox gets focus
            if ( sender is TextBox textBox)
            {
                if (textBox.Text == "Username")
                {
                    // Clear the text only if it matches the placeholder
                    textBox.Text = "";
                }
                
            }
        }

        private void loginUser(object sender, RoutedEventArgs e)
        {
            name = username.Text;
            pass = password.Password;

            
            if(AuthenticateUser(name, pass)) {
                MessageBox.Show("Login Successful");
                Window2 nt = new Window2();
                nt.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.");

            }
            

        }
    }
}
