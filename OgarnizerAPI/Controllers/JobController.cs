using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Models;
using OgarnizerAPI.Services;

namespace OgarnizerAPI.Controllers
{
    [Route("api/ogarnizer/job")]
    [ApiController]
    //[Authorize]
    public class JobController : ControllerBase
    {        
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost]
        public ActionResult CreateJob([FromBody] CreateJobDto dto)
        {            
            var id = _jobService.Create(dto); 

            return Created($"/api/ogarnizer/job/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<JobDto>> GetAll([FromQuery] JobQuery query)
        {
            var jobsDtos = _jobService.GetAll(query);

            return Ok(jobsDtos);
        }

        [HttpGet("{id}")]
        //[AllowAnonymous]
        public ActionResult<JobDto> Get([FromRoute] int id)
        {
            var job = _jobService.GetById(id);

            return Ok(job);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateJobDto dto)
        {
            _jobService.Update(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _jobService.Delete(id); 

            return NoContent();
        }

        [HttpPost("{id}")]
        public ActionResult CloseJob([FromRoute] int id, [FromQuery] bool isDone)
        {
            _jobService.Close(id, isDone);

            return Ok();
        }
    }
}
