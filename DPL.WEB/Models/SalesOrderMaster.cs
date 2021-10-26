using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPL.WEB.Models
{
    public class SalesOrderMaster
    {

        public void InitialList()
        {
            data = new List<OrderDetails>();
        }

        public List<OrderDetails> data { get; set; }


    }
 


    public class OrderMaster
    {

        public string strLocation { get; set; }
        public string strGodownName { get; set; }
         public string strOtherTerms { get; set; }
        public string strPrepareBy { get; set; }
        public string strDate { get; set; }
        public int intAppStatus { get; set; }
        public string strApprovedBy { get; set; }
        public string strApprovedDate { get; set; }
         public double dblNetQty { get; set; }
         public string strDelivery { get; set; }
         public string strPayment { get; set; }
         public string strSupport { get; set; }

         public string dteValidaty { get; set; }
        public string strRefNo { get; set; }
        public long lngIsMultiCurrency { get; set; }
        public string strNaration { get; set; }
        public string strBranchId { get; set; }
        public string strBranchName { get; set; }
        public string strLedgerName { get; set; }
        public string strCustomer { get; set; }
        public string strOrderNo { get; set; }
        public string strOrderDate { get; set; }

        public double dblTotalAmount { get; set; }
        public double dblCommitionVal { get; set; }
        public double dblNetTotal { get; set; }
        public string issue_date { get; set; }
        public string created_by { get; set; }
        public DateTime create_date { get; set; }
        public string updated_by { get; set; }
        public DateTime update_date { get; set; }
        public List<OrderDetails> detailsList { get; set; }

  
    }
    public class OrderDetails
    {

        public bool fcheck = false;
        public string strLocation { get; set; }
        public string strDescription { get; set; }
        public string strBillKey { get; set; }
        public string strItemGroup { get; set; }
        public string strItemName { get; set; }
        public string strPowerClass { get; set; }
        public string strSubGroup_Name { get; set; }
        public string strPackSize { get; set; }
        public int intItemQty { get; set; }
        public double dblItemRate { get; set; }
        public double dblBranchRate { get; set; }
        public double dblbonusQty { get; set; }
        public string strUnit { get; set; }
        public int intBonusQty { get; set; }
        public double dblTotalAmount { get; set; }
        public string strCustomer { get; set; }
        public string strDate { get; set; }
        public string strCommitionGroupItemName { get; set; }
        public double dblCommitionVal { get; set; }

        public double dblItemNetVal { get; set; }
        public double dblPercent { get; set; }
        public string strCommitionGroupN { get; set; }
        public DateTime create_date { get; set; }

        public string strVoucherNoMerz { get; set; }


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
        public double dblAmount { get; set; }
        public double dblNetAmount { get; set; }
        public double dblProcessAmount { get; set; }
        public double dblRoundOff { get; set; }
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
        

    }

    public class OrderDetails2
    {
        public string strLocation { get; set; }
        public string strDescription { get; set; }
        public string strBillKey { get; set; }
        public string strItemGroup { get; set; }
        public string strItemName { get; set; }
        public string strPowerClass { get; set; }
        public string strSubGroup_Name { get; set; }
        public string strPackSize { get; set; }
        public int intItemQty { get; set; }
        public double dblItemRate { get; set; }
        public double dblBranchRate { get; set; }
        public double dblbonusQty { get; set; }
        public string strUnit { get; set; }
        public int intBonusQty { get; set; }
        public double dblTotalAmount { get; set; }
        public string strCustomer { get; set; }
        public string strDate { get; set; }
        public string strCommitionGroupItemName { get; set; }
        public double dblCommitionVal { get; set; }

        public double dblItemNetVal { get; set; }
        public double dblPercent { get; set; }
        public string strCommitionGroupN { get; set; }
        public DateTime create_date { get; set; }


    }
}




