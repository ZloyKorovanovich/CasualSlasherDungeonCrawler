using UnityEngine;

public class Pause : MonoBehaviour
{
    public void StopGame()
    {
        ServiceLocator.GetService<ControlManager>().SetState("IsPaused", true);
    }
}
