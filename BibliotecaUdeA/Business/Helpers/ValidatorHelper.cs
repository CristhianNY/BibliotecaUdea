using System;
using System.Collections.Generic;

namespace BibliotecaUdeA.Business.Helpers
{
    public class ValidatorHelper
    {
        public string AppendValidationErrors(List<string> errors)
        {
            string message = string.Empty;
            foreach (var error in errors)
            {
                message += string.Format("- {0}\n", error);
            }

            return message;
        }
    }
}
