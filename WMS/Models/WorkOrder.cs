using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using WMS.Models;

namespace WMS.Models
{
    public class WorkOrder
    {
        public class Mission
        {
            [Flags]
            public enum RackFace : byte
            {
                None = 0b0000,
                North = 0b0001,
                East = 0b0010,
                West = 0b1000,
                South = 0b0100,
                NorthEast = 0b0011,
                SouthEast = 0b0110,
                NorthWest = 0b1001,
                SouthWest = 0b1100,
            }
            public static readonly Dictionary<RackFace, string> RackFaceDescription = new Dictionary<RackFace, string> {
                { RackFace.None, "無"},
                { RackFace.North, "北"},
                { RackFace.East, "東"},
                { RackFace.West, "西"},
                { RackFace.South, "南"},
                { RackFace.NorthEast, "東北"},
                { RackFace.SouthEast, "東南"},
                { RackFace.NorthWest, "西北"},
                { RackFace.SouthWest, "西南"},
            };

            [Name("物料名稱", "MaterialName")]
            public string MaterialName { get; set; }
            [Name("數量", "Quantity")]
            public int Quantity { get; set; }
            [Name("目的地", "Destination")]
            public string Destination { get; set; }
            public string RackName { get; set; }
            public RackFace PickUpFace { get; set; }
            public RackFace AvailableFaces { get; set; }
        }

        public enum WorkOrderState
        {
            Editing,
            Enqueued,
            Executing,
            Finished,
            Error,
        }
        public static readonly Dictionary<WorkOrderState, string> WorkOrderStateDescription = new Dictionary<WorkOrderState, string> {
                {WorkOrderState.Editing, "編輯中"},
                {WorkOrderState.Enqueued, "排隊中"},
                {WorkOrderState.Executing, "執行中"},
                {WorkOrderState.Finished, "已完成"},
                {WorkOrderState.Error, "錯誤"},
            };

        public string UUID { get; }
        public string Name { get; set; }
        public List<Mission> MissionList { get; set; }
        public WorkOrderState State { get; set; }
        public long EnqueuedTimestamp { get; set; }
        public long CompletedTimestamp { get; set; }

        public bool IsValid { get; }

        public WorkOrder()
        {
            this.UUID = Guid.NewGuid().ToString("N");
            this.Name = "WO-" + this.UUID.Substring(0, 5);
            this.MissionList = new List<Mission>();
            this.IsValid = true;
        }

        public WorkOrder(string filePath)
        {
            this.UUID = Guid.NewGuid().ToString("N");

            if (File.Exists(filePath))
            {
                try
                {
                    this.Name = Path.GetFileNameWithoutExtension(filePath);
                    var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
                    {
                        HasHeaderRecord = true,
                        HeaderValidated = null,
                        MissingFieldFound = null,
                    };
                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var streamReader = new StreamReader(fileStream, Encoding.GetEncoding("Big5")))
                    using (var csvReader = new CsvReader(streamReader, csvConfig))
                    {
                        this.MissionList = csvReader.GetRecords<WorkOrder.Mission>().ToList();
                    }
                    this.IsValid = true;
                    return;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            this.IsValid = false;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
