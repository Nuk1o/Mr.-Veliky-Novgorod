using UnityEngine;
using UnityEditor;

public class PlayerPrefsEditor : MonoBehaviour
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs очищены!");
    }
}