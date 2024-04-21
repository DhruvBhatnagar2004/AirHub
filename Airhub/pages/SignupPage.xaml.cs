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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Airhub;
using Airhub.pages;
using Airhub.files;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Airhub.pages
{
    /// <summary>
    /// Interaction logic for SignupPage.xaml
    /// </summary>
    public partial class SignupPage : UserControl
    {
        String name;
        String userName;
        String checkPass;
        String pass;
        DbConnection conn = new DbConnection();
        IMongoCollection<BsonDocument> loginCollection;
        public SignupPage()
        {
            InitializeComponent();
        }

        private Boolean insertUser(String name, String userName, String password) {
            var doc = new BsonDocument
            {
                { "name" , name },
                { "username", userName },
                { "password", password }
            };
            loginCollection = conn.DB.GetCollection<BsonDocument>("login");

            loginCollection.InsertOne(doc);

            return true;
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the text when the TextBox gets focus
            if (sender is TextBox textBox)
            {
                if (textBox.Text == "Username" || textBox.Text == "Name" || textBox.Text == "Password")
                {
                    // Clear the text only if it matches the placeholder
                    textBox.Text = "";
                }

            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void signUser(object sender, RoutedEventArgs e)
        {
            name = Name.Text; userName = Username.Text; checkPass = accPass.Text; pass = password.Password;

            if(checkPass != pass.ToString() || pass.Length < 8)
            {
                MessageBox.Show("Password does not match");
            }
            else
            {
                if(insertUser(
                    name, userName, pass
                    ))
                {
                    MessageBox.Show("succefully created user");
                    Window parentWindow = Window.GetWindow(this);

                    Window2 nt = new Window2();
                    nt.Show();
                    parentWindow.Close();
                }
                else
                {
                    MessageBox.Show("error");
                }
            }





        }
    }
}
