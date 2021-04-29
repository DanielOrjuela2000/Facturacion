using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Api.Models
{
    public class Response
    {
        public bool Result { get; set; }
        public string Description { get; set; }
        public object Data { get; set; }
    }
}
