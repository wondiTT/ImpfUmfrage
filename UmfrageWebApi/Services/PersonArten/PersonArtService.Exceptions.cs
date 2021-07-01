
using UmfrageWebApi.DbModels;
using UmfrageWebApi.Models.Person.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UmfrageWebApi.Models.Personart.Exceptions;

namespace UmfrageWebApi.Services.PersonArten
{
    public partial class PersonArtService
    {
        private delegate ValueTask<PersonArt> ReturningPersonartFunction();
        private delegate ValueTask<List<PersonArt>> ReturningQueryablePersonartFunction();
        private delegate ValueTask<bool> ReturningIfSuccessFunction();

        private async ValueTask<PersonArt> TryCatch(ReturningPersonartFunction returningPersonartFunction)
        {
            try
            {
                return await returningPersonartFunction();
            }
            catch (NullPersonartException nullPersonartException)
            {
                throw CreateAndLogValidationException(nullPersonartException);
            }
            catch (InvalidPersonartException invalidPersonartInputException)
            {
                throw CreateAndLogValidationException(invalidPersonartInputException);
            }
            catch (NotFoundPersonartException nullPersonartException)
            {
                throw CreateAndLogValidationException(nullPersonartException);
            }
            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
            catch (AlreadyExistsPersonartException alreadyExistsPersonartException)
            {
                throw CreateAndLogValidationException(alreadyExistsPersonartException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedPersonException = new LockedPersonException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyException(lockedPersonException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw CreateAndLogDependencyException(dbUpdateException);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }


        private async ValueTask<bool> TryCatch(ReturningIfSuccessFunction returningIfSuccessFunction)
        {
            try
            {
                return await returningIfSuccessFunction();
            }
            catch (NullPersonartException nullPersonartException)
            {
                throw CreateAndLogValidationException(nullPersonartException);
            }
            //catch (InvalidPersonException invalidPersonInputException)
            //{
            //    throw CreateAndLogValidationException(invalidPersonInputException);
            //}
            catch (NotFoundPersonartException nullPersonartException)
            {
                throw CreateAndLogValidationException(nullPersonartException);
            }
            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedPersonException = new LockedPersonException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyException(lockedPersonException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw CreateAndLogDependencyException(dbUpdateException);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private ValueTask<List<PersonArt>> TryCatch(ReturningQueryablePersonartFunction returningQueryablePersonartFunction)
        {
            try
            {
                return returningQueryablePersonartFunction();
            }
            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw CreateAndLogDependencyException(dbUpdateException);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }




        private PersonartServiceException CreateAndLogServiceException(Exception exception)
        {
            var PersonartServiceException = new PersonartServiceException(exception);
            //this.loggingBroker.LogError(PersonartServiceException);

            return PersonartServiceException;
        }

        private PersonartDependencyException CreateAndLogDependencyException(Exception exception)
        {
            var PersonartDependencyException = new PersonartDependencyException(exception);
            //this.loggingBroker.LogError(PersonDependencyException);

            return PersonartDependencyException;
        }

        private PersonartDependencyException CreateAndLogCriticalDependencyException(Exception exception)
        {
            var PersonartDependencyException = new PersonartDependencyException(exception);
            //this.loggingBroker.LogCritical(PersonDependencyException);

            return PersonartDependencyException;
        }

        private PersonartValidationException CreateAndLogValidationException(Exception exception)
        {
            var PersonartValidationException = new PersonartValidationException(exception);
            //this.loggingBroker.LogError(PersonValidationException);

            return PersonartValidationException;
        }
    }
}
