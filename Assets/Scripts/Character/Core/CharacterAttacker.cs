using UnityEngine;

[RequireComponent(typeof(CharacterMain))]
public class CharacterAttacker : MonoBehaviour
{
    [SerializeField]
    private Transform _rightHand;
    [SerializeField]
    private float _pickUpRadius = 3.0f;
    [SerializeField]
    private LayerMask _weaponLayer;

    private WeaponInHand _currentWeapon;
    private CharacterMain _main;
    private CharacterAnimation _animation;

    public WeaponInHand CurrentWeapon => _currentWeapon;

    private void Awake()
    {
        _main = GetComponent<CharacterMain>();
        _animation = GetComponent<CharacterAnimation>();
    }

    private void Start()
    {
        _main.OnSetInputs += UpdateInputs;
    }

    private void UpdateInputs()
    {
        
    }

    public void SetWeapon(GameObject weapon)
    {
        _currentWeapon?.Drop();
        _currentWeapon = Instantiate(weapon, _rightHand).GetComponent<WeaponInHand>();
    }
}
