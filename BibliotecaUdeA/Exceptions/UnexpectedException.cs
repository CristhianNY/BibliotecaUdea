using System;
namespace BibliotecaUdeA.Exceptions
{
    public class UnexpectedException : Exception
    {
        public override string Message
        {
            get
            {
                return "Este es un error inesperado";
            }
        }
    }
}
