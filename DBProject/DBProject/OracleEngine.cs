using System;
using System.Data;
using System.Data.OracleClient;

namespace SqlProject
{
    class OracleEngine
    {
        private static OracleEngine instance = null;
        private OracleDataAdapter dataAdapter1;
        private OracleConnection oracleConnection1;

        private OracleEngine() {
            OracleConnectionStringBuilder myCStringB = new OracleConnectionStringBuilder();
            myCStringB.UserID = "system";//put in username
            myCStringB.Password = "system";//put in password
            myCStringB.DataSource = "XE";//use this for connecting to Machon lev
            oracleConnection1 = new OracleConnection(myCStringB.ConnectionString);
            oracleConnection1.Open();
            //initialize oracle and non oracle objects
            dataAdapter1 = new OracleDataAdapter();
        }

        public static OracleEngine getInstance() {
            if (instance == null)
                instance = new OracleEngine();
            return instance;
        }

        public DataTable execSelectCommand(string SQLquery){
            dataAdapter1.SelectCommand = new OracleCommand();
            //prepare data adapter for sql query
            dataAdapter1.SelectCommand.Connection = oracleConnection1;
            dataAdapter1.SelectCommand.CommandText = SQLquery;

            DataTable dt = new DataTable();
            dataAdapter1.Fill(dt);
            //attach source in data grid for displaying results - Data GRid View is names dgvSelect
            return dt;
        }

        public bool execCommand(string SQLquery)
        {
            try
            {
                OracleCommand Command = new OracleCommand();
                //prepare data adapter for sql query
                Command.Connection = oracleConnection1;
                Command.CommandText = SQLquery;
                Command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool execStoredProcedure(String name, OracleParameter[] paramaters)
        {
            try
            {
                OracleCommand callProcCommand = new OracleCommand();
                callProcCommand.Connection = oracleConnection1;
                // callProcCommand.CommandType =
                callProcCommand.CommandType = CommandType.StoredProcedure;
                callProcCommand.CommandText = "addStudent";
                callProcCommand.Parameters.AddRange(paramaters);
                callProcCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
