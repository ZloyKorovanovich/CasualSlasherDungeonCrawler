using UnityEngine;

[RequireComponent(typeof(CharacterAttacker))]
public class WeaponGetter : MonoBehaviour
{
    [SerializeField]
    private GameObject _weapon;


    private void OnEnable()
    {
        GetComponent<CharacterAttacker>().SetWeapon(_weapon);
        Destroy(this);
    }
}
