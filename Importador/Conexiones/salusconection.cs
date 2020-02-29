using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;



namespace Importador.Conexiones
{
    class salusconection
    {
        public SqlConnection con = new
                  SqlConnection(@"server=172.16.1.18\QSOFT;database=SALUS_HOSPITAL_MARANATHA;MultipleActiveResultSets=true ;Trusted_Connection=false; password=QsoftMSDEsa2005; user id=sa;");

        public SqlConnection conectarSalus()
        {
            try
            {
                con.Open();
                return con;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                throw e;
            }
        }
        public bool desconectarSalus()
        {
            con.Dispose();
            return true;
        }

       
    }
}
