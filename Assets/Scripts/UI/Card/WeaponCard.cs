using UnityEngine;

public class WeaponCard : Card
{
    public GameObject weapon;

    protected override void Use()
    {
        _manager.player.GetComponent<CharacterAttacker>()?.SetWeapon(weapon);
        base.Use();
    }
}
