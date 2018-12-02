using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BibliotecaUdeA.Droid.Components
{
    public class CustomDelegatingHandler : HttpClientHandler
    {
        string APPId = "";
        string APIKey = "";
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            string requestContentBase64String = string.Empty;

            string requestUri = request.RequestUri.AbsolutePath.ToLower();

            string requestHttpMethod = request.Method.Method;

            DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            Debug.WriteLine(DateTime.UtcNow);
            TimeSpan timeSpan = DateTime.UtcNow - epochStart;

            string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();
            Debug.WriteLine("stamp:" + requestTimeStamp);

            string nonce = Guid.NewGuid().ToString("N");
            Debug.WriteLine("nonce:" + nonce);

            if (request.Content != null)
            {
                string contentJson = await request.Content.ReadAsStringAsync();
                Debug.WriteLine("JSON: " + contentJson);

                byte[] content = await request.Content.ReadAsByteArrayAsync();
                MD5 md5 = MD5.Create();

                byte[] requestContentHash = md5.ComputeHash(content);
                requestContentBase64String = Convert.ToBase64String(requestContentHash);
                Debug.WriteLine("Hash: " + requestContentBase64String);
            }

            string signatureRawData = string.Format("{0}{1}{2}{3}{4}{5}", APPId, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);

            var secretKeyByteArray = Convert.FromBase64String(APIKey);

            byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);

            using (HMACSHA256 hmac = new HMACSHA256(secretKeyByteArray))
            {
                byte[] signatureBytes = hmac.ComputeHash(signature);
                string requestSignatureBase64String = Convert.ToBase64String(signatureBytes);
                request.Headers.Authorization = new AuthenticationHeaderValue("hmac", string.Format("{0}:{1}:{2}:{3}", APPId, requestSignatureBase64String, nonce, requestTimeStamp));
            }

            Debug.WriteLine(request.Headers.Authorization);
            response = await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}
