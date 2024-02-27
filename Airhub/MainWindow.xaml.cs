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


        const string password = "airhub123";
        string connectionUri = String.Format("mongodb+srv://Airhub:{0}@airhub.lfn0kyn.mongodb.net/?retryWrites=true&w=majority", password);
        IMongoDatabase database;
        string dbName = "userdb";

        public Window1()
        {
            InitializeComponent();

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
            Window2 nw = new Window2();
            nw.Show();  

        }


    }
}
