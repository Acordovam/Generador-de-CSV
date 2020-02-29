using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Importador.Conexiones;
using System.Data.SqlClient;

namespace Importador.csv
{
    class generarCSV
    {
        // Export path and file.
        public salusconection conexion = new salusconection();

        // Stream writer for CSV file.
        StreamWriter csvFile = null;
        public void generarArchivo(string exportCsv, string sqlText, int consulta)
        {
            Console.WriteLine("Generando Archivo");
            // Check to see if the file path exists.
                
                try
                {
                    
                    conexion.conectarSalus();
                    Console.WriteLine("Conexión Establecida");
                    Console.WriteLine("Consultando Información");
                    // Query text incorporated into SQL command.
                    SqlCommand sqlSelect = new SqlCommand(sqlText, conexion.con);

                    Console.WriteLine("Ejecutando el Reader");
                    // Execute SQL and place data in a reader object.
                    SqlDataReader reader = sqlSelect.ExecuteReader();

                    // Stream writer for CSV file.
                    csvFile = new StreamWriter(exportCsv);
                    Console.WriteLine("Escribiendo en Archivo");
                    // Add the headers to the CSV file.
                    if (consulta == 1)
                    {
                        csvFile.WriteLine(String.Format("\"{0}\",\"{1}\",\"{2}\"," +
                        "\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\"",

                        reader.GetName(0), reader.GetName(1), reader.GetName(2),
                        reader.GetName(3), reader.GetName(4), reader.GetName(5),
                        reader.GetName(6), reader.GetName(7)));
                    }
                    else if (consulta == 2)
                    {
                        csvFile.WriteLine(String.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"",

                        reader.GetName(0), reader.GetName(1), reader.GetName(2),
                        reader.GetName(3), reader.GetName(4), reader.GetName(5)));
                    }
                  

                    // Construct CSV file data rows.
                    while (reader.Read())
                    {

                        // Add line from reader object to new CSV file.
                        if (consulta == 1)
                        {
                            csvFile.WriteLine(String.Format("\"{0}\",\"{1}\",\"{2}\"," +
                            "\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\"",
                            reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7]));
                        }
                        else if (consulta == 2)
                        {
                            csvFile.WriteLine(String.Format("\"{0}\",\"{1}\",\"{2}\"," +
                            "\"{3}\",\"{4}\",\"{5}\"",
                            reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]));
                        }
                    }

                    // Message stating export successful.
                    Console.WriteLine("Exportación de Datos Completa.");

                }
                catch (Exception e)
                {

                    // Message stating export unsuccessful.
                    Console.WriteLine("Exportación de Datos Fallida.");
                    System.Environment.Exit(0);

                }
                finally
                {

                    // Close the database connection and CSV file.
                    conexion.desconectarSalus();
                    csvFile.Close();

                }
        }
    }
}
