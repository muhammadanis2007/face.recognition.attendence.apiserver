using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using faceRecognitionApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace faceRecognitionApi.Services
{
    public class EmployeeService
    {

        private readonly IMongoCollection<Employees> _employees;

        public EmployeeService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("faceRecognitionDb"));
            var database = client.GetDatabase("facerecognitionapp");
            _employees = database.GetCollection<Employees>("employees");
        }

        public List<Employees> Get()
        {
            return _employees.Find(employee => true).ToList();
        }

        public Employees Get(string id)
        {
            return _employees.Find<Employees>(employee => employee.Id == id).FirstOrDefault();
        }

        public Employees Create(Employees employees)
        {
            _employees.InsertOne(employees);
            return employees;
        }

        public void Update(string id, Employees employeeIn)
        {
            _employees.ReplaceOne(employee => employee.Id == id, employeeIn);
        }

        public void Remove(Employees employeeIn)
        {
            _employees.DeleteOne(employee => employee.Id == employeeIn.Id);
        }

        public void Remove(string id)
        {
            _employees.DeleteOne(employee => employee.Id == id);
        }

    }
}
