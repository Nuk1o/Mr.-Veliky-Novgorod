using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Game.Buildings;
using Game.Landmarks.Model;
using Server.ServerDataProviders.UserServerDataProvider;
using UnityEngine;
using Zenject;

namespace Game.Landmarks.DataProvider
{
    public class LandmarksDataProvider : IInitializable
    {
        [Inject] private LandmarksModel _landmarksModel;
        [Inject] private UserServerDataProvider _serverController;
        
        public void Initialize()
        {
            _landmarksModel.Buildings = new SerializedDictionary<Ebuildings, LandmarkModel>();

            // _landmarksModel.Buildings[Ebuildings.Vitoslavlitsy] = new LandmarkModel()
            // {
            //     NameBuilding = "Vitoslavlitsy",
            //     BuildingPositions = new List<Vector3>()
            //     {
            //         new Vector3(0, 0, 0),
            //         new Vector3(1, 0, 0),
            //         new Vector3(0, 1, 0),
            //         new Vector3(0, 0, 1)
            //     },
            //     ImageUrls = new List<string>()
            //     {
            //       "http://kalotun123.beget.tech/storage/images/attractions/ANyGFZyXosPajXH6mgft1YN6m5ahWvWG8IEICqM6.jpg",
            //       "http://kalotun123.beget.tech/storage/images/attractions/Z7hjyUlWXkm7WLYm5zm1GjL0j2XKrlNnByfb5mDX.jpg"
            //     },
            //     DescriptionBuilding = "Description Building",
            //     HistoryBuilding = "History Building",
            // };
        }

        public async void LoadingData()
        {
            var buildingsData = await _serverController.GetBuildingsData();

            foreach (var data in buildingsData)
            {
                var coords = data.coords.Select(coord => new Vector3((float)coord[0], (float)coord[1], 0)).ToList();

                var images = data.images.ToList();

                _landmarksModel.Buildings[Ebuildings.Vitoslavlitsy] = new LandmarkModel()
                {
                    NameBuilding = data.name,
                    BuildingPositions = coords,
                    ImageUrls = images,
                    DescriptionBuilding = data.description,
                };
                
            }
        }
    }
}