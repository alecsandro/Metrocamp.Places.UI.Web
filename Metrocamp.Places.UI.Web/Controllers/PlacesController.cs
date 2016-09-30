using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using Metrocamp.Places.UI.Web.Models;

namespace Metrocamp.Places.UI.Web.Controllers
{
    public class PlacesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection controles)
        {
            var endereco = controles["endereco"];
            var tipo = controles["tipo"];

            List<Item> estabelecimentos = new List<Item>();

            if (endereco == "")
            {
                TempData.Add("mensagem-erro", "É necessário informar um endereço.");
            }
            else if (tipo == "")
            {
                TempData.Add("mensagem-erro", "É necessário selecionar um tipo.");
            }
            else 
            { 

                //////////////////////////////////////
                //descobrindo a LATITUDE e LONGITUDE//
                //////////////////////////////////////
                var address = String.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key=AIzaSyDGHbCwcpdGMvf-f3knoYTd0stiw-ADC5Q", endereco.Replace(" ", "+"));
                var result = new System.Net.WebClient().DownloadString(address);
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                RootPlace googleData = JsonConvert.DeserializeObject<RootPlace>(result);

                String latitude = Convert.ToString(googleData.results[0].geometry.location.lat).Replace(",", ".");
                String longitude = Convert.ToString(googleData.results[0].geometry.location.lng).Replace(",", ".");

                String coordenadas = latitude + "," + longitude;

                ///////////////////////////////////
                //descobrindo os LOCAIS PRÓXIMOS //
                ///////////////////////////////////
                var address2 = String.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0}&radius=500&types={1}&name=&key=AIzaSyDGHbCwcpdGMvf-f3knoYTd0stiw-ADC5Q", coordenadas, tipo);
                var result2 = new System.Net.WebClient().DownloadString(address2);

                RootPlace googleData2 = JsonConvert.DeserializeObject<RootPlace>(result2);


                //se não encontrar resultados, exibe mensagem
                if (googleData2 == null)
                {
                    TempData.Add("mensagem-erro", "nenhum resultado encontrado.");
                }
                else
                {
                    foreach (var item in googleData2.results)
                    {
                        Item local = new Item();

                        /*
                        //se possui foto busca
                        if (item.photos != null)
                        {
                            ///////////////////////
                            //descobrindo a foto //
                            ///////////////////////
                            var foto_caminho = String.Format("https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference={0}&key=AIzaSyDGHbCwcpdGMvf-f3knoYTd0stiw-ADC5Q", item.photos[0].photo_reference);

                            local.foto = foto_caminho;    
                        }*/

                        local.icone = item.icon;
                        local.name = item.name;
                        local.logradouro = item.vicinity;
                        local.coordenadas = coordenadas;

                        estabelecimentos.Add(local);
                    }
                }
    
            }
            
            ViewBag.Pesquisa = "S";

            return View(estabelecimentos);
        }

        public ActionResult Tempo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Tempo(FormCollection controles)
        {
            var cidade = controles["cidade"];

            if (cidade == "")
            {
                TempData.Add("mensagem-erro", "É necessário selecionar uma cidade.");
            }

            ////////////////////////////////////////
            //descobrindo as informações do TEMPO //
            ////////////////////////////////////////
            var address = String.Format("http://api.hgbrasil.com/weather/?format=json&cid={0}", cidade);
            var result = new System.Net.WebClient().DownloadString(address);

            RootWeather googleData = JsonConvert.DeserializeObject<RootWeather>(result);

            ViewBag.Pesquisa = "S";

            return View(googleData);
        }

        public ActionResult Geo()
        {
            return View();
        }

        public ActionResult Place()
        {
            return View();
        }

        public ActionResult HG()
        {
            return View();
        }

        public ActionResult Sobre()
        {
            return View();
        }



    }
}