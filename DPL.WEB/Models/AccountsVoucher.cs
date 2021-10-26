using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPL.WEB.Models
{



    public class AccountsVoucher
    {

        public string strVoucherNoMerz { get; set; }
        public int fcheck = 0;
        public string strLocation { get; set; }
        public string strDoctorName { get; set; }
        public long lngSlNo { get; set; }
        public string strVoucherNo { get; set; }
        public string strLedgerName { get; set; }
        public string strBankdate { get; set; }
        public string strBankPer { get; set; }
        public string strLedgerNameNew { get; set; }
        public string strMerzeName { get; set; }
        public string strToby { get; set; }
        public string strBranchID { get; set; }
        public string strBranchName { get; set; }
        public string strReverseLegder { get; set; }
        public string strNarration { get; set; }
        public string strStatus { get; set; }
        public string strTranDate { get; set; }
        public string strChequeNo { get; set; }
        public string strChequeDate { get; set; }
        public string strDrawnOn { get; set; }
        public string strAddress { get; set; }
        public string strSingleNarration { get; set; }
        public string strBatch { get; set; }
        public string strAttention { get; set; }
        public string strDesignation { get; set; }
        public string strMonthID { get; set; }
        public string strtransport { get; set; }
        public string strtrrno { get; set; }
        public double dblCrtQnty { get; set; }
        public double dblBoxQnty { get; set; }
        public double dblBankChargeAmnt { get; set; }
        public string strSalesRepresentive { get; set; }
        public double dblDebitAmount { get; set; }
        public double dblCreditAmount { get; set; }
        public double  dblAmount { get; set; }
        public double dblNetAmount { get; set; }
        public double dblProcessAmount { get; set; }
        public double dblRoundOff{ get; set; }
        public double dblAddAmount { get; set; }
        public double dblLessAmount { get; set; }
        public string strDueDate { get; set; }
        public string strComVoucherFc { get; set; }
        public string strDelivery { get; set; }
        public string strtermofPayment { get; set; }
        public string strSupport { get; set; }
        public string strValidityDate { get; set; }
        public string strOthers { get; set; }
        public string strOrderNo { get; set; }
        public string strOrderDate { get; set; }
        public string strPreparedby { get; set; }
        public string strPreparedDate { get; set; }
        public string strAgnstRefNo { get; set; }
        public string strApprovedby { get; set; }
        public string strApproveddate { get; set; }
        public string strLedgerCode { get; set; }
        public string strTeritorryCode { get; set; }
        public string strTeritorryName { get; set; }
        public string strHomeoHall { get; set; }
        public string strTC { get; set; }
        public string strPreserveSQL { get; set; }
        public int intAppStatus { get; set; }
        public int intvoucherPos { get; set; }
        public int intTrasnsfer { get; set; }
        public int intChangeType { get; set; }
        public int intAppSIRet { get; set; }
        public int intStatus { get; set; }
        public int intHalt { get; set; }
        public string strDivisionName { get; set; }
        public string strArea { get; set; }


        public class OrderDetails
        {

            public int department_code { get; set; }
            public int issue_d_no { get; set; }
            public int issue_no { get; set; }
            public string item_code { get; set; }
            public string item_name { get; set; }
            public int item_qty { get; set; }
            public string create_by { get; set; }
            public DateTime create_date { get; set; }
        }

    }
}
