using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class ContactRepository: IContactRepository
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Contact> _contacts;

        public ContactRepository()
            : this("")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public ContactRepository(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://localhost:27017";
            }

            _client = new MongoClient(connection);
            _server = _client.GetServer();
            _database = _server.GetDatabase("Contacts");
            _contacts = _database.GetCollection<Contact>("contacts");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Contact> AllContacts()
        {
            return _contacts.FindAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Contact GetById(ObjectId id)
        {
            IMongoQuery query = Query.EQ("_id", id);
            return _contacts.Find(query).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Contact Add(Contact entity)
        {
            entity.Id = ObjectId.GenerateNewId().ToString();
            _contacts.Insert(entity);
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Contact entity)
        {
            IMongoQuery query = Query.EQ("_id", ObjectId.Parse(entity.Id));

            IMongoUpdate update = MongoDB.Driver.Builders.Update
                .Set("firstName", entity.FirstName)
                .Set("lastName", entity.LastName)
                .Set("email", entity.Email)
                .Set("phone", entity.Phone);
            WriteConcernResult result = _contacts.Update(query, update);
            return result.UpdatedExisting;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            IMongoQuery query = Query.EQ("_id", ObjectId.Parse(id));
            WriteConcernResult result = _contacts.Remove(query);
            return result.DocumentsAffected == 1;
        }
    }
}