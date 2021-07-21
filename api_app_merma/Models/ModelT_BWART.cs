using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_app_merma.Models
{
    public class T_BWART
    {
        public string CLSMOV { get; set; }
        public string CLMOV_TEXT { get; set; }
        public string CLSMOV_A { get; set; }
    }
    public class T_USERS
    {
        public string CENTRO { get; set; }
        public string USUARIO { get; set; }
        public string NOMUSR { get; set; }
        public string TRAMO { get; set; }
    }
    public class T_TRAMOS
    {
        public string TRAMO { get; set; }
        public string CANT_MIN { get; set; }
        public string CANT_MAX { get; set; }
    }
    public class T_TRXAL
    {
        public string CENTRO { get; set; }
        public string ALMACEN { get; set; }
        public string TRAMO { get; set; }
    }
    public class T_COMBO
    {
        public string FOLIO { get; set; }
        public string HORA { get; set; }
        public string CENTRO { get; set; }
        public string ALMACEN { get; set; }
        public string CLASE_MOV { get; set; }
    }
    public class RESPUESTA
    {
        public List<T_BWART> T_BWART { get; set; }
        public List<T_USERS> T_USERS { get; set; }
        public List<T_TRAMOS> T_TRAMOS { get; set; }
        public List<T_TRXAL> T_TRXAL { get; set; }
        public string E_RETURN { get; set; }
        public string E_MESSAGE { get; set; }
        public string E_MAKTX { get; set; }
        public string CODIGO { get; set; }
        public string ESTADO { get; set; }
        public string E_MATNR { get; set; }
    }
    

}