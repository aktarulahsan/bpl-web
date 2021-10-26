using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPL.WEB.Models
{
    public class AccBillwise
    {
        public string LedgerName { get; set; }
        public string Branch { get; set; }
        public string strDate { get; set; }
        public string strDueDate { get; set; }
        public string strAgnstVoucherRefNo { get; set; }
        public string strAgnstVoucherRefNo1 { get; set; }
        public double dblAmount { get; set; }
        public double dblBillNetAmount { get; set; }
        public double dblQnty { get; set; }
        public double dblRate { get; set; }
        public double dblComm { get; set; }
        public double dblInt { get; set; }
        public double dblBonusQnty { get; set; }
        public double mdblCurrRate { get; set; }
        public double dblCreditAmount { get; set; }
        public double dblDebitAmount { get; set; }
        public string mstrFCsymbol { get; set; }
        public string strPer { get; set; }
        public string strRefNo { get; set; }
        public string strDrCr { get; set; }
        public string strStockItemName { get; set; }
        public string strGodownsName { get; set; }
        public string strBatchNo { get; set; }
        public string strBillKey { get; set; }
        public string strDescription { get; set; }
        public string strBillAddless { get; set; }
        public string strBillPrevNew { get; set; }
        public string strLedgerName { get; set; }
        public string strAddlessSign { get; set; }
        public string strRefType { get; set; }
        public string strStockGroupName { get; set; }
        public double dblShortQty { get; set; }
        public string strApprovedby { get; set; }
        public string strApprovedDate { get; set; }
        public string strSubgroup { get; set; }
    }
}