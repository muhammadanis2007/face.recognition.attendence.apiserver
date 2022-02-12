using System;
using System.Collections.Generic;
using faceRecognitionApi.Models;
using faceRecognitionApi.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace faceRecognitionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {


        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Employees>> Get()
        {
            return _employeeService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetEmployee")]
        public ActionResult<Employees> Get(string id)
        {
            var employee = _employeeService.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public ActionResult<Employees> Create(Employees employee)
        {
            _employeeService.Create(employee);

            return CreatedAtRoute("GetEmployee", new { id = employee.Id.ToString() }, employee);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Employees employeeIn)
        {
            var employee = _employeeService.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            _employeeService.Update(id, employeeIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var employee = _employeeService.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            _employeeService.Remove(employee.Id);

            return NoContent();
        }


    }
}