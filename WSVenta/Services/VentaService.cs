using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.models;
using WSVenta.Models;
using WSVenta.Models.Request;
using WSVenta.Models.Response;

namespace WSVenta.Services
{
    public class VentaService : IVentaService
    {
        private readonly VentaRealContext _dbContext;

        public VentaService(VentaRealContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(VentaRequest model)
        {

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
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Ocurrió un error en la inserción", ex);
            }
        }
    }
}
