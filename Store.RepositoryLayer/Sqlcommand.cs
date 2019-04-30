using System.Data.SqlClient;

namespace Store.RepositoryLayer
{
    internal class Sqlcommand
    {
        private string commandText;
        private SqlConnection sqlConnection;

        public Sqlcommand(string commandText, SqlConnection sqlConnection)
        {
            this.commandText = commandText;
            this.sqlConnection = sqlConnection;
        }
    }
}