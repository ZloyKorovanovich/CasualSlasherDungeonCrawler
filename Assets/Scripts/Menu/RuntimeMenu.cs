using UnityEngine;
using UnityEngine.SceneManagement;

public class RuntimeMenu : MonoBehaviour
{
    public GameObject settings;

    public void Resume()
    {
        var service = ServiceLocator.GetService<ControlManager>();
        service.SetState("IsPaused", false);
    }

    public void Settings()
    {
        settings.SetActive(true);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
