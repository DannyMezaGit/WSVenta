using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.models;
using WSVenta.Models.Request;
using WSVenta.Models.Response;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly VentaRealContext _dbContext;

        public ClienteController(VentaRealContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {

                var lst = _dbContext.Clientes.OrderByDescending(d=>d.Id).ToList();
                oRespuesta.Exito = 1;
                oRespuesta.Data = lst;

            }
            catch (Exception ex)
            {

                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        [HttpPost]
        public IActionResult Add(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                    var oCliente = new Cliente();
                    oCliente.Nombre = oModel.Nombre;
                    _dbContext.Clientes.Add(oCliente);
                    _dbContext.SaveChanges();
                    oRespuesta.Exito = 1;
               
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);

        }
        
        [HttpPut]
        public IActionResult Edit(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();

            

            try
            {
                var oCliente = _dbContext.Clientes.Find(oModel.Id);
                oCliente.Nombre = oModel.Nombre;
                _dbContext.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
    }
}
