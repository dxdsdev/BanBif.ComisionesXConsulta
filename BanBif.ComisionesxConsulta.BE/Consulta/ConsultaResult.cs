using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.ComisionesxConsulta.BE
{
   public class ConsultaResult
    {

        public int CodigoConsulta { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string TipoProducto { get; set; }
        public string NroProducto { get; set; }
        public bool CargoAnio { get; set; }
        public int CantidadAnios { get; set; }
        public string DescripcionOficina { get; set; }
        public string DescripcionDistrito { get; set; }
        public List<Anio> AniosSeleccionado { get; set; }

    }
}
