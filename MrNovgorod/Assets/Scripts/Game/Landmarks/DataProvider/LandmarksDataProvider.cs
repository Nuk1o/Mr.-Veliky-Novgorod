using System.Linq;
using AYellowpaper.SerializedCollections;
using Game.Buildings;
using Game.Landmarks.Model;
using Game.Others.Tools;
using Server.UserServerService;
using UnityEngine;
using Zenject;

namespace Game.Landmarks.DataProvider
{
    public class LandmarksDataProvider : IInitializable
    {
        [Inject] private LandmarksModel _landmarksModel;
        [Inject] private IUserServerService _serverController;
        [Inject] private ImageLoader _imageLoader;
        
        public void Initialize()
        {
            _landmarksModel.Buildings = new SerializedDictionary<Ebuildings, LandmarkModel>();
        }

        public async void LoadingData()
        {
            var buildingsData = await _serverController.GetBuildingsData();

            foreach (var data in buildingsData)
            {
                var coords = data.coords.Select(coord => new Vector3((float)coord[0], (float)coord[1], 0)).ToList();

                var images = data.images.ToList();
                var image = await _imageLoader.LoadSpriteAsync(images.First());

                var buildingID = BuildingTool.GetEbuildings(data.building_id);

                _landmarksModel.Buildings[buildingID] = new LandmarkModel()
                {
                    NameBuilding = data.name,
                    BuildingPositions = coords,
                    ImageUrls = images,
                    ImageBuilding = image,
                    DescriptionBuilding = data.description,
                    HistoryBuilding = data.history,
                    GlobalCoordinatesBuilding = data.global_coords,
                    Address = data.address
                };
            }
        }
    }
}