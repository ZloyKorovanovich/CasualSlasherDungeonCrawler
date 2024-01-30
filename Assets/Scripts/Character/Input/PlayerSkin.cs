using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public List<GameObject> skins = new List<GameObject>();

    void Start()
    {
        var index = PlayerPrefs.GetInt("PlayerSkin");
        foreach(GameObject obj in skins)
            obj.SetActive(false);

        skins[index]?.SetActive(true);
        Destroy(this);
    }
}
