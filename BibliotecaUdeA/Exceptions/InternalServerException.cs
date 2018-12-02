using System;
namespace BibliotecaUdeA.Exceptions
{
    public class InternalServerException : Exception
    {
        public override string Message
        {
            get
            {
                return "Error en el servidor";
            }
        }
    }
}
