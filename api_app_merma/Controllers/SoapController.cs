using api_app_merma.Models;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Http;

namespace api_app_merma.Controllers
{
    public class SoapController : ApiController
    {
        public string saveImage(string base64String, string nombre)
        {
            string fileName = "";
            try
            {
                string folderPath = HttpContext.Current.Server.MapPath("~/ImagesFolder/");
                fileName = nombre + ".jpg";
                string imagePath = folderPath + fileName;

                string base64StringData = base64String;
                string cleandata = base64StringData.Replace("data:image/png;base64,", "");
                byte[] data = System.Convert.FromBase64String(cleandata);
                MemoryStream ms = new MemoryStream(data);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                img.Save(imagePath);
            }
            catch (Exception)
            {
                return "";
            }
            return "/ImagesFolder/" + fileName;
        }
        //ZRFC_SC_MERMA
        [Route("soap/ZRFC_SC_MERMA")]
        [HttpPost]
        public IHttpActionResult ZRFC_SC_MERMA(Parameter parameter)
        {
            RESPUESTA rESPUESTA = new RESPUESTA();
            try
            {
                RfcConfigParameters var_cof_parameter = new RfcConfigParameters
                {

                    { RfcConfigParameters.AppServerHost, GetCONFIGURACION().APPSERVERHOST },
                    { RfcConfigParameters.SystemNumber, GetCONFIGURACION().SYSTEMNUMBER },
                    { RfcConfigParameters.SystemID, GetCONFIGURACION().SYSTEMID },
                    { RfcConfigParameters.User, parameter.SAP_USER },
                    { RfcConfigParameters.Password, parameter.SAP_CLAVE },
                    { RfcConfigParameters.Client, GetCONFIGURACION().CLIENT },
                    { RfcConfigParameters.Language, GetCONFIGURACION().LANGUAGE },
                    { RfcConfigParameters.Name, GetCONFIGURACION().NAME }
                };


                RfcDestination dest = RfcDestinationManager.GetDestination(var_cof_parameter);

                RfcRepository repo = dest.Repository;
                IRfcFunction func = repo.CreateFunction("ZRFC_SC_MERMA");

                func.SetValue("I_SCAN", parameter.I_SCAN);
                func.Invoke(dest);

                rESPUESTA.E_MATNR= func.GetString("E_MATNR");
                rESPUESTA.E_RETURN = func.GetString("E_RETURN");
                rESPUESTA.E_MESSAGE = func.GetString("E_MESSAGE");

            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(rESPUESTA);
        }
        //ZRFC_VL_MERMA_UNIT (validación material por uno)
        [Route("soap/ZRFC_VL_MERMA_UNIT")]
        [HttpPost]
        public IHttpActionResult ZRFC_VL_MERMA_UNIT(Parameter parameter)
        {
            RESPUESTA rESPUESTA = new RESPUESTA();
            try
            {
                RfcConfigParameters var_cof_parameter = new RfcConfigParameters
                {

                    { RfcConfigParameters.AppServerHost, GetCONFIGURACION().APPSERVERHOST },
                    { RfcConfigParameters.SystemNumber, GetCONFIGURACION().SYSTEMNUMBER },
                    { RfcConfigParameters.SystemID, GetCONFIGURACION().SYSTEMID },
                    { RfcConfigParameters.User, parameter.SAP_USER },
                    { RfcConfigParameters.Password, parameter.SAP_CLAVE },
                    { RfcConfigParameters.Client, GetCONFIGURACION().CLIENT },
                    { RfcConfigParameters.Language, GetCONFIGURACION().LANGUAGE },
                    { RfcConfigParameters.Name, GetCONFIGURACION().NAME }
                };


                RfcDestination dest = RfcDestinationManager.GetDestination(var_cof_parameter);

                RfcRepository repo = dest.Repository;
                IRfcFunction func = repo.CreateFunction("ZRFC_VL_MERMA");

                func.SetValue("I_MATNR",parameter.I_MATNR);
                func.Invoke(dest);

                rESPUESTA.E_MAKTX = func.GetString("E_MAKTX");
                rESPUESTA.E_RETURN = func.GetString("E_RETURN");
                rESPUESTA.E_MESSAGE = func.GetString("E_MESSAGE");
                rESPUESTA.CODIGO = parameter.I_MATNR;

            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(rESPUESTA);
        }
        //ZRFC_VL_MERMA (validación material)
        [Route("soap/ZRFC_VL_MERMA")]
        [HttpPost]
        public IHttpActionResult ZRFC_VL_MERMA(Parameter parameter)
        {
            List<RESPUESTA> rESPUESTA = new List<RESPUESTA>();
            try
            {
                RfcConfigParameters var_cof_parameter = new RfcConfigParameters
                {

                    { RfcConfigParameters.AppServerHost, GetCONFIGURACION().APPSERVERHOST },
                    { RfcConfigParameters.SystemNumber, GetCONFIGURACION().SYSTEMNUMBER },
                    { RfcConfigParameters.SystemID, GetCONFIGURACION().SYSTEMID },
                    { RfcConfigParameters.User, parameter.SAP_USER },
                    { RfcConfigParameters.Password, parameter.SAP_CLAVE },
                    { RfcConfigParameters.Client, GetCONFIGURACION().CLIENT },
                    { RfcConfigParameters.Language, GetCONFIGURACION().LANGUAGE },
                    { RfcConfigParameters.Name, GetCONFIGURACION().NAME }
                };


                RfcDestination dest = RfcDestinationManager.GetDestination(var_cof_parameter);

                RfcRepository repo = dest.Repository;
                IRfcFunction func = repo.CreateFunction("ZRFC_VL_MERMA");

                foreach (Parameter item in parameter.I_MATNR_LIST)
                {
                    func.SetValue("I_MATNR", item.I_MATNR);
                    func.Invoke(dest);

                    RESPUESTA rES = new RESPUESTA
                    {
                        E_MAKTX = func.GetString("E_MAKTX"),
                        E_RETURN = func.GetString("E_RETURN"),
                        E_MESSAGE = func.GetString("E_MESSAGE"),
                        CODIGO = item.I_MATNR
                    };
                    rESPUESTA.Add(rES);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(rESPUESTA);
        }
        //ZRFC_CB_MERMA (Llenado de ComboBox)
        [Route("soap/ZRFC_CB_MERMA")]
        [HttpPost]
        public IHttpActionResult ZRFC_CB_MERMA(Parameter parameter)
        {
            List<T_COMBO> t_COMBOs = new List<T_COMBO>();
            try
            {
                RfcConfigParameters var_cof_parameter = new RfcConfigParameters
                {
                    { RfcConfigParameters.AppServerHost, GetCONFIGURACION().APPSERVERHOST },
                    { RfcConfigParameters.SystemNumber, GetCONFIGURACION().SYSTEMNUMBER },
                    { RfcConfigParameters.SystemID, GetCONFIGURACION().SYSTEMID },
                    { RfcConfigParameters.User, parameter.SAP_USER },
                    { RfcConfigParameters.Password, parameter.SAP_CLAVE },
                    { RfcConfigParameters.Client, GetCONFIGURACION().CLIENT },
                    { RfcConfigParameters.Language, GetCONFIGURACION().LANGUAGE },
                    { RfcConfigParameters.Name, GetCONFIGURACION().NAME }
                };

                RfcDestination dest = RfcDestinationManager.GetDestination(var_cof_parameter);


                RfcRepository repo = dest.Repository;
                IRfcFunction func = repo.CreateFunction("ZRFC_CB_MERMA");

                //I_FECHA CHAR 8 DATUM Fecha creación 
                //I_CHECK CHAR 10 SYST_SHORT Mermas a obtener
                //I_USER CHAR 12 SYCHAR12 Usuario app(Usuario SAP)

                func.SetValue("I_FECHA", DateTime.Parse(parameter.I_FECHA));
                func.SetValue("I_CHECK", parameter.I_CHECK);
                func.SetValue("I_USER", parameter.I_USER);
                func.Invoke(dest);

                IRfcTable tabla = func["T_COMBO"].GetTable();
                foreach (IRfcStructure item in tabla)
                {
                    T_COMBO t_BWART = new T_COMBO
                    {
                        FOLIO = item.GetString("FOLIO"),
                        HORA = item.GetString("H_DIGIT"),
                        CENTRO = item.GetString("CENTRO"),
                        ALMACEN = item.GetString("ALMACEN"),
                        CLASE_MOV = item.GetString("CLSMOV")
                    };
                    t_COMBOs.Add(t_BWART);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(t_COMBOs);
        }
        //(Creación de merma)
        [Route("soap/ZRFC_CR_MERMA")]
        [HttpPost]
        public IHttpActionResult ZRFC_CR_MERMA(ModelParametro parameter)
        {

            RESPUESTA rESPUESTA = new RESPUESTA();
            try
            {
                RfcConfigParameters var_cof_parameter = new RfcConfigParameters
                {
                    { RfcConfigParameters.AppServerHost, GetCONFIGURACION().APPSERVERHOST },
                    { RfcConfigParameters.SystemNumber, GetCONFIGURACION().SYSTEMNUMBER },
                    { RfcConfigParameters.SystemID, GetCONFIGURACION().SYSTEMID },
                     { RfcConfigParameters.User, parameter.SAP_USER },
                    { RfcConfigParameters.Password, parameter.SAP_CLAVE },
                    { RfcConfigParameters.Client, GetCONFIGURACION().CLIENT },
                    { RfcConfigParameters.Language, GetCONFIGURACION().LANGUAGE },
                    { RfcConfigParameters.Name, GetCONFIGURACION().NAME }
                };

                RfcDestination dest = RfcDestinationManager.GetDestination(var_cof_parameter);


                RfcRepository repo = dest.Repository;
                IRfcFunction func = repo.CreateFunction("ZRFC_CR_MERMA");

                func.SetValue("I_BLDAT", DateTime.Parse(parameter.FECHA));
                func.SetValue("I_LGORT", parameter.ALMACEN);
                func.SetValue("I_WERKS", parameter.CENTRO);
                func.SetValue("I_BWART", parameter.CLASE_MOV);
                
                IRfcTable table = func.GetTable("T_ADD_MERMA");


                int count = 0;
                if (parameter.MERMAS != null)
                {
                    count = parameter.MERMAS.Count;
                }

                for (int i = 0; i < count; i++)
                {
                    table.Append();
                    table.SetValue("MATNR", parameter.MERMAS[i].PRODUCTO);
                    table.SetValue("CAJAS", parameter.MERMAS[i].CANT_CAJA);
                    table.SetValue("BOTELLAS", parameter.MERMAS[i].CANT_BOTE);
                    //table.SetValue("BWART", parameter.MERMAS[i].CLASE_MOV);
                    table.SetValue("BKTXT", parameter.MERMAS[i].MOTIVO);
                    table.SetValue("ARCHIVO", saveImage(parameter.MERMAS[i].IMAGE, parameter.MERMAS[i].IMG_NAME));


                }
                func.SetValue("T_ADD_MERMA", table);
                func.Invoke(dest);

                rESPUESTA.E_RETURN = func["E_RETURN"].GetString();
                rESPUESTA.E_MESSAGE = func["E_MESSAGE"].GetString();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(rESPUESTA);
        }
        //ZRFC_MD_MERMA (Modificación de merma)
        [Route("soap/ZRFC_MD_MERMA")]
        [HttpPost]
        public IHttpActionResult ZRFC_MD_MERMA(ModelMerma parameter)
        {
            RESPUESTA rESPUESTA = new RESPUESTA();
            try
            {

                RfcConfigParameters var_cof_parameter = new RfcConfigParameters
                {
                    { RfcConfigParameters.AppServerHost, GetCONFIGURACION().APPSERVERHOST },
                    { RfcConfigParameters.SystemNumber, GetCONFIGURACION().SYSTEMNUMBER },
                    { RfcConfigParameters.SystemID, GetCONFIGURACION().SYSTEMID },
                  { RfcConfigParameters.User, parameter.SAP_USER },
                    { RfcConfigParameters.Password, parameter.SAP_CLAVE },
                    { RfcConfigParameters.Client, GetCONFIGURACION().CLIENT },
                    { RfcConfigParameters.Language, GetCONFIGURACION().LANGUAGE },
                    { RfcConfigParameters.Name, GetCONFIGURACION().NAME }
                };

                RfcDestination dest = RfcDestinationManager.GetDestination(var_cof_parameter);


                RfcRepository repo = dest.Repository;
                IRfcFunction func = repo.CreateFunction("ZRFC_MD_MERMA");

                func.SetValue("I_IDENT", parameter.ID);
                func.SetValue("I_BKTXT", parameter.MOTIVO);
                func.SetValue("I_MATNR", parameter.PRODUCTO);
                func.SetValue("I_CAJAS", parameter.CANT_CAJA);
                func.SetValue("I_BOTELLAS", parameter.CANT_BOTE);
                //func.SetValue("I_BWART", parameter.CLASE_MOV);
                func.SetValue("I_ARCHIVO", saveImage(parameter.IMAGE, parameter.IMG_NAME));

                func.Invoke(dest);

                rESPUESTA.E_RETURN = func["E_RETURN"].GetString();
                rESPUESTA.E_MESSAGE = func["E_MESSAGE"].GetString();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(rESPUESTA);
        }
        //(Obtención datos de mantenedores)
        [Route("soap/ZRFC_MANT_VM")]
        [HttpPost]
        public IHttpActionResult ZRFC_MANT_VM(Parameter parameter)
        {
            RESPUESTA rESPUESTA = new RESPUESTA();

            try
            {
                RfcConfigParameters var_cof_parameter = new RfcConfigParameters
                {
                    { RfcConfigParameters.AppServerHost, GetCONFIGURACION().APPSERVERHOST },
                    { RfcConfigParameters.SystemNumber, GetCONFIGURACION().SYSTEMNUMBER },
                    { RfcConfigParameters.SystemID, GetCONFIGURACION().SYSTEMID },
                   { RfcConfigParameters.User, parameter.SAP_USER },
                    { RfcConfigParameters.Password, parameter.SAP_CLAVE },
                    { RfcConfigParameters.Client, GetCONFIGURACION().CLIENT },
                    { RfcConfigParameters.Language, GetCONFIGURACION().LANGUAGE },
                    { RfcConfigParameters.Name, GetCONFIGURACION().NAME }
                };

                RfcDestination dest = RfcDestinationManager.GetDestination(var_cof_parameter);


                RfcRepository repo = dest.Repository;
                IRfcFunction func = repo.CreateFunction("ZRFC_MANT_VM");

                func.SetValue("I_OPCION", parameter.I_OPCION);
                func.SetValue("I_WERKS", parameter.I_WERKS);
                func.Invoke(dest);
                IRfcTable tabla = null;
                switch (parameter.I_OPCION)
                {
                    case 1:
                        List<T_BWART> t_BWARTs = new List<T_BWART>();

                        tabla = func["T_BWART"].GetTable();
                        foreach (IRfcStructure item in tabla)
                        {
                            T_BWART t_BWART = new T_BWART
                            {
                                CLSMOV = item.GetString("CLSMOV"),
                                CLMOV_TEXT = item.GetString("CLMOV_TEXT"),
                                CLSMOV_A = item.GetString("CLSMOV_A")
                            };
                            t_BWARTs.Add(t_BWART);
                        }
                        rESPUESTA.T_BWART = t_BWARTs;
                        break;
                    case 2:
                        List<T_USERS> t_USERs = new List<T_USERS>();
                        tabla = func["T_USERS"].GetTable();
                        foreach (IRfcStructure item in tabla)
                        {
                            T_USERS t_USERS = new T_USERS
                            {
                                CENTRO = item.GetString("CENTRO"),
                                USUARIO = item.GetString("USUARIO"),
                                NOMUSR = item.GetString("NOMUSR"),
                                TRAMO = item.GetString("TRAMO")
                            };
                            t_USERs.Add(t_USERS);
                        }
                        rESPUESTA.T_USERS = t_USERs;
                        break;
                    case 3:
                        List<T_TRAMOS> t_TRAMOs = new List<T_TRAMOS>();
                        tabla = func["T_TRAMOS"].GetTable();
                        foreach (IRfcStructure item in tabla)
                        {
                            T_TRAMOS t_TRAMOS = new T_TRAMOS
                            {
                                TRAMO = item.GetString("TRAMO"),
                                CANT_MIN = item.GetString("CANT_MIN"),
                                CANT_MAX = item.GetString("CANT_MAX")
                            };
                            t_TRAMOs.Add(t_TRAMOS);
                        }
                        rESPUESTA.T_TRAMOS = t_TRAMOs;
                        break;
                    case 4:
                        List<T_TRXAL> t_TRXALs = new List<T_TRXAL>();
                        tabla = func["T_TRXAL"].GetTable();
                        foreach (IRfcStructure item in tabla)
                        {
                            T_TRXAL t_TRXAL = new T_TRXAL
                            {
                                CENTRO = item.GetString("CENTRO"),
                                ALMACEN = item.GetString("ALMACEN"),
                                TRAMO = item.GetString("TRAMO")
                            };
                            t_TRXALs.Add(t_TRXAL);
                        }
                        rESPUESTA.T_TRXAL = t_TRXALs;
                        break;
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(rESPUESTA);
        }
        //(Búsqueda de merma)
        [Route("soap/ZRFC_RD_VM")]
        [HttpPost]
        public IHttpActionResult ZRFC_RD_VM(Parameter parameter)
        {
            List<ModelT_MERMAS> modelT_MERMAs = new List<ModelT_MERMAS>();

            try
            {
                RfcConfigParameters var_cof_parameter = new RfcConfigParameters
                {
                    { RfcConfigParameters.AppServerHost, GetCONFIGURACION().APPSERVERHOST },
                    { RfcConfigParameters.SystemNumber, GetCONFIGURACION().SYSTEMNUMBER },
                    { RfcConfigParameters.SystemID, GetCONFIGURACION().SYSTEMID },
                { RfcConfigParameters.User, parameter.SAP_USER },
                    { RfcConfigParameters.Password, parameter.SAP_CLAVE },
                    { RfcConfigParameters.Client, GetCONFIGURACION().CLIENT },
                    { RfcConfigParameters.Language, GetCONFIGURACION().LANGUAGE },
                    { RfcConfigParameters.Name, GetCONFIGURACION().NAME }
                };

                RfcDestination dest = RfcDestinationManager.GetDestination(var_cof_parameter);


                RfcRepository repo = dest.Repository;
                IRfcFunction func = repo.CreateFunction("ZRFC_RD_MERMA");

                func.SetValue("I_CHECK", parameter.I_CHECK);
                func.SetValue("I_USER", parameter.SAP_USER);
                func.SetValue("I_FECHA", DateTime.Parse(parameter.I_FECHA));
                func.SetValue("I_FOLIO", parameter.I_FOLIO);
                func.Invoke(dest);
                IRfcTable tabla = null;
                tabla = func["T_MERMAS"].GetTable();
                foreach (IRfcStructure item in tabla)
                {
                    ModelT_MERMAS t_MERMAS = new ModelT_MERMAS
                    {
                        ID = item.GetString("ID"),
                        FOLIO = item.GetString("FOLIO"),
                        DOC_MAT = item.GetString("DOC_MAT"),
                        CENTRO = item.GetString("CENTRO"),
                        CENT_TXT = item.GetString("CENT_TXT"),
                        ALMACEN = item.GetString("ALMACEN"),
                        F_DIGIT = item.GetString("F_DIGIT"),
                        F_CONTB = item.GetString("F_CONTB"),
                        CLSMOV = item.GetString("CLSMOV"),
                        CLMOV_TEXT = item.GetString("CLMOV_TEXT"),
                        MOTIVO = item.GetString("MOTIVO"),
                        DIGITA = item.GetString("DIGITA"),
                        DGTA_TEXT = item.GetString("DGTA_TEXT"),
                        V_BUENO1 = item.GetString("V_BUENO1"),
                        V_BUENO2 = item.GetString("V_BUENO2"),
                        MAT_TEXT = item.GetString("MAT_TEXT"),
                        CAJAS = item.GetString("CAJAS"),
                        BOTELLAS = item.GetString("BOTELLAS"),
                        VALOR = item.GetString("VALOR"),
                        ESTADO = item.GetString("ESTADO"),
                        OBSERV = item.GetString("OBSERV"),
                        APROB = item.GetString("APROB"),
                        MATERIAL = item.GetString("MATERIAL"),
                        ARCHIVO = item.GetString("ARCHIVO"),
                        H_DIGIT = item.GetString("H_DIGIT")
                    };

                    modelT_MERMAs.Add(t_MERMAS);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok(modelT_MERMAs);
        }
        //(Eliminación de merma)
        [Route("soap/ZRFC_DL_MERMA")]
        [HttpPost]
        public IHttpActionResult ZRFC_DL_MERMA(Parameter parameter)
        {
            RESPUESTA rESPUESTA = new RESPUESTA();
            try
            {
                RfcConfigParameters var_cof_parameter = new RfcConfigParameters
                {
                    { RfcConfigParameters.AppServerHost, GetCONFIGURACION().APPSERVERHOST },
                    { RfcConfigParameters.SystemNumber, GetCONFIGURACION().SYSTEMNUMBER },
                    { RfcConfigParameters.SystemID, GetCONFIGURACION().SYSTEMID },
           { RfcConfigParameters.User, parameter.SAP_USER },
                    { RfcConfigParameters.Password, parameter.SAP_CLAVE },
                    { RfcConfigParameters.Client, GetCONFIGURACION().CLIENT },
                    { RfcConfigParameters.Language, GetCONFIGURACION().LANGUAGE },
                    { RfcConfigParameters.Name, GetCONFIGURACION().NAME }
                };

                RfcDestination dest = RfcDestinationManager.GetDestination(var_cof_parameter);


                RfcRepository repo = dest.Repository;
                IRfcFunction func = repo.CreateFunction("ZRFC_DL_MERMA");

                func.SetValue("I_IDENT", parameter.I_IDENT);
                func.Invoke(dest);

                rESPUESTA.E_RETURN = func["E_RETURN"].GetString();
                rESPUESTA.E_MESSAGE = func["E_MESSAGE"].GetString();
            }
            catch (Exception)
            {
                return BadRequest();
            }


            return Ok(rESPUESTA);
        }
        [Route("soap/login")]
        [HttpPost]
        public IHttpActionResult PostLogin(ModelParametro parameter)
        {
            RESPUESTA response = new RESPUESTA();
            try
            {
                RfcConfigParameters var_cof_parameter = new RfcConfigParameters
                {
                    { RfcConfigParameters.AppServerHost, GetCONFIGURACION().APPSERVERHOST },
                    { RfcConfigParameters.SystemNumber, GetCONFIGURACION().SYSTEMNUMBER },
                    { RfcConfigParameters.SystemID, GetCONFIGURACION().SYSTEMID },
                    { RfcConfigParameters.User, parameter.SAP_USER },
                    { RfcConfigParameters.Password, parameter.SAP_CLAVE },
                    { RfcConfigParameters.Client, GetCONFIGURACION().CLIENT },
                    { RfcConfigParameters.Language, GetCONFIGURACION().LANGUAGE },
                    { RfcConfigParameters.Name, GetCONFIGURACION().NAME }
                };

                RfcDestination dest = RfcDestinationManager.GetDestination(var_cof_parameter);


                RfcRepository repo = dest.Repository;
                response.ESTADO = "true";
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok(response);
        }
        public CONFIGURACION GetCONFIGURACION()
        {
            CONFIGURACION cONFIGURACION = new CONFIGURACION
            {
                APPSERVERHOST = ConfigurationManager.AppSettings["APPSERVERHOST"],
                SYSTEMNUMBER = ConfigurationManager.AppSettings["SYSTEMNUMBER"],
                SYSTEMID = ConfigurationManager.AppSettings["SYSTEMID"],
                CLIENT = ConfigurationManager.AppSettings["CLIENT"],
                LANGUAGE = ConfigurationManager.AppSettings["LANGUAGE"],
                NAME = ConfigurationManager.AppSettings["NAME"],
                USER = ConfigurationManager.AppSettings["USER"],
                CLAVE = ConfigurationManager.AppSettings["CLAVE"]

            };
            return cONFIGURACION;
        }
    }
}
