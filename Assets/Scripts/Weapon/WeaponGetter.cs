using UnityEngine;

[RequireComponent(typeof(CharacterAttacker))]
public class WeaponGetter : MonoBehaviour
{
    public GameObject weapon;

    private void Start()
    {
        GetComponent<CharacterAttacker>().SetWeapon(weapon);
        Destroy(this);
    }
}
