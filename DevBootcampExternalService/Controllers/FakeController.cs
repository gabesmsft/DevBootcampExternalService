using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Text;

namespace DevBootcampExternalService.Controllers
{
    public class FakeController : ApiController
    {
        // GET: Fake

        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;

            IEnumerable<string> headerValues;
            var UrlRequestHeaderFromClient = string.Empty;
            if (Request.Headers.TryGetValues("FakeHeader2", out headerValues))
            {
                UrlRequestHeaderFromClient = headerValues.FirstOrDefault();
            }

            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "value");
                response.Content = new StringContent("Ensure that the FakeHeader2 header is sent in the request", Encoding.Unicode);
                return response;
            }

            bool validInboundRequest = !UrlRequestHeaderFromClient.Contains("azurewebsites.net");

            if (validInboundRequest)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, "value");
                response.Content = new StringContent("request was successfully processed" + UrlRequestHeaderFromClient, Encoding.Unicode);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "value");
                response.Content = new StringContent("Request contains the string azurewebsites.net in the FakeHeader2 header, which is not allowed on this controller", Encoding.Unicode);
            }
            return response;


            /*
            public IHttpActionResult Get()
            {
                string UrlRequestHeaderFromClient;

                if (Request.Headers.Contains("FakeHeader2"))
                {
                    UrlRequestHeaderFromClient = Request.Headers.GetValues("FakeHeader2").ToString().ToLower();
                }
                else
                {
                    return new InternalServerErrorResult

                    return new HttpStatusCodeResult((HttpStatusCode)400,
                  "Ensure that the FakeHeader2 header is sent in the request");
                }

                bool validInboundRequest = !UrlRequestHeaderFromClient.Contains("azurewebsites.net");

                if (validInboundRequest)
                {
                    return new HttpStatusCodeResult((HttpStatusCode)200, Request.Headers.GetValues("FakeHeader2").ToString());
                    //return new HttpStatusCodeResult((HttpStatusCode)200, "request was successfully processed");
                }
                else
                {
                    return new HttpStatusCodeResult((HttpStatusCode)400,
                   "request contains the string azurewebsites.net in the FakeHeader2 header, which is not allowed on this controller");
                }
            }
            */
        }
    }
}