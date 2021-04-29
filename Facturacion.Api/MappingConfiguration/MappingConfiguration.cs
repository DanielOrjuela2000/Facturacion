using AutoMapper;
using Facturacion.Core.DTOs;
using Facturacion.Core.DTOs.Commands;
using Facturacion.Core.Entities;
using Facturacion.Core.Entities.Commands;

namespace Facturacion.Api.MappingConfiguration
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<ClienteDto, Cliente>();
            CreateMap<ProductoDto, Producto>();
            CreateMap<FacturaDto, Factura>();
            CreateMap<FacturaDetalleDto, FacturaDetalle>();
            CreateMap<FacturaCommandDto, FacturaCommand>();
        }
    }
}
