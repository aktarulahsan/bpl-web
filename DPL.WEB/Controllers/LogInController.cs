using DPL.WEB.App_Start;
using DPL.WEB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace DPL.WEB.Controllers
{
    public class LogInController : Controller
    {
        MPO Obj = new MPO();
        string u = "";
        string p = "";
        //
        // GET: /LogIn/
        public ActionResult LogIn()
        
        
        
        {
            LogInMo login = new LogInMo();
            return View("LogIn");
        }

   
     

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LogInMo login)
        {

            string name = "";
             u = login.username;
             p = login.password;


             if (login.userLevel != 0)
             {
                 Obj = mGetUserPassword("0001", u);
                 string databaseUser = Obj.strUserID;
                 string databasePass = Obj.strUserPassword;
                  name = AuthUser(databaseUser, databasePass);
             }

            if (!(String.IsNullOrEmpty(name)))
            {
                //Session["UserName"] = name;
                //var emp = _leaveAppService.mFillEmployeeImage("0001", Session["UserName"].ToString());
                //Session["User"] = emp;
                //return RedirectToAction("Index", "LeaveApplication");



                if (login.userLevel == 0)
                {


                    string UserID = login.username;
                    Session["USERID"] = login.username;
                    Session["userLevel"] = login.userLevel;
                    return RedirectToAction("Index", "Home");
                }
                else if (login.userLevel == 2)
                {
                    string UserID = login.username;
                    Session["USERID"] = login.username;
                    Session["userLevel"] = login.userLevel;
                    return RedirectToAction("MpoView", "SalesOrder", new { Area = "Transaction" });
                }
                else if (login.userLevel == 3)
                {
                    string UserID = login.username;
                    Session["USERID"] = login.username;
                    Session["userLevel"] = login.userLevel;
                    return RedirectToAction("AreaHeadView", "SalesOrder", new { Area = "Transaction" });
                }
                else
                {
                    string UserID = login.username;
                    Session["USERID"] = login.username;
                    Session["userLevel"] = login.userLevel;
                    return RedirectToAction("AreaHeadView", "SalesOrder", new { Area = "Transaction" });
                }
            }

            return RedirectToAction("LogIn", "LogIn", new { returnUrl = UrlParameter.Optional });


        }



        //if (login.userLevel == 0)
        //{
        //        string UserID = login.username;
        //        Session["USERID"] = login.username;
        //        Session["userLevel"] = login.userLevel;
        //        return RedirectToAction("Index", "Home");
        //}
        //else if (login.userLevel == 2)
        //{
        //    string UserID = login.username;
        //    Session["USERID"] = login.username;
        //    Session["userLevel"] = login.userLevel;
        //    return RedirectToAction("MpoView", "SalesOrder", new { Area = "Transaction" });
        //}
        //else
        //{
        //    return RedirectToAction("Index", "Home");
        //}

        public string AuthUser(string username, string password)
        {
            if (password.Equals(p) && username.Equals(u))
                return username;
            else
                return null;
        }


        public MPO  mGetUserPassword(string strDeComID, string strUserID)
        {
            string strSQL = "";

            SqlDataReader drGetGroup;
        
         
            SqlCommand cmd = new SqlCommand();

            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                cmd.Connection = gcnMain;
                strSQL = "SELECT USER_ID,PASSWORD FROM USER_ONLILE_SECURITY WHERE STATUS=0 AND USER_ID ='" + strUserID + "' ";
                cmd.CommandText = strSQL;
                drGetGroup = cmd.ExecuteReader();
                if (drGetGroup.Read())
                {

                    Obj.strUserID = drGetGroup["USER_ID"].ToString();
                    Obj.strUserPassword = drGetGroup["PASSWORD"].ToString();
                }
                else
                {
                    Obj.strUserID = "";
                    Obj.strUserPassword = "";
                }
                drGetGroup.Close();

                return Obj;

            }
        }


    }
}