using UnityEngine;

public class RuntimeManager : MonoBehaviour, IService
{
    public GameObject menu; 

    #region IService
    private void OnEnable()
    {
        ServiceLocator.RegisterService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.UnregisterService<RuntimeManager>();
    }
    #endregion

    private void Start()
    {
        menu.SetActive(false);
    }

    private void Update()
    {
        if (ServiceLocator.GetService<ControlManager>().GetState("IsPaused"))
            ActivateMenu();
        else
            DeactivateMenu();
    }

    private void ActivateMenu()
    {
        if(menu && menu.activeInHierarchy == false)
            menu.SetActive(true);

        Time.timeScale = 0f;
    }

    private void DeactivateMenu()
    {
        if (menu && menu.activeInHierarchy == true)
            menu.SetActive(false);

        Time.timeScale = 1f;
    }
}
