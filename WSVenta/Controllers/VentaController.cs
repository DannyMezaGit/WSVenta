using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.models;
using WSVenta.Models;
using WSVenta.Models.Request;
using WSVenta.Models.Response;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VentaController : ControllerBase
    {
        private readonly VentaRealContext _dbContext;

        public VentaController(VentaRealContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Add(VentaRequest model)
        {
            Respuesta respuesta = new Respuesta();

                var transaction = _dbContext.Database.BeginTransaction();
            try
            {

                var venta = new Venta();
                venta.Total = model.Conceptos.Sum(d => d.Cantidad * d.PrecioUnitario);
                venta.Fecha = DateTime.Now;
                venta.IdCliente = model.IdCliente;

                _dbContext.Venta.Add(venta);
                _dbContext.SaveChanges();

                foreach (var modelconcepto in model.Conceptos)
                {
                    var concepto = new Models.Concepto();
                    concepto.Cantidad = modelconcepto.Cantidad;
                    concepto.IdProducto = modelconcepto.IdProducto;
                    concepto.PrecioUnitario = modelconcepto.PrecioUnitario;
                    concepto.Importe = modelconcepto.Importe;
                    concepto.IdVenta = venta.Id;
                    _dbContext.Conceptos.Add(concepto);
                    _dbContext.SaveChanges();
                }

                transaction.Commit();
                respuesta.Exito = 1;
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);
        }
    }
}
