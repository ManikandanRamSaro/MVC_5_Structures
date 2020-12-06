using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace ServiceProvider.SqlService
{
    public class SqlHelper
    {
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);

        public void CloseConnection()
        {
            connect.Close();
        }
        public void connectDBUsingStoredProcedureNoReturns(string[] param,string[] value,string spName)
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand(spName, connect);
            cmd.CommandType = CommandType.StoredProcedure;
            for(int l=0;l<param.Length;l++)
            {
                cmd.Parameters.AddWithValue(param[l], value[l]);
            }
            cmd.ExecuteNonQuery();
            connect.Close();
        }

        public SqlDataReader connectDBUsingStoredProcedure(string[] param, string[] value, string spName)
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand(spName, connect);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int l = 0; l < param.Length; l++)
            {
                cmd.Parameters.AddWithValue(param[l], value[l]);
            }
            SqlDataReader read = cmd.ExecuteReader();
            return read;
        }
        public DataSet connectDBUsingStoredProcedureDataSet(string[] param, string[] value, string spName)
        {
            DataSet dataset = new DataSet();
            connect.Open();
            SqlDataAdapter cmd = new SqlDataAdapter(spName, connect);
            cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
            for (int l = 0; l < param.Length; l++)
            {
                cmd.SelectCommand.Parameters.AddWithValue(param[l], value[l]);
            }
            cmd.Fill(dataset);
            connect.Close();
            return dataset;
        }

    }
}