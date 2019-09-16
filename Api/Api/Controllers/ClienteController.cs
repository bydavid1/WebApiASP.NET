using Api.Models;
using Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class ClienteController : ApiController
    {
        static readonly ICliente db = new RCliente();

        //Metodo Get

        public HttpResponseMessage GetAll()
        {
            var items = db.GetAll();
            if (items == null)
            {
                //Construyendo respuesta del servidor
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No hay registros");

            }

            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage GetById(int id)
        {
            Cliente item = db.GetById(id);
            if (item == null)
            {
                //Construyendo respuesta del servidor
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No hay registros con ese Id" + id);

            }

            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        public HttpResponseMessage Post(Cliente item)
        {
            item = db.Post(item);
            if (item == null)
            {
                //Construyendo respuesta del servidor
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Los datos del clinete no pueden ser nulos");

            }

            return Request.CreateResponse(HttpStatusCode.Created, item);
        }

        public HttpResponseMessage Delete(int id)
        {

            var item = db.GetById(id);

            if (item == null)
            {
                //Construyendo respuesta del servidor
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No hay registros con ese Id" + id);

            }
            db.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Se Elimino el registro");
        }


        public HttpResponseMessage Put(Cliente cliente)
        {

            var item = db.GetById(cliente.Id);

            if (item == null)
            {
                //Construyendo respuesta del servidor
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No hay registros con ese Id" + cliente.Id);

            }
            var isPut = db.Put(cliente);
            if (!isPut)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, "No se pudo Actualizar + id");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Se Actualizo  el registro");
        }
    }
}
