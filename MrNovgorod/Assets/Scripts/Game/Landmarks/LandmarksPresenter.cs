using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Game.landmarks
{
    public class LandmarksPresenter
    {
        private List<LandmarksModel> _landmarks;
        public void Initialize()
        {
            _landmarks = new List<LandmarksModel>();
            var ladndmarksPins = new List<Vector2>();
            
            _landmarks.Add(new LandmarksModel()
            {
                Name = "TEST",
                Description = "TEST DESC",
                LandmarksPinPositions = new List<Vector2>()
            });
            Debug.Log("LandmarksPresenter.Initialize");
            
        }
    }
}