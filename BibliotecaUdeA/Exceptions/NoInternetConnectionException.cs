using System;
namespace BibliotecaUdeA.Exceptions
{
    public class NoInternetConnectionException : Exception
    {
        public override string Message
        {
            get
            {
                return "No tienes Internet";
            }
        }
    }
}
