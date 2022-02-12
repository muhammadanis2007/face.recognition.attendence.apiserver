using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace faceRecognitionApi.Models
{
    public class Employees
    {

        
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get;  set; }
                              
        [BsonElement("employeefullname")]
        public string EmployeeFullName { get; set; } = string.Empty;

        [BsonElement("designation")]
        public string EmployeeDesignation { get; set; } = string.Empty;

        [BsonElement("dob")]
        public DateTime  EmployeeDob { get; set; }

        [BsonElement("employeeImage")]
        public string EmployeeImage { get; set; } = string.Empty;

        [BsonElement("employeeEmail")]
        public string EmployeeEmail { get; set; } = string.Empty;

        [BsonElement("employeeCreatedAt")]
        public DateTime EmployeeCreateAt { get; set; } = DateTime.Now;



    }
}
