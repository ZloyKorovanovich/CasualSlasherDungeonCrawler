using UnityEngine;

public class WeaponOnGround : MonoBehaviour
{
    public GameObject weaponInHand;
    
    public void PickUp(CharacterAttacker attacker)
    {
        if (weaponInHand)
        {
            attacker.SetWeapon(weaponInHand);
            Destroy(gameObject);
        }
    }
}
