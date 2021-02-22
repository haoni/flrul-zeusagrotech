using System.Transactions;
using System.Net.Http;
using System.Net;
using System;
using Flurl;
using Flurl.Http;
using FlurlPOC.Dto;
using System.Collections.Generic;

namespace FlurlPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://www.cropnet.us/api/v1/";
            var urlLogin = $"{url}login";
            var urlPic = $"{url}pics";

            try
            {
                var login = new Login("api-client-public@zeusagro.com", "public");

                var credential = urlLogin
                    .WithHeaders(new { Accept = "application/json", Content_Type = "application/json" })
                    .SendJsonAsync(HttpMethod.Post, login)
                    .ReceiveJson<Credential>()
                    .GetAwaiter()
                    .GetResult();

                var pics = urlPic.WithOAuthBearerToken(credential.user.token).GetJsonAsync<List<Pic>>().GetAwaiter().GetResult();

                Pic myPic = null;

                if(pics.Count != 1)
                    throw new Exception("Deu ruim mano!");

                myPic = pics[0];

                var dateReference = DateTime.Now.AddDays(-1);
                var formatedDataReference = dateReference.ToString("yyyy-MM-dd");

                var urlMonitoring = $"https://www.cropnet.us/api/v1/pics/{myPic.picId}/monitoring?start={formatedDataReference}&end={formatedDataReference}";

                var monitorings = urlMonitoring.WithOAuthBearerToken(credential.user.token).GetJsonAsync<List<Monitoring>>().GetAwaiter().GetResult();

                if(monitorings.Count <= 0)
                    throw new Exception("Deu ruim mano!");

                var chuvaAcumulada = 0.0;

                monitorings.ForEach( item => {
                    chuvaAcumulada += item.rain;
                });

                Console.WriteLine(chuvaAcumulada);

            }
            catch(FlurlHttpException flurlHttpException)
            {
                Console.Write(flurlHttpException.Message);
            }
        }
    }
}
