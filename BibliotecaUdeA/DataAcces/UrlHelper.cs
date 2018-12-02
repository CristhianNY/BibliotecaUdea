using System;
namespace BibliotecaUdeA.DataAcces
{
    public class URLHelper
    {
        public string BuildUrl(string baseUrl, string serviceUrl)
        {
            return string.Format("{0}{1}", baseUrl, serviceUrl);
        }
    }
}
