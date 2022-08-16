using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanBif.ComisionesxConsulta.BE
{
   public class ListarOficinaResponse
    {

        public bool Result { get; set; }
        public ListarOficinaResult Data { get; set; }
        public string Mensaje { get; set; }

    }
}
