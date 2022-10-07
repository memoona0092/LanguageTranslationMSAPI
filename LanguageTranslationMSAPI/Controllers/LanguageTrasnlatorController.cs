using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using unirest_net.http;
using RestSharp;
using System.Xml;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;

namespace LanguageTranslationMSAPI.Controllers
{
    public class LanguageTrasnlatorController : Controller
    {
        // GET: LanguageTrasnlator
        public ActionResult Index()
        {
            return View();
        }
       //function to call api
        public ActionResult TranslateLang(string langchng,string txtTrans)
        {
            if (txtTrans != null)
            {

                var client = new RestClient("https://microsoft-translator-text.p.rapidapi.com/translate?to="+ langchng + "&api-version=3.0&profanityAction=NoAction&textType=plain");
                RestRequest request = new RestRequest("https://microsoft-translator-text.p.rapidapi.com/translate?to="+langchng+ "&api-version=3.0&profanityAction=NoAction&textType=plain", Method.Post);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("X-RapidAPI-Key", "fe982eeb50msh56a873feab3ef0cp15388bjsn78048a874017");
                request.AddHeader("X-RapidAPI-Host", "microsoft-translator-text.p.rapidapi.com");
                request.AddParameter("application/json", "[\r {\r\"Text\": \"" + txtTrans + "\"\r}\r]", ParameterType.RequestBody);

                RestResponse response = client.Execute(request);
                string responsecontent = response.Content.ToString();
                responsecontent = Filter(responsecontent);
                return Json(responsecontent.ToString());
            }
            else
                return View("Index");
        }

        //Filter function is use to format the api response
        public string Filter(string str)
        {
            int ind = str.IndexOf("text");
            int totallength=str.Length;
            string firstpart = str.Substring(0, ind+7);
            string lastpart=str.Substring(totallength-15);
            str= str.Replace(firstpart, String.Empty);
            str= str.Replace(lastpart, String.Empty);
            return str;
        }
    }
}