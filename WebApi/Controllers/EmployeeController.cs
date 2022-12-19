using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // // GET api/<controller>
        public IHttpActionResult GetAll()
        {
            var items = _employeeService.GetAllEmployees();
            if (items == null)
                throw new Exception("Record not found");
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(items));
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var item = _employeeService.GetEmployeeByCode(id.ToString());
            if (item == null)
                throw new Exception("Record not found");
            return Ok(_mapper.Map<EmployeeDto>(item));
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
                return BadRequest("Missing argument");

            return Ok(_employeeService.InsertEmployee(_mapper.Map<EmployeeInfo>(employeeDto)));
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
                return BadRequest("Missing argument");

            return Ok(_employeeService.UpdateEmployee(id, _mapper.Map<EmployeeInfo>(employeeDto)));
        }
    }
}