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

        private OracleEngine()
        {
            OracleConnectionStringBuilder myCStringB = new OracleConnectionStringBuilder();
            myCStringB.UserID = "system";//put in username
            myCStringB.Password = "system";//put in password
            myCStringB.DataSource = "XE";//use this for connecting to Machon lev
            oracleConnection1 = new OracleConnection(myCStringB.ConnectionString);
            //initialize oracle and non oracle objects
            dataAdapter1 = new OracleDataAdapter();
        }

        public static OracleEngine getInstance()
        {
            if (instance == null)
                instance = new OracleEngine();
            return instance;
        }

        internal OracleParameter createParamater(string v, OracleType number, string text, ParameterDirection input = ParameterDirection.Input)
        {
            OracleParameter tmp = new OracleParameter(v, number);
            tmp.Value = text;
            tmp.Direction = input;
            return tmp;
        }

        public DataTable execSelectCommand(string SQLquery, OracleParameter[] InParams = null)
        {
            try
            {
                dataAdapter1.SelectCommand = new OracleCommand();
                //prepare data adapter for sql query
                dataAdapter1.SelectCommand.Connection = oracleConnection1;
                dataAdapter1.SelectCommand.CommandText = SQLquery;
                if (InParams != null)
                    dataAdapter1.SelectCommand.Parameters.AddRange(InParams);
                oracleConnection1.Open();
                DataTable dt = new DataTable();
                dataAdapter1.Fill(dt);
                oracleConnection1.Close();
                //attach source in data grid for displaying results - Data GRid View is names dgvSelect
                return dt;
            }
            catch (Exception ex)
            {
                if (oracleConnection1.State == ConnectionState.Open)
                    oracleConnection1.Close();
                return null;
            }
        }

        public object execCommand(string SQLquery, OracleParameter[] InParams = null, OracleParameter OutParams = null)
        {
            try
            {
                OracleCommand Command = new OracleCommand();
                //prepare data adapter for sql query
                Command.Connection = oracleConnection1;
                Command.CommandText = SQLquery;
                if (InParams != null)
                    Command.Parameters.AddRange(InParams);
                if (OutParams != null)
                    Command.Parameters.Add(OutParams);
                oracleConnection1.Open();
                Command.ExecuteNonQuery();
                oracleConnection1.Close();
                if (OutParams != null)
                    return OutParams.Value;
                else
                    return true;
            }
            catch (Exception ex)
            {
                if (oracleConnection1.State == ConnectionState.Open)
                    oracleConnection1.Close();
                return false;
            }
        }

        public object execStoredProcedure(String name, OracleParameter[] InParams = null, OracleParameter OutParams = null)
        {
            try
            {
                OracleCommand callProcCommand = new OracleCommand();
                callProcCommand.Connection = oracleConnection1;
                // callProcCommand.CommandType =
                callProcCommand.CommandType = CommandType.StoredProcedure;
                callProcCommand.CommandText = name; 
                if (InParams != null)
                    callProcCommand.Parameters.AddRange(InParams);
                if (OutParams != null)
                    callProcCommand.Parameters.Add(OutParams);
                oracleConnection1.Open();
                callProcCommand.ExecuteNonQuery();
                oracleConnection1.Close();
                if (OutParams != null)
                {
                    if (OutParams.OracleType == OracleType.Cursor)
                    {
                        DataTable dt = new DataTable();
                        OracleDataAdapter adapter = new OracleDataAdapter(callProcCommand);
                        adapter.Fill(dt);
                        return dt;
                    }
                    else
                    {
                        return OutParams.Value;
                    }
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                if (oracleConnection1.State == ConnectionState.Open)
                    oracleConnection1.Close();
                return false;
            }
        }
    }
}
