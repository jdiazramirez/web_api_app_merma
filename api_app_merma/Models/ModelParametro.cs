using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_app_merma.Models
{
    public class ModelParametro
    {
        public string FECHA { get; set; }
        public string CENTRO { get; set; }
        public string ALMACEN { get; set; }
        public string SAP_USER { get; set; }
        public string SAP_CLAVE { get; set; }
        public string CLASE_MOV { get; set; }
        public List<ModelMerma> MERMAS { get; set; }
    }
}