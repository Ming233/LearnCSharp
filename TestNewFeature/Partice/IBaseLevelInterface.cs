using System;

namespace TestNewFeature
{

    public class ListofData
    {
        public string Data1 { get; set; }
        public decimal Data2 { get; set; }
        public DateTime Data3 { get; set; }
        public double Data4 { get; set; }
        public int Data5 { get; set; }

        public bool GetData1(ListofData moreData)
        {
            return Convert.ToBoolean(moreData.Data1);
        }

        public decimal GetData2(ListofData moreData)
        {
            return Convert.ToDecimal(moreData.Data4);
        }
    }
}
