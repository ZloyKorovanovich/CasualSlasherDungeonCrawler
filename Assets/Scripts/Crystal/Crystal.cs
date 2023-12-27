using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Crystal : MonoBehaviour
{
    [SerializeField]
    private int _crystalCount;

    private bool _active;

    private void OnEnable()
    {
        _active = false;
        StartCoroutine(StartingDelay());
        GetComponent<ItemAnimation>()?.StartAnimation();
    }

    private IEnumerator StartingDelay()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<ParticleAnimation>()?.Spawn();
        _active = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_active)
            return;

        if (other.tag == "Player")
        {
            ServiceLocator.GetService<CrystalManager>().AddCrystals(_crystalCount);
            GetComponent<ParticleAnimation>()?.Spawn();
            var itemAnim = GetComponent<ItemAnimation>();
            if(!itemAnim)
            {
                Destroy(gameObject);
                return;
            }

            itemAnim.PickUpAnimation();
            Destroy(this);
        }
    }
}