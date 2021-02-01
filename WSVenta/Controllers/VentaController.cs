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
using WSVenta.Services;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VentaController : ControllerBase
    {
        private readonly VentaRealContext _dbContext;
        private IVentaService _ventaService;

        public VentaController(VentaRealContext dbContext, IVentaService venta)
        {
            _dbContext = dbContext;
            _ventaService = venta;
        }

        public IActionResult Add(VentaRequest model)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                _ventaService.Add(model);
                respuesta.Exito = 1 ;
            }
            catch (Exception ex)
            {

                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);
        }
    }
}
