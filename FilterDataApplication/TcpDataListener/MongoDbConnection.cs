using FilterDataApplication.DataBaseDataFilter;
using FilterDataApplication.DecodedFrameModel;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDataApplication.TcpDataListener
{
    /// <summary>
    /// Class For Connecting The Mongodb 
    /// </summary>
    public class MongoDbConnection
    {
        public MongoClient MongoClient { get; set; }
        public IMongoDatabase ClientDataBase { get; set; }
        public IMongoCollection<DecodedFrameDto> ClientMongoCollection { get; set; }
        public MongoDbConnection(string dataBaseName, string collectionName)
        {
            string MongoClientConnectionString = "mongodb://127.0.0.1:27017";
            this.MongoClient = new MongoClient(MongoClientConnectionString);
            this.ClientDataBase = this.MongoClient.GetDatabase(dataBaseName);
            this.ClientMongoCollection = this.ClientDataBase.GetCollection<DecodedFrameDto>(collectionName);
        }
        private static MongoDbConnection _instance;
        //singleton
        public static MongoDbConnection GetInstance(string dataBaseName, string collectionName)
        {
            if (_instance == null)
            {
                _instance = new MongoDbConnection(dataBaseName, collectionName);

            }
            return _instance;
        }
    }
}
