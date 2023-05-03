using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db.Models
{
    public class PersonModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Name { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}