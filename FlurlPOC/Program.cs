using System.Transactions;
using System.Net.Http;
using System.Net;
using System;
using Flurl;
using Flurl.Http;
using FlurlPOC.Service.Dto;

using System.Collections.Generic;
using FlurlPOC.Service.Business;

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
                var zeusAgroTechService = new ZeusAgroTech();

                var usuarioApiZeusDto = new UsuarioApiZeusDto();
                usuarioApiZeusDto.Senha = "public";
                usuarioApiZeusDto.Login = "api-client-public@zeusagro.com";

                var zeusUser = zeusAgroTechService.AuthUsuarioApiZeus(urlLogin, usuarioApiZeusDto);

                var myPic = zeusAgroTechService.GetPicsApiZeus(urlPic, zeusUser);

                var chuvaAcumulada = zeusAgroTechService.GetPreciptacaoApiZeus(urlPic, myPic.picId, zeusUser);

                Console.WriteLine(chuvaAcumulada);

            }
            catch(FlurlHttpException flurlHttpException)
            {
                Console.Write(flurlHttpException.Message);
            }
        }
    }
}