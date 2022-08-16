using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.ComisionesxConsulta.BE
{
   public class ConsultaResponse
    {

        public bool Result { get; set; }
        public string Mensaje { get; set; }
        public ConsultaResult Data { get; set; }


    }
}
