using UnityEngine;

public class WeaponOnGround : MonoBehaviour
{
    [SerializeField]
    private int _level;
    [SerializeField]
    private GameObject _weaponInHand;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var attacker = other.GetComponent<CharacterAttacker>();
            if (!attacker)
                return;

            if (attacker.CurrentWeapon)
            {
                if (attacker.CurrentWeapon.Level < _level)
                    PickUp();
                
                return;
            }
            
            PickUp();

            void PickUp()
            {
                attacker.SetWeapon(_weaponInHand);
                Destroy(gameObject);
            }
        }
    }
}
