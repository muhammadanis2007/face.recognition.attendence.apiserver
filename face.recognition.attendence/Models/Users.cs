using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace faceRecognitionApi.Models
{
    [BsonIgnoreExtraElements]
    public class Users
    {

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("fullname")]
        public string fullname { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("userImage")]
        public string UserProfileImage { get; set; }

        [BsonElement("userRole")]
        public string UserRole { get; set; }

        [BsonElement("username")]
        public string UserName { get; set; }

        [BsonElement("email")]
        public string UserEmail { get; set; }

        [BsonElement("token")]
        public string Token { get; set; }



    }
}