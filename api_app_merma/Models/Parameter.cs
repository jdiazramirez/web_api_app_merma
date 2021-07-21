using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_app_merma.Models
{
    public class Parameter
    {
        public List<Parameter> I_MATNR_LIST { get; set; }
        public int I_OPCION { get; set; }
        public string I_WERKS { get; set; }
        public string I_FECHA { get; set; }
        public string I_IDENT { get; set; }
        public string I_CHECK { get; set; }
        public string I_USER { get; set; }
        public string I_FOLIO { get; set; }
        public string I_MATNR { get; set; }
        public string SAP_USER { get; set; }
        public string SAP_CLAVE { get; set; }
        public string I_SCAN { get; set; }
    }
}