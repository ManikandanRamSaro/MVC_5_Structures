using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceProvider.SqlService;
using System.Data;
using System.Data.SqlClient;
using ServiceProvider.Service;
using StructuredMVC.Models;

using System.Reflection;
namespace StructuredMVC.Controllers
{
    public class DashboardController : Controller
    {
        SqlHelper connect = new SqlHelper();
        SupportService support = new SupportService();
        // GET: Dashboard
        public ActionResult Index()
        {
            try
            {
                string[] param = { "@type", "@name", "@email", "@date", "@status" };
                string[] values = { "Mani","Manikandan","manikandan.r@gmail.com","2020-12-5","i" };
                connect.connectDBUsingStoredProcedureNoReturns(param, values, "SP_addComments");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return View();
        }

        public string ReturnOperation()
        {
            string dat = "";
            try
            {
                string[] param = { "@type", "@extra" };
                string[] values = { "Mani", "Manikandan"  };
                SqlDataReader read=connect.connectDBUsingStoredProcedure(param, values, "SP_Returns");
                if(read.Read())
                {
                    while(read.Read())
                    {
                        dat+=" "+read[1].ToString();
                        Console.WriteLine(read[1].ToString());
                    }
                }
                else
                {
                    Console.WriteLine("No data found");
                }
                read.Close();
                connect.CloseConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return dat;
        }
        [HttpGet]
        public string ReturnDatatable()
        {
            string dat = "works";
            try
            {
                string[] param = { "@type", "@extra" };
                string[] values = { "Mani", "Manikandan" };
                DataSet ds = connect.connectDBUsingStoredProcedureDataSet(param, values, "SP_Returns");
                DataTable dt = ds.Tables[0];

                dat += dt.Rows.Count;
            }
            catch (Exception e)
            {
                dat=e.ToString();
                throw;
            }
            return dat;
        }
        [HttpGet]
        public string ReturnList()
        {
            string dat = "works"; 
            List<Self> drlist = new List<Self>(); 
            try
            {
                string[] param = { "@type", "@extra" };
                string[] values = { "Mani", "Manikandan" };
                DataSet ds = connect.connectDBUsingStoredProcedureDataSet(param, values, "SP_Returns");
                DataTable dt = ds.Tables[0];

                DataTable newTable = support.convertProperDataTable(dt);
                drlist = support.ConvertDataTable<Self>(newTable);


            
            }
            catch (Exception e)
            {
                dat = e.ToString();
                throw;
            }
            dat += drlist.Count.ToString();
            return dat;
        }
        [HttpGet]
        public string DataTableCheck()
        {
            string dat = "works";
            List<DataRow> drlist = new List<DataRow>();
            try
            {
                DataTable dt = new DataTable("Student");
                dt.Columns.Add("StudentId", typeof(Int32));
                dt.Columns.Add("StudentName", typeof(string));
                dt.Columns.Add("Address", typeof(string));
                dt.Columns.Add("MobileNo", typeof(string));
                //Data  
                dt.Rows.Add(1, "Manish", "Hyderabad", "0000000000");
                dt.Rows.Add(2, "Venkat", "Hyderabad", "111111111");
                dt.Rows.Add(3, "Namit", "Pune", "1222222222");
                dt.Rows.Add(4, "Abhinav", "Bhagalpur", "3333333333");
                List<Student> studentDetails = new List<Student>();
                studentDetails = ConvertDataTable<Student>(dt);

                List<Student> studentDetailsClass = new List<Student>();
                studentDetailsClass = support.ConvertDataTable<Student>(dt);
                
            }
            catch (Exception e)
            {
                dat = e.ToString();
                throw;
            }
            dat += drlist.Count.ToString();
            return dat;
        }
         

    }
  
}