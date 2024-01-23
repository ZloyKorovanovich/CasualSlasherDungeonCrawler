using UnityEngine;

public class FPSUnlocker : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}
