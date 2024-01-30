using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public List<GameObject> skins = new List<GameObject>();

    private void Start()
    {
        SetSkin(0);
    }

    public void SetSkin(int index)
    {
        foreach(GameObject obj in skins)
            obj.SetActive(false);

        skins[index]?.SetActive(true);
        registerSkin(index);
    }

    private void registerSkin(int skin)
    {
        PlayerPrefs.SetInt("PlayerSkin", skin);
    }
}
