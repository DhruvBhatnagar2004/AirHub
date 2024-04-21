using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airhub.files
{
    internal class DbConnection
    {
        IMongoDatabase database;
        string dbName = "userdb";
        const string passwrd = "airhub123";
        string connectionUri = ($"mongodb+srv://Airhub:{passwrd}@airhub.lfn0kyn.mongodb.net/?retryWrites=true&w=majority");
        public DbConnection() {
            SetupMongoDB();
        }

        private void SetupMongoDB()
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

        }

        public IMongoDatabase DB { 
            
            get { return database; }
        }

    }
}
