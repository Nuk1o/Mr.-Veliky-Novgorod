using System.Collections.Generic;
using Game.Landmarks.Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "Custom/Building")]
public class BuildingData : ScriptableObject, IBuildingPositionProvider
{
    public string NameBuilding;
    
    public List<Vector3> BuildingPositionsSO = new List<Vector3>();
    
    public Sprite ImageBuilding;
    
    public string DescriptionBuilding;
    public string HistoryBuilding;
    
    public List<string> ImageUrls = new List<string>();
    
    public List<Vector3> BuildingPositions { get => BuildingPositionsSO; set => BuildingPositionsSO = value; }
}   