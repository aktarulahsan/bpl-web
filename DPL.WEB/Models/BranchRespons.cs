using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPL.WEB.Models
{
    public class BranchRespons
    {

        public void InitialList()
        {
            data = new List<Branch>();
            Ledgerdata = new List<Ledger>();
        }


        public List<Branch> data { get; set; }

        public List<Ledger> Ledgerdata { get; set; }
    }
    public class Branch
    {


        public int SERIAL { get; set; }
        public long lngSlNo { get; set; }
        public string strLocation { get; set; }
        public string strUnder { get; set; }
        public string strBranch { get; set; }
        public string strAddres1 { get; set; }
        public string strAddres2 { get; set; }
        public string strCity { get; set; }
        public string strPhone { get; set; }
        public string strFax { get; set; }
        public string strParentGroup { get; set; }

        public int lngDefault { get; set; }
        public int intSection { get; set; }



    }



    public class Ledger
    {


        public int SERIAL { get; set; }
        public long lngSlNo { get; set; }
        public string strArea { get; set; }
        public string strTC { get; set; }
        public string strLedgerName { get; set; }
        public string strCustomerName { get; set; }

        public string strDoctorName { get; set; }
        public string strCustomerCode{ get; set; }




    }
}