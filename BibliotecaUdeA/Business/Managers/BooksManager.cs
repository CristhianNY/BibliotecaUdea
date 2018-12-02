using System;
using System.Collections.Generic;
using System.Linq;
using BibliotecaUdeA.Business.Contracts.Repositories.Remote;
using BibliotecaUdeA.Business.DependencyInjection;
using BibliotecaUdeA.Business.Dtos;
using BibliotecaUdeA.Business.Helpers;
using BibliotecaUdeA.Business.Validators;
using BibliotecaUdeA.Exceptions;

namespace BibliotecaUdeA.Business.Managers
{
    public class BooksManager
    {
        private readonly IBooksRepository booksRepository;
        private readonly Validator validator;
        private readonly ValidatorHelper validatorHelper;

        public BooksManager()
        {
            validatorHelper = new ValidatorHelper();
            booksRepository = ServicesLocator.Get<IBooksRepository>();
            validator = new Validator();

        }


        public BaseResponse<BooksResponse> FetchBooksByName(string name)
        {
            var result = new BaseResponse<BooksResponse>();
            var errors = new List<string>();
            errors.AddRange(validator.IsNullOrEmpty(name ,"Se requiere un nombre"));
            if (errors.Any())
            {
                result.Exception = new Exception(validatorHelper.AppendValidationErrors(errors));
                result.Data = null;
                return result;
            }

            try
            {
                var response = booksRepository.FetchListBooksByName(name);
                var books = response.Books;

                if (!books.Any())
                {
                    throw new NoDataException("No se encontraron libros");
                }
                else
                {
                    result.Data = response;
                    result.Exception = null;
                }
            }
            catch (Exception e)
            {
                result.Data = null;
                result.Exception = e;
            }
            return result;
        }
    }
}
