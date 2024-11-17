using Game.Buildings;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "Building Data")]
public class BuildingData : ScriptableObject
{
    public Ebuildings Ebuildings;
    public string LogoText;
    public Sprite ImageBuilding;

    [TextArea] public string DescriptionText;

    [TextArea] public string HistoryText;
}