
using System.Collections.Generic;
using UnityEngine;

namespace Game.Landmarks.Interface
{
    public interface IBuildingPositionProvider
    {
        List<Vector3> BuildingPositions { get; set; }
    }
}