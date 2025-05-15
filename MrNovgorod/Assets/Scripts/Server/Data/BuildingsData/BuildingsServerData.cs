using System;
using JetBrains.Annotations;

namespace UserServerService.Data.BuildingsData
{
    [Serializable]
    public class BuildingsServerData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string global_coords { get; set; }
        public string building_id { get; set; }
        [CanBeNull] public string history { get; set; }
        public string[] images { get; set; }
        public double[][] coords { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public int rating { get; set; }
    }
}