﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Quake.WebAPI.Controllers
{
    public class BaseController : ApiController
    {
        public HttpResponseMessage responseMessage = null;

        public BaseController()
        {
            responseMessage = new HttpResponseMessage();
        }

        public Task<HttpResponseMessage> CreateResponse(HttpStatusCode code, object result)
        {
            var retorno = result;
            try
            {
                responseMessage = Request.CreateResponse(code, retorno);
            }
            catch (Exception error)
            {
                responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, new { errors = error.InnerException });
            }
            return Task.FromResult<HttpResponseMessage>(responseMessage);
        }
    }
}