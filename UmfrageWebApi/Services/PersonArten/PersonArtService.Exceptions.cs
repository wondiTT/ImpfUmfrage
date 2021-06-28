
using UmfrageWebApi.DbModels;
using UmfrageWebApi.Models.Person.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Services.Personen
{
    public partial class PersonenService
    {
        private delegate ValueTask<Person> ReturningPersonFunction();
        private delegate ValueTask<IQueryable<Person>> ReturningQueryablePersonFunction();
        private delegate ValueTask<bool> ReturningIfSuccessFunction();

        private async ValueTask<Person> TryCatch(ReturningPersonFunction returningPersonFunction)
        {
            try
            {
                return await returningPersonFunction();
            }
            catch (NullPersonException nullPersonException)
            {
                throw CreateAndLogValidationException(nullPersonException);
            }
            catch (InvalidPersonException invalidPersonInputException)
            {
                throw CreateAndLogValidationException(invalidPersonInputException);
            }
            catch (NotFoundPersonException nullPersonException)
            {
                throw CreateAndLogValidationException(nullPersonException);
            }
            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
            catch (AlreadyExistsPersonException alreadyExistsPersonException)
            {
                throw CreateAndLogValidationException(alreadyExistsPersonException);
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
            catch (NullPersonException nullPersonException)
            {
                throw CreateAndLogValidationException(nullPersonException);
            }
            //catch (InvalidPersonException invalidPersonInputException)
            //{
            //    throw CreateAndLogValidationException(invalidPersonInputException);
            //}
            catch (NotFoundPersonException nullPersonException)
            {
                throw CreateAndLogValidationException(nullPersonException);
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

        private ValueTask<IQueryable<Person>> TryCatch(ReturningQueryablePersonFunction returningQueryablePersonFunction)
        {
            try
            {
                return returningQueryablePersonFunction();
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




        private PersonServiceException CreateAndLogServiceException(Exception exception)
        {
            var PersonServiceException = new PersonServiceException(exception);
            //this.loggingBroker.LogError(PersonServiceException);

            return PersonServiceException;
        }

        private PersonDependencyException CreateAndLogDependencyException(Exception exception)
        {
            var PersonDependencyException = new PersonDependencyException(exception);
            //this.loggingBroker.LogError(PersonDependencyException);

            return PersonDependencyException;
        }

        private PersonDependencyException CreateAndLogCriticalDependencyException(Exception exception)
        {
            var PersonDependencyException = new PersonDependencyException(exception);
            //this.loggingBroker.LogCritical(PersonDependencyException);

            return PersonDependencyException;
        }

        private PersonValidationException CreateAndLogValidationException(Exception exception)
        {
            var PersonValidationException = new PersonValidationException(exception);
            //this.loggingBroker.LogError(PersonValidationException);

            return PersonValidationException;
        }
    }
}
