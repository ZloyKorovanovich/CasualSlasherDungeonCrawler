using UnityEngine;
using UnityEngine.SceneManagement;

public class RuntimeMenu : MonoBehaviour
{
    public void Resume()
    {
        var service = ServiceLocator.GetService<ControlManager>();
        service.SetState("IsPaused", false);
    }

    public void Settings()
    {

    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
