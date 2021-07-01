using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UmfrageWebApi.DbModels;
using UmfrageWebApi.Models.Personart.Exceptions;
using UmfrageWebApi.Services.PersonArten;

namespace UmfrageWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonArtenController : RESTFulController
    {
        private readonly IPersonArtService personartService;
        public PersonArtenController(IPersonArtService personartService) =>
            this.personartService = personartService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var personarten = personartService.AllePersonArtenAbrufenAsync();
            return Ok(personarten);
        }


        [HttpGet("{idPersonart}")]
        public async ValueTask<ActionResult<PersonArt>> Get(int idPersonart)
        {
            try
            {
                PersonArt personartDb = await this.personartService.PersonArtAbrufenFromIdAsync(idPersonart);

                return Ok(personartDb);
            }
            catch (PersonartValidationException PersonartValidationException)
            {
                string innerMessage = GetInnerMessage(PersonartValidationException);

                return BadRequest(PersonartValidationException);
            }
            catch (PersonartDependencyException PersonDependencyException)
               when (PersonDependencyException.InnerException is LockedPersonartException)
            {
                string innerMessage = GetInnerMessage(PersonDependencyException);

                return Locked(innerMessage);
            }
            catch (PersonartDependencyException PersonartDependencyException)
            {
                return Problem(PersonartDependencyException.Message);
            }
            catch (PersonartServiceException PersonartServiceException)
            {
                return Problem(PersonartServiceException.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonArt personart)
        {
            try
            {
                PersonArt personartDb = await this.personartService.PersonArtErzeugenAsync(personart);

                return Ok(personartDb);
            }
            catch (PersonartValidationException PersonValidationException)
                when (PersonValidationException.InnerException is AlreadyExistsPersonartException)
            {
                string innerMessage = GetInnerMessage(PersonValidationException);

                return Conflict(innerMessage);
            }
            catch (PersonartValidationException PersonartValidationException)
            {
                string innerMessage = GetInnerMessage(PersonartValidationException);

                return BadRequest(innerMessage);
            }
            catch (PersonartDependencyException PersonartDependencyException)
            {
                return Problem(PersonartDependencyException.Message);
            }
            catch (PersonartServiceException PersonartServiceException)
            {
                return Problem(PersonartServiceException.Message);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Person>> Put(PersonArt personart)
        {
            try
            {
                PersonArt changedPersonartDb =
                    await this.personartService.PersonArtAendernAsync(personart);

                return Ok(changedPersonartDb);
            }
            catch (PersonartValidationException PersonartValidationException)
                when (PersonartValidationException.InnerException is NotFoundPersonartException)
            {
                string innerMessage = GetInnerMessage(PersonartValidationException);

                return NotFound(innerMessage);
            }
            catch (PersonartValidationException PersonValidationException)
            {
                string innerMessage = GetInnerMessage(PersonValidationException);

                return BadRequest(innerMessage);
            }
            catch (PersonartDependencyException PersonartDependencyException)
                when (PersonartDependencyException.InnerException is LockedPersonartException)
            {
                string innerMessage = GetInnerMessage(PersonartDependencyException);

                return Locked(innerMessage);
            }
            catch (PersonartDependencyException PersonDependencyException)
            {
                return Problem(PersonDependencyException.Message);
            }
            catch (PersonartServiceException PersonServiceException)
            {
                return Problem(PersonServiceException.Message);
            }
        }

        [HttpDelete("{idPersonart}")]
        public async Task<ActionResult> Delete(int idPerson)
        {
            try
            {
                bool success = await this.personartService.PersonArtLoeschenAsync(idPerson);
                return Ok(success);
            }
            catch (PersonartValidationException PersonartValidationException)
                when (PersonartValidationException.InnerException is NotFoundPersonartException)
            {
                string innerMessage = GetInnerMessage(PersonartValidationException);

                return NotFound(innerMessage);
            }
            catch (PersonartValidationException PersonValidationException)
            {
                string innerMessage = GetInnerMessage(PersonValidationException);

                return BadRequest(innerMessage);
            }
            catch (PersonartDependencyException PersonartDependencyException)
                when (PersonartDependencyException.InnerException is LockedPersonartException)
            {
                string innerMessage = GetInnerMessage(PersonartDependencyException);

                return Locked(innerMessage);
            }
            catch (PersonartDependencyException PersonDependencyException)
            {
                return Problem(PersonDependencyException.Message);
            }
            catch (PersonartServiceException PersonServiceException)
            {
                return Problem(PersonServiceException.Message);
            }
        }

        public static string GetInnerMessage(Exception exception) =>
        exception.InnerException.Message;
    }
}
