using System;
using System.Collections.Generic;
using BibliotecaUdeA.Business.Contracts.Common;

namespace BibliotecaUdeA.Business.Validators
{
    public class Validator : IValidator
    {
        public List<string> IsNullOrEmpty(string field, string errorMessage)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(field))
            {
                errors.Add(errorMessage);
            }

            return errors;
        }
    }
}
