using AYellowpaper.SerializedCollections;
using Game.Buildings;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingsCollection", menuName = "Custom/BuildingsCollection")]
public class BuildingsData : ScriptableObject
{
    public SerializedDictionary<Ebuildings,BuildingData> Buildings;
}