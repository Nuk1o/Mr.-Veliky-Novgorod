using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Game.Buildings;
using Game.Landmarks.Interface;
using UnityEngine;

namespace Game.Landmarks.Model
{
    public class LandmarksModel
    {
        public SerializedDictionary<Ebuildings,LandmarkModel> Buildings;
    }

    public class LandmarkModel : IBuildingPositionProvider
    {
        public string NameBuilding;

        public Sprite ImageBuilding;
    
        public string DescriptionBuilding;
        public string HistoryBuilding;
        public List<Vector3> BuildingPositions { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}