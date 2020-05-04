using Luminous.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Luminous.Models
{
    public class UploadViewModel
    {
        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();

        public List<Item_New> Item_NewList = new List<Item_New>();
        //public List<Item_New_Log> Item_NewList_Log = new List<Item_New_Log>();
        public List<Item> ItemList { get; set; }
        public List<State> StateList = new List<State>();

        public List<Item_New> getDataWithStateWithOutItemsLst(string[] arrStateResult, DateTime sDate, DateTime eDate)
        {
            List<Item_New> Item_NewList = new List<Item_New>();

            var query = from data in db.WRS_Multiplier_Master
                        where arrStateResult.Contains(data.State_Name) && data.Created_Date.Value.Day >= sDate.Day &&
                        data.Created_Date.Value.Month >= sDate.Month && data.Created_Date.Value.Year >= sDate.Year && data.Created_Date.Value.Day <= eDate.Day &&
                        data.Created_Date.Value.Month <= eDate.Month && data.Created_Date.Value.Year <= eDate.Year
                        select data;

            if (query.Count() == 0)
            {
                return Item_NewList;
            }
            else
            {
                foreach (var item in query.ToList())
                {
                    Item_NewList.Add(new Item_New
                    {
                        Material_Code = item.Material_Code,
                        State_Name = item.State_Name,
                        Valid_Start_Date = item.Valid_Start_Date.ToString(),
                        Valid_End_Date = item.Valid_End_Date.ToString(),
                        Sale_Start_Date = item.Sale_Start_Date.ToString(),
                        Sale_End_Date = item.Sale_End_Date.ToString(),
                        SerialNo_Entry_Start_Date = item.SerialNo_Entry_Start_Date.ToString(),
                        SerialNo_Entry_End_Date = item.SerialNo_Entry_End_Date.ToString(),
                        itemdesc = item.itemdesc.ToString(),
                        Item_Type = item.Item_Type.ToString(),
                        Entries_Count = item.Entries_Count.ToString(),
                        Multiplier_Count = item.Multiplier_Count.ToString(),
                        Multiplier_Type = item.Multiplier_Type,
                    });
                }
            }
            return Item_NewList;
        }

        public List<Item_New> getDataWithOutStateWithOutItemsLst(DateTime sDate, DateTime eDate)
        {
            List<Item_New> Item_NewList = new List<Item_New>();

            var query = from data in db.WRS_Multiplier_Master
                        where data.Created_Date.Value.Day >= sDate.Day &&
                        data.Created_Date.Value.Month >= sDate.Month && data.Created_Date.Value.Year >= sDate.Year && data.Created_Date.Value.Day <= eDate.Day &&
                        data.Created_Date.Value.Month <= eDate.Month && data.Created_Date.Value.Year <= eDate.Year
                        select data;

            if (query.Count() == 0)
            {
                return Item_NewList;
            }
            else
            {
                foreach (var item in query.ToList())
                {
                    Item_NewList.Add(new Item_New
                    {
                        Material_Code = item.Material_Code,
                        State_Name = item.State_Name,
                        Valid_Start_Date = item.Valid_Start_Date.ToString(),
                        Valid_End_Date = item.Valid_End_Date.ToString(),
                        Sale_Start_Date = item.Sale_Start_Date.ToString(),
                        Sale_End_Date = item.Sale_End_Date.ToString(),
                        SerialNo_Entry_Start_Date = item.SerialNo_Entry_Start_Date.ToString(),
                        SerialNo_Entry_End_Date = item.SerialNo_Entry_End_Date.ToString(),
                        itemdesc = item.itemdesc.ToString(),
                        Item_Type = item.Item_Type.ToString(),
                        Entries_Count = item.Entries_Count.ToString(),
                        Multiplier_Count = item.Multiplier_Count.ToString(),
                        Multiplier_Type = item.Multiplier_Type,
                    });
                }
            }
            return Item_NewList;
        }

        public List<Item_New> getDataWithStateLst(string hdnMaterialcode, string[] arrItemMetCode, string[] arrStateResult, string[] arrItemResultNew, DateTime sDate, DateTime eDate)
        {
            List<Item_New> Item_NewList = new List<Item_New>();

            if (hdnMaterialcode != "")
            {
                //var query = from data in db.WRS_Multiplier_Master where arrItemMetCode.Contains(data.Material_Code) && arrStateResult.Contains(data.State_Name) && data.Created_Date >= sDate && data.Created_Date <= eDate select data;

                var query = from data in db.WRS_Multiplier_Master
                            where arrItemMetCode.Contains(data.Material_Code) && arrStateResult.Contains(data.State_Name) && data.Created_Date.Value.Day >= sDate.Day &&
                            data.Created_Date.Value.Month >= sDate.Month && data.Created_Date.Value.Year >= sDate.Year && data.Created_Date.Value.Day <= eDate.Day &&
                            data.Created_Date.Value.Month <= eDate.Month && data.Created_Date.Value.Year <= eDate.Year
                            select data;

                if (query.Count() == 0)
                {
                    // TempData["NoData"] = "Record Not Found.";
                    return Item_NewList;
                }
                else
                {
                    foreach (var item in query.ToList())
                    {

                        //  downloadField(Item_New item_New);
                        Item_NewList.Add(new Item_New
                        {
                            Material_Code = item.Material_Code,
                            State_Name = item.State_Name,
                            Valid_Start_Date = item.Valid_Start_Date.ToString(),
                            Valid_End_Date = item.Valid_End_Date.ToString(),
                            Sale_Start_Date = item.Sale_Start_Date.ToString(),
                            Sale_End_Date = item.Sale_End_Date.ToString(),
                            SerialNo_Entry_Start_Date = item.SerialNo_Entry_Start_Date.ToString(),
                            SerialNo_Entry_End_Date = item.SerialNo_Entry_End_Date.ToString(),
                            itemdesc = item.itemdesc.ToString(),
                            Item_Type = item.Item_Type.ToString(),
                            Entries_Count = item.Entries_Count.ToString(),
                            Multiplier_Count = item.Multiplier_Count.ToString(),
                            Multiplier_Type = item.Multiplier_Type,
                        });
                    }
                }
            }
            else
            {
                //arrItemMetCode = hdnMaterialcode.Split(',');
                var query2 = from data in db.WRS_ItemMaster where arrItemResultNew.Contains(data.ItemType) select data.ItemCode;
                arrItemMetCode = query2.ToArray();
                //var query = from data in db.WRS_Multiplier_Master where arrItemMetCode.Contains(data.Material_Code) && arrStateResult.Contains(data.State_Name) && data.Created_Date >= sDate && data.Created_Date <= eDate select data;

                var query = from data in db.WRS_Multiplier_Master
                            where arrItemMetCode.Contains(data.Material_Code) && arrStateResult.Contains(data.State_Name) && data.Created_Date.Value.Day >= sDate.Day &&
                            data.Created_Date.Value.Month >= sDate.Month && data.Created_Date.Value.Year >= sDate.Year && data.Created_Date.Value.Day <= eDate.Day &&
                            data.Created_Date.Value.Month <= eDate.Month && data.Created_Date.Value.Year <= eDate.Year
                            select data;

                if (query.Count() == 0)
                {
                    return Item_NewList;
                    //TempData["NoData"] = "Record Not Found.";
                    //return View("Index");
                }
                else
                {
                    foreach (var item in query)
                    {
                        Item_NewList.Add(new Item_New
                        {
                            Material_Code = item.Material_Code,
                            State_Name = item.State_Name,
                            Valid_Start_Date = item.Valid_Start_Date.ToString(),
                            Valid_End_Date = item.Valid_End_Date.ToString(),
                            Sale_Start_Date = item.Sale_Start_Date.ToString(),
                            Sale_End_Date = item.Sale_End_Date.ToString(),
                            SerialNo_Entry_Start_Date = item.SerialNo_Entry_Start_Date.ToString(),
                            SerialNo_Entry_End_Date = item.SerialNo_Entry_End_Date.ToString(),
                            itemdesc = item.itemdesc.ToString(),
                            Item_Type = item.Item_Type.ToString(),
                            Entries_Count = item.Entries_Count.ToString(),
                            Multiplier_Count = item.Multiplier_Count.ToString(),
                            Multiplier_Type = item.Multiplier_Type
                        });
                    }
                }
            }

            return Item_NewList;
        }


        public List<Item_New> getDataWithOutStateLst(string hdnMaterialcode, string[] arrItemMetCode, string[] arrItemResultNew, DateTime sDate, DateTime eDate)
        {
            List<Item_New> Item_NewList = new List<Item_New>();

            if (hdnMaterialcode != "")
            {
                var query = from data in db.WRS_Multiplier_Master
                            where arrItemMetCode.Contains(data.Material_Code) && data.Created_Date.Value.Day >= sDate.Day &&
                            data.Created_Date.Value.Month >= sDate.Month && data.Created_Date.Value.Year >= sDate.Year && data.Created_Date.Value.Day <= eDate.Day &&
                            data.Created_Date.Value.Month <= eDate.Month && data.Created_Date.Value.Year <= eDate.Year
                            select data;

                if (query.Count() == 0)
                {
                    return Item_NewList;
                }
                else
                {
                    foreach (var item in query.ToList())
                    {

                        //  downloadField(Item_New item_New);
                        Item_NewList.Add(new Item_New
                        {
                            Material_Code = item.Material_Code,
                            State_Name = item.State_Name,
                            Valid_Start_Date = item.Valid_Start_Date.ToString(),
                            Valid_End_Date = item.Valid_End_Date.ToString(),
                            Sale_Start_Date = item.Sale_Start_Date.ToString(),
                            Sale_End_Date = item.Sale_End_Date.ToString(),
                            SerialNo_Entry_Start_Date = item.SerialNo_Entry_Start_Date.ToString(),
                            SerialNo_Entry_End_Date = item.SerialNo_Entry_End_Date.ToString(),
                            itemdesc = item.itemdesc.ToString(),
                            Item_Type = item.Item_Type.ToString(),
                            Entries_Count = item.Entries_Count.ToString(),
                            Multiplier_Count = item.Multiplier_Count.ToString(),
                            Multiplier_Type = item.Multiplier_Type,
                        });
                    }
                }
            }
            else
            {
                //arrItemMetCode = hdnMaterialcode.Split(',');
                var query2 = from data in db.WRS_ItemMaster where arrItemResultNew.Contains(data.ItemType) select data.ItemCode;
                arrItemMetCode = query2.ToArray();
                //var query = from data in db.WRS_Multiplier_Master where arrItemMetCode.Contains(data.Material_Code) && arrStateResult.Contains(data.State_Name) && data.Created_Date >= sDate && data.Created_Date <= eDate select data;

                var query = from data in db.WRS_Multiplier_Master
                            where arrItemMetCode.Contains(data.Material_Code) && data.Created_Date.Value.Day >= sDate.Day &&
                            data.Created_Date.Value.Month >= sDate.Month && data.Created_Date.Value.Year >= sDate.Year && data.Created_Date.Value.Day <= eDate.Day &&
                            data.Created_Date.Value.Month <= eDate.Month && data.Created_Date.Value.Year <= eDate.Year
                            select data;

                if (query.Count() == 0)
                {
                    return Item_NewList;
                    //TempData["NoData"] = "Record Not Found.";
                    //return View("Index");
                }
                else
                {
                    foreach (var item in query)
                    {
                        Item_NewList.Add(new Item_New
                        {
                            Material_Code = item.Material_Code,
                            State_Name = item.State_Name,
                            Valid_Start_Date = item.Valid_Start_Date.ToString(),
                            Valid_End_Date = item.Valid_End_Date.ToString(),
                            Sale_Start_Date = item.Sale_Start_Date.ToString(),
                            Sale_End_Date = item.Sale_End_Date.ToString(),
                            SerialNo_Entry_Start_Date = item.SerialNo_Entry_Start_Date.ToString(),
                            SerialNo_Entry_End_Date = item.SerialNo_Entry_End_Date.ToString(),
                            itemdesc = item.itemdesc.ToString(),
                            Item_Type = item.Item_Type.ToString(),
                            Entries_Count = item.Entries_Count.ToString(),
                            Multiplier_Count = item.Multiplier_Count.ToString(),
                            Multiplier_Type = item.Multiplier_Type
                        });
                    }
                }
            }

            return Item_NewList;
        }

    }


    public class State
    {
        public string State_Name { get; set; }
    }

    public class Item
    {
        public string itemcode { get; set; }
        public string itemdesc { get; set; }
        public string Item_Type { get; set; }
    }

    public class Item_New
    {
        //public string Sr_No { get; set; }
        public string Material_Code { get; set; }
        public string itemdesc { get; set; }
        public string Item_Type { get; set; }
        public string State_Name { get; set; }


        public string Valid_Start_Date{ get; set; }
        public string Valid_End_Date { get; set; }
        public string Sale_Start_Date { get; set; }
        public string Sale_End_Date { get; set; }

        public string SerialNo_Entry_Start_Date { get; set; }
        public string SerialNo_Entry_End_Date { get; set; }
        public string Entries_Count { get; set; }
        public string Multiplier_Count { get; set; }
        public string Multiplier_Type { get; set; }


    }


    public class Item_New_Log
    {
        public string Material_Code { get; set; }
        public string itemdesc { get; set; }
        public string Item_Type { get; set; }
        public string State_Name { get; set; }


        public string Valid_Start_Date { get; set; }
        public string Valid_End_Date { get; set; }
        public string Sale_Start_Date { get; set; }
        public string Sale_End_Date { get; set; }

        public string SerialNo_Entry_Start_Date { get; set; }
        public string SerialNo_Entry_End_Date { get; set; }
        public string Entries_Count { get; set; }
        public string Multiplier_Count { get; set; }
        public string Multiplier_Type { get; set; }


    }
}