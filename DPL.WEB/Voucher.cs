using Dutility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPL.WEB
{
    public class Voucher
    {
        #region "Insert"
        public static string gInsertInvStockReqisitionChild(
                                                          string vstrRefNo,
                                                          string vstrItemName,
                                                          double vdblQty,
                                                          double dblRate,
                                                          string vstrUom,
                                                          double vdblTotalAmount)
        {


            string strSQL = "";
            strSQL = "INSERT INTO INV_STOCK_REQUISITION_CHILD";
            strSQL = strSQL + "(REQUISITION_NO, STOCKITEM_NAME, ITEM_QTY, ITEM_RATE, UNIT, ITEM_AMOUNT";
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES(";
            strSQL = strSQL + "'" + vstrRefNo.Trim() + "',";
            strSQL = strSQL + "'" + vstrItemName.Replace("'", "''") + "',";//            'Item Name
            strSQL = strSQL + "'" + vdblQty + "',";//                'Qty
            strSQL = strSQL + "'" + dblRate + "',";//                'Rate
            strSQL = strSQL + "'" + vstrUom + "',";//              'Unit
            strSQL = strSQL + " " + vdblTotalAmount + "";//         'Net Amount
            strSQL = strSQL + ")";
            return strSQL;

        }

        public static string gInsertStockRequisitionMaster(string VstrRefNo,
                                                        string VstrDate,
                                                        string vstrBranchID,
                                                        string VstrProcessName,
                                                        string VstrGodownName,
                                                        string VstrNarrations,
                                                        int Vintstatus,
                                                        double VdblNetQty,
                                                        double VdblNetAmount,
                                                        long VmlngVType,
                                                        string VstrInvLog
                                                       )
        {


            string strSQL;

            strSQL = "INSERT INTO INV_STOCK_REQUISITION_MASTER ";
            strSQL = strSQL + "(REQUISITION_NO, INVOICE_DATE, BRANCH_ID";
            strSQL = strSQL + ",PROCESS_NAME";
            strSQL = strSQL + ", GODOWNS_NAME, NARRATIONS, REQ_STATUS, NET_TOTAL, TOTLA_QTY, INV_VOUCHER_TYPE ";
            if (VstrInvLog != "")
            {
                strSQL = strSQL + ",INV_LOG_NO";
            }
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES (";
            strSQL = strSQL + "'" + VstrRefNo + "',";
            strSQL = strSQL + "" + Utility.cvtSQLDateString(VstrDate) + ",";
            if (vstrBranchID != "")
            {
                strSQL = strSQL + "'" + vstrBranchID + "',";
            }
            else
            {
                strSQL = strSQL + "'0001',";
            }
            if (VstrProcessName != "")
            {
                strSQL = strSQL + "'" + VstrProcessName + "',";
            }
            else
            {
                strSQL = strSQL + "Null,";
            }
            strSQL = strSQL + "'" + VstrGodownName + "',";
            if (VstrNarrations != "")
            {
                strSQL = strSQL + "'" + VstrNarrations + "',";
            }
            else
            {
                strSQL = strSQL + "'',";
            }
            if (Vintstatus != 0)
            {
                strSQL = strSQL + "" + Vintstatus + ",";
            }
            else
            {
                strSQL = strSQL + "0,";
            }
            strSQL = strSQL + "" + VdblNetAmount + ", ";
            strSQL = strSQL + "" + VdblNetQty + ", ";
            strSQL = strSQL + "" + VmlngVType + "";
            if (VstrInvLog != "")
            {
                strSQL = strSQL + ",'" + VstrInvLog.Replace("'", "''") + "'";
            }
            strSQL = strSQL + ")";
            return strSQL;
        }
        public static string gInventoryInsertPurchaseReturn(string vstrRefNo,
                                                       string vstrBillKey,
                                                       long vlngLoop,
                                                       double vdblBillRate,
                                                       double vdblTotalAmount,
                                                       long vlngAgnstRefTyp,
                                                       string vstrItem,
                                                       string vstrGodown,
                                                       string vstrInOut,
                                                       double vdblQuantity,
                                                       double vdblBonusQty,
                                                       double dblCostRate,
                                                       long vlngVType,
                                                       string vdteDate,
                                                       string vstrBranchID,
                                                       string vstrBatchNo,
                                                       long vlngIsBonus,
                                                       string vstrPer = "",
                                                       string vstrUom = "",
                                                       double vdblDiscount = 0,
                                                       int intvType = 13)
        {


            string strSQL;
            double dblSalesAmt = 0;

            dblSalesAmt = vdblBillRate * vdblQuantity;
            vdblQuantity = vdblQuantity + vdblBonusQty;

            //dblCostRate = Utility.gdblGetCostPrice(vstrItem, vdteDate);
            //'dblCostRate = gdblGetCostPrice1(vstrItem, vdteDate)
            strSQL = "INSERT INTO INV_TRAN ";
            strSQL = strSQL + "(BRANCH_ID,INV_TRAN_KEY,INV_TRAN_POSITION,INV_REF_NO, STOCKITEM_NAME,INV_VOUCHER_TYPE,INV_DATE, ";
            strSQL = strSQL + "INV_TRAN_QUANTITY, INV_TRAN_RATE,GODOWNS_NAME, ";
            strSQL = strSQL + "INV_TRAN_AMOUNT, INV_INOUT_FLAG,BILL_TRAN_AGST_VOUCHER_TYPE,";
            strSQL = strSQL + "OUTWARD_QUANTITY,OUTWARD_COST_AMOUNT,OUTWARD_SALES_AMOUNT,INV_TRAN_IS_GIFT ";
            if (vstrPer != "")
            {
                strSQL = strSQL + ",INV_PER ";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",INV_UOM";
            }

            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",INV_LOG_NO";
            }
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES (";
            strSQL = strSQL + "'" + vstrBranchID + "',";
            strSQL = strSQL + "'" + vstrBillKey.Trim() + "'," + vlngLoop + ",";
            strSQL = strSQL + "'" + vstrRefNo.Trim() + "',";
            strSQL = strSQL + "'" + vstrItem + "',";
            strSQL = strSQL + " " + vlngVType + ",";
            strSQL = strSQL + "" + Utility.cvtSQLDateString(vdteDate) + ",";
            strSQL = strSQL + " " + vdblQuantity + ",";
            strSQL = strSQL + "" + vdblBillRate + ", ";
            strSQL = strSQL + "'" + vstrGodown + "',";
            strSQL = strSQL + "0,";
            strSQL = strSQL + "'" + vstrInOut + "',";
            strSQL = strSQL + "" + vlngAgnstRefTyp + ",";
            strSQL = strSQL + " " + vdblQuantity + ",";
            strSQL = strSQL + "0,";//    'Cost Amount
            strSQL = strSQL + " -4800 ";
            strSQL = strSQL + "," + vlngIsBonus + " ";
            if (vstrPer != "")
            {
                strSQL = strSQL + ",'" + vstrPer + "'";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",'" + vstrUom + "'";
            }
            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",'" + vstrBatchNo + "'";
            }
            strSQL = strSQL + ")";
            return strSQL;
        }
        public static string gInventoryInsertTranSalesReturn(string vstrRefNo,
                                                        string vstrBillKey,
                                                        long vlngLoop,
                                                        double vdblBillRate,
                                                        double vdblTotalAmount,
                                                        long vlngAgnstRefTyp,
                                                        string vstrItem,
                                                        string vstrGodown,
                                                        string vstrInOut,
                                                        double vdblQuantity,
                                                        double vdblBonusQty,
                                                        double dblCostRate,
                                                        long vlngVType,
                                                        string vdteDate,
                                                        string vstrBranchID,
                                                        string vstrBatchNo,
                                                        long vlngIsBonus,
                                                        string vstrPer = "",
                                                        string vstrUom = "",
                                                        double vdblDiscount = 0,
                                                        int intvType = 13)
        {


            string strSQL;
            //string strBillWiseRef;
            double dblSalesAmt = 0;

            dblSalesAmt = vdblBillRate * vdblQuantity;
            vdblQuantity = vdblQuantity + vdblBonusQty;

            strSQL = "INSERT INTO INV_TRAN ";
            strSQL = strSQL + "(BRANCH_ID,INV_TRAN_KEY,INV_TRAN_POSITION,INV_REF_NO, STOCKITEM_NAME,INV_VOUCHER_TYPE,INV_DATE, ";
            strSQL = strSQL + "INV_TRAN_QUANTITY, INV_TRAN_RATE,GODOWNS_NAME, ";
            strSQL = strSQL + "INV_TRAN_AMOUNT, INV_INOUT_FLAG,BILL_TRAN_AGST_VOUCHER_TYPE,";
            strSQL = strSQL + "OUTWARD_QUANTITY,OUTWARD_COST_AMOUNT,OUTWARD_SALES_AMOUNT,INV_TRAN_IS_GIFT ";
            if (vstrPer != "")
            {
                strSQL = strSQL + ",INV_PER ";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",INV_UOM";
            }

            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",INV_LOG_NO";
            }
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES (";
            strSQL = strSQL + "'" + vstrBranchID + "',";
            strSQL = strSQL + "'" + vstrBillKey.Trim() + "'," + vlngLoop + ",";
            strSQL = strSQL + "'" + vstrRefNo.Trim() + "',";
            strSQL = strSQL + "'" + vstrItem + "',";
            strSQL = strSQL + " " + vlngVType + ",";
            strSQL = strSQL + "" + Utility.cvtSQLDateString(vdteDate) + ",";
            strSQL = strSQL + " " + vdblQuantity + ",";
            strSQL = strSQL + "" + vdblBillRate + ", ";
            strSQL = strSQL + "'" + vstrGodown + "',";
            if (vlngVType != (long)Utility.VOUCHER_TYPE.vtSALES_CHALLAN)
            {
                strSQL = strSQL + -1 * dblCostRate * Math.Abs(vdblQuantity) + ", ";
            }
            else
            {
                strSQL = strSQL + "0,";
            }
            strSQL = strSQL + "'" + vstrInOut + "',";
            strSQL = strSQL + "" + vlngAgnstRefTyp + ",";
            strSQL = strSQL + " " + vdblQuantity + ",";
            if (vlngVType != (long)Utility.VOUCHER_TYPE.vtSALES_CHALLAN)
            {
                strSQL = strSQL + " " + -1 * dblCostRate * Math.Abs(vdblQuantity) + ",";//    'Cost Amount
            }
            else
            {
                strSQL = strSQL + "0,";
            }
            if (intvType == 13)
            {
                strSQL = strSQL + " 0 ";
            }
            else
            {
                strSQL = strSQL + " " + dblSalesAmt + vdblDiscount + " ";
            }
            strSQL = strSQL + "," + vlngIsBonus + " ";
            if (vstrPer != "")
            {
                strSQL = strSQL + ",'" + vstrPer + "'";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",'" + vstrUom + "'";
            }
            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",'" + vstrBatchNo + "'";
            }
            strSQL = strSQL + ")";
            return strSQL;
        }
        public static string gInsertCompanyVoucher(string vstrRefNumber,
                                                   long vlngVoucherType,
                                                   string vdteDate,
                                                   string vstrMonthID,
                                                   string vdteDueDate,
                                                   string vstrLedgerName,
                                                   double vdblAmount,
                                                   double vdblNetAmount,
                                                   double vdblAddAmount,
                                                   double vdblLessAmount,
                                                   long vlngAgstType,
                                                   string vstrNarrations,
                                                   string vstrBranchID,
                                                   long vlngIsMultiCurrency = 0,
                                                   string vstrAgnstRefNo = "",
                                                   string vstrSalesRep = "",
                                                   string vstrDelivery = "",
                                                   string vstrPayment = "",
                                                   string vstrSupport = "",
                                                   string vstrValidaty = "",
                                                   string vstrOtherTerms = "",
                                                   string vstrOrderNo = "",
                                                   string vstrOrderDate = "",
                                                   string vstrPreparedBy = "",
                                                   string vstrPreparedDate = "",
                                                   string vstrDestination = "",
                                                   string vstrTransport = "",
                                                   double dblCtrQnty = 0,
                                                   double dblBoxQty = 0,
                                                   double dblProcessAmount = 0,
                                                   double dblRoundOff = 0,
                                                   int intSampleStatus = 0,
                                                   int intAppStatus = 0,
                                                   string vstrApprovedBy = "",
                                                   string vstrApprovedDate = "",
                                                   string vstrTeritorryCode = "",
                                                   string vstrCustomerName = "",
                                                   double dblQty = 0,
                                                   int intonLine = 0
                                                  )
        {
            string strSQL = "";


            strSQL = "INSERT INTO ACC_COMPANY_VOUCHER";
            strSQL = strSQL + "(BRANCH_ID,COMP_REF_NO,COMP_VOUCHER_MONTH_ID,COMP_VOUCHER_TYPE,COMP_VOUCHER_DATE,COMP_VOUCHER_DUE_DATE,";
            strSQL = strSQL + "LEDGER_NAME,COMP_VOUCHER_AMOUNT,COMP_VOUCHER_NET_AMOUNT,";
            strSQL = strSQL + "COMP_VOUCHER_ADD_AMOUNT,COMP_VOUCHER_LESS_AMOUNT,";
            strSQL = strSQL + "COMP_AGAINST_REF,COMP_VOUCHER_FC";
            if (vstrNarrations != "")
            {
                strSQL = strSQL + ",COMP_VOUCHER_NARRATION ";
            }
            if (vstrAgnstRefNo != "")
            {
                strSQL = strSQL + ",AGNST_COMP_REF_NO,COMP_VOUCHER_IS_AUTO ";
            }
            if (vstrSalesRep != "" && vstrSalesRep != Utility.gcEND_OF_LIST)
            {
                strSQL = strSQL + ",SALES_REP ";
            }
            if (vstrDelivery != "")
            {
                strSQL = strSQL + ",COMP_DELIVERY ";
            }
            if (vstrPayment != "")
            {
                strSQL = strSQL + ",COMP_TERM_OF_PAYMENTS ";
            }
            if (vstrSupport != "")
            {
                strSQL = strSQL + ",COMP_SUPPORT ";
            }
            if (vstrValidaty != "")
            {
                strSQL = strSQL + ",COMP_VALIDITY_DATE ";
            }
            if (vstrOtherTerms != "")
            {
                strSQL = strSQL + ",COMP_OTHERS ";
            }

            strSQL = strSQL + ",ORDER_NO ";
            strSQL = strSQL + ",ORDER_DATE ";
            strSQL = strSQL + ",PREPARED_BY ";
            strSQL = strSQL + ",PREPARED_DATE ";

            strSQL = strSQL + ",COMP_VOUCHER_DESTINATION ";
            strSQL = strSQL + ",TRANSPORT_NAME ";
            strSQL = strSQL + ",CRT_QTY ";
            strSQL = strSQL + ",BOX_QTY ";
            strSQL = strSQL + ",COMP_VOUCHER_PROCESS_AMOUNT ";
            strSQL = strSQL + ",COMP_ROUND_OFF_AMOUNT ";
            strSQL = strSQL + ",SAMPLE_STATUS ";
            strSQL = strSQL + ",APP_STATUS ";
            strSQL = strSQL + ",APPROVED_BY ";
            strSQL = strSQL + ",APPROVED_DATE ";
            strSQL = strSQL + ",APPS_TERITORRY_CODE ";
            strSQL = strSQL + ",APPS_CUSTOMER_MERZE ";
            strSQL = strSQL + ",APPS_COMP_QTY ";
            strSQL = strSQL + ",ONLINE ";
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES(";
            strSQL = strSQL + "'" + vstrBranchID + "',";
            strSQL = strSQL + "'" + vstrRefNumber.Trim() + "',";
            strSQL = strSQL + "'" + vstrMonthID + "',";
            strSQL = strSQL + " " + vlngVoucherType + ",";
            //strSQL = strSQL + "Convert (DateTime  ,'" + vdteDate + "', 103) ,";
            strSQL = strSQL + " " + Utility.cvtSQLDateString(vdteDate) + ",";
            //strSQL = strSQL + "Convert (DateTime  ,'" + vdteDate + "', 103) ,";
            strSQL = strSQL + " " + Utility.cvtSQLDateString(vdteDueDate) + ",";
            strSQL = strSQL + "'" + vstrLedgerName + "',";
            strSQL = strSQL + " " + vdblAmount + ",";
            strSQL = strSQL + " " + vdblNetAmount + ",";
            strSQL = strSQL + " " + vdblAddAmount + ",";
            strSQL = strSQL + " " + vdblLessAmount + ",";
            strSQL = strSQL + " " + vlngAgstType + ",";
            strSQL = strSQL + " " + vlngIsMultiCurrency + "";
            if (vstrNarrations != "")
            {
                strSQL = strSQL + ",'" + vstrNarrations + "'";
            }
            if (vstrAgnstRefNo != "")
            {
                strSQL = strSQL + ",'" + vstrAgnstRefNo + "'";
                strSQL = strSQL + ",1";
            }
            if (vstrSalesRep != "" && vstrSalesRep != Utility.gcEND_OF_LIST)
            {
                strSQL = strSQL + ",'" + vstrSalesRep + "' ";
            }
            if (vstrDelivery != "")
            {
                strSQL = strSQL + ",'" + vstrDelivery + "' ";
            }
            if (vstrPayment != "")
            {
                strSQL = strSQL + ",'" + vstrPayment + "' ";
            }
            if (vstrSupport != "")
            {
                strSQL = strSQL + ",'" + vstrSupport + "' ";
            }
            if (vstrValidaty != "")
            {
                //strSQL = strSQL + ",Convert (DateTime  ,'" + vstrValidaty + "', 103) ";
                strSQL = strSQL + "," + Utility.cvtSQLDateString(vstrValidaty) + " ";
            }
            if (vstrOtherTerms != "")
            {
                strSQL = strSQL + ",'" + vstrOtherTerms + "' ";
            }

            if (vstrOrderNo != "")
            {
                strSQL = strSQL + ",'" + vstrOrderNo + "' ";
            }
            else
            {
                strSQL = strSQL + ",Null";
            }
            if (vstrOrderDate != "")
            {
                strSQL = strSQL + "," + Utility.cvtSQLDateString(vstrOrderDate) + "";
            }
            else
            {
                strSQL = strSQL + ",Null";
            }
            if (vstrPreparedBy != "")
            {
                strSQL = strSQL + ",'" + vstrPreparedBy + "' ";
            }
            else
            {
                strSQL = strSQL + ",Null";
            }
            if (vstrPreparedDate != "")
            {
                strSQL = strSQL + "," + Utility.cvtSQLDateString(vstrPreparedDate) + "";
            }
            else
            {
                strSQL = strSQL + ",Null";
            }

            if (vstrDestination != "")
            {
                strSQL = strSQL + ",'" + vstrDestination + "' ";
            }
            else
            {
                strSQL = strSQL + ",Null";
            }
            if (vstrTransport != "")
            {
                strSQL = strSQL + ",'" + vstrTransport + "' ";
            }
            else
            {
                strSQL = strSQL + ",Null";
            }

            strSQL = strSQL + "," + dblCtrQnty + " ";

            strSQL = strSQL + "," + dblBoxQty + " ";
            strSQL = strSQL + "," + dblProcessAmount + " ";
            strSQL = strSQL + "," + dblRoundOff + " ";
            strSQL = strSQL + "," + intSampleStatus + " ";
            strSQL = strSQL + "," + intAppStatus + " ";
            if (vstrApprovedBy != "")
            {
                strSQL = strSQL + "," + vstrApprovedBy + " ";
            }
            else
            {
                strSQL = strSQL + ",NULL";
            }
            if (vstrApprovedDate != "")
            {
                strSQL = strSQL + "," + Utility.cvtSQLDateString(vstrApprovedDate) + "";
            }
            else
            {
                strSQL = strSQL + ",NULL";
            }

            strSQL = strSQL + ",'" + vstrCustomerName.Replace("'", "''") + "' ";
            strSQL = strSQL + ",'" + vstrTeritorryCode + "' ";
            strSQL = strSQL + "," + dblQty + " ";
            strSQL = strSQL + "," + intonLine + " ";
            strSQL = strSQL + ")";


            return strSQL;

        }

        public static string gIncreaseVoucher(int vintVoucherValue)
        {


            string strSQL = "";

            strSQL = "UPDATE ACC_VOUCHER_TYPE ";
            strSQL = strSQL + "SET VOUCHER_TYPE_TOTAL_VOUCHER = VOUCHER_TYPE_TOTAL_VOUCHER + 1 ";
            strSQL = strSQL + "WHERE VOUCHER_TYPE_VALUE = " + vintVoucherValue + " ";
            return strSQL;

        }
        public static string gDecreaseVoucher(int vintVoucherValue)
        {


            string strSQL = "";

            strSQL = "UPDATE ACC_VOUCHER_TYPE ";
            strSQL = strSQL + "SET VOUCHER_TYPE_TOTAL_VOUCHER = VOUCHER_TYPE_TOTAL_VOUCHER - 1 ";
            strSQL = strSQL + "WHERE VOUCHER_TYPE_VALUE = " + vintVoucherValue + " ";
            return strSQL;

        }

        public static string mInsertBatch(string vstrLogNo,
                                  string strdate)
        {


            string strSQL = "";

            strSQL = "INSERT INTO INV_BATCH ";
            strSQL = strSQL + "(INV_LOG_NO,INV_LOG_START,INV_LOG_END,INV_LOG_STATUS) ";
            strSQL = strSQL + "VALUES ('" + vstrLogNo.Replace("'", "''") + "',";
            strSQL = strSQL + "" + Utility.cvtSQLDateString(strdate) + "," + Utility.cvtSQLDateString(strdate) + ",";
            strSQL = strSQL + "'Active')";
            return strSQL;

        }
        public static string mInsertInvLog(string vstrInvLog,
                                     string vstrRefNoIN,
                                     string vstrDate,
                                     double vdblAmount,
                                     string vstrProcess,
                                     string vstrRefNoOUT,
                                     string vstrBranchID, string vstrRefnoWastage, int intConvertType)
        {


            string strSQL = "";

            strSQL = "INSERT INTO INV_PRODUCTION_LOG";
            strSQL = strSQL + "(INV_LOG_NO,INV_REF_NO_IN,INV_LOG_DATE,INV_LOG_AMOUNT,INV_REF_NO_OUT,INV_LOG_PROCESS_NAME,BRANCH_ID,INV_REF_NO_WASTAGE,CONVERTTYPE";
            strSQL = strSQL + ") VALUES(";
            strSQL = strSQL + "'" + vstrInvLog.Replace("'", "''") + "',";
            strSQL = strSQL + "'" + vstrRefNoIN + "',";
            strSQL = strSQL + " " + Utility.cvtSQLDateString(vstrDate) + ",";
            strSQL = strSQL + " " + vdblAmount + ",";
            strSQL = strSQL + "'" + vstrRefNoOUT + "',";
            strSQL = strSQL + "'" + vstrProcess + "',";
            strSQL = strSQL + "'" + vstrBranchID + "',";
            strSQL = strSQL + "'" + vstrRefnoWastage + "',";
            strSQL = strSQL + "" + intConvertType + "";
            strSQL = strSQL + ")";
            return strSQL;

        }
        public static string mInsertTranOutwardMFG(string vstrInvKey,
                                                long vlngLoop,
                                                string vstrRefNo,
                                                string vstrItemName,
                                                long vlngVType,
                                                string vdteDate,
                                                double vdblQty,
                                                double vdblrate,
                                                string vstrGodownsName,
                                                double vdblAmount,
                                                string vstrInOutFlag,
                                                string vstrBranchID,
                                                string vstrInvLog = "",
                                                string vstrProcess = "",
                                                string vstrUom = "",
                                                string vstrAlt = "",
                                                string vstrSection = "", int intConverType = 0)
        {


            string strSQL = "";

            strSQL = "INSERT INTO INV_TRAN";
            strSQL = strSQL + "(INV_TRAN_KEY,INV_TRAN_POSITION,INV_REF_NO,STOCKITEM_NAME,INV_VOUCHER_TYPE,INV_DATE,";
            strSQL = strSQL + "INV_TRAN_QUANTITY,INV_TRAN_RATE,GODOWNS_NAME,";
            strSQL = strSQL + "INV_TRAN_AMOUNT,INV_INOUT_FLAG,OUTWARD_QUANTITY,OUTWARD_COST_AMOUNT,OUTWARD_SALES_AMOUNT,";
            strSQL = strSQL + "BRANCH_ID";
            if (vstrInvLog != "")
            {
                strSQL = strSQL + ",INV_LOG_NO ";
            }
            if (vstrProcess != "")
            {
                strSQL = strSQL + ",PROCESS_NAME ";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",INV_UOM ";
            }
            if (vstrAlt != "")
            {
                strSQL = strSQL + ",INV_PER ";
            }
            if (vstrSection != "")
            {
                strSQL = strSQL + ",SECTION_NAME ";
            }
            strSQL = strSQL + ",CONVERSION_TYPE";
            strSQL = strSQL + ") VALUES(";
            strSQL = strSQL + "'" + vstrInvKey + "',";
            strSQL = strSQL + " " + vlngLoop + ",";
            strSQL = strSQL + "'" + vstrRefNo + "',";
            strSQL = strSQL + "'" + vstrItemName + "',";
            strSQL = strSQL + " " + vlngVType + ",";
            strSQL = strSQL + " " + Utility.cvtSQLDateString(vdteDate) + " ,";
            strSQL = strSQL + " " + vdblQty * -1 + ",";
            strSQL = strSQL + " " + vdblrate + ",";
            strSQL = strSQL + "'" + vstrGodownsName + "', ";
            strSQL = strSQL + " " + vdblAmount * -1 + ",";
            strSQL = strSQL + "'" + vstrInOutFlag + "',";
            strSQL = strSQL + " " + vdblQty * -1 + ",";
            strSQL = strSQL + " " + vdblAmount * -1 + ",";
            if (intConverType == 1)
            {
                //strSQL = strSQL + " " + vdblQty * vdblrate + ", ";
                strSQL = strSQL + " " + vdblAmount + ", ";
            }
            else
            {
                strSQL = strSQL + " " + vdblQty * vdblrate * -1 + ", ";
            }
            strSQL = strSQL + "'" + vstrBranchID + "'";
            if (vstrInvLog != "")
            {
                strSQL = strSQL + ",'" + vstrInvLog.Replace("'", "''") + "'";
            }
            if (vstrProcess != "")
            {
                strSQL = strSQL + ",'" + vstrProcess + "'";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",'" + vstrUom + "'";
            }
            if (vstrAlt != "")
            {
                strSQL = strSQL + ",'" + vstrAlt + "'";
            }
            if (vstrSection != "")
            {
                strSQL = strSQL + ",'" + vstrSection.Replace("'", "''") + "'";
            }
            strSQL = strSQL + "," + intConverType + " ";
            strSQL = strSQL + ")";
            return strSQL;

        }
        public static string mInsertTranOutwardConsumption(string vstrInvKey,
                                                long vlngLoop,
                                                string vstrRefNo,
                                                string vstrItemName,
                                                long vlngVType,
                                                string vdteDate,
                                                double vdblQty,
                                                double vdblrate,
                                                string vstrGodownsName,
                                                double vdblAmount,
                                                string vstrInOutFlag,
                                                string vstrBranchID,
                                                string vstrInvLog = "",
                                                string vstrProcess = "",
                                                string vstrUom = "",
                                                string vstrAlt = "",
                                                string vstrSection = "", int intConverType = 0)
        {


            string strSQL = "";

            strSQL = "INSERT INTO INV_TRAN";
            strSQL = strSQL + "(INV_TRAN_KEY,INV_TRAN_POSITION,INV_REF_NO,STOCKITEM_NAME,INV_VOUCHER_TYPE,INV_DATE,";
            strSQL = strSQL + "INV_TRAN_QUANTITY,INV_TRAN_RATE,GODOWNS_NAME,";
            strSQL = strSQL + "INV_TRAN_AMOUNT,INV_INOUT_FLAG,OUTWARD_QUANTITY,OUTWARD_COST_AMOUNT,OUTWARD_SALES_AMOUNT,";
            strSQL = strSQL + "BRANCH_ID";
            if (vstrInvLog != "")
            {
                strSQL = strSQL + ",INV_LOG_NO ";
            }
            if (vstrProcess != "")
            {
                strSQL = strSQL + ",PROCESS_NAME ";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",INV_UOM ";
            }
            if (vstrAlt != "")
            {
                strSQL = strSQL + ",INV_PER ";
            }
            if (vstrSection != "")
            {
                strSQL = strSQL + ",SECTION_NAME ";
            }
            strSQL = strSQL + ",CONVERSION_TYPE";
            strSQL = strSQL + ") VALUES(";
            strSQL = strSQL + "'" + vstrInvKey + "',";
            strSQL = strSQL + " " + vlngLoop + ",";
            strSQL = strSQL + "'" + vstrRefNo + "',";
            strSQL = strSQL + "'" + vstrItemName + "',";
            strSQL = strSQL + " " + vlngVType + ",";
            strSQL = strSQL + " " + Utility.cvtSQLDateString(vdteDate) + " ,";
            strSQL = strSQL + " " + vdblQty * -1 + ",";
            strSQL = strSQL + " " + vdblrate + ",";
            strSQL = strSQL + "'" + vstrGodownsName + "', ";
            strSQL = strSQL + " " + vdblAmount * -1 + ",";
            strSQL = strSQL + "'" + vstrInOutFlag + "',";
            strSQL = strSQL + " " + vdblQty * -1 + ",";
            strSQL = strSQL + " " + vdblAmount * -1 + ",";
            strSQL = strSQL + " " + vdblQty * -1 * vdblrate + ", ";
            strSQL = strSQL + "'" + vstrBranchID + "'";
            if (vstrInvLog != "")
            {
                strSQL = strSQL + ",'" + vstrInvLog.Replace("'", "''") + "'";
            }
            if (vstrProcess != "")
            {
                strSQL = strSQL + ",'" + vstrProcess + "'";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",'" + vstrUom + "'";
            }
            if (vstrAlt != "")
            {
                strSQL = strSQL + ",'" + vstrAlt + "'";
            }
            if (vstrSection != "")
            {
                strSQL = strSQL + ",'" + vstrSection.Replace("'", "''") + "'";
            }
            strSQL = strSQL + "," + intConverType + " ";
            strSQL = strSQL + ")";
            return strSQL;

        }
        public static string mInsertTranOutward(string vstrInvKey,
                                                long vlngLoop,
                                                string vstrRefNo,
                                                string vstrItemName,
                                                long vlngVType,
                                                string vdteDate,
                                                double vdblQty,
                                                double vdblrate,
                                                string vstrGodownsName,
                                                double vdblAmount,
                                                string vstrInOutFlag,
                                                string vstrBranchID,
                                                string vstrInvLog = "",
                                                string vstrProcess = "",
                                                string vstrUom = "",
                                                string vstrAlt = "",
                                                string vstrSection = "",
                                                int intConverType = 0, string vstrToGodownsName = "")
        {


            string strSQL = "";

            strSQL = "INSERT INTO INV_TRAN";
            strSQL = strSQL + "(INV_TRAN_KEY,INV_TRAN_POSITION,INV_REF_NO,STOCKITEM_NAME,INV_VOUCHER_TYPE,INV_DATE,";
            strSQL = strSQL + "INV_TRAN_QUANTITY,INV_TRAN_RATE,GODOWNS_NAME,GODOWNS_NAME1,";
            strSQL = strSQL + "INV_TRAN_AMOUNT,INV_INOUT_FLAG,OUTWARD_QUANTITY,OUTWARD_COST_AMOUNT,OUTWARD_SALES_AMOUNT,";
            strSQL = strSQL + "BRANCH_ID";
            if (vstrInvLog != "")
            {
                strSQL = strSQL + ",INV_LOG_NO ";
            }
            if (vstrProcess != "")
            {
                strSQL = strSQL + ",PROCESS_NAME ";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",INV_UOM ";
            }
            if (vstrAlt != "")
            {
                strSQL = strSQL + ",INV_PER ";
            }
            if (vstrSection != "")
            {
                strSQL = strSQL + ",SECTION_NAME ";
            }
            strSQL = strSQL + ",CONVERSION_TYPE";
            strSQL = strSQL + ") VALUES(";
            strSQL = strSQL + "'" + vstrInvKey + "',";
            strSQL = strSQL + " " + vlngLoop + ",";
            strSQL = strSQL + "'" + vstrRefNo + "',";
            strSQL = strSQL + "'" + vstrItemName + "',";
            strSQL = strSQL + " " + vlngVType + ",";
            strSQL = strSQL + " " + Utility.cvtSQLDateString(vdteDate) + " ,";
            strSQL = strSQL + " " + vdblQty * -1 + ",";
            strSQL = strSQL + " " + vdblrate + ",";
            strSQL = strSQL + "'" + vstrGodownsName + "', ";
            strSQL = strSQL + "'" + vstrToGodownsName + "', ";
            strSQL = strSQL + " " + vdblAmount * -1 + ",";
            strSQL = strSQL + "'" + vstrInOutFlag + "',";
            strSQL = strSQL + " " + vdblQty * -1 + ",";
            strSQL = strSQL + " " + vdblAmount * -1 + ",";
            if (intConverType == 1)
            {
                //strSQL = strSQL + " " + vdblQty * vdblrate + ", ";
                strSQL = strSQL + " " + vdblAmount + ", ";
            }
            else
            {
                strSQL = strSQL + " " + vdblQty * -1 * vdblrate + ", ";
            }
            strSQL = strSQL + "'" + vstrBranchID + "'";
            if (vstrInvLog != "")
            {
                strSQL = strSQL + ",'" + vstrInvLog.Replace("'", "''") + "'";
            }
            if (vstrProcess != "")
            {
                strSQL = strSQL + ",'" + vstrProcess + "'";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",'" + vstrUom + "'";
            }
            if (vstrAlt != "")
            {
                strSQL = strSQL + ",'" + vstrAlt + "'";
            }
            if (vstrSection != "")
            {
                strSQL = strSQL + ",'" + vstrSection.Replace("'", "''") + "'";
            }
            strSQL = strSQL + "," + intConverType + " ";
            strSQL = strSQL + ")";
            return strSQL;

        }

        public static string mInsertTranOutwardPhysical(string vstrInvKey,
                                                long vlngLoop,
                                                string vstrRefNo,
                                                string vstrItemName,
                                                long vlngVType,
                                                string vdteDate,
                                                double vdblQty,
                                                double vdblrate,
                                                string vstrGodownsName,
                                                double vdblAmount,
                                                string vstrInOutFlag,
                                                string vstrBranchID,
                                                string vstrInvLog = "",
                                                string vstrProcess = "",
                                                string vstrUom = "",
                                                string vstrAlt = "",
                                                string vstrSection = "", int intConverType = 0, double dblCurrentQTy = 0)
        {


            string strSQL = "";

            strSQL = "INSERT INTO INV_TRAN";
            strSQL = strSQL + "(INV_TRAN_KEY,INV_TRAN_POSITION,INV_REF_NO,STOCKITEM_NAME,INV_VOUCHER_TYPE,INV_DATE,";
            strSQL = strSQL + "INV_TRAN_QUANTITY,INV_TRAN_RATE,GODOWNS_NAME,";
            strSQL = strSQL + "INV_TRAN_AMOUNT,INV_INOUT_FLAG,OUTWARD_QUANTITY,";
            strSQL = strSQL + "BRANCH_ID";
            if (vstrInvLog != "")
            {
                strSQL = strSQL + ",INV_LOG_NO ";
            }
            if (vstrProcess != "")
            {
                strSQL = strSQL + ",PROCESS_NAME ";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",INV_UOM ";
            }
            if (vstrAlt != "")
            {
                strSQL = strSQL + ",INV_PER ";
            }
            if (vstrSection != "")
            {
                strSQL = strSQL + ",SECTION_NAME ";
            }
            strSQL = strSQL + ",CONVERSION_TYPE,INV_CURRENT_STOCK";
            strSQL = strSQL + ") VALUES(";
            strSQL = strSQL + "'" + vstrInvKey + "',";
            strSQL = strSQL + " " + vlngLoop + ",";
            strSQL = strSQL + "'" + vstrRefNo + "',";
            strSQL = strSQL + "'" + vstrItemName + "',";
            strSQL = strSQL + " " + vlngVType + ",";
            strSQL = strSQL + " " + Utility.cvtSQLDateString(vdteDate) + " ,";
            strSQL = strSQL + " " + vdblQty * -1 + ",";
            strSQL = strSQL + " " + vdblrate + ",";
            strSQL = strSQL + "'" + vstrGodownsName + "', ";
            strSQL = strSQL + " " + vdblAmount * -1 + ",";
            strSQL = strSQL + "'" + vstrInOutFlag + "',";
            strSQL = strSQL + " " + vdblQty * -1 + ",";
            strSQL = strSQL + "'" + vstrBranchID + "'";
            if (vstrInvLog != "")
            {
                strSQL = strSQL + ",'" + vstrInvLog.Replace("'", "''") + "'";
            }
            if (vstrProcess != "")
            {
                strSQL = strSQL + ",'" + vstrProcess + "'";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",'" + vstrUom + "'";
            }
            if (vstrAlt != "")
            {
                strSQL = strSQL + ",'" + vstrAlt + "'";
            }
            if (vstrSection != "")
            {
                strSQL = strSQL + ",'" + vstrSection.Replace("'", "''") + "'";
            }
            strSQL = strSQL + "," + intConverType + " ";
            strSQL = strSQL + "," + dblCurrentQTy + " ";
            strSQL = strSQL + ")";
            return strSQL;

        }
        public static string gInsertRepacking(string vstrRefNo, string vstrItemName, double dblQty)
        {

            string strSQL;

            try
            {

                strSQL = "INSERT INTO INV_TRAN_REPACKING(";
                strSQL = strSQL + "INV_REF_NO,STOCKITEM_NAME,QNTY";
                strSQL = strSQL + ") ";
                strSQL = strSQL + "VALUES";
                strSQL = strSQL + "(";
                strSQL = strSQL + ",'" + vstrRefNo + "',";
                strSQL = strSQL + ",'" + vstrItemName + "',";
                strSQL = strSQL + "," + dblQty + " ";
                strSQL = strSQL + ")";
                return strSQL;

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
            }

        }
        public static string mInsertTranInward(string vstrInvKey,
                                                long vlngLoop,
                                                string vstrRefNo,
                                                string vstrItemName,
                                                long vlngVType,
                                                string vdteDate,
                                                double vdblQty,
                                                double vdblrate,
                                                string vstrGodownsName,
                                                double vdblAmount,
                                                string vstrInOutFlag,
                                                string vstrBranchID,
                                                string vstrInvLog = "",
                                                string vstrProcess = "",
                                                string vstrUom = "",
                                                string vstrAlt = "",
                                                string vstrSection = "",
                                                int intConverType = 0,
                                                double dblCurrentQty = 0,
                                                string vstrGodownsName1 = "")
        {


            string strSQL = "";

            strSQL = "INSERT INTO INV_TRAN";
            strSQL = strSQL + "(INV_TRAN_KEY,INV_TRAN_POSITION,INV_REF_NO,STOCKITEM_NAME,INV_VOUCHER_TYPE,INV_DATE,";
            strSQL = strSQL + "INV_TRAN_QUANTITY,INV_TRAN_RATE,GODOWNS_NAME,GODOWNS_NAME1,";
            strSQL = strSQL + "INV_TRAN_AMOUNT,INWARD_AMOUNT, INV_INOUT_FLAG,INWARD_QUANTITY,";
            strSQL = strSQL + "BRANCH_ID";
            if (vstrInvLog != "")
            {
                strSQL = strSQL + ",INV_LOG_NO ";
            }
            if (vstrProcess != "")
            {
                strSQL = strSQL + ",PROCESS_NAME ";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",INV_UOM ";
            }
            if (vstrAlt != "")
            {
                strSQL = strSQL + ",INV_PER ";
            }
            if (vstrSection != "")
            {
                strSQL = strSQL + ",SECTION_NAME ";
            }
            strSQL = strSQL + ",CONVERSION_TYPE,INV_CURRENT_STOCK";
            strSQL = strSQL + ") VALUES(";
            strSQL = strSQL + "'" + vstrInvKey + "',";
            strSQL = strSQL + " " + vlngLoop + ",";
            strSQL = strSQL + "'" + vstrRefNo + "',";
            strSQL = strSQL + "'" + vstrItemName + "',";
            strSQL = strSQL + " " + vlngVType + ",";
            strSQL = strSQL + " " + Utility.cvtSQLDateString(vdteDate) + " ,";
            strSQL = strSQL + " " + vdblQty + ",";
            strSQL = strSQL + " " + vdblrate + ",";
            strSQL = strSQL + "'" + vstrGodownsName + "', ";
            strSQL = strSQL + "'" + vstrGodownsName1 + "', ";
            strSQL = strSQL + " " + vdblAmount + ",";
            strSQL = strSQL + " " + vdblAmount + ",";
            strSQL = strSQL + "'" + vstrInOutFlag + "',";
            strSQL = strSQL + " " + vdblQty + ",";
            //strSQL = strSQL + " " + vdblAmount + ",";
            //strSQL = strSQL + " " + vdblAmount + ", ";
            strSQL = strSQL + "'" + vstrBranchID + "'";
            if (vstrInvLog != "")
            {
                strSQL = strSQL + ",'" + vstrInvLog.Replace("'", "''") + "'";
            }
            if (vstrProcess != "")
            {
                strSQL = strSQL + ",'" + vstrProcess + "'";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",'" + vstrUom + "'";
            }
            if (vstrAlt != "")
            {
                strSQL = strSQL + ",'" + vstrAlt + "'";
            }
            if (vstrSection != "")
            {
                strSQL = strSQL + ",'" + vstrSection.Replace("'", "''") + "'";
            }
            strSQL = strSQL + "," + intConverType + " ";
            strSQL = strSQL + "," + dblCurrentQty + " ";
            strSQL = strSQL + ")";
            return strSQL;

        }

        public static string gInsertBillTranProcess(string vstrRefNoKey,
                                  string vstrBranchID,
                                  long vlngLoop,
                                  string vstrCompRefNo,
                                  string vstrAgstCompRefNo,
                                  long vlngVType,
                                  string vdteDate,
                                  string vstrItemName,
                                  string vstrGodownName,
                                  double vdblQty,
                                  string vstrUom,
                                  string vstrAgstRefNoKey,
                                  double vdblDiscAmt,
                                  double vdblNetAmt,
                                  string vstrPer = "")
        {


            string strSQL = "";

            strSQL = "INSERT INTO ACC_BILL_TRAN_PROCESS";
            strSQL = strSQL + "(BRANCH_ID,BILL_TRAN_KEY,BILL_TRAN_POSITION,COMP_REF_NO,AGST_COMP_REF_NO,";
            strSQL = strSQL + "COMP_VOUCHER_TYPE,COMP_VOUCHER_DATE,";
            strSQL = strSQL + "STOCKITEM_NAME,GODOWNS_NAME,BILL_QUANTITY,BILL_UOM,BILL_PER,AGST_REF_NO_KEY,ADD_LESS_AMOUNT,BILL_NET_AMOUNT";
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES";
            strSQL = strSQL + "( ";
            strSQL = strSQL + "'" + vstrBranchID + "',";
            strSQL = strSQL + "'" + vstrRefNoKey.Trim() + "'," + vlngLoop + ",";
            strSQL = strSQL + "'" + vstrCompRefNo.Trim() + "','" + vstrAgstCompRefNo.Trim() + "',";
            strSQL = strSQL + "" + vlngVType + ",";
            strSQL = strSQL + "" + Utility.cvtSQLDateString(vdteDate) + ",";
            strSQL = strSQL + "'" + vstrItemName + "','" + vstrGodownName + "',";
            strSQL = strSQL + " " + vdblQty + ",'" + vstrUom + "','" + vstrPer + "',";
            strSQL = strSQL + "'" + vstrAgstRefNoKey.Trim() + "',";
            strSQL = strSQL + " " + vdblDiscAmt + ",";
            strSQL = strSQL + " " + vdblNetAmt + "";
            strSQL = strSQL + ")";
            return strSQL;

        }

        public static string gInsertBillTran(string vstrBillKey,
                                       string vstrRefNo,
                                       long vlngVType,
                                       string vstrDate,
                                       string vstrItemName,
                                       string vstrGodownName,
                                       double vdblQty,
                                       double vdblBonusQty,
                                       string vstrUom,
                                       double vdblrate,
                                       double vdblTotalAmount,
                                       string vstrAddLess,
                                       double vdblDiscount,
                                       double vdblDebitValue,
                                       string vstrDrCr,
                                       long vlngposition,
                                       string vstrBranchID,
                                       string vstrCurrrency = "",
                                       string vstrPer = "",
                                       string vstrSalesRep = "",
                                       string vstrAgnstRefNo = "",
                                       string vstrBatchNo = "",
                                       string vstrItemDesc = "",
                                       string strOrderRefNo = "", string vstrItemNameBangla = "",
                                       string vstrGroupName = "",
                                       double dblShort = 0, double dblComPer = 0)
        {


            string strSQL = "";

            strSQL = "INSERT INTO ACC_BILL_TRAN";
            strSQL = strSQL + "(BRANCH_ID,BILL_TRAN_KEY,COMP_REF_NO,COMP_VOUCHER_TYPE,COMP_VOUCHER_DATE,";
            strSQL = strSQL + "STOCKITEM_NAME,GODOWNS_NAME,BILL_QUANTITY,BILL_QUANTITY_BONUS,";
            strSQL = strSQL + "BILL_UOM,BILL_PER,BILL_RATE,BILL_AMOUNT,";
            strSQL = strSQL + "BILL_ADD_LESS,BILL_ADD_LESS_AMOUNT ,";
            strSQL = strSQL + "BILL_NET_AMOUNT,BILL_TRAN_TOBY,";
            strSQL = strSQL + "BILL_TRAN_POSITION";
            if (vstrCurrrency != "")
            {
                strSQL = strSQL + ",VOUCHER_CURRENCY_SYMBOL ";
            }

            if (vstrAgnstRefNo != "")
            {
                strSQL = strSQL + ",AGNST_COMP_REF_NO ";
            }
            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",INV_LOG_NO ";
            }
            if (vstrItemDesc != "")
            {
                strSQL = strSQL + ",STOCKITEM_DESCRIPTION ";
            }
            if (strOrderRefNo != "")
            {
                strSQL = strSQL + ",AGNST_COMP_REF_NO1";
            }
            if (vstrItemNameBangla != "")
            {
                strSQL = strSQL + ",ITEM_NAME_BANGLA";
            }
            if (vstrGroupName != "")
            {
                strSQL = strSQL + ",STOCKGROUP_NAME";
            }
            strSQL = strSQL + ",SHORT_QTY";
            strSQL = strSQL + ",G_COMM_PER";
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES(";
            strSQL = strSQL + "'" + vstrBranchID + "',";
            strSQL = strSQL + "'" + vstrBillKey.Trim() + "',";
            strSQL = strSQL + "'" + vstrRefNo.Trim() + "'," + vlngVType + ",";
            strSQL = strSQL + " " + Utility.cvtSQLDateString(vstrDate) + ",";
            strSQL = strSQL + "'" + vstrItemName + "',";//            'Item Name
            strSQL = strSQL + "'" + vstrGodownName + "',";//        'Location
            strSQL = strSQL + "'" + vdblQty + "',";//                'Qty
            strSQL = strSQL + "'" + vdblBonusQty + "',";//           'BonusQty
            strSQL = strSQL + "'" + vstrUom + "',";//              'Unit
            strSQL = strSQL + "'" + vstrPer + "',";//                 'Unit
            strSQL = strSQL + " " + vdblrate + ",";//              'Rate
            strSQL = strSQL + " " + vdblDebitValue + ",";//          'Total
            strSQL = strSQL + " " + vdblDiscount + ",";//         'Add/Less
            strSQL = strSQL + " " + vdblDiscount * -1 + ",";//
            strSQL = strSQL + " " + vdblTotalAmount + ",";//         'Net Amount
            strSQL = strSQL + "'" + vstrDrCr + "',";//
            strSQL = strSQL + "" + vlngposition + "";
            if (vstrCurrrency != "")
            {
                strSQL = strSQL + ",'" + vstrCurrrency + "'";
            }
            if (vstrAgnstRefNo != "")
            {
                strSQL = strSQL + ",'" + vstrAgnstRefNo + "'";
            }
            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",'" + vstrBatchNo + "'";
            }

            if (vstrItemDesc != "")
            {
                strSQL = strSQL + ",'" + vstrItemDesc + "'";
            }
            if (strOrderRefNo != "")
            {
                strSQL = strSQL + ",'" + strOrderRefNo + "'";
            }
            if (vstrItemNameBangla != "")
            {
                strSQL = strSQL + ",'" + vstrItemNameBangla.Replace("'", "''");
            }
            if (vstrGroupName != "")
            {
                strSQL = strSQL + ",'" + vstrGroupName.Replace("'", "''") + "'";
            }
            strSQL = strSQL + "," + dblShort + " ";
            strSQL = strSQL + "," + dblComPer + " ";
            strSQL = strSQL + ")";
            return strSQL;

        }


        public static string gInsertBillTranFC(string vstrBillKey,
                                         string vstrRefNo,
                                         long vlngVType,
                                         string vdteDate,
                                         string vstrItemName,
                                         string vstrGodownName,
                                         double vdblQty,
                                         double vdblBonusQty,
                                         string vstrUom,
                                         double vdblrate,
                                         double vdblTotalAmount,
                                         string vstrAddLess,
                                         double vdblDiscount,
                                         double vdblDebitValue,
                                         string vstrDrCr,
                                         double vdblCurrRate,
                                         double vdblFCDebit,
                                         string vstrBranchID,
                                         string vstrCurrFormalName,
                                         long vlngposition = 0,
                                         string vstrPer = "",
                                         string vstrSalesRep = "",
                                         string vstrBatchNo = "", string strOrderRefNo = "", string vstrItemNameBangla = "")
        {


            string strSQL = "";

            strSQL = "INSERT INTO ACC_BILL_TRAN";
            strSQL = strSQL + "(BRANCH_ID,BILL_TRAN_KEY,COMP_REF_NO,COMP_VOUCHER_TYPE,COMP_VOUCHER_DATE,";
            strSQL = strSQL + "STOCKITEM_NAME,GODOWNS_NAME,BILL_QUANTITY,";
            strSQL = strSQL + "BILL_UOM,BILL_PER,BILL_RATE,BILL_AMOUNT,BILL_ADD_LESS,BILL_ADD_LESS_AMOUNT ,";
            strSQL = strSQL + "BILL_NET_AMOUNT,BILL_TRAN_TOBY,";
            strSQL = strSQL + "FC_CONVERSION_RATE,VOUCHER_FC_AMOUNT,VOUCHER_CURRENCY_SYMBOL ";
            if (vlngposition != 0)
            {
                strSQL = strSQL + ",BILL_TRAN_POSITION";
            }

            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",INV_LOG_NO ";
            }
            if (strOrderRefNo != "")
            {
                strSQL = strSQL + ",ORDER_REF_NO";
            }
            if (vstrItemNameBangla != "")
            {
                strSQL = strSQL + ",ITEM_NAME_BANGLA";
            }
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES(";
            strSQL = strSQL + "'" + vstrBranchID + "',";
            strSQL = strSQL + "'" + vstrBillKey.Trim() + "',";
            strSQL = strSQL + "'" + vstrRefNo.Trim() + "'," + vlngVType + ",";
            strSQL = strSQL + " " + Utility.cvtSQLDateString(vdteDate) + ",";
            strSQL = strSQL + "'" + vstrItemName.Trim() + "',"; // 'Item Name
            strSQL = strSQL + "'" + vstrGodownName.Trim() + "',";// 'Location
            strSQL = strSQL + "'" + vdblQty + "',";            //'Qty
            strSQL = strSQL + "'" + vstrUom + "',";            //  'Unit
            strSQL = strSQL + "'" + vstrPer + "',";           //    'Per
            strSQL = strSQL + " " + vdblrate + ",";          //   'Rate
            strSQL = strSQL + " " + vdblTotalAmount + ",";       //   'Total
            strSQL = strSQL + "'" + vstrAddLess + "',";       //    'Add/Less
            strSQL = strSQL + " " + vdblDiscount + ",";
            strSQL = strSQL + " " + vdblDebitValue + ",";    //      'Net Total
            strSQL = strSQL + "'" + vstrDrCr + "'";
            strSQL = strSQL + "," + vdblCurrRate + ", ";
            strSQL = strSQL + " " + vdblFCDebit + ",";
            strSQL = strSQL + "'" + vstrCurrFormalName + "' ";
            if (vlngposition != 0)
            {
                strSQL = strSQL + ", " + vlngposition + "";
            }
            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",'" + vstrBatchNo + "'";
            }
            if (strOrderRefNo != "")
            {
                strSQL = strSQL + ",'" + strOrderRefNo + "'";
            }
            if (vstrItemNameBangla != "")
            {
                strSQL = strSQL + ",'" + vstrItemNameBangla.Replace("'", "''") + "'";
            }
            strSQL = strSQL + ")";
            return strSQL;

        }



        public static string mInsertVector(string vstrRefNumber, string strCategoryName, string strNaster,
                                            string vdteDate,
                                            string vstrLedgerName,
                                            string vstrDrCr,
                                            long lngvoucherPos,
                                            long lngvpos,
                                            Int16 vlngledgerType,
                                            string vstrBranchID,
                                            double dblVectorAmount,
                                            double mdblCurrRate,
                                            string mstrFCsymbol,
                                            long mlngVType)
        {


            string strSQL = "";
            double dblFcAmount = 0;
            int lngMainRow = 0;
            strSQL = "INSERT INTO VECTOR_TRANSACTION ";
            strSQL = strSQL + "(COMP_REF_NO,VT_TRAN_DATE,VT_POSITION, ";
            if (strCategoryName != "")
            {
                strSQL = strSQL + "VCATEGORY_NAME,";
            }
            strSQL = strSQL + "VT_TYPE,";
            if (strNaster != "")
            {
                strSQL = strSQL + "VMASTER_NAME,";
            }
            strSQL = strSQL + "LEDGER_NAME,VT_TRAN_AMOUNT,VT_TRAN_TOBY,COMP_VOUCHER_POSITION,";
            if (mdblCurrRate != 0)
            {
                if (mlngVType == (long)Utility.VOUCHER_TYPE.vtSALES_INVOICE)
                {
                    if (vlngledgerType == 1)
                    {
                        strSQL = strSQL + "VOUCHER_FC_DEBIT_AMOUNT ";
                    }
                }
                if (mlngVType == (long)Utility.VOUCHER_TYPE.vtSALES_INVOICE)
                {
                    if (vlngledgerType == 2)
                    {
                        strSQL = strSQL + "VOUCHER_FC_CREDIT_AMOUNT";
                    }
                }
                if (mlngVType == (long)Utility.VOUCHER_TYPE.vtPURCHASE_INVOICE)
                {
                    if (vlngledgerType == 2)
                    {
                        strSQL = strSQL + "VOUCHER_FC_DEBIT_AMOUNT";
                    }
                }
                if (mlngVType == (long)Utility.VOUCHER_TYPE.vtPURCHASE_INVOICE)
                {
                    if (vlngledgerType == 1)
                    {
                        strSQL = strSQL + "VOUCHER_FC_CREDIT_AMOUNT";
                    }
                }
                strSQL = strSQL + ",FC_CONVERSION_RATE,VT_CURRENCY_SYMBOL,";
            }
            strSQL = strSQL + "BRANCH_ID) ";
            strSQL = strSQL + "Values('" + vstrRefNumber + "',";
            strSQL = strSQL + " " + Utility.cvtSQLDateString(vdteDate) + ", ";
            strSQL = strSQL + "'" + lngMainRow + "', ";
            if (strCategoryName != "")
            {
                strSQL = strSQL + "'" + strCategoryName.Replace("'", "''") + "',";
            }
            strSQL = strSQL + "" + mlngVType + ", ";
            if (strNaster != "")
            {
                strSQL = strSQL + "'" + strNaster.Replace("'", "''") + "',";
            }
            strSQL = strSQL + "'" + vstrLedgerName.Replace("'", "''") + "',";
            if (vstrDrCr == "Dr")
            {
                strSQL = strSQL + " " + dblVectorAmount * -1 + ", ";
            }
            else
            {
                strSQL = strSQL + " " + dblVectorAmount + ", ";
            }
            strSQL = strSQL + "'" + vstrDrCr + "',";
            strSQL = strSQL + "" + lngvoucherPos + ",";
            // strSQL = strSQL + "" + lngvpos  + ",";

            if (mdblCurrRate != 0)
            {
                dblFcAmount = dblVectorAmount; // mdblCurrRate
                if (vlngledgerType == 2)
                {
                    strSQL = strSQL + " " + dblFcAmount + " ";
                }
                strSQL = strSQL + "," + mdblCurrRate + "";
                strSQL = strSQL + ",'" + mstrFCsymbol + "' ";
                strSQL = strSQL + ",";
            }
            strSQL = strSQL + "'" + vstrBranchID + "' ";
            strSQL = strSQL + ") ";
            return strSQL;

        }


        public static string gInsertmaster(string vstrRef,
                                             string vstrBranch,
                                             long vlngVType,
                                             string vdteDate,
                                             double vdblAmount,
                                             string vstrNarration,
                                             string vstrInvLog = "",
                                             long vlngCostOption = 0,
                                             string vstrProcess = "",
                                             string vlngManuVouAuto = "")
        {


            string strSQL = "";
            strSQL = "INSERT INTO INV_MASTER";
            strSQL = strSQL + "(INV_REF_NO,INV_VOUCHER_TYPE,";
            strSQL = strSQL + "INV_DATE,INV_AMOUNT,";
            if (vstrNarration != "")
            {
                strSQL = strSQL + "INV_NARRATIONS,";
            }
            if (vstrInvLog != "")
            {
                strSQL = strSQL + "INV_LOG_NO,";
            }
            if (vlngManuVouAuto != "0")
            {
                strSQL = strSQL + "INV_MANU_VOUCHER_AUTO,";
            }
            if (vlngCostOption != 0)
            {
                strSQL = strSQL + "INV_COST_OPTION,";
            }
            if (vstrProcess != "")
            {
                strSQL = strSQL + "PROCESS_NAME,";
            }
            strSQL = strSQL + "BRANCH_ID ";
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES";
            strSQL = strSQL + "(";
            strSQL = strSQL + "'" + vstrRef + "',";
            strSQL = strSQL + " " + vlngVType + ", ";
            strSQL = strSQL + " " + Utility.cvtSQLDateString(vdteDate) + ",";
            strSQL = strSQL + " " + vdblAmount + ",";
            if (vstrNarration != "")
            {
                strSQL = strSQL + "'" + vstrNarration + "',";
            }
            if (vstrInvLog != "")
            {
                strSQL = strSQL + "'" + vstrInvLog + "',";
            }
            if (vlngManuVouAuto != "0")
            {
                strSQL = strSQL + " " + vlngManuVouAuto + ",";
            }
            if (vlngCostOption != 0)
            {
                strSQL = strSQL + " " + vlngCostOption + ",";
            }
            if (vlngCostOption != 0)
            {
                strSQL = strSQL + "'" + vstrProcess + "',";
            }
            strSQL = strSQL + "'" + vstrBranch + "'";
            strSQL = strSQL + ")";
            return strSQL;

        }



        public static string gInsertmaster(string vstrRef, string vstrBranch,
                                                        long vlngVType, string vdteDate, double vdblAmount,
                                                        string vstrNarration, string vstrInvLog = "",
                                                        long vlngCostOption = 0, string vstrProcess = "",
                                                        int vlngManuVouAuto = 0)
        {


            string strSQL = "";
            strSQL = "INSERT INTO INV_MASTER";
            strSQL = strSQL + "(INV_REF_NO,INV_VOUCHER_TYPE,";
            strSQL = strSQL + "INV_DATE,INV_AMOUNT";
            if (vstrNarration != "")
            {
                strSQL = strSQL + ",INV_NARRATIONS";
            }
            if (vstrInvLog != "")
            {
                strSQL = strSQL + ",INV_LOG_NO";
            }
            if (vlngManuVouAuto != 0)
            {
                strSQL = strSQL + ",INV_MANU_VOUCHER_AUTO";
            }
            if (vlngCostOption != 0)
            {
                strSQL = strSQL + ",INV_COST_OPTION";
            }
            if (vstrProcess != Utility.gcEND_OF_LIST)
            {
                strSQL = strSQL + ",PROCESS_NAME";
            }

            strSQL = strSQL + ",BRANCH_ID ";
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES";
            strSQL = strSQL + "(";
            strSQL = strSQL + "'" + vstrRef + "',";
            strSQL = strSQL + "'" + vlngVType + "',";
            strSQL = strSQL + "" + Utility.cvtSQLDateString(vdteDate) + ",";
            strSQL = strSQL + "" + vdblAmount + "";
            if (vstrNarration != "")
            {
                strSQL = strSQL + ",'" + vstrNarration + "'";
            }
            if (vstrInvLog != "")
            {
                strSQL = strSQL + ",'" + vstrInvLog + "'";
            }
            if (vlngManuVouAuto != 0)
            {
                strSQL = strSQL + ",'" + vlngManuVouAuto + "'";
            }
            if (vlngCostOption != 0)
            {
                strSQL = strSQL + "," + vlngCostOption + " ";
            }
            if (vstrProcess != Utility.gcEND_OF_LIST)
            {
                strSQL = strSQL + ",'" + vstrProcess + "'";
            }
            strSQL = strSQL + ",'" + vstrBranch + "'";
            strSQL = strSQL + ")";
            return strSQL;

        }
        public static string gInteractInvInsertMaster(string vstrLedger, string vstrRefNo,
                                                        long vlngVType, string vdteDate, string vstrBranchID, string vstrNarration)
        {


            string strSQL = "";

            strSQL = "INSERT INTO INV_MASTER";
            strSQL = strSQL + "(BRANCH_ID,INV_REF_NO,INV_VOUCHER_TYPE,LEDGER_NAME,";
            strSQL = strSQL + "INV_DATE ";
            if (vstrNarration != "")
            {
                strSQL = strSQL + ",INV_NARRATIONS";
            }
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES";
            strSQL = strSQL + "(";
            strSQL = strSQL + "'" + vstrBranchID + "',";
            strSQL = strSQL + "'" + vstrRefNo + "',";
            strSQL = strSQL + " " + vlngVType + ", ";
            strSQL = strSQL + "'" + vstrLedger + "',";
            strSQL = strSQL + "" + Utility.cvtSQLDateString(vdteDate) + "";
            if (vstrNarration != "")
            {
                strSQL = strSQL + ",'" + vstrNarration + "'";
            }
            strSQL = strSQL + ")";
            return strSQL;

        }
        public static string gInsertBillWise(string vstrBranchID,
                                               string vstrRefNumber,
                                               string vdteDate,
                                               long vlngVoucherType,
                                               string vstrLedger,
                                               long vlngBillWisePos,
                                               string vstrRefType,
                                               double vdblAmount,
                                               string vstrToBy,
                                               string vstrAgstRefNumber,
                                               long vlngVoucherRowPos,
                                               string vstrDueDate = "",
                                               double dblComission = 0,
                                               double dblInterest = 0)
        {


            string strSQL;
            string strBillWiseRef;
            long lngVoucherPreNew;

            strBillWiseRef = vstrRefNumber + vlngBillWisePos.ToString();
            lngVoucherPreNew = (long)Utility.glngGetRefType(vstrRefType);
            if (vstrToBy.ToUpper() == "DR")
            {
                vdblAmount = vdblAmount * -1;
            }
            strSQL = "INSERT INTO ACC_BILL_WISE(BRANCH_ID,VOUCHER_REF_KEY,COMP_REF_NO,AGAINST_VOUCHER_NO,";
            strSQL = strSQL + "COMP_VOUCHER_TYPE,COMP_VOUCHER_DATE,";
            strSQL = strSQL + "BILL_WISE_POSITION,VOUCHER_ROW_POSITION,BILL_WISE_PREV_NEW,";
            strSQL = strSQL + "LEDGER_NAME,BILL_WISE_AMOUNT,";
            if (lngVoucherPreNew != 2)
            {
                strSQL = strSQL + "BILL_WISE_BALANCE,";
            }
            strSQL = strSQL + "BILL_WISE_TOBY";
            if (vstrDueDate != "")
            {
                strSQL = strSQL + ",BILL_WISE_DUE_DATE ";
            }
            strSQL = strSQL + ",COMISSION ";
            strSQL = strSQL + ",INTEREST ";
            strSQL = strSQL + ") VALUES (";
            strSQL = strSQL + "'" + vstrBranchID + "',";
            strSQL = strSQL + "'" + strBillWiseRef + "',";
            strSQL = strSQL + "'" + vstrRefNumber + "',";
            strSQL = strSQL + "'" + vstrAgstRefNumber + "',";
            strSQL = strSQL + "" + vlngVoucherType + " , ";
            strSQL = strSQL + " " + Utility.cvtSQLDateString(vdteDate) + ",";
            strSQL = strSQL + "'" + vlngBillWisePos + "',";
            strSQL = strSQL + " " + vlngVoucherRowPos + ",";
            strSQL = strSQL + "" + lngVoucherPreNew + " ,";
            strSQL = strSQL + "'" + vstrLedger + "', ";
            strSQL = strSQL + "" + vdblAmount + ",";
            if (lngVoucherPreNew != 2)
            {
                strSQL = strSQL + "" + vdblAmount + ",";
            }
            strSQL = strSQL + "'" + vstrToBy + "'";
            if (vstrDueDate != "")
            {
                strSQL = strSQL + "," + Utility.cvtSQLDateString(vstrDueDate) + "  ";
            }
            strSQL = strSQL + "," + dblComission + "";
            strSQL = strSQL + "," + dblInterest + "";
            strSQL = strSQL + ")";
            return strSQL;
        }
        public static string gInventoryInsertTranPurchases(string vstrRefNo,
                                                        string vstrBillKey,
                                                        long vlngLoop,
                                                        double vdblBillRate,
                                                        double vdblTotalAmount,
                                                        long vlngAgnstRefTyp,
                                                        string vstrItem,
                                                        string vstrGodown,
                                                        string vstrInOut,
                                                        double vdblQuantity,
                                                        long vlngVType,
                                                        string vdteDate,
                                                        string vstrBranchID,
                                                        string vstrBatchNo,
                                                        string vstrPer = "",
                                                        string vstrUom = "")
        {


            string strSQL;
            //string strBillWiseRef;
            //double dblSalesAmt = 0;

            //dblSalesAmt = vdblBillRate * vdblQuantity;
            //vdblQuantity = vdblQuantity + vdblBonusQty;

            strSQL = "INSERT INTO INV_TRAN ";
            strSQL = strSQL + "(BRANCH_ID,INV_TRAN_KEY,INV_TRAN_POSITION,INV_REF_NO, STOCKITEM_NAME,INV_VOUCHER_TYPE,INV_DATE, ";
            strSQL = strSQL + "INV_TRAN_QUANTITY, INV_TRAN_RATE,GODOWNS_NAME, ";
            strSQL = strSQL + "INV_TRAN_AMOUNT,INV_INOUT_FLAG,BILL_TRAN_AGST_VOUCHER_TYPE,";
            strSQL = strSQL + "INWARD_QUANTITY,INWARD_AMOUNT ";
            if (vstrPer != "")
            {
                strSQL = strSQL + ",INV_PER ";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",INV_UOM";
            }

            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",INV_LOG_NO";
            }
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES ";
            strSQL = strSQL + "(";
            strSQL = strSQL + "'" + vstrBranchID + "','" + vstrBillKey.Trim() + "',";
            strSQL = strSQL + "" + vlngLoop + ",'" + vstrRefNo.Trim() + "',";
            strSQL = strSQL + "'" + vstrItem + "'," + vlngVType + ",";
            strSQL = strSQL + "" + Utility.cvtSQLDateString(vdteDate) + ",";
            strSQL = strSQL + " " + vdblQuantity + "," + vdblBillRate + ", ";
            strSQL = strSQL + "'" + vstrGodown + "',";
            strSQL = strSQL + "" + vdblTotalAmount + ", ";
            strSQL = strSQL + "'" + vstrInOut + "',";
            strSQL = strSQL + "" + vlngAgnstRefTyp + ",";
            strSQL = strSQL + " " + vdblQuantity + ",";
            strSQL = strSQL + " " + vdblTotalAmount + " ";
            if (vstrPer != "")
            {
                strSQL = strSQL + ",'" + vstrPer + "'";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",'" + vstrUom + "'";
            }
            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",'" + vstrBatchNo + "'";
            }
            strSQL = strSQL + ")";
            return strSQL;
        }
        public static string gInventoryInsertTranSalesChallanClass(string vstrRefNo,
                                                         string vstrBillKey,
                                                         long vlngLoop,
                                                         double vdblBillRate,
                                                         double vdblTotalAmount,
                                                         long vlngAgnstRefTyp,
                                                         string vstrItem,
                                                         string vstrGodown,
                                                         string vstrInOut,
                                                         double vdblQuantity,
                                                         double vdblBonusQty,
                                                         double dblCostRate,
                                                         long vlngVType,
                                                         string vdteDate,
                                                         string vstrBranchID,
                                                         string vstrBatchNo,
                                                         long vlngIsBonus,
                                                         string vstrPer = "",
                                                         string vstrUom = "",
                                                         double vdblDiscount = 0)
        {


            string strSQL;
            //string strBillWiseRef;
            double dblSalesAmt = 0;

            dblSalesAmt = vdblBillRate * vdblQuantity;
            vdblQuantity = vdblQuantity + vdblBonusQty;

            strSQL = "INSERT INTO INV_TRAN ";
            strSQL = strSQL + "(BRANCH_ID,INV_TRAN_KEY,INV_TRAN_POSITION,INV_REF_NO, STOCKITEM_NAME,INV_VOUCHER_TYPE,INV_DATE, ";
            strSQL = strSQL + "INV_TRAN_QUANTITY, INV_TRAN_RATE,GODOWNS_NAME, ";
            strSQL = strSQL + "INV_TRAN_AMOUNT, INV_INOUT_FLAG,BILL_TRAN_AGST_VOUCHER_TYPE,";
            strSQL = strSQL + "OUTWARD_QUANTITY,OUTWARD_COST_AMOUNT,OUTWARD_SALES_AMOUNT,INV_TRAN_IS_GIFT ";
            if (vstrPer != "")
            {
                strSQL = strSQL + ",INV_PER ";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",INV_UOM";
            }

            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",INV_LOG_NO";
            }
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES (";
            strSQL = strSQL + "'" + vstrBranchID + "',";
            strSQL = strSQL + "'" + vstrBillKey.Trim() + "'," + vlngLoop + ",";
            strSQL = strSQL + "'" + vstrRefNo.Trim() + "',";
            strSQL = strSQL + "'" + vstrItem + "',";
            strSQL = strSQL + " " + vlngVType + ",";
            strSQL = strSQL + "" + Utility.cvtSQLDateString(vdteDate) + ",";
            strSQL = strSQL + " " + vdblQuantity + ",";
            strSQL = strSQL + "" + vdblBillRate + ", ";
            strSQL = strSQL + "'" + vstrGodown + "',";
            if (vlngVType != (long)Utility.VOUCHER_TYPE.vtSALES_CHALLAN)
            {
                strSQL = strSQL + -1 * dblCostRate * Math.Abs(vdblQuantity) + ", ";
            }
            else
            {
                strSQL = strSQL + "0,";
            }
            strSQL = strSQL + "'" + vstrInOut + "',";
            strSQL = strSQL + "" + vlngAgnstRefTyp + ",";
            strSQL = strSQL + " " + vdblQuantity + ",";
            if (vlngVType != (long)Utility.VOUCHER_TYPE.vtSALES_CHALLAN)
            {
                strSQL = strSQL + " " + -1 * dblCostRate * Math.Abs(vdblQuantity) + ",";//    'Cost Amount
            }
            else
            {
                strSQL = strSQL + "0,";
            }

            strSQL = strSQL + " 0 "; //sales amnt

            strSQL = strSQL + "," + vlngIsBonus + " ";
            if (vstrPer != "")
            {
                strSQL = strSQL + ",'" + vstrPer + "'";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",'" + vstrUom + "'";
            }
            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",'" + vstrBatchNo + "'";
            }
            strSQL = strSQL + ")";
            return strSQL;
        }

        public static string gInventoryInsertTranSalesChallan(string vstrRefNo,
                                                         string vstrBillKey,
                                                         long vlngLoop,
                                                         double vdblBillRate,
                                                         double vdblTotalAmount,
                                                         long vlngAgnstRefTyp,
                                                         string vstrItem,
                                                         string vstrGodown,
                                                         string vstrInOut,
                                                         double vdblQuantity,
                                                         double vdblBonusQty,
                                                         double dblCostRate,
                                                         long vlngVType,
                                                         string vdteDate,
                                                         string vstrBranchID,
                                                         string vstrBatchNo,
                                                         long vlngIsBonus,
                                                         string vstrPer = "",
                                                         string vstrUom = "",
                                                         double vdblDiscount = 0)
        {


            string strSQL;
            //string strBillWiseRef;
            double dblSalesAmt = 0;

            dblSalesAmt = vdblBillRate * vdblQuantity;
            vdblQuantity = vdblQuantity + vdblBonusQty;

            //dblCostRate = Utility.gdblGetCostPrice(vstrItem, vdteDate);
            //'dblCostRate = gdblGetCostPrice1(vstrItem, vdteDate)
            strSQL = "INSERT INTO INV_TRAN ";
            strSQL = strSQL + "(BRANCH_ID,INV_TRAN_KEY,INV_TRAN_POSITION,INV_REF_NO, STOCKITEM_NAME,INV_VOUCHER_TYPE,INV_DATE, ";
            strSQL = strSQL + "INV_TRAN_QUANTITY, INV_TRAN_RATE,GODOWNS_NAME, ";
            strSQL = strSQL + "INV_TRAN_AMOUNT, INV_INOUT_FLAG,BILL_TRAN_AGST_VOUCHER_TYPE,";
            strSQL = strSQL + "OUTWARD_QUANTITY,OUTWARD_COST_AMOUNT,OUTWARD_SALES_AMOUNT,INV_TRAN_IS_GIFT ";
            if (vstrPer != "")
            {
                strSQL = strSQL + ",INV_PER ";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",INV_UOM";
            }

            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",INV_LOG_NO";
            }
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES (";
            strSQL = strSQL + "'" + vstrBranchID + "',";
            strSQL = strSQL + "'" + vstrBillKey.Trim() + "'," + vlngLoop + ",";
            strSQL = strSQL + "'" + vstrRefNo.Trim() + "',";
            strSQL = strSQL + "'" + vstrItem + "',";
            strSQL = strSQL + " " + vlngVType + ",";
            strSQL = strSQL + "" + Utility.cvtSQLDateString(vdteDate) + ",";
            strSQL = strSQL + " " + vdblQuantity + ",";
            strSQL = strSQL + "" + vdblBillRate + ", ";
            strSQL = strSQL + "'" + vstrGodown + "',";
            if (vlngVType != (long)Utility.VOUCHER_TYPE.vtSALES_CHALLAN)
            {
                strSQL = strSQL + -1 * dblCostRate * Math.Abs(vdblQuantity) + ", ";
            }
            else
            {
                strSQL = strSQL + "0,";
            }
            strSQL = strSQL + "'" + vstrInOut + "',";
            strSQL = strSQL + "" + vlngAgnstRefTyp + ",";
            strSQL = strSQL + " " + vdblQuantity + ",";
            if (vlngVType != (long)Utility.VOUCHER_TYPE.vtSALES_CHALLAN)
            {
                strSQL = strSQL + " " + -1 * dblCostRate * Math.Abs(vdblQuantity) + ",";//    'Cost Amount
            }
            else
            {
                strSQL = strSQL + "0,";
            }

            strSQL = strSQL + " " + vdblBillRate * (vdblQuantity + Math.Abs(vdblBonusQty)) + " "; //sales amnt

            strSQL = strSQL + "," + vlngIsBonus + " ";
            if (vstrPer != "")
            {
                strSQL = strSQL + ",'" + vstrPer + "'";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",'" + vstrUom + "'";
            }
            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",'" + vstrBatchNo + "'";
            }
            strSQL = strSQL + ")";
            return strSQL;
        }

        public static string gInventoryInsertTranSales(string vstrRefNo,
                                                        string vstrBillKey,
                                                        long vlngLoop,
                                                        double vdblBillRate,
                                                        double vdblTotalAmount,
                                                        long vlngAgnstRefTyp,
                                                        string vstrItem,
                                                        string vstrGodown,
                                                        string vstrInOut,
                                                        double vdblQuantity,
                                                        long vlngVType,
                                                        string vdteDate,
                                                        string vstrBranchID,
                                                        string vstrBatchNo,
                                                        string vstrPer = "",
                                                        string vstrUom = "")
        {


            string strSQL;


            strSQL = "INSERT INTO INV_TRAN ";
            strSQL = strSQL + "(BRANCH_ID,INV_TRAN_KEY,INV_TRAN_POSITION,INV_REF_NO, STOCKITEM_NAME,INV_VOUCHER_TYPE,INV_DATE, ";
            strSQL = strSQL + "INV_TRAN_QUANTITY, INV_TRAN_RATE,GODOWNS_NAME, ";
            strSQL = strSQL + "INV_TRAN_AMOUNT,INV_INOUT_FLAG,BILL_TRAN_AGST_VOUCHER_TYPE,";
            strSQL = strSQL + "INWARD_QUANTITY,INWARD_AMOUNT ";
            if (vstrPer != "")
            {
                strSQL = strSQL + ",INV_PER ";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",INV_UOM";
            }

            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",INV_LOG_NO";
            }
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES ";
            strSQL = strSQL + "(";
            strSQL = strSQL + "'" + vstrBranchID + "','" + vstrBillKey.Trim() + "',";
            strSQL = strSQL + "" + vlngLoop + ",'" + vstrRefNo.Trim() + "',";
            strSQL = strSQL + "'" + vstrItem + "'," + vlngVType + ",";
            strSQL = strSQL + "" + Utility.cvtSQLDateString(vdteDate) + ",";
            strSQL = strSQL + " " + vdblQuantity + "," + vdblBillRate + ", ";
            strSQL = strSQL + "'" + vstrGodown + "',";
            strSQL = strSQL + "" + vdblTotalAmount + ", ";
            strSQL = strSQL + "'" + vstrInOut + "',";
            strSQL = strSQL + "" + vlngAgnstRefTyp + ",";
            strSQL = strSQL + " " + vdblQuantity + ",";
            strSQL = strSQL + " " + vdblTotalAmount + " ";
            if (vstrPer != "")
            {
                strSQL = strSQL + ",'" + vstrPer + "'";
            }
            if (vstrUom != "")
            {
                strSQL = strSQL + ",'" + vstrUom + "'";
            }
            if (vstrBatchNo != "")
            {
                strSQL = strSQL + ",'" + vstrBatchNo + "'";
            }
            strSQL = strSQL + ")";
            return strSQL;
        }

        public static string gInsertSalesVoucher(string vstrRefNumber,
                               string vdteDate,
                               long vintPosition,
                               string vstrLedger,
                               string vstrDrCr,
                               double vdblAmount,
                               long vlngVType,
                               string vstrReverseLedger,
                               string vstrBranchID,
                               long vlngCashFlow,
                               string vstrAddLessSign,
                               string vstrChequeNo = "",
                               string vdteChequedate = "",
                               string vstrDrawnOn = "",
                               string vstrAgnstRefNo = "",
                               string vstrInvLogNo = "")
        {


            string strSQL = "";
            double dblDebit = 0, dblCredit = 0;

            if (vstrDrCr.ToUpper() == "DR")
            {
                dblDebit = vdblAmount;
            }
            if (vstrDrCr.ToUpper() == "CR")
            {
                dblCredit = vdblAmount;
            }
            strSQL = "INSERT INTO ACC_VOUCHER(VOUCHER_REF_KEY,COMP_REF_NO,COMP_VOUCHER_TYPE,";
            strSQL = strSQL + "COMP_VOUCHER_DATE,COMP_VOUCHER_POSITION,";
            strSQL = strSQL + "LEDGER_NAME,VOUCHER_DEBIT_AMOUNT,VOUCHER_CREDIT_AMOUNT,";
            strSQL = strSQL + "VOUCHER_ADD_AMOUNT,VOUCHER_LESS_AMOUNT,VOUCHER_TOBY,VOUCHER_REVERSE_LEDGER,VOUCHER_CASHFLOW,VOUCHER_CURRENCY_SYMBOL,";
            if (vstrAddLessSign != "")
            {
                strSQL = strSQL + "VOUCHER_ADD_LESS_SIGN,";
            }
            if (vstrChequeNo != "")
            {
                strSQL = strSQL + "VOUCHER_CHEQUE_NUMBER,";
            }
            if (vdteChequedate != "")
            {
                strSQL = strSQL + "VOUCHER_CHEQUE_DATE,";
            }
            if (vstrDrawnOn != "")
            {
                strSQL = strSQL + "VOUCHER_CHEQUE_DRAWN_ON,";
            }
            if (vstrAgnstRefNo != "")
            {
                strSQL = strSQL + "AGNST_COMP_REF_NO, ";
            }
            if (vstrInvLogNo != "")
            {
                strSQL = strSQL + "INV_LOG_NO, ";
            }
            strSQL = strSQL + "BRANCH_ID ";
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES('" + vstrRefNumber + vintPosition.ToString().PadLeft(4, '0') + "','" + vstrRefNumber + "',";
            strSQL = strSQL + "" + vlngVType + "," + Utility.cvtSQLDateString(vdteDate) + "," + vintPosition + ",";
            strSQL = strSQL + "'" + vstrLedger + "'," + dblDebit + "," + dblCredit + "," + dblDebit + ",";
            strSQL = strSQL + "" + dblCredit + ",'" + vstrDrCr + "','" + vstrReverseLedger + "'," + vlngCashFlow + ",'" + Utility.gstrBaseCurrency + "',";
            if (vstrAddLessSign != "")
            {
                strSQL = strSQL + "'" + vstrAddLessSign + "',";
            }
            if (vstrChequeNo != "")
            {
                strSQL = strSQL + "'" + vstrChequeNo + "' ,";
            }
            if (vdteChequedate != "")
            {
                strSQL = strSQL + " " + Utility.cvtSQLDateString(vdteChequedate) + ", ";
            }
            if (vstrDrawnOn != "")
            {
                strSQL = strSQL + "'" + vstrDrawnOn + "', ";
            }
            if (vstrAgnstRefNo != "")
            {
                strSQL = strSQL + "'" + vstrAgnstRefNo + "',";
            }
            if (vstrInvLogNo != "")
            {
                strSQL = strSQL + "'" + vstrInvLogNo + "',";
            }
            strSQL = strSQL + "'" + vstrBranchID + "' ";
            strSQL = strSQL + ") ";
            return strSQL;

        }


        public static string gInsertSalesVoucherFC(string vstrRefNumber,
                                                     string vdteDate,
                                                     long vintPosition,
                                                     string vstrLedger,
                                                     string vstrDrCr,
                                                     double vdblAmount,
                                                     long vlngVType,
                                                     string vstrReverseLedger,
                                                     string vstrBranchID,
                                                     double vdblFCRate,
                                                     string vstrFCSymbol,
                                                     long vlngCashFlow,
                                                     string vstrAddLessSign = "",
                                                     string vstrChequeNo = "",
                                                     string vstrChequedate = "",
                                                     string vstrDrawnOn = "",
                                                     string vstrInvLogNo = "")
        {


            string strSQL;
            double dblDebit = 0, dblCredit = 0, dblDebitFC = 0, dblAddAmount = 0, dblLessAmount = 0, dblCreditFC = 0;

            if (vstrDrCr.ToUpper() == "DR")
            {
                dblDebit = vdblAmount;
                dblDebitFC = vdblAmount / vdblFCRate;
            }
            if (vstrDrCr.ToUpper() == "CR")
            {
                dblCredit = vdblAmount;
                dblCreditFC = vdblAmount / vdblFCRate;
            }
            if (vstrAddLessSign != "")
            {
                if (Utility.Left(vstrAddLessSign, 1) == "-")
                {
                    dblLessAmount = vdblAmount;
                }
                else
                {
                    dblAddAmount = vdblAmount;
                }
            }
            else
            {
                dblAddAmount = vdblAmount;
            }
            strSQL = "INSERT INTO ACC_VOUCHER(BRANCH_ID,VOUCHER_REF_KEY,COMP_REF_NO,COMP_VOUCHER_TYPE,";
            strSQL = strSQL + "COMP_VOUCHER_DATE,COMP_VOUCHER_POSITION,";
            strSQL = strSQL + "LEDGER_NAME,VOUCHER_DEBIT_AMOUNT,VOUCHER_CREDIT_AMOUNT,";
            strSQL = strSQL + "VOUCHER_ADD_AMOUNT,";
            strSQL = strSQL + "VOUCHER_LESS_AMOUNT,";
            strSQL = strSQL + "VOUCHER_TOBY,VOUCHER_REVERSE_LEDGER,";
            strSQL = strSQL + "VOUCHER_CURRENCY_SYMBOL,FC_CONVERSION_RATE,";
            strSQL = strSQL + "VOUCHER_FC_DEBIT_AMOUNT,VOUCHER_FC_CREDIT_AMOUNT";
            if (vstrAddLessSign != "")
            {
                strSQL = strSQL + ",VOUCHER_ADD_LESS_SIGN";
            }
            if (vstrChequeNo != "")
            {
                strSQL = strSQL + ",VOUCHER_CHEQUE_NUMBER,VOUCHER_CHEQUE_DATE,VOUCHER_CHEQUE_DRAWN_ON";
            }
            if (vstrInvLogNo != "")
            {
                strSQL = strSQL + ",INV_LOG_NO";
            }
            strSQL = strSQL + ") ";
            strSQL = strSQL + "VALUES(";
            strSQL = strSQL + "'" + vstrBranchID + "',";
            strSQL = strSQL + "'" + vstrRefNumber + vintPosition.ToString().PadLeft(4, '0') + "',";
            strSQL = strSQL + "'" + vstrRefNumber + "',";
            strSQL = strSQL + "" + vlngVType + "," + Utility.cvtSQLDateString(vdteDate) + "," + vintPosition + ",";
            strSQL = strSQL + "'" + vstrLedger + "',";
            strSQL = strSQL + dblDebit + ",";
            strSQL = strSQL + dblCredit + ",";
            strSQL = strSQL + dblAddAmount + ",";
            strSQL = strSQL + dblLessAmount + ",";
            strSQL = strSQL + "'" + vstrDrCr + "','" + vstrReverseLedger + "',";
            strSQL = strSQL + "'" + vstrFCSymbol + "'," + vdblFCRate + ",";
            strSQL = strSQL + dblDebitFC + ",";
            strSQL = strSQL + dblCreditFC + "";
            if (vstrAddLessSign != "")
            {
                strSQL = strSQL + ",'" + vstrAddLessSign + "'";
            }
            if (vstrChequeNo != "")
            {
                strSQL = strSQL + ",'" + vstrChequeNo + "'," + Utility.cvtSQLDateString(vstrChequedate) + ",'" + vstrDrawnOn + "' ";
            }
            if (vstrInvLogNo != "")
            {
                strSQL = strSQL + ",'" + vstrInvLogNo + "'";
            }
            strSQL = strSQL + ") ";
            return strSQL;

        }


        public static string gInsertADDLESS(string vstrRefNumber,
                                            string vstrLedger,
                                            string vstrDate,
                                            double vdblAddAmount,
                                            double vdblLessAmount, string vstrBranch)
        {


            string strSQL;
            strSQL = "INSERT INTO ACC_ADD_LESS(";
            strSQL = strSQL + "ADD_LESS_LEDGER,ADD_LESS_COMP_REF_NO,ADD_LESS_DATE,";
            strSQL = strSQL + "ADD_LESS_ADD_AMOUNT,ADD_LESS_LESS_AMOUNT,BRANCH_ID";
            strSQL = strSQL + ") VALUES (";
            strSQL = strSQL + "'" + vstrLedger + "',";
            strSQL = strSQL + "'" + vstrRefNumber + "',";
            strSQL = strSQL + "" + Utility.cvtSQLDateString(vstrDate) + ",";
            strSQL = strSQL + "" + vdblAddAmount + ",";
            strSQL = strSQL + "" + vdblLessAmount + ",";
            strSQL = strSQL + "'" + vstrBranch + "'";
            strSQL = strSQL + ")";
            return strSQL;

        }




        #endregion


    }
}
