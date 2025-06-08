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
        public string serverName;
        public int serverId;
    
        public string DescriptionBuilding;
        public string HistoryBuilding;
        public string GlobalCoordinatesBuilding;
        public string Address;
        public List<Vector3> BuildingPositions { get; set; }
        public List<string> ImageUrls { get; set; }
        public List<LandmarkReviews> Reviews { get; set; }
    }

    public class LandmarkReviews
    {
        public int Id;
        public int Rating;
        public string Comment;
        public string UserName;
        public string UserAvatar;
    }
}