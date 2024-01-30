#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class HierarchyOrganizer : MonoBehaviour
{
    private static string[] _names = new string[]
        {
            "--- ENVIRONMENT ---",
            "--- Gameplay ---",
            "--- UI ---",
            "--- LOGIC ---",
        };

    [MenuItem("Tools/Organize Hierarchy")]
    static void OrganizeHierarchy()
    {
        foreach (var name in _names)
            if (GameObject.Find(name) == false)
                new GameObject(name);
    }
}
#endif