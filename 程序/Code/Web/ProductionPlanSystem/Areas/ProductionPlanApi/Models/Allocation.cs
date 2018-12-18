using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductionPlanSystem.Areas.ProductionPlanApi.Models
{
    public class AllocationInfo
    {
        public int StateCode { get; set; }
        public string ReaSon { get; set; }
        public List<Allocation> Act { get; set; }
    }
    public class Allocation
    {
        private int allocationID;
        public int AllocationID
        {
            get { return allocationID; }
            set { allocationID = value; }
        }

        private string allocationName;
        public string AllocationName
        {
            get { return allocationName; }
            set { allocationName = value; }
        }

        private string allocationTitle;
        public string AllocationTitle
        {
            get { return allocationTitle; }
            set { allocationTitle = value; }
        }

        private int serial;
        public int Serial
        {
            get { return serial; }
            set { serial = value; }
        }

        private string fieldWidth;
        public string FieldWidth
        {
            get { return fieldWidth; }
            set { fieldWidth = value; }
        }
    }
}