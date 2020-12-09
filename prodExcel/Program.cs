using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetLight;
using System.Configuration;
namespace prodExcel
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                var pathTemplate = ConfigurationManager.AppSettings["pathTemplate"];
                var pathDestino = ConfigurationManager.AppSettings["pathDestino"];
                SLDocument sl = new SLDocument(pathTemplate+"templateEpidemio.xlsx");
                string shortDateString = DateTime.Now.ToShortDateString();
                shortDateString = shortDateString.Replace("/", "_");
                List<GetExcel_Result> Lista = new List<GetExcel_Result>();
                using (AzumedEntities db = new AzumedEntities())
                {

                    sl.AddWorksheet("Desplaza");
                    sl.SelectWorksheet("Desplaza");



                    Lista = db.GetExcel().ToList();

                    string[] array = new string[Lista.Count];
                    Random rnd = new Random();
                    string[] horasviables = { "16:30", "16:33", "16:40", "16:43", "16:50", "16:53", "17:00", "17:03", "17:07", "17:10", "17:17", "17:20", "17:23", "17:30", "17:33", "17:40", "17:43", "17:50", "17:53", "18:00" };
                    int hIndex = rnd.Next(horasviables.Length);
                    int? localid = 0;
                    int cont = 0;
                    for (int i = 0; i < Lista.Count; i++)
                    {

                        if (localid == Lista[i].idSolicitudSCE)
                        {
                           // Console.WriteLine("dato previamente insertado");
                        }
                        else
                        {
                            for (int o = cont; o < cont + 11; o++) { array[o] = Convert.ToString(Lista[o].Respuesta); }//Iteracion de respuestas
                            int x = cont;
                            int x1 = cont + 1;
                            int x2 = cont + 2;
                            int x3 = cont + 3;
                            int x4 = cont + 4;
                            int x5 = cont + 5;
                            int x6 = cont + 6;
                            int x7 = cont + 7;
                            int x8 = cont + 8;
                            int x9 = cont + 9;
                            int x10 = cont + 10;
                            int x11 = cont + 11;
                            string n = array[x1] + array[x2] + array[x];
                            sl.CopyWorksheet("Formato", n);
                            sl.SelectWorksheet(n);
                            char sexo = Convert.ToChar(array[x3]);
                            if (sexo == 'M')
                            {
                                sl.SetCellValue("D18", array[x3]);
                            }
                            else
                            {
                                sl.SetCellValue("D19", array[x3]);
                            }

                            string[] newFecha = array[x4].Split('-');
                            sl.SetCellValue("I16", newFecha[0]);
                            sl.SetCellValue("G16", newFecha[1]);
                            sl.SetCellValue("E16", newFecha[2]);
                            sl.SetCellValue("E14", array[x1]);
                            sl.SetCellValue("K14", array[x2]);
                            sl.SetCellValue("Q14", array[x]);
                            sl.SetCellValue("N16", "AQUI VA EL CURP");
                            sl.SetCellValue("E27", Lista[i].Calle);
                            sl.SetCellValue("M27", Lista[i].NoExtrerior);
                            sl.SetCellValue("R27", Lista[i].NoInterior);
                            sl.SetCellValue("E31", Lista[i].Colonia);
                            sl.SetCellValue("J31", Lista[i].CP + "");
                            sl.SetCellValue("P31", Lista[i].Telefono);
                            //Apellido Paterno E14 *
                            //Apellido Materno K14*
                            //Nombres Q14*
                            //Dia Nac E16*
                            //Mes Nac G16*
                            //Año Nac I16*
                            //CURP N16*
                            //SEXO H D18*
                            //SEXI N D19*
                            //CALLE E27
                            //NUMERO EXTERNO M27
                            //NUMERO INTERNO R27
                            //COLONIA E31
                            //C.P. J31
                            //TELEFONO P31

                            localid = Lista[i].idSolicitudSCE;

                        }
                        cont++;
                    }
                    sl.SelectWorksheet("Formato");
                    sl.DeleteWorksheet("Desplaza");
                    sl.SaveAs(pathDestino+"cuestionarios_" + shortDateString + "_.xlsx");
                }



            }
            catch (Exception e)
            {


                using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"log.txt", true))
                {
                    file.WriteLine(e.Message);
                }
            }


        }
    }
}
