//using DPL.WEB.App_Start;
using DPL.WEB.Models;
using Dutility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace DPL.WEB.Areas.Transaction.Controllers
{
    public class SalesOrderController : Controller
    {
        List<AccountsVoucher> ooAcmss = new List<AccountsVoucher>();
        List<OrderDetails> ooAcms = new List<OrderDetails>();

        string strLedgerName = "";
        string strBranchName = "";
        string strLedgerNameMearz = "";
        public ActionResult SalesOrder()
        {

            ViewBag.BranchName = mGetBranch();
            //ViewBag.RefNo = Utility.gstrLastNumber("0003", 12);
            return View();
        }

        public ActionResult LastOrderNO()
        {

            return Json(Utility.gstrLastNumber("0003", 12), JsonRequestBehavior.AllowGet);
        }
       //[AuthorizationFilter]
        public ActionResult Logout()
        {
            Session["USERID"] = null;
            Session["userLevel"] = null;
            return RedirectToAction("LogIn", "LogIn");
        }

        public ActionResult AreaHeadView()
        {
            if (Session["USERID"].ToString() != null)
            {

                if (Session["userLevel"].ToString() == "3")
                {
                    mGetAreaLedgerInfo(Session["USERID"].ToString());
                    ViewBag.MName = strLedgerName;
                    ViewBag.MNameMerz = "Area Head" + ":" + strLedgerName;
                    ViewBag.BranchName = strBranchName;
                    ViewBag.UserLevel = 3;
                    ViewBag.RefNo = Utility.gstrLastNumber("0003", 12);
                    return View("AreaHeadView");
                }
                else
                {
                    mGetAreaLedgerInfo(Session["USERID"].ToString());
                    ViewBag.MName = strLedgerName;
                    ViewBag.MNameMerz = "Division Head" + ":" + strLedgerName;
                    ViewBag.BranchName = strBranchName;
                    ViewBag.UserLevel = 4;
                    ViewBag.RefNo = Utility.gstrLastNumber("0003", 12);
                    return View("AreaHeadView");
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult MpoView()
        {
            if (Session["USERID"].ToString() != null)
            {
                mGetLedgerInfo(Session["USERID"].ToString());
                ViewBag.MName = strLedgerName;
                ViewBag.MNameMerz = strLedgerNameMearz;
                ViewBag.BranchName = strBranchName;
                ViewBag.RefNo = Utility.gstrLastNumber("0003", 12);
                return View();
            }
            else
            {
                return View();
            }
        }
        public string mGetAreaLedgerInfo(string responseId)
        {

            string strSQL;
            SqlDataReader drGetGroup;
            List<Ledger> oogrp = new List<Ledger>();
            BranchRespons response = new BranchRespons();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            //strSQL = "select   L.AREA, ACC_BRANCH.BRANCH_NAME  ,l.LEDGER_NAME_MERZE,l.LEDGER_NAME from ACC_LEDGER_Z_D_A L  , ACC_BRANCH where  L.BRANCH_ID= ACC_BRANCH.BRANCH_ID and L.BRANCH_ID='0001' and l.LEDGER_STATUS=0  ";
            //if (responseId != null)
            //{
            //    strSQL = strSQL + " and L.AREA ='AH-AM-Md. Jalal Uddin-Sunamganj' ";
            //}
            //strSQL = strSQL + "order by l.LEDGER_NAME_MERZE ";

            strSQL = "select  LEDGER_NAME,B.BRANCH_NAME ";
            strSQL = strSQL + " from USER_ONLILE_SECURITY U,ACC_BRANCH B  ";
            if (responseId != null)
            {
                strSQL = strSQL + "where  u.USER_ID='" + responseId + "'   ";
            }
            strSQL = strSQL + "and BRANCH_ID= B.BRANCH_ID  and STATUS=0  ";
            strSQL = strSQL + "and b.BRANCH_ID='0001' ";


            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                drGetGroup.Read();
                {
                    strLedgerName = drGetGroup["LEDGER_NAME"].ToString();
                    strBranchName = drGetGroup["BRANCH_NAME"].ToString();

                }
                drGetGroup.Close();
                gcnMain.Dispose();
                response.Ledgerdata = oogrp;

                return "";


            }

        }
        public string mGetLedgerInfo(string responseId)
        {

            string strSQL;
            SqlDataReader drGetGroup;
            List<Ledger> oogrp = new List<Ledger>();
            BranchRespons response = new BranchRespons();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            //strSQL = "select  ACC_BRANCH.BRANCH_NAME  ,l.LEDGER_NAME_MERZE,l.LEDGER_NAME from ACC_LEDGER L  , ACC_BRANCH where  L.BRANCH_ID= ACC_BRANCH.BRANCH_ID and L.BRANCH_ID='0001' and l.LEDGER_STATUS=0  ";
            //if (responseId != null)
            //{
            //    strSQL = strSQL + " and TERITORRY_CODE='" + responseId + "' ";
            //    //strSQL = strSQL + " and TERITORRY_CODE='221' ";
            //}
            //strSQL = strSQL + "order by l.LEDGER_NAME_MERZE ";

            strSQL = "select  L.LEDGER_NAME, L.LEDGER_NAME_MERZE,B.BRANCH_NAME,L.LEDGER_NAME_MERZE  from USER_ONLILE_SECURITY U, ACC_LEDGER L,ACC_BRANCH B ";
            strSQL = strSQL + "where u.TC=L.TERITORRY_CODE  ";
            if (responseId !="")
            {
                strSQL = strSQL + " and  u.USER_ID='" + responseId + "' ";

            }
           strSQL = strSQL + " and L.LEDGER_GROUP=202 and L.BRANCH_ID= B.BRANCH_ID ";

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                drGetGroup.Read();
                {
                    strLedgerNameMearz = drGetGroup["LEDGER_NAME_MERZE"].ToString();
                    strLedgerName = drGetGroup["LEDGER_NAME"].ToString();
                    strBranchName = drGetGroup["BRANCH_NAME"].ToString();

                }
                drGetGroup.Close();
                gcnMain.Dispose();
                response.Ledgerdata = oogrp;

                return "";


            }

        }
        public List<Branch> mGetBranch()
        {

            string strSQL;
            SqlDataReader drGetGroup;
            List<Branch> oogrp = new List<Branch>();
            BranchRespons response = new BranchRespons();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            strSQL = "SELECT BRANCH_NAME from ACC_BRANCH ORDER BY BRANCH_NAME ";

            //if (searchTerm != null)
            //{
            //    strSQL = "SELECT GODOWNS_SERIAL,GODOWNS_NAME,BRANCH_ID,GODOWNS_DEFAULT,GODOWNS_PHONE,GODOWNS_ADDRESS1 FROM INV_GODOWNS WHERE GODOWNS_NAME=" + "'" + searchTerm + "'";
            //}
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {
                    Branch ogrp = new Branch();
                    //ogrp.lngSlNo = Convert.ToInt64(drGetGroup["GODOWNS_SERIAL"].ToString());
                    //ogrp.strLocation = drGetGroup["GODOWNS_NAME"].ToString();
                    //ogrp.strPhone = drGetGroup["GODOWNS_PHONE"].ToString();
                    //ogrp.strAddres1 = drGetGroup["GODOWNS_ADDRESS1"].ToString();

                    if (drGetGroup["BRANCH_NAME"].ToString() != "")
                    {
                        //ogrp.strBranch = Utility.gstrGetBranchName("0001", drGetGroup["BRANCH_ID"].ToString());
                        ogrp.strBranch = drGetGroup["BRANCH_NAME"].ToString();
                    }
                    else
                    {
                        ogrp.strBranch = "";
                    }
                    //ogrp.lngDefault = Convert.ToInt32(drGetGroup["GODOWNS_DEFAULT"]);
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                return oogrp;
            }

        }

        public JsonResult mGetLocation(string strBranchName)
        {

            string strSQL;
            string strBranchId = "";
            SqlDataReader drGetGroup;
            List<Location> oogrp = new List<Location>();
            LocationResponse response = new LocationResponse();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            if (strBranchName != "")
            {
                strBranchId = Utility.gstrGetBranchID("0003", strBranchName);
            }

            strSQL = "SELECT GODOWNS_SERIAL,GODOWNS_NAME,BRANCH_ID,GODOWNS_DEFAULT,GODOWNS_PHONE,GODOWNS_ADDRESS1 FROM INV_GODOWNS  ";
            strSQL = strSQL + " WHERE INV_GODOWNS.SECTION_STATUS=0  ";
            if (strBranchId != "")
            {
                strSQL = strSQL + " AND INV_GODOWNS.BRANCH_ID='" + strBranchId + "'";
            }
            strSQL = strSQL + "AND GODOWNS_NAME IN('Main Location')  ORDER BY GODOWNS_NAME ";

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {
                    Location ogrp = new Location();
                    ogrp.lngSlNo = Convert.ToInt64(drGetGroup["GODOWNS_SERIAL"].ToString());
                    ogrp.strLocation = drGetGroup["GODOWNS_NAME"].ToString();
                    ogrp.strPhone = drGetGroup["GODOWNS_PHONE"].ToString();
                    ogrp.strAddres1 = drGetGroup["GODOWNS_ADDRESS1"].ToString();

                    if (drGetGroup["BRANCH_ID"].ToString() != "")
                    {
                        ogrp.strBranch = Utility.gstrGetBranchName("0001", drGetGroup["BRANCH_ID"].ToString());
                    }
                    else
                    {
                        ogrp.strBranch = "";
                    }
                    ogrp.lngDefault = Convert.ToInt32(drGetGroup["GODOWNS_DEFAULT"]);
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                response.data = oogrp;

                return Json(oogrp, JsonRequestBehavior.AllowGet);


            }

        }
        public JsonResult mGetAreaMPO(string strCompaniID, string strBranchName, string strDivision_Area_Head, int userlevel)
        {

            string strSQL;
            string strBranchId = "";
            SqlDataReader drGetGroup;
            List<Ledger> oogrp = new List<Ledger>();
            BranchRespons response = new BranchRespons();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            if (strBranchName != "")
            {
                strBranchId = Utility.gstrGetBranchID("0003", strBranchName);
            }
         
            strSQL = " select l.LEDGER_NAME_MERZE,l.LEDGER_NAME  from ACC_LEDGER_Z_D_A L where l.LEDGER_STATUS=0 ";
            if (userlevel == 3)
            {
                if (strDivision_Area_Head != "")
                {
                    strSQL = strSQL + " and AREA='" + strDivision_Area_Head + "'";
                }
            }
            else
            {
                if (strDivision_Area_Head != "")
                {
                    strSQL = strSQL + " and DIVISION='" + strDivision_Area_Head + "'";
                }
            }
            strSQL = strSQL + " and   L.BRANCH_ID='" + strBranchId + "' ";



            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {
                    Ledger ogrp = new Ledger();
                    ogrp.strLedgerName = drGetGroup["LEDGER_NAME_MERZE"].ToString();
                    ogrp.strTC = drGetGroup["LEDGER_NAME"].ToString();
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                response.Ledgerdata = oogrp;

                return Json(oogrp, JsonRequestBehavior.AllowGet);


            }

        }
        public JsonResult mGetMPO(string strCompaniID,string strBranchName)
        {

            string strSQL;
            string strBranchId = "";
            SqlDataReader drGetGroup;
            List<Ledger> oogrp = new List<Ledger>();
            BranchRespons response = new BranchRespons();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            if (strBranchName != "")
            {
                strBranchId = Utility.gstrGetBranchID("0003", strBranchName);
            }


            strSQL = "select l.LEDGER_NAME_MERZE,l.LEDGER_NAME from ACC_LEDGER L where l.LEDGER_STATUS=0  ";
            if (strBranchName != "")
            {
                strSQL = strSQL + " and   L.BRANCH_ID='" + strBranchId + "' ";
            }
            //if (strTerritory != null)
            //{
            //    strSQL = strSQL + " and TERITORRY_CODE='" + strTerritory + "' ";
            //}
            strSQL = strSQL + "order by l.LEDGER_NAME_MERZE ";

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {
                    Ledger ogrp = new Ledger();
                    //ogrp.lngSlNo = Convert.ToInt64(drGetGroup["LEDGER_NAME_MERZE"].ToString());
                    ogrp.strLedgerName = drGetGroup["LEDGER_NAME_MERZE"].ToString();
                    ogrp.strTC = drGetGroup["LEDGER_NAME"].ToString();
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                response.Ledgerdata = oogrp;

                return Json(oogrp, JsonRequestBehavior.AllowGet);


            }

        }
        public JsonResult mGetCustomer(string strLedgerName)
        {

            string strSQL;
            SqlDataReader drGetGroup;
            List<Ledger> oogrp = new List<Ledger>();
            BranchRespons response = new BranchRespons();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            strSQL = "SELECT L.LEDGER_NAME_MERZE,L.LEDGER_CODE,L.LEDGER_NAME from ACC_LEDGER L where l.LEDGER_GROUP=204 and l.LEDGER_REP_NAME= '" + strLedgerName + "' ORDER BY L.LEDGER_NAME_MERZE ";

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {
                    Ledger ogrp = new Ledger();
                    //ogrp.lngSlNo = Convert.ToInt64(drGetGroup["LEDGER_NAME_MERZE"].ToString());
                    ogrp.strCustomerName = drGetGroup["LEDGER_NAME_MERZE"].ToString();
                    ogrp.strCustomerCode = drGetGroup["LEDGER_CODE"].ToString();
                    ogrp.strDoctorName = drGetGroup["LEDGER_NAME"].ToString();
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                response.Ledgerdata = oogrp;

                return Json(oogrp, JsonRequestBehavior.AllowGet);


            }

        }
        public JsonResult mFillStockGroup(string strDeComID, string strPrefix, string vstrUserName)
        {
            string strSQL;
            SqlDataReader drGetGroup;
            List<StockItem> oogrp = new List<StockItem>();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            strSQL = "SELECT DISTINCT INV_STOCKGROUP.STOCKGROUP_NAME  FROM INV_STOCKITEM ,INV_STOCKGROUP WHERE INV_STOCKITEM.STOCKGROUP_NAME=INV_STOCKGROUP.STOCKGROUP_NAME ";
            strSQL = strSQL + "and INV_STOCKGROUP.STOCKGROUP_PRIMARY='Finished Goods' ";
            if (vstrUserName != "")
            {
                strSQL = strSQL + "AND INV_STOCKGROUP.STOCKGROUP_NAME IN (SELECT STOCKGROUP_NAME  FROM USER_PRIVILEGES_STOCKGROUP WHERE USER_LOGIN_NAME='" + vstrUserName + "')";
            }
            if (strPrefix == "SI")
            {
                strSQL = strSQL + "and G_STATUS =0 ";
            }
            else if (strPrefix == "SIN")
            {
                strSQL = strSQL + "and G_STATUS IN (0,1) ";
            }
            else if (strPrefix == "SAMPLE")
            {
                strSQL = strSQL + "and G_STATUS IN (0,1) ";
                strSQL = strSQL + "and INV_STOCKGROUP.STOCKGROUP_NAME like '%Sample' ";
            }
            strSQL = strSQL + " Order By INV_STOCKGROUP.STOCKGROUP_NAME ASC ";
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {
                    StockItem ogrp = new StockItem();
                    ogrp.strItemGroup = drGetGroup["STOCKGROUP_NAME"].ToString();
                    oogrp.Add(ogrp);

                }
                drGetGroup.Close();
                gcnMain.Dispose();
                //response.Ledgerdata = oogrp;

                return Json(oogrp, JsonRequestBehavior.AllowGet);
                //drGetGroup.Close();
                //gcnMain.Dispose();
                //return oogrp;

            }
        }
        public JsonResult gFillStockItemNew(string strDeComID, string vstrRoot, string vstrGodown)
        {
            string strSQL;
            SqlDataReader drGetGroup;
            List<StockItem> oogrp = new List<StockItem>();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            strSQL = "SELECT INV_STOCKITEM.STOCKITEM_NAME,INV_STOCKITEM.STOCKITEM_BASEUNITS,SUM(INV_TRAN.INV_TRAN_QUANTITY) CLS";
            strSQL = strSQL + " FROM INV_TRAN ,INV_STOCKITEM WHERE INV_STOCKITEM.STOCKITEM_NAME =INV_TRAN.STOCKITEM_NAME ";
            if (vstrRoot != "")
            {
                strSQL = strSQL + " AND INV_STOCKITEM.STOCKGROUP_NAME = '" + vstrRoot + "' ";
            }
            strSQL = strSQL + " AND INV_TRAN.GODOWNS_NAME='" + vstrGodown + "' ";
            strSQL = strSQL + "AND INV_STOCKITEM.STOCKITEM_STATUS=0 ";
            strSQL = strSQL + "GROUP BY INV_STOCKITEM.STOCKITEM_NAME,INV_STOCKITEM.STOCKITEM_BASEUNITS  ORDER by INV_STOCKITEM.STOCKITEM_NAME ASc ";


            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {
                    StockItem ogrp = new StockItem();
                    ogrp.strItemName = drGetGroup["STOCKITEM_NAME"].ToString();
                    ogrp.strUnit = drGetGroup["STOCKITEM_BASEUNITS"].ToString();
                    ogrp.dblClsBalance = Convert.ToDouble(drGetGroup["CLS"].ToString());
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                return Json(oogrp, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult gFillStockItemRate(string strDeComID, string strItemGroup, string strItemName, string strOrderDate, int intQty, string strBranchName)
        {
            //string strSQL;
            //SqlDataReader drGetGroup;
            List<OrderDetails> oogrp = new List<OrderDetails>();

            //DateTime ddsd = Utility.cvtSQLDateString(strOrderDate);
            //DateTime d;
            //if (DateTime.TryParseExact(encrypt[1], "dd/MM/yyyy", CultureInfo.InvariantCulture, out d))
            //{ }
            //else if (DateTime.TryParseExact(encrypt[1], "yyyy/MM/dd", CultureInfo.InvariantCulture, out d))
            //{ }
            //string myString = DrpDD.SelectedItem.Text + " " + DrpMM.SelectedItem.Text + " " + DrpYY.SelectedItem.Text;

            //DateTime myDate = DateTime.ParseExact(strOrderDate, "dd MM yy", null);

            double dblrate = 0;


            string strUOM = "", strPowerClass = "", strPackSize = "", strSubGroup = "";
            strSubGroup = Utility.mGetStockGroupFromItemGroup(strDeComID, strItemGroup);
            strUOM = Utility.gGetBaseUOM(strDeComID, strItemName);
            strPowerClass = Utility.mGetPowerClass(strDeComID, strItemName);
            strPackSize = Utility.mGetPackSize(strDeComID, strItemName);
            dblrate = Utility.gdblGetEnterpriseSalesPrice(strDeComID, strItemName, strOrderDate, intQty, 0, "");
            string strBranchID = Utility.gstrGetBranchID(strDeComID, strBranchName);

            //double dblbonus = Math.Round(Utility.mdblGetBonus(strDeComID, strItemName, strBranchID, intQty, "14/09/2021"), 2);
            double dblbonus = Math.Round(Utility.mdblGetBonus(strDeComID, strItemName, strBranchID, intQty, strOrderDate), 2);
            //double dblbonusQty = 4;
            OrderDetails ogrp = new OrderDetails();

            ogrp.strUnit = strUOM;
            ogrp.strPowerClass = strPowerClass;
            ogrp.strPackSize = strPackSize;
            ogrp.dblBranchRate = dblrate;
            ogrp.dblbonusQty = dblbonus;
            ogrp.strSubGroup_Name = strSubGroup;
            oogrp.Add(ogrp);

            return Json(ogrp, JsonRequestBehavior.AllowGet);

            //}
        }

        public JsonResult mShowMasterData(string strUserName, int intStatusCol)
        {


            int mintVType = (int)Utility.VOUCHER_TYPE.vtSALES_ORDER;
            //Utility.gstrUserName, 
            string mdteVFromDate = Utility.FirstDayOfMonth(DateTime.Now).ToString("dd-MM-yyyy");
            string mdteVToDate = Utility.LastDayOfMonth(DateTime.Now).ToString("dd-MM-yyyy");
            //string mdteVFromDate = "01-04-2021";
            //string mdteVToDate = "01-08-2021";
            string strmySql = "", strFind = "", strExpression = "", strDeComID = "0003";


            int intAreaStaus = 0, intSPJ = 0;
            mintVType = (int)(Utility.VOUCHER_TYPE.vtSALES_ORDER);



            string strSQL = null;
            SqlDataReader dr;
            List<AccountsVoucher> ooAccLedger = new List<AccountsVoucher>();
            SqlCommand cmdInsert = new SqlCommand();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                cmdInsert.Connection = gcnMain;
                if (strmySql == "")
                {
                    if (mintVType == 1)
                    {

                        strSQL = "SELECT ACC_COMPANY_VOUCHER.APPS_COMM_CAL,ACC_COMPANY_VOUCHER.COMP_REF_NO ,ACC_COMPANY_VOUCHER.COMP_VOUCHER_TYPE,ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE,ACC_LEDGER.LEDGER_NAME_MERZE ,ACC_LEDGER.LEDGER_NAME  ,";
                        strSQL = strSQL + "RV_VOUCHER_VIEW.LEDGER_NAME VOUCHER_REVERSE_LEDGER,  ACC_BRANCH.BRANCH_NAME, ACC_COMPANY_VOUCHER.COMP_VOUCHER_NET_AMOUNT, ACC_COMPANY_VOUCHER.COMP_VOUCHER_NET_AMOUNT, ";
                        strSQL = strSQL + " ACC_LEDGER.LEDGER_NAME_MERZE,ACC_COMPANY_VOUCHER.APP_STATUS,APPS_SI_RET,'' DIVISION,'' AREA,ACC_COMPANY_VOUCHER.COMP_VOUCHER_STATUS  ";
                        strSQL = strSQL + " FROM RV_VOUCHER_VIEW,ACC_BRANCH,ACC_LEDGER,ACC_COMPANY_VOUCHER  WHERE ACC_COMPANY_VOUCHER.COMP_REF_NO =RV_VOUCHER_VIEW.COMP_REF_NO AND ACC_LEDGER.LEDGER_NAME =ACC_COMPANY_VOUCHER.LEDGER_NAME  AND ";
                        strSQL = strSQL + " ACC_BRANCH.BRANCH_ID =ACC_COMPANY_VOUCHER.BRANCH_ID ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_TYPE = " + mintVType + " ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER.SP_JOURNAL= " + intSPJ + " ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER.SAMPLE_STATUS=0 ";
                        if (strFind == "Voucher Number")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_REF_NO like '%" + strExpression + "%'";
                        }
                        else if (strFind == "Voucher Date")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        else if (strFind == "Ledger Name")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.LEDGER_NAME = '" + strExpression + "'";
                        }
                        else if (strFind == "Branch Name")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.BRANCH_NAME =  '" + Utility.gstrGetBranchName(strDeComID, strExpression) + "'";
                        }
                        else if (strFind == "Amount")
                        {
                            if (strExpression != "")
                            {
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_NET_AMOUNT like '%" + strExpression + "%'";
                            }
                        }
                        else if (strFind == "Narrations")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_NARRATION = '" + strExpression + "'";
                        }
                        else
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.BRANCH_ID in (select BRANCH_ID from USER_PRIVILEGES_BRANCH where USER_LOGIN_NAME  ='" + strUserName + "')";
                        strSQL = strSQL + " ORDER By ACC_COMPANY_VOUCHER.COMP_REF_NO,ACC_LEDGER.TERITORRY_CODE,ACC_LEDGER.LEDGER_CODE,ACC_LEDGER.LEDGER_NAME  ";

                    }
                    else if (mintVType == 12)
                    {
                        strSQL = "SELECT distinct t.GODOWNS_NAME, ACC_LEDGER_Z_D_A.LEDGER_NAME ,ACC_COMPANY_VOUCHER_BRANCH_VIEW.SALES_REP  , ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_SERIAL ,ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL,ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_REF_NO,ACC_COMPANY_VOUCHER_BRANCH_VIEW.REF_NO,ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_TYPE,ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_DATE,";
                        strSQL = strSQL + "ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME , ACC_COMPANY_VOUCHER_BRANCH_VIEW.BRANCH_NAME, ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_AMOUNT, ";
                        strSQL = strSQL + "ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_NET_AMOUNT,ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_CODE,ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME_MERZE,'' VOUCHER_REVERSE_LEDGER ";
                        strSQL = strSQL + ",APP_STATUS,APPS_SI_RET,ACC_LEDGER_Z_D_A.DIVISION,ACC_LEDGER_Z_D_A.AREA,ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS From ACC_COMPANY_VOUCHER_BRANCH_VIEW,ACC_LEDGER_Z_D_A, ACC_BILL_TRAN t WHERE  ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_REF_NO=t.COMP_REF_NO and ACC_LEDGER_Z_D_A.LEDGER_NAME =ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME  ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_TYPE = " + mintVType + " ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.SP_JOURNAL= " + intSPJ + " ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.SAMPLE_STATUS=0 ";

                        if (mintVType == (int)Utility.VOUCHER_TYPE.vtSALES_ORDER)
                        {
                            if ((intStatusCol != 5) && (intStatusCol != 1))
                            {
                                if (intAreaStaus == 1)
                                {
                                    strSQL = strSQL + " AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APP_STATUS =0 ";
                                    strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS=0 ";
                                }

                            }

                        }

                        if (strFind == "Voucher Number")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_REF_NO like '%" + strExpression + "%'";
                        }
                        else if (strFind == "Voucher Date")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        else if (strFind == "Ledger Name")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME = '" + strExpression + "'";
                        }
                        else if (strFind == "Branch Name")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.BRANCH_NAME =  '" + Utility.gstrGetBranchName(strDeComID, strExpression) + "'";
                        }
                        else if (strFind == "Amount")
                        {
                            if (strExpression != "")
                            {
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_NET_AMOUNT like '%" + strExpression + "%'";
                            }
                        }
                        else if (strFind == "Narrations")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_NARRATION = '" + strExpression + "'";
                        }
                        else
                        {
                            if (mdteVFromDate != "")
                            {
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_DATE BETWEEN ";
                                strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                                strSQL = strSQL + "AND ";
                                strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                            }
                        }
                        //strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.BRANCH_ID in (select BRANCH_ID from USER_PRIVILEGES_BRANCH where USER_LOGIN_NAME  ='" + strUserName + "')";
                        //strSQL = strSQL + " AND ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + strUserName + "')";

                        //strSQL = strSQL + " AND ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + strUserName + "')";

                        strSQL = strSQL + "and ACC_LEDGER_Z_D_A.LEDGER_NAME='" + strUserName + "' ";

                        if (intStatusCol != 0)
                        {
                            if (intStatusCol == 1)
                            {
                                //bill Done
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL= 1  ";
                                strSQL = strSQL + "AND APP_STATUS=4  ";
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS= 1 ";
                            }
                            if (intStatusCol == 2)
                            {
                                //Order Return
                                strSQL = strSQL + "AND APPS_SI_RET IN(1,2) ";
                            }
                            if (intStatusCol == 3)
                            {
                                //Commi.Cal
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL=1 ";
                                strSQL = strSQL + "AND APPS_SI_RET <>1 ";
                                strSQL = strSQL + "AND APP_STATUS IN(1,0)  ";
                            }
                            if (intStatusCol == 4)
                            {
                                //New order
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL IN(0)  ";
                                strSQL = strSQL + "AND APP_STATUS=0  ";
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS= 0  ";
                            }
                            if (intStatusCol == 5)
                            {
                                //ZH Approved
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL= 1  ";
                                strSQL = strSQL + "AND APP_STATUS=2  ";
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS= 0  ";
                            }
                        }


                        strSQL = strSQL + " ORDER By ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_SERIAL DESC,ACC_COMPANY_VOUCHER_BRANCH_VIEW.REF_NO,ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_CODE,ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME  ";

                    }

                    else
                    {
                        strSQL = "SELECT ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL,COMP_REF_NO,REF_NO,COMP_VOUCHER_TYPE,COMP_VOUCHER_DATE,";
                        strSQL = strSQL + "LEDGER_NAME , BRANCH_NAME, COMP_VOUCHER_AMOUNT, COMP_VOUCHER_NET_AMOUNT,LEDGER_CODE,LEDGER_NAME_MERZE,'' VOUCHER_REVERSE_LEDGER ";
                        strSQL = strSQL + ",APP_STATUS,APPS_SI_RET,'' DIVISION,'' AREA,COMP_VOUCHER_STATUS  From ACC_COMPANY_VOUCHER_BRANCH_VIEW ";
                        strSQL = strSQL + "WHERE COMP_VOUCHER_TYPE = " + mintVType + " ";
                        strSQL = strSQL + " AND SP_JOURNAL= " + intSPJ + " ";
                        strSQL = strSQL + " AND SAMPLE_STATUS=0 ";
                        if (mintVType == (int)Utility.VOUCHER_TYPE.vtSALES_ORDER)
                        {
                            if (intAreaStaus == 1)
                            {
                                strSQL = strSQL + " AND APP_STATUS =0 ";
                            }
                            else
                            {
                                strSQL = strSQL + " AND APP_STATUS =1 ";
                            }
                        }
                        if (strFind == "Voucher Number")
                        {
                            strSQL = strSQL + "AND COMP_REF_NO like '%" + strExpression + "%'";
                        }
                        else if (strFind == "Voucher Date")
                        {
                            strSQL = strSQL + "AND COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        else if (strFind == "Ledger Name")
                        {
                            strSQL = strSQL + "AND LEDGER_NAME = '" + strExpression + "'";
                        }
                        else if (strFind == "Branch Name")
                        {
                            strSQL = strSQL + "AND BRANCH_NAME =  '" + Utility.gstrGetBranchName(strDeComID, strExpression) + "'";
                        }
                        else if (strFind == "Amount")
                        {
                            if (strExpression != "")
                            {
                                strSQL = strSQL + "AND COMP_VOUCHER_NET_AMOUNT like '%" + strExpression + "%'";
                            }
                        }
                        else if (strFind == "Narrations")
                        {
                            strSQL = strSQL + "AND COMP_VOUCHER_NARRATION = '" + strExpression + "'";
                        }
                        else
                        {
                            strSQL = strSQL + "AND COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        strSQL = strSQL + "AND BRANCH_ID in (select BRANCH_ID from USER_PRIVILEGES_BRANCH where USER_LOGIN_NAME  ='" + strUserName + "')";
                        strSQL = strSQL + " ORDER By REF_NO,TERITORRY_CODE,LEDGER_CODE,LEDGER_NAME  ";
                    }
                }
                else
                {
                    strSQL = strmySql;
                }

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AccountsVoucher oLedg = new AccountsVoucher();
                    oLedg.strVoucherNoMerz = dr["COMP_REF_NO"].ToString();
                    oLedg.strVoucherNo = Utility.Mid(dr["COMP_REF_NO"].ToString(), 6, dr["COMP_REF_NO"].ToString().Length - 6);
                    oLedg.strLedgerName = dr["LEDGER_NAME"].ToString();
                    oLedg.strLocation = dr["GODOWNS_NAME"].ToString();
                    oLedg.strDoctorName = dr["SALES_REP"].ToString();
                    oLedg.strBranchName = dr["BRANCH_NAME"].ToString();
                    oLedg.strTranDate = Convert.ToDateTime(dr["COMP_VOUCHER_DATE"]).ToString("dd/MM/yyyy");
                    oLedg.dblAmount = Convert.ToDouble(dr["COMP_VOUCHER_NET_AMOUNT"].ToString());

                    if (dr["VOUCHER_REVERSE_LEDGER"].ToString() != "")
                    {
                        oLedg.strReverseLegder = dr["VOUCHER_REVERSE_LEDGER"].ToString();
                    }
                    else
                    {
                        oLedg.strReverseLegder = "";
                    }

                    oLedg.strMerzeName = dr["LEDGER_NAME_MERZE"].ToString();
                    oLedg.intAppStatus = Convert.ToInt16(dr["APP_STATUS"].ToString());
                    oLedg.intvoucherPos = Convert.ToInt16(dr["APPS_COMM_CAL"].ToString());
                    oLedg.intAppSIRet = Convert.ToInt16(dr["APPS_SI_RET"].ToString());
                    if (dr["DIVISION"].ToString() != "")
                    {
                        oLedg.strDivisionName = dr["DIVISION"].ToString();
                    }
                    else
                    {
                        oLedg.strDivisionName = "";
                    }
                    if (dr["AREA"].ToString() != "")
                    {
                        oLedg.strArea = dr["AREA"].ToString();
                    }
                    else
                    {
                        oLedg.strArea = "";
                    }
                    if (intStatusCol != 5)
                    {
                        oLedg.dblCreditAmount = Convert.ToDouble(creditlimit(dr["LEDGER_NAME"].ToString()).ToString());
                        oLedg.intStatus = 1;
                    }
                    else
                    {
                        oLedg.dblCreditAmount = 0;
                        oLedg.intStatus = 2;
                    }
                    oLedg.strTC = dr["LEDGER_NAME"].ToString();
                    oLedg.intStatus = Convert.ToInt16(dr["COMP_VOUCHER_STATUS"].ToString());
                    oLedg.strPreserveSQL = strSQL;

                    ooAccLedger.Add(oLedg);
                }
                //return ooAccLedger;
                dr.Close();
                gcnMain.Close();
                gcnMain.Dispose();

                return Json(ooAccLedger, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult mShowMasterDataArea(string strUserName, int intStatusCol)
        {


            int mintVType = (int)Utility.VOUCHER_TYPE.vtSALES_ORDER;
            //Utility.gstrUserName, 
            string mdteVFromDate = Utility.FirstDayOfMonth(DateTime.Now).ToString("dd-MM-yyyy");
            string mdteVToDate = Utility.LastDayOfMonth(DateTime.Now).ToString("dd-MM-yyyy");
            //string mdteVFromDate = "01-04-2021";
            //string mdteVToDate = "01-08-2021";
            string strmySql = "", strFind = "", strExpression = "", strDeComID = "0003";


            int intAreaStaus = 0, intSPJ = 0;
            mintVType = (int)(Utility.VOUCHER_TYPE.vtSALES_ORDER);

            //int mintVType = (int)Utility.VOUCHER_TYPE.vtSALES_ORDER;
            ////Utility.gstrUserName, 
            //string mdteVFromDate = Utility.FirstDayOfMonth(DateTime.Now).ToString("dd-MM-yyyy");

            //string  strmySql = "", strFind = "", mdteVFromDate = "01-04-2021", strExpression = "", mdteVToDate = "01-08-2021", strDeComID = "0003";

            //int mintVType = 0;
            //int intAreaStaus = 1, intSPJ = 0, intStatusCol = 0;
            //mintVType = (int)(Utility.VOUCHER_TYPE.vtSALES_ORDER);

            string strSQL = null;
            SqlDataReader dr;
            List<AccountsVoucher> ooAccLedger = new List<AccountsVoucher>();
            SqlCommand cmdInsert = new SqlCommand();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                cmdInsert.Connection = gcnMain;
                if (strmySql == "")
                {
                    if (mintVType == 1)
                    {

                        strSQL = "SELECT ACC_COMPANY_VOUCHER.APPS_COMM_CAL,ACC_COMPANY_VOUCHER.COMP_REF_NO ,ACC_COMPANY_VOUCHER.COMP_VOUCHER_TYPE,ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE,ACC_LEDGER.LEDGER_NAME_MERZE ,ACC_LEDGER.LEDGER_NAME  ,";
                        strSQL = strSQL + "RV_VOUCHER_VIEW.LEDGER_NAME VOUCHER_REVERSE_LEDGER,  ACC_BRANCH.BRANCH_NAME, ACC_COMPANY_VOUCHER.COMP_VOUCHER_NET_AMOUNT, ACC_COMPANY_VOUCHER.COMP_VOUCHER_NET_AMOUNT, ";
                        strSQL = strSQL + " ACC_LEDGER.LEDGER_NAME_MERZE,ACC_COMPANY_VOUCHER.APP_STATUS,APPS_SI_RET,'' DIVISION,'' AREA,ACC_COMPANY_VOUCHER.COMP_VOUCHER_STATUS  ";
                        strSQL = strSQL + " FROM RV_VOUCHER_VIEW,ACC_BRANCH,ACC_LEDGER,ACC_COMPANY_VOUCHER  WHERE ACC_COMPANY_VOUCHER.COMP_REF_NO =RV_VOUCHER_VIEW.COMP_REF_NO AND ACC_LEDGER.LEDGER_NAME =ACC_COMPANY_VOUCHER.LEDGER_NAME  AND ";
                        strSQL = strSQL + " ACC_BRANCH.BRANCH_ID =ACC_COMPANY_VOUCHER.BRANCH_ID ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_TYPE = " + mintVType + " ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER.SP_JOURNAL= " + intSPJ + " ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER.SAMPLE_STATUS=0 ";
                        if (strFind == "Voucher Number")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_REF_NO like '%" + strExpression + "%'";
                        }
                        else if (strFind == "Voucher Date")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        else if (strFind == "Ledger Name")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.LEDGER_NAME = '" + strExpression + "'";
                        }
                        else if (strFind == "Branch Name")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.BRANCH_NAME =  '" + Utility.gstrGetBranchName(strDeComID, strExpression) + "'";
                        }
                        else if (strFind == "Amount")
                        {
                            if (strExpression != "")
                            {
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_NET_AMOUNT like '%" + strExpression + "%'";
                            }
                        }
                        else if (strFind == "Narrations")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_NARRATION = '" + strExpression + "'";
                        }
                        else
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.BRANCH_ID in (select BRANCH_ID from USER_PRIVILEGES_BRANCH where USER_LOGIN_NAME  ='" + strUserName + "')";
                        strSQL = strSQL + " ORDER By ACC_COMPANY_VOUCHER.COMP_REF_NO,ACC_LEDGER.TERITORRY_CODE,ACC_LEDGER.LEDGER_CODE,ACC_LEDGER.LEDGER_NAME  ";

                    }
                    else if (mintVType == 12)
                    {
                        strSQL = "SELECT distinct t.GODOWNS_NAME, ACC_LEDGER_Z_D_A.LEDGER_NAME ,ACC_COMPANY_VOUCHER_BRANCH_VIEW.SALES_REP  , ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_SERIAL ,ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL,ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_REF_NO,ACC_COMPANY_VOUCHER_BRANCH_VIEW.REF_NO,ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_TYPE,ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_DATE,";
                        strSQL = strSQL + "ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME , ACC_COMPANY_VOUCHER_BRANCH_VIEW.BRANCH_NAME, ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_AMOUNT, ";
                        strSQL = strSQL + "ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_NET_AMOUNT,ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_CODE,ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME_MERZE,'' VOUCHER_REVERSE_LEDGER ";
                        strSQL = strSQL + ",APP_STATUS,APPS_SI_RET,ACC_LEDGER_Z_D_A.DIVISION,ACC_LEDGER_Z_D_A.AREA,ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS From ACC_COMPANY_VOUCHER_BRANCH_VIEW,ACC_LEDGER_Z_D_A, ACC_BILL_TRAN t WHERE  ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_REF_NO=t.COMP_REF_NO and ACC_LEDGER_Z_D_A.LEDGER_NAME =ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME  ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_TYPE = " + mintVType + " ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.SP_JOURNAL= " + intSPJ + " ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.SAMPLE_STATUS=0 ";

                        if (mintVType == (int)Utility.VOUCHER_TYPE.vtSALES_ORDER)
                        {
                            if ((intStatusCol != 5) && (intStatusCol != 1))
                            {
                                if (intAreaStaus == 1)
                                {
                                    strSQL = strSQL + " AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APP_STATUS =0 ";
                                    strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS=0 ";
                                }

                            }

                        }

                        if (strFind == "Voucher Number")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_REF_NO like '%" + strExpression + "%'";
                        }
                        else if (strFind == "Voucher Date")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        else if (strFind == "Ledger Name")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME = '" + strExpression + "'";
                        }
                        else if (strFind == "Branch Name")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.BRANCH_NAME =  '" + Utility.gstrGetBranchName(strDeComID, strExpression) + "'";
                        }
                        else if (strFind == "Amount")
                        {
                            if (strExpression != "")
                            {
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_NET_AMOUNT like '%" + strExpression + "%'";
                            }
                        }
                        else if (strFind == "Narrations")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_NARRATION = '" + strExpression + "'";
                        }
                        else
                        {
                            if (mdteVFromDate != "")
                            {
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_DATE BETWEEN ";
                                strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                                strSQL = strSQL + "AND ";
                                strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                            }
                        }
                        //strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.BRANCH_ID in (select BRANCH_ID from USER_PRIVILEGES_BRANCH where USER_LOGIN_NAME  ='" + strUserName + "')";
                        //strSQL = strSQL + " AND ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + strUserName + "')";

                        //strSQL = strSQL + " AND ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + strUserName + "')";

                        strSQL = strSQL + "and ACC_LEDGER_Z_D_A.AREA='" + strUserName + "' ";

                        if (intStatusCol != 0)
                        {
                            if (intStatusCol == 1)
                            {
                                //bill Done
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL= 1  ";
                                strSQL = strSQL + "AND APP_STATUS=4  ";
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS= 1 ";
                            }
                            if (intStatusCol == 2)
                            {
                                //Order Return
                                strSQL = strSQL + "AND APPS_SI_RET IN(1,2) ";
                            }
                            if (intStatusCol == 3)
                            {
                                //Commi.Cal
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL=1 ";
                                strSQL = strSQL + "AND APPS_SI_RET <>1 ";
                                strSQL = strSQL + "AND APP_STATUS IN(1,0)  ";
                            }
                            if (intStatusCol == 4)
                            {
                                //New order
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL IN(0)  ";
                                strSQL = strSQL + "AND APP_STATUS=1  ";
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS= 0  ";
                            }
                            if (intStatusCol == 5)
                            {
                                //ZH Approved
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL= 1  ";
                                strSQL = strSQL + "AND APP_STATUS=2  ";
                                //strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS= 0  ";
                            }
                        }


                        strSQL = strSQL + " ORDER By ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_SERIAL DESC,ACC_COMPANY_VOUCHER_BRANCH_VIEW.REF_NO,ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_CODE,ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME  ";

                    }

                    else
                    {
                        strSQL = "SELECT ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL,COMP_REF_NO,REF_NO,COMP_VOUCHER_TYPE,COMP_VOUCHER_DATE,";
                        strSQL = strSQL + "LEDGER_NAME , BRANCH_NAME, COMP_VOUCHER_AMOUNT, COMP_VOUCHER_NET_AMOUNT,LEDGER_CODE,LEDGER_NAME_MERZE,'' VOUCHER_REVERSE_LEDGER ";
                        strSQL = strSQL + ",APP_STATUS,APPS_SI_RET,'' DIVISION,'' AREA,COMP_VOUCHER_STATUS  From ACC_COMPANY_VOUCHER_BRANCH_VIEW ";
                        strSQL = strSQL + "WHERE COMP_VOUCHER_TYPE = " + mintVType + " ";
                        strSQL = strSQL + " AND SP_JOURNAL= " + intSPJ + " ";
                        strSQL = strSQL + " AND SAMPLE_STATUS=0 ";
                        if (mintVType == (int)Utility.VOUCHER_TYPE.vtSALES_ORDER)
                        {
                            if (intAreaStaus == 1)
                            {
                                strSQL = strSQL + " AND APP_STATUS =0 ";
                            }
                            else
                            {
                                strSQL = strSQL + " AND APP_STATUS =1 ";
                            }
                        }
                        if (strFind == "Voucher Number")
                        {
                            strSQL = strSQL + "AND COMP_REF_NO like '%" + strExpression + "%'";
                        }
                        else if (strFind == "Voucher Date")
                        {
                            strSQL = strSQL + "AND COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        else if (strFind == "Ledger Name")
                        {
                            strSQL = strSQL + "AND LEDGER_NAME = '" + strExpression + "'";
                        }
                        else if (strFind == "Branch Name")
                        {
                            strSQL = strSQL + "AND BRANCH_NAME =  '" + Utility.gstrGetBranchName(strDeComID, strExpression) + "'";
                        }
                        else if (strFind == "Amount")
                        {
                            if (strExpression != "")
                            {
                                strSQL = strSQL + "AND COMP_VOUCHER_NET_AMOUNT like '%" + strExpression + "%'";
                            }
                        }
                        else if (strFind == "Narrations")
                        {
                            strSQL = strSQL + "AND COMP_VOUCHER_NARRATION = '" + strExpression + "'";
                        }
                        else
                        {
                            strSQL = strSQL + "AND COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        strSQL = strSQL + "AND BRANCH_ID in (select BRANCH_ID from USER_PRIVILEGES_BRANCH where USER_LOGIN_NAME  ='" + strUserName + "')";
                        strSQL = strSQL + " ORDER By REF_NO,TERITORRY_CODE,LEDGER_CODE,LEDGER_NAME  ";
                    }
                }
                else
                {
                    strSQL = strmySql;
                }

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AccountsVoucher oLedg = new AccountsVoucher();
                    oLedg.strVoucherNoMerz = dr["COMP_REF_NO"].ToString();
                    oLedg.strVoucherNo = Utility.Mid(dr["COMP_REF_NO"].ToString(), 6, dr["COMP_REF_NO"].ToString().Length - 6);
                    oLedg.strLedgerName = dr["LEDGER_NAME"].ToString();
                    oLedg.strLocation = dr["GODOWNS_NAME"].ToString();
                    oLedg.strDoctorName = dr["SALES_REP"].ToString();
                    oLedg.strBranchName = dr["BRANCH_NAME"].ToString();
                    oLedg.strTranDate = Convert.ToDateTime(dr["COMP_VOUCHER_DATE"]).ToString("dd/MM/yyyy");
                    oLedg.dblAmount = Convert.ToDouble(dr["COMP_VOUCHER_NET_AMOUNT"].ToString());

                    if (dr["VOUCHER_REVERSE_LEDGER"].ToString() != "")
                    {
                        oLedg.strReverseLegder = dr["VOUCHER_REVERSE_LEDGER"].ToString();
                    }
                    else
                    {
                        oLedg.strReverseLegder = "";
                    }

                    oLedg.strMerzeName = dr["LEDGER_NAME_MERZE"].ToString();
                    oLedg.intAppStatus = Convert.ToInt16(dr["APP_STATUS"].ToString());
                    oLedg.intvoucherPos = Convert.ToInt16(dr["APPS_COMM_CAL"].ToString());
                    oLedg.intAppSIRet = Convert.ToInt16(dr["APPS_SI_RET"].ToString());
                    if (dr["DIVISION"].ToString() != "")
                    {
                        oLedg.strDivisionName = dr["DIVISION"].ToString();
                    }
                    else
                    {
                        oLedg.strDivisionName = "";
                    }
                    if (dr["AREA"].ToString() != "")
                    {
                        oLedg.strArea = dr["AREA"].ToString();
                    }
                    else
                    {
                        oLedg.strArea = "";
                    }
                    if (intStatusCol != 5)
                    {
                        oLedg.dblCreditAmount = Convert.ToDouble(creditlimit(dr["LEDGER_NAME"].ToString()).ToString());
                        oLedg.intStatus = 1;
                    }
                    else
                    {
                        oLedg.dblCreditAmount = 0;
                        oLedg.intStatus = 2;
                    }
                    oLedg.strTC = dr["LEDGER_NAME"].ToString();
                    oLedg.intStatus = Convert.ToInt16(dr["COMP_VOUCHER_STATUS"].ToString());
                    oLedg.strPreserveSQL = strSQL;

                    ooAccLedger.Add(oLedg);
                }
                //return ooAccLedger;
                dr.Close();
                gcnMain.Close();
                gcnMain.Dispose();

                return Json(ooAccLedger, JsonRequestBehavior.AllowGet);

            }

        }
        private string creditlimit(string strMedicalRep)
        {

            string strComID = "0003";
            int i = 0;
            double lblCreditLimit = 0;
            var lblPending = "";
            string lblCurrentBalance = "";
            lblCreditLimit = Convert.ToDouble(Utility.gdblCreditLimit(strComID, strMedicalRep, DateTime.Now.ToString("MMMyy")).ToString());
            double dblCls = Utility.dblLedgerClosingBalance(strComID, "01/01/2020 12:00:00 AM", "31/12/2021 12:00:00 AM", strMedicalRep, "");
            if (dblCls < 0)
            {
                lblCurrentBalance = Math.Abs(dblCls) + "Dr";
            }
            else
            {
                lblCurrentBalance = Math.Abs(dblCls) + "Cr";
            }
            if (lblCreditLimit != 0)
            {
                string strFDate = Utility.FirstDayOfMonth(DateTime.Now).ToString();
                if (dblCls > 0)
                {
                    lblPending = Math.Round(lblCreditLimit + Math.Abs(dblCls), 2).ToString();
                }
                else
                {
                    lblPending = Math.Round(lblCreditLimit - Math.Abs(dblCls), 2).ToString();
                }

            }
            return lblPending;
        }
        public JsonResult CommossionCalculet(List<OrderDetails> objStockItem)
        {
            string strSubGroup = "", strItemGroup = "", str2ndGroup = "", strGrid = "", strOrdate = "", strCustomer = "", strOldRefNo = "";
            double dblAmount = 0, dblItemAmount = 0;
            int m_action = 1;
            string strComID = "0003";
            string strBranchID = "0001";

            double dbltotalAmount = 0, dblTotalItem = 0, dblTotaCommissition = 0;

            List<OrderDetails> oogrp = new List<OrderDetails>();
            foreach (var s in objStockItem)
            {


                strOrdate = s.strDate;
                strCustomer = s.strCustomer;
            }

            List<StockGroup> ooSample = StockGrouplsit(strComID);  //commision
            foreach (StockGroup oobj in ooSample)
            {
                strItemGroup = oobj.GroupName;

                for (int int2nd = 0; int2nd < objStockItem.Count; int2nd++)
                {
                    if (objStockItem[int2nd].strItemName != null)
                    {
                        str2ndGroup = objStockItem[int2nd].strSubGroup_Name;
                        if (strItemGroup == str2ndGroup)
                        {

                            dblAmount = objStockItem[int2nd].intItemQty * objStockItem[int2nd].dblItemRate;
                            dblItemAmount = dblItemAmount + dblAmount;
                            dblTotaCommissition += objStockItem[int2nd].dblCommitionVal;
                            dbltotalAmount += objStockItem[int2nd].dblTotalAmount;
                            dblTotalItem += objStockItem[int2nd].intItemQty;
                        }
                    }
                }
                if (dblItemAmount != 0)
                {
                    strGrid += strItemGroup + "|" + dblItemAmount + "~";   //    Group-A|565665 ~ Group-B|565665, Group-C|565665
                }
                dblItemAmount = 0;
            }

            //}

            if (strGrid != "")
            {
                double dblPercent = 0, dblFixedPercent = 0;
                string strFDate = "", strTdate = "";
                string[] words = strGrid.Split('~');
                foreach (string ooassets in words)
                {
                    string[] oAssets = ooassets.Split('|');
                    if (oAssets[0] != "")
                    {
                        dblPercent = Utility.mdblGetCommiPercen(strComID, oAssets[0], Utility.Val(oAssets[1]), strBranchID);
                        strFDate = Utility.FirstDayOfMonth(Convert.ToDateTime(strOrdate)).ToString("dd/MM/yyyy");
                        strTdate = Convert.ToDateTime(strOrdate).ToString("dd-MM-yyyy");
                        if (m_action == 1)
                        {
                            dblFixedPercent = Utility.mdblGetMaxCommiPercen(strComID, strCustomer, oAssets[0], strFDate, strTdate, strBranchID, "");
                        }
                        else
                        {
                            dblFixedPercent = Utility.mdblGetMaxCommiPercen(strComID, strCustomer, oAssets[0], strFDate, strTdate, strBranchID, strOldRefNo);
                        }
                        if (dblFixedPercent == 40)
                        {
                            dblPercent = 40;
                        }



                        for (int int2nd = 0; int2nd < objStockItem.Count; int2nd++)
                        {
                            if (objStockItem[int2nd].strSubGroup_Name != null)
                            {
                                str2ndGroup = objStockItem[int2nd].strSubGroup_Name;
                                if (oAssets[0] == str2ndGroup)
                                {




                                    OrderDetails objg = new OrderDetails();
                                    double dblCommitionValp = 0;

                                    objg.strItemGroup = objStockItem[int2nd].strItemGroup;
                                    objg.strItemName = objStockItem[int2nd].strItemName;
                                    objg.strPowerClass = objStockItem[int2nd].strPowerClass;
                                    objg.strSubGroup_Name = objStockItem[int2nd].strSubGroup_Name;
                                    objg.strPackSize = objStockItem[int2nd].strPackSize;
                                    objg.intItemQty = objStockItem[int2nd].intItemQty;
                                    objg.dblItemRate = objStockItem[int2nd].dblItemRate;
                                    objg.strUnit = objStockItem[int2nd].strUnit;
                                    objg.intBonusQty = objStockItem[int2nd].intBonusQty;
                                    objg.dblTotalAmount = objStockItem[int2nd].dblTotalAmount;
                                    objg.strCustomer = objStockItem[int2nd].strCustomer;
                                    objg.strDate = objStockItem[int2nd].strDate;


                                    objg.strCommitionGroupItemName = objStockItem[int2nd].strItemName;
                                    objg.dblCommitionVal = (((objStockItem[int2nd].intItemQty) * (objStockItem[int2nd].dblItemRate)) * dblPercent) / 100;
                                    dblCommitionValp = (((objStockItem[int2nd].intItemQty) * (objStockItem[int2nd].dblItemRate)) * dblPercent) / 100;
                                    objg.dblItemNetVal = (((objStockItem[int2nd].dblTotalAmount) - dblCommitionValp));
                                    objg.dblPercent = dblPercent;
                                    objg.strCommitionGroupN = oAssets[0];
                                    oogrp.Add(objg);

                                }
                            }
                        }

                        dblItemAmount = 0;
                    }
                }
                //calculateTotal();
            }



            return Json(oogrp, JsonRequestBehavior.AllowGet);
        }

        List<StockGroup> StockGrouplsit(string strDeComID)
        {
            string strSQL;
            SqlDataReader drGetGroup;
            List<StockGroup> oogrp = new List<StockGroup>();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            strSQL = "SELECT GR_NAME_SERIAL, GR_NAME ";
            strSQL = strSQL + "FROM INV_GROUP_MASTER ORDER BY GR_NAME ASC ";
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {
                    StockGroup ogrp = new StockGroup();
                    ogrp.lngslNo = Convert.ToInt64(drGetGroup["GR_NAME_SERIAL"].ToString());
                    ogrp.GroupName = drGetGroup["GR_NAME"].ToString();
                    oogrp.Add(ogrp);

                }
                drGetGroup.Close();
                gcnMain.Dispose();
                return oogrp;

            }
        }
        public List<AccBillwise> DisplayCommonInvoice(string strDeComID, string vstrVoucherRefNumber)
        {
            string strSQL = null;
            SqlDataReader dr;
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<AccBillwise> ooAcms = new List<AccBillwise>();
            strSQL = "SELECT BILL_TRAN_KEY,STOCKITEM_NAME,STOCKITEM_DESCRIPTION,GODOWNS_NAME,BILL_QUANTITY,BILL_QUANTITY_BONUS,";
            strSQL = strSQL + "BILL_UOM,BILL_RATE,BILL_PER,BILL_AMOUNT,BILL_ADD_LESS,";
            strSQL = strSQL + "BILL_NET_AMOUNT,INV_LOG_NO,BILL_QUANTITY_BONUS,VOUCHER_FC_AMOUNT,VOUCHER_CURRENCY_SYMBOL,STOCKGROUP_NAME,AGNST_COMP_REF_NO,AGNST_COMP_REF_NO1,SHORT_QTY,G_COMM_PER FROM ACC_BILL_TRAN ";
            strSQL = strSQL + " WHERE COMP_REF_NO = '" + vstrVoucherRefNumber + "' ";
            strSQL = strSQL + "ORDER BY BILL_TRAN_KEY";
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AccBillwise oBra = new AccBillwise();
                    oBra.strBillKey = dr["BILL_TRAN_KEY"].ToString();
                    oBra.strStockItemName = dr["STOCKITEM_NAME"].ToString();
                    if (dr["STOCKITEM_DESCRIPTION"].ToString() != "")
                    {
                        oBra.strDescription = dr["STOCKITEM_DESCRIPTION"].ToString();
                    }
                    else
                    {
                        oBra.strDescription = dr["STOCKITEM_DESCRIPTION"].ToString();
                    }

                    oBra.strGodownsName = dr["GODOWNS_NAME"].ToString();
                    if (dr["BILL_ADD_LESS"].ToString() != "")
                    {
                        oBra.strBillAddless = dr["BILL_ADD_LESS"].ToString();
                    }
                    else
                    {
                        oBra.strBillAddless = "";
                    }


                    if (dr["BILL_PER"].ToString() != "")
                    {
                        oBra.strPer = dr["BILL_PER"].ToString();
                    }
                    else
                    {
                        oBra.strPer = dr["BILL_UOM"].ToString();
                    }
                    oBra.dblAmount = Convert.ToDouble(dr["BILL_AMOUNT"].ToString());
                    oBra.dblBillNetAmount = Convert.ToDouble(dr["BILL_NET_AMOUNT"].ToString());
                    oBra.dblQnty = Convert.ToDouble(dr["BILL_QUANTITY"].ToString());
                    oBra.dblRate = Convert.ToDouble(dr["BILL_RATE"].ToString());
                    oBra.dblBonusQnty = Convert.ToDouble(dr["BILL_QUANTITY_BONUS"].ToString());
                    if (dr["INV_LOG_NO"].ToString() != "")
                    {
                        oBra.strBatchNo = dr["INV_LOG_NO"].ToString();
                    }
                    else
                    {
                        oBra.strBatchNo = "";
                    }
                    if (dr["STOCKGROUP_NAME"].ToString() != "")
                    {
                        oBra.strStockGroupName = dr["STOCKGROUP_NAME"].ToString();
                    }
                    else
                    {
                        oBra.strStockGroupName = "";
                    }
                    if (dr["AGNST_COMP_REF_NO"].ToString() != "")
                    {
                        oBra.strAgnstVoucherRefNo = dr["AGNST_COMP_REF_NO"].ToString();
                    }
                    else
                    {
                        oBra.strAgnstVoucherRefNo = "";
                    }

                    if (dr["AGNST_COMP_REF_NO1"].ToString() != "")
                    {
                        oBra.strAgnstVoucherRefNo1 = dr["AGNST_COMP_REF_NO1"].ToString();
                    }
                    else
                    {
                        oBra.strAgnstVoucherRefNo1 = "";
                    }

                    if (dr["AGNST_COMP_REF_NO1"].ToString() != "")
                    {
                        oBra.strSubgroup = dr["AGNST_COMP_REF_NO1"].ToString();
                    }
                    else
                    {
                        oBra.strSubgroup = "";
                    }
                    oBra.dblShortQty = Convert.ToDouble(dr["SHORT_QTY"].ToString());
                    oBra.dblComm = Convert.ToDouble(dr["G_COMM_PER"].ToString());
                    ooAcms.Add(oBra);
                }
                return ooAcms;
                dr.Close();
                gcnMain.Close();
                gcnMain.Dispose();
            }

        }



        [HttpPost]
        public ActionResult deleteItemById(string voucherNo)
        {
            string strSQL = "";

            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();


                    SqlCommand cmdDelete = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdDelete.Connection = gcnMain;
                    cmdDelete.Transaction = myTrans;

                    strSQL = "DELETE FROM ACC_COMPANY_VOUCHER ";
                    strSQL = strSQL + "WHERE COMP_REF_NO = '" + voucherNo + "' ";
                    cmdDelete.CommandText = strSQL;
                    cmdDelete.ExecuteNonQuery();

                    cmdDelete.Transaction.Commit();
                    gcnMain.Close();

                    return Json("OK", JsonRequestBehavior.AllowGet);
                }

                catch (Exception ex)
                {

                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    gcnMain.Close();
                }

            }

        }

        public JsonResult mpoApprove(OrderMaster gItemList)
        {
            string strSQL = "";
            long lngBillPosition = 1, lngloop = 1;
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }

                try
                {
                    gcnMain.Open();

                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;

                    for (int i = 0; i < gItemList.detailsList.Count; i++)
                    {

                        strSQL = "UPDATE ACC_COMPANY_VOUCHER SET APP_STATUS=1,APPS_COMM_CAL=1,ORDER_DATE= " + Utility.cvtSQLDateString(gItemList.detailsList[i].strTranDate) + " WHERE COMP_REF_NO='" + gItemList.detailsList[i].strVoucherNoMerz + "' ";
                        cmdInsert.CommandText = strSQL;
                        cmdInsert.ExecuteNonQuery();
                        lngBillPosition += 1;
                        lngloop += 1;

                    }


                    cmdInsert.Transaction.Commit();




                    gcnMain.Close();
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    gcnMain.Close();


                }

            }
        }
        
        #region "Sales Order Save"

        public JsonResult msaveSalesOrder(OrderMaster gItemList)
        {
            string strSQL = "", strMonthID = "", strBranchId = "0001", strPrepareBy = "MISK";
            string strBillKey = "", strItemName = "", strPer = "", strBatchNo = "", strGroupName = "", strsubFroup = "";
            long lngBillPosition = 1, lngloop = 1;
            double dblqty = 0, dblRate = 0, dblDebitValue, dblbonus = 0, dblCommPer = 0, dblCommAmnt = 0, dblTotalamnt;
            int mlngVType = 12, intAppStatus = 0;
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }

                try
                {
                    gcnMain.Open();

                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;

                    strMonthID = Convert.ToDateTime(gItemList.strOrderDate).ToString("MMMyy");


                    strSQL = Voucher.gInsertCompanyVoucher(gItemList.strOrderNo, mlngVType, Convert.ToDateTime(gItemList.strOrderDate).ToString("dd-MM-yyyy"), strMonthID, gItemList.strOrderDate, gItemList.strLedgerName,
                                                           gItemList.dblNetTotal, gItemList.dblTotalAmount, 0, gItemList.dblCommitionVal, 0, gItemList.strNaration,
                                                           strBranchId, gItemList.lngIsMultiCurrency, Utility.Mid(gItemList.strOrderNo, 9, gItemList.strOrderNo.Length - 9),
                                                           gItemList.strCustomer, gItemList.strDelivery, "", gItemList.strSupport, gItemList.dteValidaty,
                                                           gItemList.strOtherTerms, "", "", strPrepareBy, gItemList.strOrderDate, "", "", 0, 0, 0, 0, 0, intAppStatus,
                                                           "", "", "", "", gItemList.dblNetQty);

                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();


                    for (int i = 0; i < gItemList.detailsList.Count; i++)
                    {
                        string ss = gItemList.detailsList[i].strItemName;


                        strBillKey = gItemList.strOrderNo + lngBillPosition.ToString().PadLeft(4, '0');
                        strGroupName = gItemList.detailsList[i].strItemGroup;
                        strItemName = gItemList.detailsList[i].strItemName;
                        dblqty = gItemList.detailsList[i].intItemQty;
                        strPer = gItemList.detailsList[i].strUnit;
                        dblRate = gItemList.detailsList[i].dblItemRate;
                        dblDebitValue = gItemList.detailsList[i].dblTotalAmount;
                        dblbonus = gItemList.detailsList[i].dblbonusQty;
                        strsubFroup = gItemList.detailsList[i].strSubGroup_Name;
                        dblTotalamnt = gItemList.detailsList[i].dblCommitionVal;
                        dblCommAmnt = gItemList.detailsList[i].dblItemNetVal;
                        dblCommPer = gItemList.detailsList[i].dblPercent;



                        strSQL = Voucher.gInsertBillTran(strBillKey, gItemList.strOrderNo, mlngVType, Convert.ToDateTime(gItemList.strOrderDate).ToString("dd-MM-yyyy"), strItemName, gItemList.strLocation, dblqty, dblbonus,
                                                            strPer, dblRate, dblTotalamnt, "0", dblCommAmnt, dblDebitValue, "Cr", lngloop, strBranchId,
                                                             Utility.gstrBaseCurrency, strPer, "", "", strBatchNo, "", strsubFroup, "", strGroupName, 0, dblCommPer);
                        cmdInsert.CommandText = strSQL;
                        cmdInsert.ExecuteNonQuery();
                        //}

                        strSQL = Voucher.gInsertBillTranProcess(strBillKey, strBranchId, lngloop, gItemList.strOrderNo, gItemList.strOrderNo, mlngVType, Convert.ToDateTime(gItemList.strOrderDate).ToString("dd-MM-yyyy"),
                                                                strItemName, gItemList.strLocation, dblqty, strPer, strBillKey, 0, dblDebitValue, strPer);

                        cmdInsert.CommandText = strSQL;
                        cmdInsert.ExecuteNonQuery();

                        strSQL = "UPDATE ACC_COMPANY_VOUCHER SET APPS_SYNCHONIZED =1,ORDER_DATE= " + Utility.cvtSQLDateString(gItemList.strOrderDate) + " WHERE COMP_REF_NO='" + gItemList.strOrderNo + "' ";
                        cmdInsert.CommandText = strSQL;
                        cmdInsert.ExecuteNonQuery();
                        lngBillPosition += 1;
                        lngloop += 1;

                    }

                    cmdInsert.Transaction.Commit();




                    gcnMain.Close();
                    return Json("OK", JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                   
                    return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    gcnMain.Close();

                }
            }
        }
        #endregion
        #region "Sales Order Update"
        public JsonResult mUpdateSalesOrder(OrderMaster gItemList)
        {
            string strSQL = "", strMonthID = "", strBranchId = "0001", strPrepareBy = "MISK", strRefNo = "";
            string strBillKey = "", strItemName = "", strPer = "", strBatchNo = "", strGroupName = "", strsubFroup = "";
            long lngBillPosition = 1, lngloop = 1;
            double dblqty = 0, dblRate = 0, dblDebitValue, dblbonus = 0, dblCommPer = 0, dblCommAmnt = 0, dblTotalamnt, dblLessAmount = 0;
            int mlngVType = 12, intAppStatus = 1;

            //strRefNo = gobjVoucherName.VoucherName.GetVoucherString(intVtype) + strBranchID + Utility.gstrLastNumber(strComID, (int)intVtype);

            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }

                try
                {
                    gcnMain.Open();

                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;
                    strSQL = "DELETE FROM ACC_BILL_TRAN WHERE COMP_REF_NO = '" + gItemList.strOrderNo + "'";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    strSQL = "DELETE FROM ACC_BILL_TRAN_PROCESS WHERE COMP_REF_NO = '" + gItemList.strOrderNo + "'";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    strMonthID = Convert.ToDateTime(gItemList.strOrderDate).ToString("MMMyy");
                    strSQL = "UPDATE ACC_COMPANY_VOUCHER SET ";
                    if (gItemList.strOrderNo != "")
                    {
                        strSQL = strSQL + "COMP_REF_NO='" + gItemList.strOrderNo + "'";
                    }
                    if (strBranchId != "")
                    {
                        strSQL = strSQL + ",BRANCH_ID = '" + strBranchId + "'";
                    }
                    if (gItemList.strLedgerName != "")
                    {
                        strSQL = strSQL + ",LEDGER_NAME= '" + gItemList.strLedgerName + "'";
                    }
                    //strSQL = strSQL + "APPS_CUSTOMER_MERZE= '" + strLedgerName + "',";
                    if (gItemList.strOrderDate != null)
                    {
                        strSQL = strSQL + ",COMP_VOUCHER_DATE = " + Utility.cvtSQLDateString(gItemList.strOrderDate) + "";
                    }
                    if (strMonthID != "")
                    {
                        strSQL = strSQL + ",COMP_VOUCHER_MONTH_ID = '" + strMonthID + "'";
                    }
                    if (gItemList.dblTotalAmount != 0)
                    {
                        strSQL = strSQL + ",COMP_VOUCHER_NET_AMOUNT ='" + gItemList.dblTotalAmount + "'";
                    }
                    if (gItemList.dblTotalAmount != 0)
                    {
                        strSQL = strSQL + ",COMP_VOUCHER_AMOUNT ='" + gItemList.dblTotalAmount + "'";
                    }
                    if (gItemList.dblNetQty != 0)
                    {
                        strSQL = strSQL + ",APPS_COMP_QTY ='" + gItemList.dblNetQty + "'";
                    }
                    if (dblLessAmount != 0)
                    {
                        strSQL = strSQL + ",COMP_VOUCHER_LESS_AMOUNT ='" + dblLessAmount + "'";
                    }
                    if (gItemList.strOrderDate != null)
                    {
                        strSQL = strSQL + ",COMP_VOUCHER_DUE_DATE = " + Utility.cvtSQLDateString(gItemList.strOrderDate) + " ";
                    }
                    if (gItemList.strNaration != null)
                    {
                        strSQL = strSQL + ",COMP_VOUCHER_NARRATION = '" + gItemList.strNaration + "'";
                    }
                    //strSQL = strSQL + ",AGNST_COMP_REF_NO ='" + Utility.Mid(strRefNo, 10, strRefNo.Length - 10) + "'";
                    if (gItemList.strApprovedBy != null)
                    {
                        strSQL = strSQL + ",APPROVED_BY ='" + gItemList.strApprovedBy.Replace("'", "''") + "' ";
                    }
                    //strSQL = strSQL + ",APPS_CHANGE =" + intChaneType + " ";
                    //if (strApprovedDate != "")
                    //{
                    //    strSQL = strSQL + ",APPROVED_DATE =" + Utility.cvtSQLDateString(strApprovedDate) + "";
                    //}
                    //else
                    //{
                    //    strSQL = strSQL + ",APPROVED_DATE =NULL";
                    //}
                    //if (strDelivery != "")
                    //{
                    //    strSQL = strSQL + ",COMP_DELIVERY = '" + strDelivery + "'";
                    //}
                    //if (strPayment != "")
                    //{
                    //    strSQL = strSQL + ",COMP_TERM_OF_PAYMENTS = '" + strPayment + "'";
                    //}
                    //if (strSupport != "")
                    //{
                    //    strSQL = strSQL + ",COMP_SUPPORT = '" + strSupport + "'";
                    //}
                    //if (dteValidaty != "")
                    //{
                    //    strSQL = strSQL + ",COMP_VALIDITY_DATE = " + Utility.cvtSQLDateString(dteValidaty) + "";
                    //}
                    //else
                    //{
                    //    strSQL = strSQL + ",COMP_VALIDITY_DATE = null";
                    //}
                    //if (strOtherTerms != "")
                    //{
                    //    strSQL = strSQL + ",COMP_OTHERS = '" + strOtherTerms + "'";
                    //}
                    //if (strSalesRep != Utility.gcEND_OF_LIST)
                    //{
                    //    strSQL = strSQL + ",SALES_REP = '" + strSalesRep + "' ";
                    //}
                    //else
                    //{
                    //    strSQL = strSQL + ",SALES_REP = ''";
                    //}
                    strSQL = strSQL + "WHERE COMP_REF_NO = '" + gItemList.strOrderNo + "'";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();


                    for (int i = 0; i < gItemList.detailsList.Count; i++)
                    {
                        string ss = gItemList.detailsList[i].strItemName;


                        strBillKey = gItemList.strOrderNo + lngBillPosition.ToString().PadLeft(4, '0');
                        strGroupName = gItemList.detailsList[i].strItemGroup;
                        strItemName = gItemList.detailsList[i].strItemName;
                        dblqty = gItemList.detailsList[i].intItemQty;
                        strPer = gItemList.detailsList[i].strUnit;
                        dblRate = gItemList.detailsList[i].dblItemRate;
                        dblDebitValue = gItemList.detailsList[i].dblTotalAmount;
                        dblbonus = gItemList.detailsList[i].dblbonusQty;
                        strsubFroup = gItemList.detailsList[i].strSubGroup_Name;
                        dblTotalamnt = gItemList.detailsList[i].dblCommitionVal;
                        dblCommAmnt = gItemList.detailsList[i].dblItemNetVal;
                        dblCommPer = gItemList.detailsList[i].dblPercent;



                        strSQL = Voucher.gInsertBillTran(strBillKey, gItemList.strOrderNo, mlngVType, Convert.ToDateTime(gItemList.strOrderDate).ToString("dd-MM-yyyy"), strItemName, gItemList.strLocation, dblqty, dblbonus,
                                                            strPer, dblRate, dblTotalamnt, "0", dblCommAmnt, dblDebitValue, "Cr", lngloop, strBranchId,
                                                             Utility.gstrBaseCurrency, strPer, "", "", strBatchNo, "", strsubFroup, "", strGroupName, 0, dblCommPer);
                        cmdInsert.CommandText = strSQL;
                        cmdInsert.ExecuteNonQuery();
                        //}

                        strSQL = Voucher.gInsertBillTranProcess(strBillKey, strBranchId, lngloop, gItemList.strOrderNo, gItemList.strOrderNo, mlngVType, Convert.ToDateTime(gItemList.strOrderDate).ToString("dd-MM-yyyy"),
                                                                strItemName, gItemList.strLocation, dblqty, strPer, strBillKey, 0, dblDebitValue, strPer);

                        cmdInsert.CommandText = strSQL;
                        cmdInsert.ExecuteNonQuery();

                        strSQL = "UPDATE ACC_COMPANY_VOUCHER SET APPS_SYNCHONIZED =1,ORDER_DATE= " + Utility.cvtSQLDateString(gItemList.strOrderDate) + " WHERE COMP_REF_NO='" + gItemList.strOrderNo + "' ";
                        cmdInsert.CommandText = strSQL;
                        cmdInsert.ExecuteNonQuery();
                        lngBillPosition += 1;
                        lngloop += 1;

                    }




                    cmdInsert.Transaction.Commit();



                    gcnMain.Close();
            
                    return Json("Updated", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
          
                }
                finally
                {
                    gcnMain.Close();

                }
            }



        }

        #endregion

        #region "SalesComm"


        public JsonResult mUpdateSalesOrderOnlineComm(List<OrderDetails> objStockItem)
        {
            string strsubFroup = "", strSQL = "";
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand();

                SqlCommand cmdInsert = new SqlCommand();
                SqlTransaction myTrans;

                cmdInsert.Connection = gcnMain;

                try
                {
                    //using (SqlConnection gcnMain = new SqlConnection(connectionString))
                    //{
                    //    if (gcnMain.State == ConnectionState.Open)
                    //    {
                    //        gcnMain.Close();
                    //    }

                    //    try
                    //    {
                    //gcnMain.Open();

                    //SqlCommand cmdInsert = new SqlCommand();
                    //SqlTransaction myTrans;
                    //SqlDataReader dr;
                    //myTrans = gcnMain.BeginTransaction();
                    //cmdInsert.Connection = gcnMain;
                    //cmdInsert.Transaction = myTrans;


                    foreach (var s in objStockItem)
                    {


                        List<OrderDetails2> orderDetails = new List<OrderDetails2>();


                        int m_action = 1;
                        string strBillKey = "", strItemName = "", strPer = "", strBatchNo = "", strGroupName = "", strVoucherNoMerz = "", strBranchId = "", strOrdate = "", strCustomer = "", strComID = "";
                        long lngBillPosition = 1, lngloop = 1, mlngVType = 12;
                        double dblqty = 0, dblRate = 0, dblDebitValue, dblbonus = 0, dblCommPer = 0, dblCommAmnt = 0, dblTotalamnt = 0;

                        string strSubGroup = "", strItemGroup = "", str2ndGroup = "", strGrid = "", strOldRefNo = "";
                        double dblAmount = 0, dblItemAmount = 0;
                        double dbltotalAmount = 0, dblTotalItem = 0, dblTotaCommissition = 0;

                        string strDate = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");
                        strBranchId = "0001";
                        strComID = "0003";


                        strOrdate = s.strTranDate;
                        strCustomer = s.strDoctorName;






                        strSQL = "select  BILL_TRAN_KEY,I.STOCKITEM_NAME,I.STOCKITEM_DESCRIPTION,GODOWNS_NAME,BILL_QUANTITY,BILL_QUANTITY_BONUS, ";
                        strSQL = strSQL + "BILL_UOM,BILL_RATE,BILL_PER,BILL_AMOUNT,BILL_ADD_LESS,I.POWER_CLASS,I.STOCKGROUP_NAME, ";
                        strSQL = strSQL + "T.BILL_NET_AMOUNT,INV_LOG_NO,BILL_QUANTITY_BONUS,VOUCHER_FC_AMOUNT,VOUCHER_CURRENCY_SYMBOL,I.STOCKGROUP_NAME,L.LEDGER_NAME_MERZE,V.ORDER_DATE, ";
                        strSQL = strSQL + "T.AGNST_COMP_REF_NO,AGNST_COMP_REF_NO1,SHORT_QTY,G_COMM_PER from  ACC_BILL_TRAN T,INV_STOCKITEM I ,ACC_COMPANY_VOUCHER V ,ACC_LEDGER L  ";
                        strSQL = strSQL + "where  T.STOCKITEM_NAME= I.STOCKITEM_NAME and  T.COMP_REF_NO=V.COMP_REF_NO and v.SALES_REP=L.LEDGER_NAME ";
                        strSQL = strSQL + "and SUBSTRING(T.COMP_REF_NO, 7, 23) = '" + s.strVoucherNo + "' ";
                        cmdInsert.CommandText = strSQL;
                        dr = cmdInsert.ExecuteReader();
                        while (dr.Read())
                        {

                            OrderDetails oBra = new OrderDetails();
                            oBra.strBillKey = dr["BILL_TRAN_KEY"].ToString();
                            oBra.strItemName = dr["STOCKITEM_NAME"].ToString();
                            oBra.strPowerClass = Utility.mGetPowerClass(strComID, oBra.strItemName);
                            oBra.strPackSize = Utility.mGetPackSize(strComID, oBra.strItemName);
                            oBra.strCustomer = dr["LEDGER_NAME_MERZE"].ToString();
                            oBra.strItemGroup = dr["STOCKGROUP_NAME"].ToString();
                            oBra.strSubGroup_Name = dr["AGNST_COMP_REF_NO1"].ToString();
                            if (dr["STOCKITEM_DESCRIPTION"].ToString() != "")
                            {
                                oBra.strDescription = dr["STOCKITEM_DESCRIPTION"].ToString();
                            }
                            else
                            {
                                oBra.strDescription = dr["STOCKITEM_DESCRIPTION"].ToString();
                            }

                            oBra.strLocation = dr["GODOWNS_NAME"].ToString();

                            if (dr["BILL_PER"].ToString() != "")
                            {
                                oBra.strUnit = dr["BILL_PER"].ToString();
                            }
                            else
                            {
                                oBra.strUnit = dr["BILL_UOM"].ToString();
                            }
                            oBra.dblTotalAmount = Convert.ToDouble(dr["BILL_AMOUNT"].ToString());

                            oBra.intItemQty = Convert.ToInt16(dr["BILL_QUANTITY"]);
                            oBra.dblItemRate = Convert.ToDouble(dr["BILL_RATE"].ToString());
                            oBra.intBonusQty = Convert.ToInt16(dr["BILL_QUANTITY_BONUS"]);
                            oBra.strDate = Convert.ToString(dr["ORDER_DATE"].ToString());
                            ooAcms.Add(oBra);
                        }


                        dr.Close();


                        List<StockGroup> ooSample = StockGrouplsit(strComID);  //commision
                        foreach (StockGroup oobj in ooSample)
                        {
                            strItemGroup = oobj.GroupName;

                            for (int int2nd = 0; int2nd < ooAcms.Count; int2nd++)
                            {
                                if (ooAcms[int2nd].strItemName != null)
                                {
                                    str2ndGroup = ooAcms[int2nd].strSubGroup_Name;
                                    if (strItemGroup == str2ndGroup)
                                    {

                                        dblAmount = ooAcms[int2nd].intItemQty * ooAcms[int2nd].dblItemRate;
                                        dblItemAmount = dblItemAmount + dblAmount;
                                        dblTotaCommissition += ooAcms[int2nd].dblCommitionVal;
                                        dbltotalAmount += ooAcms[int2nd].dblTotalAmount;
                                        dblTotalItem += ooAcms[int2nd].intItemQty;
                                    }
                                }
                            }
                            if (dblItemAmount != 0)
                            {
                                strGrid += strItemGroup + "|" + dblItemAmount + "~";   //    Group-A|565665 ~ Group-B|565665, Group-C|565665
                            }
                            dblItemAmount = 0;
                        }






                        if (strGrid != "")
                        {
                            double dblPercent = 0, dblFixedPercent = 0;
                            string strFDate = "", strTdate = "";
                            string[] words = strGrid.Split('~');
                            foreach (string ooassets in words)
                            {
                                string[] oAssets = ooassets.Split('|');
                                if (oAssets[0] != "")
                                {
                                    dblPercent = Utility.mdblGetCommiPercen(strComID, oAssets[0], Utility.Val(oAssets[1]), strBranchId);
                                    strFDate = Utility.FirstDayOfMonth(Convert.ToDateTime(strOrdate)).ToString("dd/MM/yyyy");
                                    strTdate = Convert.ToDateTime(strOrdate).ToString("dd-MM-yyyy");
                                    if (m_action == 1)
                                    {
                                        dblFixedPercent = Utility.mdblGetMaxCommiPercen(strComID, strCustomer, oAssets[0], strFDate, strTdate, strBranchId, "");
                                    }
                                    else
                                    {
                                        dblFixedPercent = Utility.mdblGetMaxCommiPercen(strComID, strCustomer, oAssets[0], strFDate, strTdate, strBranchId, strOldRefNo);
                                    }
                                    if (dblFixedPercent == 40)
                                    {
                                        dblPercent = 40;
                                    }



                                    for (int int2nd = 0; int2nd < ooAcms.Count; int2nd++)
                                    {
                                        if (ooAcms[int2nd].strSubGroup_Name != null)
                                        {
                                            str2ndGroup = ooAcms[int2nd].strSubGroup_Name;
                                            if (oAssets[0] == str2ndGroup)
                                            {




                                                OrderDetails2 objg = new OrderDetails2();
                                                double dblCommitionValp = 0;

                                                objg.strItemGroup = ooAcms[int2nd].strItemGroup;
                                                objg.strItemName = ooAcms[int2nd].strItemName;
                                                objg.strPowerClass = ooAcms[int2nd].strPowerClass;
                                                objg.strSubGroup_Name = ooAcms[int2nd].strSubGroup_Name;
                                                objg.strPackSize = ooAcms[int2nd].strPackSize;
                                                objg.intItemQty = ooAcms[int2nd].intItemQty;
                                                objg.dblItemRate = ooAcms[int2nd].dblItemRate;
                                                objg.strUnit = ooAcms[int2nd].strUnit;
                                                objg.intBonusQty = ooAcms[int2nd].intBonusQty;
                                                objg.dblTotalAmount = ooAcms[int2nd].dblTotalAmount;
                                                objg.strCustomer = ooAcms[int2nd].strCustomer;
                                                objg.strDate = ooAcms[int2nd].strDate;


                                                objg.strCommitionGroupItemName = ooAcms[int2nd].strItemName;
                                                objg.dblCommitionVal = (((ooAcms[int2nd].intItemQty) * (ooAcms[int2nd].dblItemRate)) * dblPercent) / 100;
                                                dblCommitionValp = (((ooAcms[int2nd].intItemQty) * (ooAcms[int2nd].dblItemRate)) * dblPercent) / 100;
                                                objg.dblItemNetVal = (((ooAcms[int2nd].dblTotalAmount) - dblCommitionValp));
                                                objg.dblPercent = dblPercent;
                                                objg.strCommitionGroupN = oAssets[0];
                                                orderDetails.Add(objg);

                                            }
                                        }
                                    }

                                    dblItemAmount = 0;
                                }
                            }
                            //calculateTotal();
                        }





                        myTrans = gcnMain.BeginTransaction();
                        cmdInsert.Transaction = myTrans;

                        strSQL = "DELETE FROM ACC_BILL_TRAN WHERE COMP_REF_NO = '" + s.strVoucherNoMerz + "'";
                        cmdInsert.CommandText = strSQL;
                        cmdInsert.ExecuteNonQuery();
                        strSQL = "DELETE FROM ACC_BILL_TRAN_PROCESS WHERE COMP_REF_NO = '" + s.strVoucherNoMerz + "'";
                        cmdInsert.CommandText = strSQL;
                        cmdInsert.ExecuteNonQuery();



                        //mloadCommtissionMasterData(s.strVoucherNo);










                        strBillKey = Utility.Mid(s.strVoucherNoMerz, 9, s.strVoucherNoMerz.Length - 9) + lngBillPosition.ToString().PadRight(4, '0');
                        strSQL = "SELECT BILL_TRAN_KEY FROM ACC_BILL_TRAN WHERE BILL_TRAN_KEY='" + strBillKey + "' ";
                        cmdInsert.CommandText = strSQL;
                        dr = cmdInsert.ExecuteReader();
                        if (dr.Read())
                        {
                            lngBillPosition += 1;
                            strBillKey = Utility.Mid(s.strVoucherNoMerz, 9, s.strVoucherNoMerz.Length - 9) + lngBillPosition.ToString().PadRight(4, '0');
                        }
                        dr.Close();

                        //foreach (var item in orderDetails)
                        //{
                        //    int u=0;
                        //}

                        ////DisplayCompVoucherList("0003", s.strVoucherNo, 12);
                        //List<AccBillwise> itemlist = DisplayCommonInvoice("0003", s.strVoucherNo);

                        foreach (var i in orderDetails)
                        {
                            strGroupName = i.strItemGroup.ToString();
                            strItemName = i.strItemName.ToString();
                            dblqty = Utility.Val(i.intItemQty.ToString());
                            strPer = i.dblPercent.ToString();
                            dblRate = Utility.Val(i.dblItemRate.ToString());
                            dblDebitValue = Utility.Val(i.dblCommitionVal.ToString());
                            dblbonus = Utility.Val(i.dblbonusQty.ToString());
                            strsubFroup = i.strSubGroup_Name.ToString();
                            dblTotalamnt = Utility.Val(i.dblTotalAmount.ToString());
                            dblCommAmnt = Utility.Val(i.dblCommitionVal.ToString());
                            dblCommPer = Utility.Val(i.dblPercent.ToString());

                            strSQL = Voucher.gInsertBillTran(strBillKey, s.strVoucherNoMerz, mlngVType, strDate, strItemName, s.strLocation, dblqty, dblbonus,
                                                                      strPer, dblRate, dblCommAmnt, "0", dblTotalamnt, dblDebitValue, "Cr", lngloop, strBranchId,
                                                                       Utility.gstrBaseCurrency, strPer, "", "", strBatchNo, "", strsubFroup, "", strGroupName, 0, dblCommPer);
                            cmdInsert.CommandText = strSQL;
                            cmdInsert.ExecuteNonQuery();

                            strSQL = Voucher.gInsertBillTranProcess(strBillKey, strBranchId, lngloop, s.strVoucherNoMerz, s.strVoucherNoMerz, mlngVType, strDate,
                                                                    strItemName, s.strLocation, dblqty, strPer, strBillKey, 0, dblDebitValue, strPer);

                            cmdInsert.CommandText = strSQL;
                            cmdInsert.ExecuteNonQuery();

                            strSQL = "UPDATE ACC_COMPANY_VOUCHER SET APPS_COMM_CAL=1 ";
                            strSQL = strSQL + "WHERE COMP_REF_NO='" + s.strVoucherNo + "' ";
                            cmdInsert.CommandText = strSQL;
                            cmdInsert.ExecuteNonQuery();
                            lngBillPosition += 1;
                            lngloop += 1;
                        }
                    }

                    cmdInsert.Transaction.Commit();




                    gcnMain.Close();
                    gcnMain.Dispose();

                }

                catch (Exception ex)
                {
                    //return (ex.ToString());
                    return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    gcnMain.Close();

                }
            }

            return Json("", JsonRequestBehavior.AllowGet);

        }
        public string mloadCommtissionMasterData(string strRefNo)
        {

            string strBranchID = "0001", strLocation = "";
            string strSQL = null;

            SqlDataReader dr;
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //List<OrderMaster> ooAcms = new List<OrderMaster>();



            strSQL = "SELECT  * FROM ACC_COMPANY_VOUCHER WHERE COMP_REF_NO = '" + strRefNo + "'  ";

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {






                    OrderDetails oBra = new OrderDetails();
                    oBra.strDate = dr["COMP_VOUCHER_DATE"].ToString();
                    oBra.strCustomer = dr["SALES_REP"].ToString();
                    //oBra.strPowerClass = Utility.mGetPowerClass("0003", oBra.strItemName);
                    //oBra.strPackSize = Utility.mGetPackSize("0003", oBra.strItemName);
                    //oBra.strCustomer = dr["LEDGER_NAME_MERZE"].ToString();
                    //oBra.strItemGroup = dr["STOCKGROUP_NAME"].ToString();
                    //oBra.strSubGroup_Name = dr["AGNST_COMP_REF_NO1"].ToString();
                    //if (dr["STOCKITEM_DESCRIPTION"].ToString() != "")
                    //{
                    //    oBra.strDescription = dr["STOCKITEM_DESCRIPTION"].ToString();
                    //}
                    //else
                    //{
                    //    oBra.strDescription = dr["STOCKITEM_DESCRIPTION"].ToString();
                    //}

                    //oBra.strLocation = dr["GODOWNS_NAME"].ToString();

                    //if (dr["BILL_PER"].ToString() != "")
                    //{
                    //    oBra.strUnit = dr["BILL_PER"].ToString();
                    //}
                    //else
                    //{
                    //    oBra.strUnit = dr["BILL_UOM"].ToString();
                    //}
                    //oBra.dblTotalAmount = Convert.ToDouble(dr["BILL_AMOUNT"].ToString());

                    //oBra.intItemQty = Convert.ToInt16(dr["BILL_QUANTITY"]);
                    //oBra.dblItemRate = Convert.ToDouble(dr["BILL_RATE"].ToString());
                    //oBra.intBonusQty = Convert.ToInt16(dr["BILL_QUANTITY_BONUS"]);
                    //oBra.strDate = Convert.ToString(dr["ORDER_DATE"].ToString());
                    ooAcms.Add(oBra);
                }



                return "";


            }

        }
        public List<AccountsVoucher> DisplayCompVoucherList(string strDeComID, string vstrVoucherRefNumber, long mlngVoucherAs)
        {
            string strSQL = null;
            SqlDataReader dr;

            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


            strSQL = "SELECT ACC_COMPANY_VOUCHER.COMP_REF_NO,ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE,ACC_COMPANY_VOUCHER.BRANCH_ID,ACC_COMPANY_VOUCHER.COMP_VOUCHER_MONTH_ID,ACC_LEDGER.LEDGER_NAME LEDGER_NAME1,ACC_LEDGER.LEDGER_NAME_MERZE LEDGER_NAME,ACC_COMPANY_VOUCHER.COMP_VOUCHER_NARRATION ,";
            strSQL = strSQL + "ACC_COMPANY_VOUCHER.SALES_REP,ACC_COMPANY_VOUCHER.COMP_VOUCHER_DUE_DATE,ACC_COMPANY_VOUCHER.COMP_VOUCHER_FC,";
            strSQL = strSQL + "ACC_COMPANY_VOUCHER.COMP_DELIVERY,ACC_COMPANY_VOUCHER.COMP_TERM_OF_PAYMENTS,ACC_COMPANY_VOUCHER.COMP_SUPPORT,ACC_COMPANY_VOUCHER.COMP_VALIDITY_DATE,ACC_COMPANY_VOUCHER.COMP_OTHERS,ACC_COMPANY_VOUCHER.ORDER_NO,";
            strSQL = strSQL + "ACC_COMPANY_VOUCHER.ORDER_DATE,ACC_COMPANY_VOUCHER.PREPARED_BY,ACC_COMPANY_VOUCHER.PREPARED_DATE,ACC_COMPANY_VOUCHER.COMP_VOUCHER_DESTINATION,ACC_COMPANY_VOUCHER.TRANSPORT_NAME,ACC_COMPANY_VOUCHER.CRT_QTY,ACC_COMPANY_VOUCHER.BOX_QTY,";
            strSQL = strSQL + "ACC_COMPANY_VOUCHER.COMP_VOUCHER_AMOUNT,ACC_COMPANY_VOUCHER.COMP_VOUCHER_NET_AMOUNT,ACC_COMPANY_VOUCHER.COMP_VOUCHER_PROCESS_AMOUNT,ACC_COMPANY_VOUCHER.COMP_VOUCHER_ADD_AMOUNT,ACC_COMPANY_VOUCHER.COMP_VOUCHER_LESS_AMOUNT,";
            strSQL = strSQL + "ACC_COMPANY_VOUCHER.COMP_ROUND_OFF_AMOUNT,ACC_COMPANY_VOUCHER.AGNST_COMP_REF_NO,APPROVED_BY,APPROVED_DATE,APPS_CHANGE FROM ACC_COMPANY_VOUCHER,ACC_LEDGER ";
            strSQL = strSQL + "WHERE ACC_LEDGER.LEDGER_NAME =ACC_COMPANY_VOUCHER.LEDGER_NAME  ";
            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_REF_NO = '" + vstrVoucherRefNumber + "' ";
            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_TYPE = " + mlngVoucherAs + " ";


            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    AccountsVoucher oBra = new AccountsVoucher();
                    oBra.strVoucherNo = vstrVoucherRefNumber;
                    oBra.strTranDate = Convert.ToDateTime(dr["COMP_VOUCHER_DATE"]).ToString("dd/MM/yyyy");
                    oBra.strMerzeName = dr["LEDGER_NAME"].ToString();
                    oBra.strLedgerName = dr["LEDGER_NAME1"].ToString();
                    oBra.strLedgerNameNew = dr["LEDGER_NAME1"].ToString();
                    //oBra.strDueDate = Convert.ToDateTime(dr["COMP_VOUCHER_DUE_DATE"]).ToString("dd/MM/yyyy");
                    oBra.strBranchID = dr["BRANCH_ID"].ToString();
                    oBra.strNarration = dr["COMP_VOUCHER_NARRATION"].ToString();
                    if (dr["SALES_REP"].ToString() != "")
                    {
                        oBra.strSalesRepresentive = dr["SALES_REP"].ToString();
                    }
                    else
                    {
                        oBra.strSalesRepresentive = "";
                    }
                    oBra.strDueDate = Convert.ToDateTime(dr["COMP_VOUCHER_DUE_DATE"]).ToString("dd/MM/yyyy");
                    if (dr["COMP_VOUCHER_FC"].ToString() != "")
                    {
                        oBra.strComVoucherFc = dr["COMP_VOUCHER_FC"].ToString();
                    }
                    if (dr["COMP_DELIVERY"].ToString() != "")
                    {
                        oBra.strDelivery = dr["COMP_DELIVERY"].ToString();
                    }
                    if (dr["COMP_TERM_OF_PAYMENTS"].ToString() != "")
                    {
                        oBra.strtermofPayment = dr["COMP_TERM_OF_PAYMENTS"].ToString();
                    }
                    if (dr["COMP_SUPPORT"].ToString() != "")
                    {
                        oBra.strSupport = dr["COMP_SUPPORT"].ToString();
                    }
                    if (dr["COMP_VALIDITY_DATE"].ToString() != "")
                    {
                        oBra.strValidityDate = Convert.ToDateTime(dr["COMP_VALIDITY_DATE"]).ToString("dd/MM/yyyy");
                    }
                    if (dr["COMP_OTHERS"].ToString() != "")
                    {
                        oBra.strOthers = dr["COMP_OTHERS"].ToString();
                    }
                    if (dr["COMP_VOUCHER_NARRATION"].ToString() != "")
                    {
                        oBra.strNarration = dr["COMP_VOUCHER_NARRATION"].ToString();
                    }

                    if (dr["ORDER_NO"].ToString() != "")
                    {
                        oBra.strOrderNo = dr["ORDER_NO"].ToString();
                    }
                    else
                    {
                        oBra.strOrderNo = "";
                    }
                    if (dr["ORDER_DATE"].ToString() != "")
                    {
                        oBra.strOrderDate = Convert.ToDateTime(dr["ORDER_DATE"]).ToString("dd/MM/yyyy");
                    }
                    if (dr["PREPARED_BY"].ToString() != "")
                    {
                        oBra.strPreparedby = dr["PREPARED_BY"].ToString();
                    }
                    else
                    {
                        oBra.strPreparedby = "";
                    }
                    if (dr["PREPARED_DATE"].ToString() != "")
                    {
                        oBra.strPreparedDate = Convert.ToDateTime(dr["PREPARED_DATE"]).ToString("dd/MM/yyyy");
                    }

                    if (dr["COMP_VOUCHER_DESTINATION"].ToString() != "")
                    {
                        oBra.strDesignation = dr["COMP_VOUCHER_DESTINATION"].ToString();
                    }
                    else
                    {
                        oBra.strDesignation = "";
                    }
                    if (dr["TRANSPORT_NAME"].ToString() != "")
                    {
                        oBra.strtransport = dr["TRANSPORT_NAME"].ToString();
                    }
                    else
                    {
                        oBra.strtransport = "";
                    }
                    if (dr["COMP_VOUCHER_MONTH_ID"].ToString() != "")
                    {
                        oBra.strMonthID = dr["COMP_VOUCHER_MONTH_ID"].ToString();
                    }
                    else
                    {
                        oBra.strMonthID = "";
                    }
                    if (dr["AGNST_COMP_REF_NO"].ToString() != "")
                    {
                        oBra.strAgnstRefNo = dr["AGNST_COMP_REF_NO"].ToString();
                    }
                    else
                    {
                        oBra.strAgnstRefNo = "";
                    }
                    if (dr["APPROVED_BY"].ToString() != "")
                    {
                        oBra.strApprovedby = dr["APPROVED_BY"].ToString();
                    }
                    else
                    {
                        oBra.strApprovedby = "";
                    }
                    if (dr["APPROVED_DATE"].ToString() != "")
                    {
                        oBra.strApproveddate = Convert.ToDateTime(dr["APPROVED_DATE"]).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        oBra.strApproveddate = "";
                    }

                    oBra.dblCrtQnty = Convert.ToDouble(dr["CRT_QTY"].ToString());
                    oBra.dblBoxQnty = Convert.ToDouble(dr["BOX_QTY"].ToString());

                    oBra.dblAmount = Convert.ToDouble(dr["COMP_VOUCHER_AMOUNT"].ToString());
                    oBra.dblNetAmount = Convert.ToDouble(dr["COMP_VOUCHER_NET_AMOUNT"].ToString());
                    oBra.dblProcessAmount = Convert.ToDouble(dr["COMP_VOUCHER_PROCESS_AMOUNT"].ToString());
                    oBra.dblAddAmount = Convert.ToDouble(dr["COMP_VOUCHER_ADD_AMOUNT"].ToString());
                    oBra.dblLessAmount = Convert.ToDouble(dr["COMP_VOUCHER_LESS_AMOUNT"].ToString());
                    oBra.intChangeType = Convert.ToInt32(dr["APPS_CHANGE"].ToString());
                    oBra.dblRoundOff = Convert.ToDouble(dr["COMP_ROUND_OFF_AMOUNT"].ToString());
                    ooAcmss.Add(oBra);
                }
                return ooAcmss;
                dr.Close();
                gcnMain.Close();
                gcnMain.Dispose();
            }

        }
        #endregion
        #region "Edit Data get"

        public JsonResult mloadCusotmer(OrderMaster objOrderDetails)
        {

            string strBranchID = "0001", strLocation = "";
            string strSQL = null;

            SqlDataReader dr;
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //List<OrderMaster> ooAcms = new List<OrderMaster>();



            strSQL = "select  GODOWNS_NAME ,";
            strSQL = strSQL + "T.AGNST_COMP_REF_NO,AGNST_COMP_REF_NO1,SHORT_QTY,G_COMM_PER from  ACC_BILL_TRAN T,INV_STOCKITEM I ,ACC_COMPANY_VOUCHER V ,ACC_LEDGER L  ";
            strSQL = strSQL + "where  T.STOCKITEM_NAME= I.STOCKITEM_NAME and  T.COMP_REF_NO=V.COMP_REF_NO and v.SALES_REP=L.LEDGER_NAME ";
            strSQL = strSQL + "and SUBSTRING(T.COMP_REF_NO, 7, 23) = '" + objOrderDetails.strOrderNo + "' ";
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                dr.Read();
                {
                    strLocation = dr["GODOWNS_NAME"].ToString();
                }


                dr.Close();
                gcnMain.Close();
                gcnMain.Dispose();




                return Json(strLocation, JsonRequestBehavior.AllowGet);


            }

        }
        public JsonResult mloadLocationDetails(OrderMaster objOrderDetails)
        {
            string strComID = "0003", strItemGroup = "", str2ndGroup = "", strGrid = "", strOrdate = "", strCustomer = "", strOldRefNo = "";
            string strBranchID = "0001";
            string strSQL = null;
            double dblAmount = 0, dblItemAmount = 0;
            int m_action = 1;
            SqlDataReader dr;
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            List<OrderDetails2> orderDetails = new List<OrderDetails2>();


            strSQL = "select  BILL_TRAN_KEY,I.STOCKITEM_NAME,I.STOCKITEM_DESCRIPTION,GODOWNS_NAME,BILL_QUANTITY,BILL_QUANTITY_BONUS, ";
            strSQL = strSQL + "BILL_UOM,BILL_RATE,BILL_PER,BILL_AMOUNT,BILL_ADD_LESS,I.POWER_CLASS,I.STOCKGROUP_NAME, ";
            strSQL = strSQL + "T.BILL_NET_AMOUNT,INV_LOG_NO,BILL_QUANTITY_BONUS,VOUCHER_FC_AMOUNT,VOUCHER_CURRENCY_SYMBOL,I.STOCKGROUP_NAME,L.LEDGER_NAME_MERZE,V.ORDER_DATE, ";
            strSQL = strSQL + "T.AGNST_COMP_REF_NO,AGNST_COMP_REF_NO1,SHORT_QTY,G_COMM_PER from  ACC_BILL_TRAN T,INV_STOCKITEM I ,ACC_COMPANY_VOUCHER V ,ACC_LEDGER L  ";
            strSQL = strSQL + "where  T.STOCKITEM_NAME= I.STOCKITEM_NAME and  T.COMP_REF_NO=V.COMP_REF_NO and v.SALES_REP=L.LEDGER_NAME ";
            strSQL = strSQL + "and SUBSTRING(T.COMP_REF_NO, 7, 23) = '" + objOrderDetails.strOrderNo + "' ";
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    OrderDetails oBra = new OrderDetails();
                    oBra.strBillKey = dr["BILL_TRAN_KEY"].ToString();
                    oBra.strItemName = dr["STOCKITEM_NAME"].ToString();
                    oBra.strPowerClass = Utility.mGetPowerClass(strComID, oBra.strItemName);
                    oBra.strPackSize = Utility.mGetPackSize(strComID, oBra.strItemName);
                    oBra.strCustomer = dr["LEDGER_NAME_MERZE"].ToString();
                    oBra.strItemGroup = dr["STOCKGROUP_NAME"].ToString();
                    oBra.strSubGroup_Name = dr["AGNST_COMP_REF_NO1"].ToString();
                    if (dr["STOCKITEM_DESCRIPTION"].ToString() != "")
                    {
                        oBra.strDescription = dr["STOCKITEM_DESCRIPTION"].ToString();
                    }
                    else
                    {
                        oBra.strDescription = dr["STOCKITEM_DESCRIPTION"].ToString();
                    }

                    oBra.strLocation = dr["GODOWNS_NAME"].ToString();

                    if (dr["BILL_PER"].ToString() != "")
                    {
                        oBra.strUnit = dr["BILL_PER"].ToString();
                    }
                    else
                    {
                        oBra.strUnit = dr["BILL_UOM"].ToString();
                    }
                    oBra.dblTotalAmount = Convert.ToDouble(dr["BILL_AMOUNT"].ToString());

                    oBra.intItemQty = Convert.ToInt16(dr["BILL_QUANTITY"]);
                    oBra.dblItemRate = Convert.ToDouble(dr["BILL_RATE"].ToString());
                    oBra.intBonusQty = Convert.ToInt16(dr["BILL_QUANTITY_BONUS"]);
                    oBra.strDate = Convert.ToString(dr["ORDER_DATE"].ToString());
                    ooAcms.Add(oBra);
                }


                dr.Close();
                gcnMain.Close();
                gcnMain.Dispose();





                foreach (var s in ooAcms)
                {


                    strOrdate = s.strDate;
                    strCustomer = s.strCustomer;
                }

                List<StockGroup> ooSample = StockGrouplsit(strComID);  //commision
                foreach (StockGroup oobj in ooSample)
                {
                    strItemGroup = oobj.GroupName;

                    for (int int2nd = 0; int2nd < ooAcms.Count; int2nd++)
                    {
                        if (ooAcms[int2nd].strItemName != null)
                        {
                            str2ndGroup = ooAcms[int2nd].strSubGroup_Name;
                            if (strItemGroup == str2ndGroup)
                            {

                                dblAmount = ooAcms[int2nd].intItemQty * ooAcms[int2nd].dblItemRate;
                                dblItemAmount = dblItemAmount + dblAmount;
                            }
                        }
                    }
                    if (dblItemAmount != 0)
                    {
                        strGrid += strItemGroup + "|" + dblItemAmount + "~";   //    Group-A|565665 ~ Group-B|565665, Group-C|565665
                    }
                    dblItemAmount = 0;
                }


                if (strGrid != "")
                {
                    double dblPercent = 0, dblFixedPercent = 0;
                    string strFDate = "", strTdate = "";
                    string[] words = strGrid.Split('~');
                    foreach (string ooassets in words)
                    {
                        string[] oAssets = ooassets.Split('|');
                        if (oAssets[0] != "")
                        {
                            dblPercent = Utility.mdblGetCommiPercen(strComID, oAssets[0], Utility.Val(oAssets[1]), strBranchID);
                            strFDate = Utility.FirstDayOfMonth(Convert.ToDateTime(strOrdate)).ToString("dd/MM/yyyy");
                            strTdate = Convert.ToDateTime(strOrdate).ToString("dd-MM-yyyy");
                            if (m_action == 1)
                            {
                                dblFixedPercent = Utility.mdblGetMaxCommiPercen(strComID, strCustomer, oAssets[0], strFDate, strTdate, strBranchID, "");
                            }
                            else
                            {
                                dblFixedPercent = Utility.mdblGetMaxCommiPercen(strComID, strCustomer, oAssets[0], strFDate, strTdate, strBranchID, strOldRefNo);
                            }
                            if (dblFixedPercent == 40)
                            {
                                dblPercent = 40;
                            }



                            for (int int2nd = 0; int2nd < ooAcms.Count; int2nd++)
                            {
                                if (ooAcms[int2nd].strSubGroup_Name != null)
                                {
                                    str2ndGroup = ooAcms[int2nd].strSubGroup_Name;
                                    if (oAssets[0] == str2ndGroup)
                                    {




                                        OrderDetails2 objg = new OrderDetails2();
                                        double dblCommitionValp = 0;

                                        objg.strItemGroup = ooAcms[int2nd].strItemGroup;
                                        objg.strItemName = ooAcms[int2nd].strItemName;
                                        objg.strPowerClass = ooAcms[int2nd].strPowerClass;
                                        objg.strSubGroup_Name = ooAcms[int2nd].strSubGroup_Name;
                                        objg.strPackSize = ooAcms[int2nd].strPackSize;
                                        objg.intItemQty = ooAcms[int2nd].intItemQty;
                                        objg.dblItemRate = ooAcms[int2nd].dblItemRate;
                                        objg.strUnit = ooAcms[int2nd].strUnit;
                                        objg.intBonusQty = ooAcms[int2nd].intBonusQty;
                                        objg.dblTotalAmount = ooAcms[int2nd].dblTotalAmount;
                                        objg.strCustomer = ooAcms[int2nd].strCustomer;
                                        objg.strDate = ooAcms[int2nd].strDate;


                                        objg.strCommitionGroupItemName = ooAcms[int2nd].strItemName;
                                        objg.dblCommitionVal = (((ooAcms[int2nd].intItemQty) * (ooAcms[int2nd].dblItemRate)) * dblPercent) / 100;
                                        dblCommitionValp = (((ooAcms[int2nd].intItemQty) * (ooAcms[int2nd].dblItemRate)) * dblPercent) / 100;
                                        objg.dblItemNetVal = (((ooAcms[int2nd].dblTotalAmount) - dblCommitionValp));
                                        objg.dblPercent = dblPercent;
                                        objg.strCommitionGroupN = oAssets[0];
                                        orderDetails.Add(objg);

                                    }
                                }
                            }

                            dblItemAmount = 0;
                        }
                    }
                    //calculateTotal();
                }




                return Json(orderDetails, JsonRequestBehavior.AllowGet);


            }

        }

        #endregion
    }
}