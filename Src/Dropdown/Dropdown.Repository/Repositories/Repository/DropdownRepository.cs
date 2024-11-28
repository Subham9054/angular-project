using Dapper;
using Dropdown.Model;
using Dropdown.Repository.Factory;
using Dropdown.Repository.Repositories.Interfaces;
using Dropdown.Repository.Repositories.Repository.BaseRepository;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dropdown.Repository.Repositories.Repository
{
    public class DropdownRepository : db_PHED_CGRCRepositoryBase, IDropdownRepository
    {
        public DropdownRepository(Idb_PHED_CGRCConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
        public async Task<List<District>> GetDistricts()
        {
            try
            {

                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    var districts = await Connection.QueryAsync<District>("GetActiveDistricts", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return districts.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Block>> GetBlocks(int distid)
        {
            try
            {
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@distid", distid);
                    var blocks = await Connection.QueryAsync<Block>("GetActiveBlocksByDistrict", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return blocks.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<GP>> GetGp(int blockid)
        {
            try
            {
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@blockid", blockid);
                    var gps = await Connection.QueryAsync<GP>("GetActiveGPByBlock", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return gps.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Village>> Getvillage(int gpid)
        {
            try
            {
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@gpid", gpid);
                    var villages = await Connection.QueryAsync<Village>("GetActiveVillagesByGP", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return villages.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Ward>> Getward(int villageid)
        {
            try
            {
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@villageid", villageid);
                    var villages = await Connection.QueryAsync<Ward>("GetActiveWardssByVillage", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return villages.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<ComplaintStatus>> GetComplaints()
        {
            try
            {
             
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    var complaints = await Connection.QueryAsync<ComplaintStatus>("GetComplaintStatus", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return complaints.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Complaintlogtype>> GetComplaintslogtype()
        {
            try
            {
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    var complaintslog = await Connection.QueryAsync<Complaintlogtype>("GetComplaintlogtype", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return complaintslog.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Category>> GetCategories()
        {
            try
            {
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    var categories = await Connection.QueryAsync<Category>("GetActiveCategories", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return categories.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<subCategory>> GetSubCategories(int catid)
        {
            try
            {
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@catid", catid);
                    var subcategories = await Connection.QueryAsync<subCategory>("GetActivesubCategories", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return subcategories.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Designation>> getDesignation()
        {
            try
            {
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    var designations = await Connection.QueryAsync<Designation>("GetDesignation", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return designations.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Location>> getLocation()
        {
            try
            {
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    var designations = await Connection.QueryAsync<Location>("GetLoclvl", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return designations.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<ComplaintPriority>> GetComplaintPriority()
        {
            try
            {

                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    var priority = await Connection.QueryAsync<ComplaintPriority>("GetActiveComplaintPriorities", dynamicParameters, commandType: CommandType.StoredProcedure);
                    return priority.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public async Task<List<Rootdemo>> Getdemo()
        //{
        //    try
        //    {

        //        {
        //            DynamicParameters dynamicParameters = new DynamicParameters();
        //            var complaints = await Connection.QueryAsync<Rootdemo>("GetActiveDemo", dynamicParameters, commandType: CommandType.StoredProcedure);
        //            return complaints.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        //public async Task<List<Districtdemo>> GetActiveDistricts()
        //{
        //    var result = await Connection.QueryAsync<Districtdemo, Blockdemo, Gpdemo, Villagedemo, Warddemo, Districtdemo>(
        //        "GetActiveDemo", // The stored procedure name
        //        (district, block, gp, village, ward) =>
        //        {
        //            // Directly assign block, gp, village, and ward to the appropriate objects in the hierarchy
        //            district.Blocks ??= new List<Blockdemo>(); // If Blocks is null, initialize it
        //            var blockEntry = district.Blocks.FirstOrDefault(b => b.IntBlockId == block.IntBlockId);
        //            if (blockEntry == null)
        //            {
        //                blockEntry = block;
        //                blockEntry.Gps = new List<Gpdemo>(); // Initialize Gps list for the block
        //                district.Blocks.Add(blockEntry);
        //            }

        //            blockEntry.Gps ??= new List<Gpdemo>(); // If Gps is null, initialize it

        //            // Check if 'gp' is null before proceeding
        //            if (gp != null)
        //            {
        //                var gpEntry = blockEntry.Gps.FirstOrDefault(g => g.IntGpId == gp.IntGpId);
        //                if (gpEntry == null)
        //                {
        //                    gpEntry = new Gpdemo
        //                    {
        //                        IntGpId = gp.IntGpId,
        //                        VchGpName = gp.VchGpName,
        //                        Villages = new List<Villagedemo>() // Initialize Villages list for the GP
        //                    };
        //                    blockEntry.Gps.Add(gpEntry); // Add the new GP to the block's Gps list
        //                }

        //                gpEntry.Villages ??= new List<Villagedemo>(); // If Villages is null, initialize it

        //                // Check if 'village' is null before proceeding
        //                if (village != null)
        //                {
        //                    var villageEntry = gpEntry.Villages.FirstOrDefault(v => v.IntVillageId == village.IntVillageId);
        //                    if (villageEntry == null)
        //                    {
        //                        villageEntry = new Villagedemo
        //                        {
        //                            IntVillageId = village.IntVillageId,
        //                            VchVillageName = village.VchVillageName,
        //                            WardList = new List<Warddemo>() // Initialize WardList for the village
        //                        };
        //                        gpEntry.Villages.Add(villageEntry); // Add the new Village to the GP's Villages list
        //                    }

        //                    villageEntry.WardList ??= new List<Warddemo>(); // If WardList is null, initialize it
        //                    villageEntry.WardList.Add(ward); // Add the ward to the WardList
        //                }
        //            }

        //            return district; // Return the district which will contain all the nested objects
        //        },
        //         splitOn: "IntBlockId, IntGpId, IntVillageId, IntWardId", // Split the result set based on these columns
        //        commandType: CommandType.StoredProcedure // Use stored procedure
        //    );

        //    return result.ToList(); // Return the list of districts with all the related nested objects
        //}

        public async Task<string> GetActiveDemoAsync()
        {
            try
            {
                var dynamicParameters = new DynamicParameters();

                var result = await Connection.QueryAsync<dynamic>("GetActiveDemo", dynamicParameters, commandType: CommandType.StoredProcedure);

                // Remove duplicates by grouping
                var districts = result
                    .Where(row => row.IntDistrictId != null && row.VchDistrictName != null)
                    .GroupBy(row => new { row.IntDistrictId, row.VchDistrictName })
                    .Select(group => new
                    {
                        intDistrictId = group.Key.IntDistrictId,
                        vchDistrictName = group.Key.VchDistrictName
                    });

                var blocks = result
                    .Where(row => row.IntBlockId != null && row.VchBlockName != null)
                    .GroupBy(row => new { row.IntBlockId, row.VchBlockName, row.IntDistrictId })
                    .Select(group => new
                    {
                        intBlockId = group.Key.IntBlockId,
                        vchBlockName = group.Key.VchBlockName,
                        intDistrictId = group.Key.IntDistrictId
                    });

                var gps = result
                    .Where(row => row.IntGpId != null && row.VchGpName != null)
                    .GroupBy(row => new { row.IntGpId, row.VchGpName, row.IntBlockId })
                    .Select(group => new
                    {
                        intGpId = group.Key.IntGpId,
                        vchGpName = group.Key.VchGpName,
                        intBlockId = group.Key.IntBlockId
                    });

                var villages = result
                    .Where(row => row.IntVillageId != null && row.VchVillageName != null)
                    .GroupBy(row => new { row.IntVillageId, row.VchVillageName, row.IntGpId })
                    .Select(group => new
                    {
                        intVillageId = group.Key.IntVillageId,
                        vchVillageName = group.Key.VchVillageName,
                        intGpId = group.Key.IntGpId
                    });

                var wardList = result
                    .Where(row => row.IntWardId != null && row.VchWardName != null)
                    .GroupBy(row => new { row.IntWardId, row.VchWardName, row.IntVillageId })
                    .Select(group => new
                    {
                        intWardId = group.Key.IntWardId,
                        vchWardName = group.Key.VchWardName,
                        intVillageId = group.Key.IntVillageId
                    });


                // Serialize each group into JSON
                var json = JsonConvert.SerializeObject(new
                {
                    Districtdemo = districts,
                    Blockdemo = blocks,
                    Gpdemo = gps,
                    Villagedemo = villages,
                    WardListdemo = wardList
                }, (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented);

                return json;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching data.", ex);
            }
        }

        public async Task<string> GetActivecatsubDemoAsync()
        {
            try
            {
                var dynamicParameters = new DynamicParameters();

                var result = await Connection.QueryAsync<dynamic>("GetCategorySubcategoryData", dynamicParameters, commandType: CommandType.StoredProcedure);

                var category = result
                    .Where(row => row.INT_CATEGORY_ID != null && row.VCH_CATEGORY != null)
                    .GroupBy(row => new { row.INT_CATEGORY_ID, row.VCH_CATEGORY, row.NVCH_CATEGORY })
                    .Select(group => new
                    {
                        INT_CATEGORY_ID = group.Key.INT_CATEGORY_ID,
                        VCH_CATEGORY = group.Key.VCH_CATEGORY,
                        NVCH_CATEGORY = group.Key.NVCH_CATEGORY,
                    });

                var subcat = result
                    .Where(row => row.INT_SUB_CATEGORY_ID != null && row.VCH_SUB_CATEGORY != null)
                    .GroupBy(row => new { row.INT_SUB_CATEGORY_ID, row.VCH_SUB_CATEGORY, row.NVCH_SUB_CATEGORY, row.INT_CATEGORY_ID })
                    .Select(group => new
                    {
                        INT_SUB_CATEGORY_ID = group.Key.INT_SUB_CATEGORY_ID,
                        VCH_SUB_CATEGORY = group.Key.VCH_SUB_CATEGORY,
                        NVCH_SUB_CATEGORY = group.Key.NVCH_SUB_CATEGORY,
                        INT_CATEGORY_ID = group.Key.INT_CATEGORY_ID, 
                    });

                var json = JsonConvert.SerializeObject(new
                {
                    Category = category,
                    Subcategory = subcat
                }, (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented); // Use Newtonsoft.Json.Formatting.Indented directly

                return json;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching data.", ex);
            }
        }



    }
}
