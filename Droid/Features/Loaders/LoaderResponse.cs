using System;
using BibliotecaUdeA.Business.Dtos;

namespace BibliotecaUdeA.Droid.Features.Loaders
{
    public class LoaderResponse : Java.Lang.Object
    {
        public Exception Exception { get; private set; }

        public LoaderResponse() { }

        public LoaderResponse(Exception exception)
        {
            Exception = exception;
        }
    }

    public class LoaderResponse<TResponse> : LoaderResponse
        {
         
            public TResponse Response { get; private set; }

            public LoaderResponse(TResponse response) : this(response, null) { }

            public LoaderResponse(Exception exception) : this(default(TResponse), exception) { }

            public LoaderResponse(TResponse response, Exception exception) : base(exception)
            {
                Response = response;
            }

        }
  
}
