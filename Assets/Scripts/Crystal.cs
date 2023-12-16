using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Crystal : MonoBehaviour
{
    [SerializeField]
    private int _crystalCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ServiceLocator.GetService<CrystalManager>().AddCrystals(_crystalCount);
            Destroy(gameObject);
        }
    }
}