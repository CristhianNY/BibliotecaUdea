using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using BibliotecaUdeA.Business.Contracts.Platform;
using BibliotecaUdeA.Business.DependencyInjection;
using BibliotecaUdeA.Business.Dtos;
using BibliotecaUdeA.Exceptions;
using Newtonsoft.Json;

namespace BibliotecaUdeA.DataAcces
{
    public class RESTConsumer<PRequest, TResponse>
    {
        private HttpClientHandler handler;

        public RESTConsumer(HttpClientHandler handler)
        {
            this.handler = handler;
        }

        public TResponse ConsumeRestService(PRequest requestObject, string url, HttpMethod method, Dictionary<string, string> headers = null, double timeOutInSeconds = 60)
        {
            HttpResponseMessage response;
            try
            {
                    Debug.WriteLine("URL: " + url);

                    HttpRequestMessage requestMessage = new HttpRequestMessage(method, url);
                    if (method == HttpMethod.Post || method == HttpMethod.Put)
                    {
                        string serializedRequestObject = JsonConvert.SerializeObject(requestObject);
                        StringContent serializedRequest = new StringContent(serializedRequestObject, Encoding.UTF8, "application/json");
                        requestMessage.Content = serializedRequest;
                    }

                    HttpClient client = new HttpClient(handler);

                    client.Timeout = TimeSpan.FromSeconds(timeOutInSeconds);
                    if (headers != null)
                    {
                        foreach (var item in headers)
                        {
                            client.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }

                    response = client.SendAsync(requestMessage).Result;

            }
            catch (NoInternetConnectionException exception)
            {
                Debug.WriteLine("No tienes Internet");
                Debug.WriteLine(exception);

                throw new NoInternetConnectionException();
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Un error ha pasado");
                Debug.WriteLine(exception);

                throw new InternalServerException();
            }

            return ManageResponseMessages(response);
        }

        private TResponse ManageResponseMessages(HttpResponseMessage response)
        {
            string responseString = response.Content.ReadAsStringAsync().Result;
            Debug.WriteLine("Json Response: " + responseString);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return JsonConvert.DeserializeObject<TResponse>(responseString);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("error procesando la respuesta del servicio");
                    Debug.WriteLine(ex);

                    throw new UnexpectedException();
                }
            }
            else
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseString);

                switch ((int)response.StatusCode)
                {
                    case 500:
                        throw new InternalServerException();
                    case 403:
                    case 404:
                    case 409:
                        throw new Exception(errorResponse.Error.Errors[0].Reason);
                    case 504:
                        throw new TimeoutException("Error TimeOut");
                    default:
                        throw new UnexpectedException();
                }
            }
        }
    }
}
