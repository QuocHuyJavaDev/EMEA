using Oracle.ManagedDataAccess.Client;

namespace EMEA_API.DBConnect
{
    public class AbsDb
    {
        public static OracleConnection conn = new OracleConnection("Data Source=(LQHDB =(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)" +
            "(HOST = DESKTOP-RPJP4E8)" +
           "(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = lqhdb))));user id=EMEADB;password=EMEADB");
    }
}
