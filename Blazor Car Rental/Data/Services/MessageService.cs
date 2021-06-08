using Blazor_Car_Rental.Data.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_Car_Rental.Data.Services
{
    public class MessageService
    {
        private MongoClient _mongClient = null;
        private IMongoDatabase _database = null;
        private IMongoCollection<Message> _messageTable = null;

        public MessageService()
        {
            _mongClient = new MongoClient("mongodb://127.0.0.1:27017/");
            _database = _mongClient.GetDatabase("BlazorApp");
            _messageTable = _database.GetCollection<Message>("Message");
        }

        public List<Message> GetMessages()
        {
            List<Message> Messages = _messageTable.Find(FilterDefinition<Message>.Empty).ToList();
            return Messages;
        }

        public void DeleteMessage(string id)
        {
             _messageTable.DeleteOneAsync(x=> x.Id == id);
        }

        public void AddMessage(Message message)
        {
            _messageTable.InsertOne(message);
        }
    }
}
