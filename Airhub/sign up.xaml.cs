using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

namespace Airhub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        const string password = "airhub123";
        string connectionUri = String.Format("mongodb+srv://Airhub:{0}@airhub.lfn0kyn.mongodb.net/?retryWrites=true&w=majority", password);
        IMongoDatabase database;
        string dbName = "userdb";
        public MainWindow()
        {
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(settings);
       

            try
            {
                database = client.GetDatabase(dbName);
                Console.Write("\n\nconnection successful\n\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ---------------------\n", ex);
            }
            InitializeComponent();

            

        }

        private void login(object sender, RoutedEventArgs e)
        {
           MainWindow mw = new MainWindow();

            this.Visibility = Visibility.Hidden();
            mw.show();

        }

        
    }
}