using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Publication.Rabbit.Subscription.Storage.Models;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db.Models
{
    public class PersonModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}