using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UmfrageWebApi.DbModels;
using UmfrageWebApi.Models.Person.Exceptions;
using UmfrageWebApi.Services.Personen;

namespace UmfrageWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonenController : RESTFulController
    {
        private readonly IPersonenService personService;
        public PersonenController(IPersonenService personService) =>
            this.personService = personService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var personen = personService.AllePersonenAbrufenAsync();
            return Ok(personen);
        }


        [HttpGet("{idPerson}")]
        public async ValueTask<ActionResult<Person>> Get(int idPerson)
        {
            try
            {
                Person personDb = await this.personService.PersonAbrufenFromIdAsync(idPerson);

                return Ok(personDb);
            }
            catch (PersonValidationException PersonValidationException)
            {
                string innerMessage = GetInnerMessage(PersonValidationException);

                return BadRequest(PersonValidationException);
            }
            catch (PersonDependencyException PersonDependencyException)
               when (PersonDependencyException.InnerException is LockedPersonException)
            {
                string innerMessage = GetInnerMessage(PersonDependencyException);

                return Locked(innerMessage);
            }
            catch (PersonDependencyException PersonDependencyException)
            {
                return Problem(PersonDependencyException.Message);
            }
            catch (PersonServiceException PersonServiceException)
            {
                return Problem(PersonServiceException.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            try
            {
                Person personDb = await this.personService.PersonErzeugenAsync(person);

                return Ok(personDb);
            }
            catch (PersonValidationException PersonValidationException)
                when (PersonValidationException.InnerException is AlreadyExistsPersonException)
            {
                string innerMessage = GetInnerMessage(PersonValidationException);

                return Conflict(innerMessage);
            }
            catch (PersonValidationException PersonValidationException)
            {
                string innerMessage = GetInnerMessage(PersonValidationException);

                return BadRequest(innerMessage);
            }
            catch (PersonDependencyException PersonDependencyException)
            {
                return Problem(PersonDependencyException.Message);
            }
            catch (PersonServiceException PersonServiceException)
            {
                return Problem(PersonServiceException.Message);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Person>> Put(Person person)
        {
            try
            {
                Person changedPersonDb =
                    await this.personService.PersonAendernAsync(person);

                return Ok(changedPersonDb);
            }
            catch (PersonValidationException PersonValidationException)
                when (PersonValidationException.InnerException is NotFoundPersonException)
            {
                string innerMessage = GetInnerMessage(PersonValidationException);

                return NotFound(innerMessage);
            }
            catch (PersonValidationException PersonValidationException)
            {
                string innerMessage = GetInnerMessage(PersonValidationException);

                return BadRequest(innerMessage);
            }
            catch (PersonDependencyException PersonDependencyException)
                when (PersonDependencyException.InnerException is LockedPersonException)
            {
                string innerMessage = GetInnerMessage(PersonDependencyException);

                return Locked(innerMessage);
            }
            catch (PersonDependencyException PersonDependencyException)
            {
                return Problem(PersonDependencyException.Message);
            }
            catch (PersonServiceException PersonServiceException)
            {
                return Problem(PersonServiceException.Message);
            }
        }

        [HttpDelete("{idPerson}")]
        public async Task<ActionResult> Delete(int idPerson)
        {
            try
            {
                bool success = await this.personService.PersonLoeschenAsync(idPerson);
                return Ok(success);
            }
            catch (PersonValidationException PersonValidationException)
                when (PersonValidationException.InnerException is NotFoundPersonException)
            {
                string innerMessage = GetInnerMessage(PersonValidationException);

                return NotFound(innerMessage);
            }
            catch (PersonValidationException PersonValidationException)
            {
                string innerMessage = GetInnerMessage(PersonValidationException);

                return BadRequest(innerMessage);
            }
            catch (PersonDependencyException PersonDependencyException)
                when (PersonDependencyException.InnerException is LockedPersonException)
            {
                string innerMessage = GetInnerMessage(PersonDependencyException);

                return Locked(innerMessage);
            }
            catch (PersonDependencyException PersonDependencyException)
            {
                return Problem(PersonDependencyException.Message);
            }
            catch (PersonServiceException PersonServiceException)
            {
                return Problem(PersonServiceException.Message);
            }
        }

        public static string GetInnerMessage(Exception exception) =>
        exception.InnerException.Message;
    }
}
