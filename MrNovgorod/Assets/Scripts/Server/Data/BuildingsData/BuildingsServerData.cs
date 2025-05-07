using System;

namespace Server.Data.BuildingsData
{
    [Serializable]
    public class BuildingsServerData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string[] images { get; set; }
        public double[][] coords { get; set; }
        public string description { get; set; }
        //public int category_id { get; set; }
        public string address { get; set; }
        public int rating { get; set; }
    }
}