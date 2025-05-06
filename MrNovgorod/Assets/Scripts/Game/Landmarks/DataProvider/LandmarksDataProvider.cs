using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Game.Buildings;
using Game.Landmarks.Model;
using UnityEngine;
using Zenject;

namespace Game.Landmarks.DataProvider
{
    public class LandmarksDataProvider : IInitializable
    {
        [Inject] private LandmarksModel _landmarksModel;
        
        public void Initialize()
        {
            _landmarksModel.Buildings = new SerializedDictionary<Ebuildings, LandmarkModel>();

            _landmarksModel.Buildings[Ebuildings.Vitoslavlitsy] = new LandmarkModel()
            {
                NameBuilding = "Vitoslavlitsy",
                BuildingPositions = new List<Vector3>()
                {
                    new Vector3(0, 0, 0),
                    new Vector3(1, 0, 0),
                    new Vector3(0, 1, 0),
                    new Vector3(0, 0, 1)
                },
                DescriptionBuilding = "Description Building",
                HistoryBuilding = "History Building",
            };
        }
    }
}