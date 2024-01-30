using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioSource source;

    private void Start()
    {
        if(PlayerPrefs.HasKey("Volume"))
        {
            source.volume = PlayerPrefs.GetFloat("Volume");
            GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("Volume");
            return;
        }

        PlayerPrefs.SetFloat("Volume", 1f);
        source.volume = 1f;
    }

    public void SetVolume(float value)
    {
        source.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }
}
