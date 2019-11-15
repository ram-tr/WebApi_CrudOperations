using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentDb.Model;
using StudentDb.DataAccess;
using StudentDb.BusinessLogic;

namespace StudentDb.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/[controller]/[action]")]
    [ApiController]
    public class studentController : ControllerBase
    {
        // GET: api/student
        [HttpGet]
        public ActionResult<List<Student>> Get()
        {
            StudentProcess process = new StudentProcess();
            var result = process.GetStudents();
            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No Records Found");
            }


        }
        // GET: api/student/5
        [HttpGet("{id:int}")]
        public ActionResult<Student> GetById(int id)
        {
            StudentProcess studentProcess = new StudentProcess();
            var result = studentProcess.getById(id);
            if (result.RollNumber == id && result.IsActive == true)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No Records found with Given ID");
            }

        }
        [HttpGet("{name}")]
        public ActionResult<Student> GetByName(string name)
        {
            StudentProcess studentProcess = new StudentProcess();
            var result = studentProcess.GetDataByNmae(name);
            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No Records found with given Name");
            }

        }
        // PUT: api/student/5
        [HttpPut]
        public ActionResult Put([FromBody] List<Student> student)
        {
            StudentProcess process = new StudentProcess();
            var result = process.update(student);
            if (result == 1)
            {
                return Ok("updated  Successfully");
            }
            else
            {
                return NotFound(" failed to update");
            }


        }
        // POST: api/student
        [HttpPost]
        public ActionResult Post([FromBody] List<Student> student)
        {
            StudentProcess Process = new StudentProcess();
            var result = Process.Create(student);
            if (result == 1)
            {
                return Ok("Insertion is done Successfully");
            }
            else
            {
                return NotFound("Insertion failed ");
            }


        }
        [HttpPost("GetByObject")]
        public ActionResult GetByObject([FromBody] Student student)
        {
            StudentProcess process = new StudentProcess();
            var result = process.GetByObject(student);
            if(result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No records found with given object");
            }
           
        }
        [HttpPost("DeleteMultiple")]
        public ActionResult DeleteMultiple( [FromBody] List<int> list)
        {
            StudentProcess process = new StudentProcess();
            var result = process.DeleteMultiple(list);
            if(result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No records found with given id List");
            }
           

        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            StudentProcess process = new StudentProcess();
            var result = process.delete(id);
            if (result > 0)
            {

                return Ok("Deleted successfully");
            }
            else
            {
                return NotFound("No record with given ID");
            }

        }

    }
}
