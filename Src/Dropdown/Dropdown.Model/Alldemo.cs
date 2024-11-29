using System;
using System.Collections.Generic;

namespace Dropdown.Model
{
    

    public class Districtdemo
    {
        public int? intDistrictId { get; set; }  // Nullable int
        public string vchDistrictName { get; set; }
    }

    public class Blockdemo
    {
        public int? intBlockId { get; set; }  // Nullable int
        public string vchBlockName { get; set; }
        public int? intDistrictId { get; set; }
    }

    public class Gpdemo
    {
        public int? intGpId { get; set; }  // Nullable int
        public string vchGpName { get; set; }
        public int? intBlockId { get; set; }  // Nullable int
    }

    public class Villagedemo
    {
        public int? intVillageId { get; set; }  // Nullable int
        public string vchVillageName { get; set; }
        public int? intGpId { get; set; }  // Nullable int
    }

    public class WardListdemo
    {
        public int? intWardId { get; set; }  // Nullable int
        public string vchWardName { get; set; }
        public int? intVillageId { get; set; }  // Nullable int
    }


    public class ComplaintCategorydemo
    {
        public int INT_CATEGORY_ID { get; set; }
        public string VCH_CATEGORY { get; set; }
        public string NVCH_CATEGORY { get; set; }
    }

    public class ComplaintSubCategorydemo
    {
        public int INT_SUB_CATEGORY_ID { get; set; }
        public string VCH_SUB_CATEGORY { get; set; }
        public string NVCH_SUB_CATEGORY { get; set; }
        public int INT_CATEGORY_ID { get; set; }

    }
}
