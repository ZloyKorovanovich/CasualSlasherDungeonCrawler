using UnityEngine;

[RequireComponent(typeof(CharacterAttacker))]
public class WeaponGetter : MonoBehaviour
{
    [SerializeField]
    private GameObject _weapon;


    private void Start()
    {
        GetComponent<CharacterAttacker>().SetWeapon(_weapon);
        Destroy(this);
    }
}
