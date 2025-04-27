using System.Collections.Generic;
using Game.Buildings;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "Custom/Building")]
public class BuildingData : ScriptableObject
{
    public string NameBuilding;
    
    public List<Vector3> BuildingPositions = new List<Vector3>();
    
    public Sprite ImageBuilding;
    
    public string DescriptionBuilding;
    public string HistoryBuilding;
}   