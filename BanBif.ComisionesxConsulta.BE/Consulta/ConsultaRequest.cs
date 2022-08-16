using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.ComisionesxConsulta.BE
{
  public class ConsultaRequest
    {
        public int CodigoCliente { get; set; }
        public string Token { get; set; }
        public int CargoTarjeta { get; set; }
        public int TerminosCondiciones { get; set; }
        public int Tarifario { get; set; }
        public int CodigoOficina { get; set; }
        public int CodigoDistrito { get; set; }
        public List<Anio> ListaAnios { get; set; }

    }
}
