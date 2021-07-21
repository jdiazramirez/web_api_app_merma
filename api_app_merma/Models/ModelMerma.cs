using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_app_merma.Models
{
    public class ModelMerma
    {
        public string ID { get; set; }
        public string ALMACEN { get; set; }
        public string PRODUCTO { get; set; }
        public string CANT_CAJA { get; set; }
        public string CANT_BOTE { get; set; }
        public string CLASE_MOV { get; set; }
        public string MOTIVO { get; set; }
        public string IMAGE { get; set; }
        public string IMG_NAME { get; set; }
        public string SAP_USER { get; set; }
        public string SAP_CLAVE { get; set; }
    }
}