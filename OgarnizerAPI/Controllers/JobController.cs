using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Models;
using OgarnizerAPI.Services;

namespace OgarnizerAPI.Controllers
{
    [Route("api/ogarnizer")]
    [ApiController]
    [Authorize]
    public class JobController : ControllerBase
    {        
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _jobService.Delete(id); 

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateJobDto dto)
        {
            _jobService.Update(id, dto); 

            return NotFound();
        }

        [HttpPost]
        //[Authorize(Roles = "Manager")]
        public ActionResult CreateJob([FromBody] CreateJobDto dto)
        {            
            var id = _jobService.Create(dto); 

            return Created($"/api/ogarnizer/{id}", null);
        }

        [HttpGet]        
        //[Authorize(Policy = "HasNationality")]
        //[Authorize(Policy = "Atleast20"}]  albo Policy = "CreatedAtleast2Jobs"
        public ActionResult<IEnumerable<JobDto>> GetAll([FromQuery]JobQuery query)
        {
            var jobsDtos = _jobService.GetAll(query); 

            return Ok(jobsDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<JobDto> Get([FromRoute] int id)
        {
            var job = _jobService.GetById(id);
            
            return Ok(job);
        }
    }
}
