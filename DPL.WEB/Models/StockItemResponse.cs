using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DPL.WEB.Models
{
    public class StockItemResponse
    {
        public void InitialList()
        {
            data = new List<StockItem>();
        }

        public List<StockItem> data { get; set; }

       
    }

    public class GLocation{

        public double intQty { get; set; }
        public double dblBranchRate { get; set; }
        public double dblBranchAmnout { get; set; }
        public string strBatch { get; set; }
        public string strBranchName { get; set; }
       
    
    
    }


    public class StockItem
    {

        public string strCommitionGroupN { get; set; }
        public string strCommitionGroupItemName { get; set; }
        public double dblPercent { get; set; }
        public double dblCommitionVal { get; set; }
        public double dblItemNetVal { get; set; }

        public int intBrand_ID { get; set; }
        public string strSubGroup_Name { get; set; }
        public string strBrand_Name { get; set; }
        public string imageSrc { get; set; }
        public byte[] itemImage { get; set; }
        public HttpPostedFileWrapper ImageFile { get; set; }
        public string strInOUT { get; set; }
        public int intItemQty { get; set; }
        public double dblItemRate { get; set; }
        public double dblCommission { get; set; }
        public int intBonusQty { get; set; }
        public double dblTotalAmount { get; set; }

        public string strItemName { get; set; }
        public string strCustomer { get; set; }
        public string strItemNameBangla { get; set; }
        public string strItemcode { get; set; }
        public string strIOldtemcode { get; set; }
        public string strPackSize { get; set; }
        public string strItemDescription { get; set; }
        public string strItemGroup { get; set; }
        public string strItemCategory { get; set; }
        public string strUnit { get; set; }
        public string strAltUnit { get; set; }
        public string strWhere { get; set; }
        public string strToUnit { get; set; }
        public string strBatch { get; set; }
        public double dblMinimumStock { get; set; }
        public double dblReorderQty { get; set; }
        public string strManufacturer { get; set; }
        public string strStatus { get; set; }
        public int intAltUnit { get; set; }
        public int intSPItem { get; set; }
        public int intMaintainBatch { get; set; }
        public double dblOpnQty { get; set; }
        public double dblOpnRate { get; set; }
        public double dblOpnValue { get; set; }
        public string strBranchName { get; set; }
        public double dblBranchQty { get; set; }
        public double dblBranchRate { get; set; }
        public double dblBranchAmnout { get; set; }
        public long lngSlNo { get; set; }
        public long lngVtype { get; set; }
        public string strLedgerName { get; set; }
        public string strConversion { get; set; }
        public string strDenominator { get; set; }
        public string strParentGroup { get; set; }
        public string strLocation { get; set; }
        public double dblClsBalance { get; set; }
        public string strMatType { get; set; }
        public string strPowerClass { get; set; }
        public string strRefNo { get; set; }
        public string strDate { get; set; }
        public string strNarration { get; set; }
        public string strToLocation { get; set; }
        public string strFromLocation { get; set; }
        public string strBillKey { get; set; }
        public string strProcess { get; set; }
        public string strAgnstRefNo { get; set; }
        public string strPreserveSQL { get; set; }
        public HttpPostedFileBase imgfile { get; set; }
        public string imgfiles { get; set; }
        public List<GLocation> gLocationList { get; set; }
        //public File imgfi { get; set; }
        public double dblbonusQty { get; set; }

        public void InitialList()
        {
            gLocationList = new List<GLocation>();
        }

    }

    public class StockNode
    {
        public StockNode()
        {
            Childs = new List<StockNode>();         
        }
        //Cat Id  

        //public int ID { get; set; }

        //Cat Name  
        public string Name { get; set; }
        public string Parent { get; set; }

        //Cat Description  
        //public string Description { get; set; }

        //represnts Parent ID and it's nullable  
        //public int? Pid { get; set; }
        //[ForeignKey("Pid")]
        //public virtual StockNode Parent { get; set; }
        public virtual ICollection<StockNode> Childs { get; set; }
    } 
}