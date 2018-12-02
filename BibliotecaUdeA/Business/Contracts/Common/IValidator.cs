using System;
using System.Collections.Generic;

namespace BibliotecaUdeA.Business.Contracts.Common
{
    public interface IValidator
    {
        List<string> IsNullOrEmpty(string field, string errorMessage);
    }
}
