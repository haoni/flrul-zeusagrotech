using System.Runtime.InteropServices;
using FlurlPOC.Service.Dto;
using Flurl;
using Flurl.Http;
using System.Net.Http;
using System.Collections.Generic;
using System;

namespace FlurlPOC.Service.Business
{
    public class ZeusAgroTech
    {
        public User AuthUsuarioApiZeus(string urlLogin, UsuarioApiZeusDto usuario)
        {
            // var login = new Login("api-client-public@zeusagro.com", "public");
            var login = new Login(usuario.Login, usuario.Senha);

            var credential = urlLogin
                .WithHeaders(new { Accept = "application/json", Content_Type = "application/json" })
                .SendJsonAsync(HttpMethod.Post, login)
                .ReceiveJson<Credential>()
                .GetAwaiter()
                .GetResult();

                return credential.user;
        }

        public Pic GetPicsApiZeus(string urlPic, User user)
        {
            var pics = urlPic.WithOAuthBearerToken(user.token).GetJsonAsync<List<Pic>>().GetAwaiter().GetResult();

            Pic myPic = null;

            myPic = pics[0];

            return myPic;
        }

        public double GetPreciptacaoApiZeus(string urlPic, int picId, User user)
        {
            var dateReference = DateTime.Now.AddDays(-1);
            var formatedDataReference = dateReference.ToString("yyyy-MM-dd");
            var urlMonitoring = $"{urlPic}/{picId}/monitoring?start={formatedDataReference}&end={formatedDataReference}";

            var monitorings = urlMonitoring.WithOAuthBearerToken(user.token).GetJsonAsync<List<Monitoring>>().GetAwaiter().GetResult();

            var chuvaAcumulada =  0.0;

            monitorings.ForEach(item => {
                chuvaAcumulada += item.rain;
            });

            return chuvaAcumulada;
        }
    }
}